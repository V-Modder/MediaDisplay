import logging
import platform
import pyautogui
import sys

from PyQt6.QtCore import pyqtSignal, Qt, QTimer, QDateTime
from PyQt6.QtGui import QIcon, QCloseEvent
from PyQt6.QtWidgets import QApplication, QMainWindow, QLabel, QStackedWidget, QWidget

from rpi_backlight import Backlight
from rpi_backlight.utils import FakeBacklightSysfs

from Xlib import X
from Xlib import display

from metric.metric import Metric 
from server.cpu_panel import CpuPanel
from server.gpu_panel import GpuPanel
from server.gui_helper import GuiHelper
from server.pysense import PySense
from server.pytemp import PyTemp
from server.pyrelay import PyRelay
from server.server import MetricServer

def main():
    app = QApplication(sys.argv)
    global window 
    window = PyStream()
    sys.exit(app.exec())

class PyStream(QMainWindow):
    receive_signal = pyqtSignal(Metric)
    reinit_signal = pyqtSignal(int)

    def __init__(self):
        super().__init__()
        self.__server = MetricServer(self)
        self.__server.start()
        self.__relay = PyRelay()
        self.__temp = PyTemp()
        self.__pysense = PySense()
        self.__stats_tab_index = 0
        self.__buttons_tab_index = 1
        self.timer = QTimer()
        self.timer.timeout.connect(self.__timer_tick)
    
        try:
            self.backlight = Backlight()
        except:
            self.fakeBacklightSysfs = FakeBacklightSysfs()
            self.fakeBacklightSysfs.__enter__()
            self.backlight = Backlight(backlight_sysfs_path=self.fakeBacklightSysfs.path)
        
        self.initUI()
        self.enable_screensaver()

    def initUI(self):
        logging.info("[GUI] Init main frame")
        if self.is_raspberry_pi():
            self.setWindowFlags(Qt.WindowType.FramelessWindowHint)
            self.setGeometry(0, 0, 800, 480)
            self.setCursor(Qt.CursorShape.BlankCursor)
        else:
            self.setFixedWidth(800)
            self.setFixedHeight(480)
        self.setWindowTitle("MediaDisplay-Server")
        self.setWindowIcon(QIcon("server/resource/pyalarm.png"))

        self.is_updating = False

        self.stack = QStackedWidget(self)
        self.stack.setGeometry(0, 0, 800, 480)
        self.panel_1 = QWidget()
        self.panel_2 = QWidget()
        self.stack.addWidget(self.panel_1)
        self.stack.addWidget(self.panel_2)

        #####################
        ##### Panel 1
        background_1 = QLabel(self.panel_1)
        background_1.setGeometry(0, 0, 800, 480)
        background_1.setStyleSheet("background-image: url(server/resource/page_1.jpg);")

        self.cpu_panel = CpuPanel(self.panel_1)
        self.cpu_panel.setGeometry(26, 25, 748, 350)

        self.gpu_panel = GpuPanel(self.panel_1)
        self.gpu_panel.setGeometry(0, 384, 265, 96)

        GuiHelper.create_label(self.panel_1, 280, 395, text="Down")
        GuiHelper.create_label(self.panel_1, 280, 419, text="Up")
        self.label_net_down = GuiHelper.create_label(self.panel_1, 430, 395, width=100, height=25, text="0")
        self.label_net_down.setAlignment(Qt.AlignmentFlag.AlignRight)
        self.label_net_up = GuiHelper.create_label(self.panel_1, 430, 419, width=100, height=25, text="0")
        self.label_net_up.setAlignment(Qt.AlignmentFlag.AlignRight)

        GuiHelper.create_label(self.panel_1, 551, 390, text="Memory", font_size=18)
        self.progress_mem_load = GuiHelper.create_progressbar(self.panel_1, 551, 421, 203, 20)
        
        self.btn_right = GuiHelper.create_button(self.panel_1, 774, 227, 26, 26, "arrow_right.png", lambda:self.__change_page("Forward"))

        #####################
        ##### Panel 2
        background_2 = QLabel(self.panel_2)
        background_2.setGeometry(0, 0, 800, 480)
        background_2.setStyleSheet("background-image: url(server/resource/page_2.jpg);")

        GuiHelper.create_button(self.panel_2, 125, 180, 100, 120, "desk_lamp.png", lambda:self.__relay.toggle_relay(PyRelay.BIG_2), checkable=True)
        GuiHelper.create_button(self.panel_2, 350, 180, 100, 120, "keyboard.png", press=lambda:self.__relay.activate_relay(PyRelay.SMALL_1), release=lambda:self.__relay.deactivate_relay(PyRelay.SMALL_1))
        self.label_active_usb = GuiHelper.create_label(self.panel_2, 350, 280, width=100, height=25, text="1")
        self.label_active_usb.setAlignment(Qt.AlignmentFlag.AlignCenter)
        GuiHelper.create_button(self.panel_2, 575, 180, 100, 120, "laptop.png", lambda:self.__relay.toggle_relay(PyRelay.BIG_1), checkable=True)
        
        self.btn_left = GuiHelper.create_button(self.panel_2, 0, 227, 26, 26, "arrow_left.png", lambda:self.__change_page("Backward"))
        #####################

        self.label_room_temp = GuiHelper.create_label(self, 97, 0, width=137, height=30, text="--°C")
        self.label_room_temp.setAlignment(Qt.AlignmentFlag.AlignHCenter)
        self.label_time = GuiHelper.create_label(self, 570, 0, width=135, height=30, text="00:00")
        self.label_time.setAlignment(Qt.AlignmentFlag.AlignHCenter)

        self.restore_gui()
        logging.info("[GUI] Init done")
        self.timer.start(1000)
        self.receive_signal.connect(self.receive_gui)
        self.reinit_signal.connect(self.reinit_gui)
        self.show()

    def is_raspberry_pi(self) -> bool:
        return platform.machine() == 'armv71'

    def __timer_tick(self):
        time = QDateTime.currentDateTime()
        timeDisplay = time.toString('hh:mm')
        temp = self.__temp.read()
        active_usb = "2" if self.__pysense.check_state(PySense.INPUT_1) else "1"

        self.label_time.setText(timeDisplay)
        self.label_room_temp.setText("%1.0f°C" % temp)
        self.label_active_usb.setText(active_usb)

    def __change_page(self, direction):
        if direction == "Forward":
            if self.stack.currentIndex() < self.stack.count() - 1: 
                self.stack.setCurrentIndex(self.stack.currentIndex() + 1)
        elif direction == "Backward":
            if self.stack.currentIndex() > 0: 
                self.stack.setCurrentIndex(self.stack.currentIndex() - 1)

    def udpate_gui(self, data:Metric):
        i = 0
        for cpu in data.cpus:
            self.cpu_panel.update_value(i, cpu)
            i += 1

        self.progress_mem_load.setValue(data.memory_load)
        
        if data.gpu is not None:
            self.gpu_panel.show_gui(True)
            self.gpu_panel.update_value(data.gpu)
        else:
            self.gpu_panel.show_gui(False)
        
        if data.network is not None:
            self.label_net_down.setText(data.network.get_down_str())
            self.label_net_up.setText(data.network.get_up_str())
        else:
            self.label_net_down.setText("0B")
            self.label_net_up.setText("0B")

    def receive_gui(self, data:Metric):
        if self.is_updating == False:
            self.is_updating = True
            try:
                self.udpate_gui(data)
            except Exception as e:
                print(e)
            finally:
                self.is_updating = False
        else: 
            print("Gui is locked")

    def receive(self, data:Metric):
        self.receive_signal.emit(data)

    def reinit_gui(self, data:int):
        self.cpu_panel.create_cpus(data)

    def restore(self, cpu_count:int):
        self.disable_screensaver()
        self.enable_gui()
        self.reinit_signal.emit(cpu_count)

    def reset(self):
        logging.info("[GUI] Restoring initial image")
        self.restore_gui()
        self.enable_screensaver()
        self.cpu_panel.clear()
        self.gpu_panel.show_gui(False)

    def set_brightness(self, brightness):
        if brightness is not None and brightness >= 0 and brightness <= 100:
            self.backlight.brightness = brightness

    def get_brightness(self):
        return self.backlight.brightness

    def enable_gui(self):
        self.stack.setCurrentIndex(self.__stats_tab_index)
        self.btn_left.setEnabled(True)
        self.btn_left.setVisible(True)

    def restore_gui(self):
        if self.stack.currentIndex() != self.__buttons_tab_index:
            self.stack.setCurrentIndex(self.__buttons_tab_index)
        
        self.btn_left.setEnabled(False)
        self.btn_left.setVisible(False)
    
    def disable_screensaver(self):
        if self.is_raspberry_pi():
            disp = display.Display()
            disp.set_screen_saver(0, 0, X.DontPreferBlanking, X.AllowExposures)
            disp.sync()
            pyautogui.moveRel(0, 10)
            pyautogui.moveRel(0, -10)

    def enable_screensaver(self):
        if self.is_raspberry_pi():
            disp = display.Display()
            screensaver = disp.get_screen_saver()
            if screensaver.timeout != 60:
                disp.set_screen_saver(60, 60, X.DefaultBlanking, X.AllowExposures)
                disp.sync()

    def closeEvent(self, a0: QCloseEvent) -> None:
        self.__server.stop()
        return super().closeEvent(a0)