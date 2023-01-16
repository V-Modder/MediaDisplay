from __future__ import annotations
from typing import Protocol
import sys

from PyQt5.QtCore import QDateTime, QTimer
from PyQt5.QtWidgets import QApplication

from metric.metric import Metric
from server.devices.pyrelay import PyRelay
from server.devices.pysense import PySense
from server.devices.pytemp import PyTemp
from server.os.screensaver import Screensaver
#https://github.com/ArjanCodes/2022-gui/blob/main/mvp
#https://forum.qt.io/topic/47887/solved-how-to-make-text-font-size-to-be-auto-adjusted/3

class PyStreamProtocol(Protocol):
    def init_ui(self, presenter:PyStreamPresenter) -> None:
        ...

    def receive(self, client_id:str, data:Metric) -> None:
        ...
    
    def add_metric_page(self, client_id:str, name:str, cpu_count:int) -> None:
        ...
    
    def remove_metric_page(self, client_id:str) -> None:
        ...

    def update_text(self, time:str, temp:str, active_usb:str) -> None:
        ...
    
    @property
    def metric_panel_count(self) -> int:
        ...

class ServerProtocol(Protocol):
    def init_server(self, presenter:PyStreamPresenter) -> None:
        ...

    def start(self) -> None:
        ...

    def stop(self) -> None:
        ...

class PyStreamPresenter:
    view:PyStreamProtocol
    model:ServerProtocol
    relay:PyRelay

    def __init__(self, view:PyStreamProtocol, model:ServerProtocol) -> None:
        self.view = view
        self.model = model
        self.relay = PyRelay()
        self.__temp = PyTemp()
        self.__temp.start()
        self.__pysense = PySense()
        self.timer = QTimer()
        self.timer.timeout.connect(self.timer_tick)

    def run(self, app:QApplication) -> None:
        self.view.init_ui(self)
        self.model.init_server(self)
        self.timer.start(1000)

        self.model.start()
        Screensaver.enable_screensaver()
        sys.exit(app.exec())      

    def close(self) -> None:
        self.model.stop()

    def desklamp_click(self) -> None:
        self.relay.toggle_relay(PyRelay.BIG_2)

    def usb_switch_activate(self) -> None:
        self.relay.activate_relay(PyRelay.SMALL_1)
    
    def usb_switch_deactivate(self) -> None:
        self.relay.deactivate_relay(PyRelay.SMALL_1)

    def laptop_click(self) -> None:
        self.relay.toggle_relay(PyRelay.BIG_1)
    
    def timer_tick(self) -> None:
        time = QDateTime.currentDateTime()
        timeDisplay = time.toString('hh:mm')
        temp = self.__temp.temperature
        active_usb = "2" if self.__pysense.check_state(PySense.INPUT_1) else "1"

        self.view.update_text(timeDisplay, "%1.0f°C" % temp, active_usb)

    def on_receive(self, client_id:str, data:Metric) -> None:
        self.view.receive(client_id, data)
    
    def on_connect(self, client_id:str, name:str, cpu_count:int) -> None:
        Screensaver.disable_screensaver()
        self.view.add_metric_page(client_id, name, cpu_count)
    
    def on_disconnect(self, client_id:str) -> None:
        self.view.remove_metric_page(client_id)
        if self.view.metric_panel_count == 1:
            Screensaver.enable_screensaver()
