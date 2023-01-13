from enum import Enum
import logging
from typing import Dict, Protocol

from PyQt5.QtCore import pyqtSignal, Qt, QMetaObject, pyqtSlot, Q_ARG
from PyQt5.QtGui import QIcon, QCloseEvent
from PyQt5.QtWidgets import QHBoxLayout, QLabel, QMainWindow, QStackedWidget, QToolButton, QWidget

from metric.metric import Metric 
from server.gui.gui_helper import GuiHelper
from server.gui.metric_panel import MetricPanel
from server.os.platform import Platform

logger = logging.getLogger(__name__)

class PageDirection(Enum):
    FORWARD = 1
    BACKWARD = 2

class PyStreamPresenterProtocol(Protocol):
    def close(self) -> None:
        ...

    def desklamp_click(self) -> None:
        ...

    def usb_switch_activate(self) -> None:
        ...
    
    def usb_switch_deactivate(self) -> None:
        ...

    def laptop_click(self) -> None:
        ...

class PyStream(QMainWindow):
    presenter:PyStreamPresenterProtocol
    metric_panels : Dict[str, MetricPanel]
    receive_signal = pyqtSignal(str, Metric)
    add_metric_page_signal = pyqtSignal(str, int)
    remove_metric_page_signal = pyqtSignal(str)

    def __init__(self) -> None:
        super().__init__()
        
        self.metric_panels = {}

    def init_ui(self, presenter:PyStreamPresenterProtocol) -> None:
        self.presenter = presenter
        logger.info("[GUI] Init main frame")
        if Platform.is_raspberry_pi():
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
        ##### Button Panel
        button_panel = QWidget()
        
        background_2 = QLabel(button_panel)
        background_2.setGeometry(0, 0, 800, 480)
        background_2.setStyleSheet("background-image: url(server/resource/page_2.jpg);")

        grid = QHBoxLayout()
        grid.addStretch()
        grid.addWidget(GuiHelper.create_button(width=100, height=120, image="desk_lamp.png", click=self.presenter.desklamp_click, checkable=True))
        grid.addStretch()
        self.button_change_usb = GuiHelper.create_button(width=100, height=140, text="1", image="keyboard.png", press=self.presenter.usb_switch_activate, release=self.presenter.usb_switch_deactivate, button_type=QToolButton)
        grid.addWidget(self.button_change_usb)
        grid.addStretch()
        grid.addWidget(GuiHelper.create_button(width=100, height=120, image="laptop.png", click=self.presenter.laptop_click, checkable=True))
        grid.addStretch()

        button_panel.setLayout(grid)
        #####################

        self.stack = QStackedWidget(self)
        self.stack.setGeometry(0, 0, 800, 480)
        self.stack.addWidget(button_panel)

        self.btn_left = GuiHelper.create_button(parent=self, x=0, y=190, width=26, height=100, image="arrow_left.png", click=lambda:self.__change_page(PageDirection.BACKWARD))
        self.btn_right = GuiHelper.create_button(parent=self, x=774, y=190, width=26, height=100, image="arrow_right.png", click=lambda:self.__change_page(PageDirection.FORWARD))

        self.set_page_button_visibility()

        self.label_room_temp = GuiHelper.create_label(self, 97, 0, width=137, height=30, text="--°C")
        self.label_room_temp.setAlignment(Qt.AlignmentFlag.AlignHCenter)
        self.label_time = GuiHelper.create_label(self, 570, 0, width=135, height=30, text="00:00")
        self.label_time.setAlignment(Qt.AlignmentFlag.AlignHCenter)

        logger.info("[GUI] Init done")
        self.show()

    def __change_page(self, direction:PageDirection) -> None:
        if direction == PageDirection.FORWARD:
            if self.stack.currentIndex() < self.stack.count() - 1: 
                self.stack.setCurrentIndex(self.stack.currentIndex() + 1)
        elif direction == PageDirection.BACKWARD:
            if self.stack.currentIndex() > 0: 
                self.stack.setCurrentIndex(self.stack.currentIndex() - 1)
        
        self.set_page_button_visibility()

    def update_text(self, time:str, temp:str, active_usb:str) -> None:
        self.label_time.setText(time)
        self.label_room_temp.setText(temp)
        self.button_change_usb.setText(active_usb)

    def receive(self, client_id:str, data:Metric) -> None:
        self.invoke_method("receive_gui", client_id, data)

    @pyqtSlot(str, Metric)
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

    def add_metric_page(self, client_id:str, cpu_count:int) -> None:
        self.invoke_method("add_metric_page_gui", client_id, cpu_count)

    @pyqtSlot(str, int)
    def add_metric_page_gui(self, client_id:str, cpu_count:int) -> None:
        try:
            self.metric_panels[client_id] = MetricPanel(cpu_count)
            self.stack.insertWidget(0, self.metric_panels[client_id])
            self.stack.setCurrentIndex(0)
            self.set_page_button_visibility()
        except Exception as e:
            raise e

    def remove_metric_page(self, client_id:str) -> None:
        self.invoke_method("remove_metric_page_gui", client_id)

    @pyqtSlot(str)
    def remove_metric_page_gui(self, client_id:str) -> None:
        logger.info("[GUI] Restoring initial image")
        panel = self.metric_panels.pop(client_id)
        self.stack.removeWidget(panel)
        
        if self.stack.currentIndex() != 0:
            self.stack.setCurrentIndex(0)
        
        self.set_page_button_visibility()
 
    def closeEvent(self, a0: QCloseEvent) -> None:
        self.presenter.close()
        return super().closeEvent(a0)

    def set_page_button_visibility(self) -> None:
        i = self.stack.currentIndex()
        show_left = i >= 1
        show_right = i < self.stack.count() - 1

        self.btn_left.setVisible(show_left)
        self.btn_left.setEnabled(show_left)
        self.btn_right.setVisible(show_right)
        self.btn_right.setEnabled(show_right)

    @property
    def metric_panel_count(self) -> int:
        return len(self.metric_panels)
    
    def invoke_method(self, methode:str, *args) -> None:
        if len(args) == 0:
            QMetaObject.invokeMethod(self, methode)
        elif len(args) == 1:
            QMetaObject.invokeMethod(self, methode, Q_ARG(type(args[0]), args[0]))
        elif len(args) == 2:
            QMetaObject.invokeMethod(self, methode, Q_ARG(type(args[0]), args[0]), Q_ARG(type(args[1]), args[1]))
        elif len(args) == 3:
            QMetaObject.invokeMethod(self, methode, Q_ARG(type(args[0]), args[0]), Q_ARG(type(args[1]), args[1]), Q_ARG(type(args[2]), args[2]))
        elif len(args) == 4:
            QMetaObject.invokeMethod(self, methode, Q_ARG(type(args[0]), args[0]), Q_ARG(type(args[1]), args[1]), Q_ARG(type(args[2]), args[2]), Q_ARG(type(args[3]), args[3]))
        elif len(args) == 5:
            QMetaObject.invokeMethod(self, methode, Q_ARG(type(args[0]), args[0]), Q_ARG(type(args[1]), args[1]), Q_ARG(type(args[2]), args[2]), Q_ARG(type(args[3]), args[3]), Q_ARG(type(args[4]), args[4]))
