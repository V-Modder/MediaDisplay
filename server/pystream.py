import logging
import platform
import pyautogui
import sys
import time
from typing import Dict

from PyQt5.QtCore import pyqtSignal, Qt, QDateTime, QTimer
from PyQt5.QtGui import QIcon, QCloseEvent
from PyQt5.QtWidgets import QApplication, QHBoxLayout, QLabel, QMainWindow, QStackedWidget, QToolButton, QWidget

from Xlib import X
from Xlib import display

from metric.metric import Metric 
from server.metric_protocol import MetricProtocol
from server.devices.pytemp import PyTemp
from server.devices.pyrelay import PyRelay
from server.devices.pysense import PySense
from server.gui.gui_helper import GuiHelper
from server.gui.metric_panel import MetricPanel
from server.server import MetricServer

logger = logging.getLogger(__name__)

def main() -> None:
    app = QApplication(sys.argv)
    global window 
    window = PyStream()
    sys.exit(app.exec())

class PyStream(QMainWindow, MetricProtocol):
    metric_panels : Dict[str, MetricPanel]
    receive_signal = pyqtSignal(str, Metric)
    reinit_signal = pyqtSignal(str, int)
    reset_signal = pyqtSignal(str)

    def __init__(self) -> None:
        super().__init__()
        self.__server = MetricServer(self)
        self.__server.start()
        self.__relay = PyRelay()
        self.__temp = PyTemp()
        self.__temp.start()
        self.__pysense = PySense()
        self.timer = QTimer()
        self.timer.timeout.connect(self.__timer_tick)
        
        self.metric_panels = {}

        self.initUI()
        self.enable_screensaver()

    def initUI(self) -> None:
        logger.info("[GUI] Init main frame")
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

        #####################
        ##### Panel 2
        button_panel = QWidget()
        
        background_2 = QLabel(button_panel)
        background_2.setGeometry(0, 0, 800, 480)
        background_2.setStyleSheet("background-image: url(server/resource/page_2.jpg);")

        grid = QHBoxLayout()
        grid.addStretch()
        grid.addWidget(GuiHelper.create_button(width=100, height=120, image="desk_lamp.png", click=lambda:self.__relay.toggle_relay(PyRelay.BIG_2), checkable=True))
        grid.addStretch()
        self.button_change_usb = GuiHelper.create_button(width=100, height=140, text="1", image="keyboard.png", press=lambda:self.__relay.activate_relay(PyRelay.SMALL_1), release=lambda:self.__relay.deactivate_relay(PyRelay.SMALL_1), button_type=QToolButton)
        grid.addWidget(self.button_change_usb)
        grid.addStretch()
        grid.addWidget(GuiHelper.create_button(width=100, height=120, image="laptop.png", click=lambda:self.__relay.toggle_relay(PyRelay.BIG_1), checkable=True))
        grid.addStretch()

        button_panel.setLayout(grid)
        #####################

        self.stack = QStackedWidget(self)
        self.stack.setGeometry(0, 0, 800, 480)
        self.stack.addWidget(button_panel)

        self.btn_left = GuiHelper.create_button(parent=self, x=0, y=190, width=26, height=100, image="arrow_left.png", click=lambda:self.__change_page("Backward"))
        self.btn_right = GuiHelper.create_button(parent=self, x=774, y=190, width=26, height=100, image="arrow_right.png", click=lambda:self.__change_page("Forward"))

        self.set_page_button_visibility()

        self.label_room_temp = GuiHelper.create_label(self, 97, 0, width=137, height=30, text="--°C")
        self.label_room_temp.setAlignment(Qt.AlignmentFlag.AlignHCenter)
        self.label_time = GuiHelper.create_label(self, 570, 0, width=135, height=30, text="00:00")
        self.label_time.setAlignment(Qt.AlignmentFlag.AlignHCenter)

        logger.info("[GUI] Init done")
        self.timer.start(1000)
        self.receive_signal.connect(self.receive_gui)
        self.reinit_signal.connect(self.reinit_gui)
        self.reset_signal.connect(self.reset_gui)
        self.show()

    def is_raspberry_pi(self) -> bool:
        return platform.machine() == 'armv7l'

    def __timer_tick(self) -> None:
        time = QDateTime.currentDateTime()
        timeDisplay = time.toString('hh:mm')
        temp = self.__temp.temperature
        active_usb = "2" if self.__pysense.check_state(PySense.INPUT_1) else "1"

        self.label_time.setText(timeDisplay)
        self.label_room_temp.setText("%1.0f°C" % temp)
        self.button_change_usb.setText(active_usb)

    def __change_page(self, direction) -> None:
        if direction == "Forward":
            if self.stack.currentIndex() < self.stack.count() - 1: 
                self.stack.setCurrentIndex(self.stack.currentIndex() + 1)
        elif direction == "Backward":
            if self.stack.currentIndex() > 0: 
                self.stack.setCurrentIndex(self.stack.currentIndex() - 1)

    def receive_gui(self, client_id:str, data:Metric) -> None:
        if self.is_updating == False:
            self.is_updating = True
            try:
                 panel = self.metric_panels.get(client_id)
                 if panel is not None:
                    panel.receive(data)
            except Exception as e:
                logger.error(e)
            finally:
                self.is_updating = False
        else: 
            logger.error("Gui is locked")

    def receive(self, client_id:str, data:Metric) -> None:
        self.receive_signal.emit(client_id, data)

    def reinit_gui(self, client_id:str, data:int) -> None:
        try:
            self.metric_panels[client_id] = MetricPanel(data)
            self.stack.insertWidget(0, self.metric_panels[client_id])
            self.enable_gui()
        except Exception as e:
            #self.cpu_panel.clear()
            raise e

    def restore(self, client_id:str, cpu_count:int) -> None:
        self.disable_screensaver()
        self.reinit_signal.emit(client_id, cpu_count)

    def reset(self, client_id:str) -> None:
        logger.info("[GUI] Restoring initial image")
        self.reset_signal.emit(client_id)

    def reset_gui(self, client_id:str) -> None:
        panel = self.metric_panels.pop(client_id)
        self.stack.removeWidget(panel)
        self.restore_gui()
        if self.stack.count() == 1:
            self.enable_screensaver()
        
        self.set_page_button_visibility()
        #self.cpu_panel.clear()
        #self.gpu_panel.show_gui(False)

    #def set_brightness(self, brightness) -> None:
    #    self.backlight.set_brightness(brightness)

    #def get_brightness(self) -> None:
    #    return self.backlight.get_brightness()

    def enable_gui(self) -> None:
        self.stack.setCurrentIndex(0)
        self.set_page_button_visibility()

    def restore_gui(self) -> None:
        #if self.stack.currentIndex() != 0:
        self.stack.setCurrentIndex(0)
    
    def disable_screensaver(self) -> None:
        if self.is_raspberry_pi():
            disp = display.Display()
            disp.set_screen_saver(0, 0, X.DontPreferBlanking, X.AllowExposures)
            disp.sync()
            step = 1
            if pyautogui.position().x <= disp.screen()["height_in_pixels"]:
                step *= -1
            pyautogui.moveRel(step, 0)
            time.sleep(0.5)
            pyautogui.moveRel(-step, 0)

    def enable_screensaver(self) -> None:
        if self.is_raspberry_pi():
            disp = display.Display()
            screensaver = disp.get_screen_saver()
            if screensaver.timeout != 60:
                disp.set_screen_saver(60, 60, X.DefaultBlanking, X.AllowExposures)
                disp.sync()

    def closeEvent(self, a0: QCloseEvent) -> None:
        self.__server.stop()
        return super().closeEvent(a0)

    def set_page_button_visibility(self) -> None:
        i = self.stack.currentIndex()
        show_left = i >= 1
        show_right = i < self.stack.count() - 1

        self.btn_left.setVisible(show_left)
        self.btn_left.setEnabled(show_left)
        self.btn_right.setVisible(show_right)
        self.btn_right.setEnabled(show_right)
