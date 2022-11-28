import sys

from PyQt5.QtCore import Qt, QTimer
from PyQt5.QtGui import QPalette
from PyQt5.QtWidgets import QLineEdit, QWidget, QLabel, QGridLayout, QSlider

from pydesktop.config import Config
from pydesktop.connection_status import ConnectionStatus
from pydesktop.metric_sender import MetricSender

class PyDesktopConfig(QWidget):
    def __init__(self, sender:MetricSender):
        super().__init__()

        self.setFixedWidth(800)
        self.setFixedHeight(600)
        layout = QGridLayout()
        layout.setSpacing(10)
        self.setLayout(layout)
        self.setWindowTitle("Media-Display client")

        self.lbl_status = QLabel("Disconnected")
        #self.lbl_status.setStyleSheet(
        #    """font-size: 20;"""#color: "red"; 
        #)
        font = self.lbl_status.font()
        font.setPointSize(font.pointSize() + 10)
        self.lbl_status.setFont(font)

        self.txt_server_address = QLineEdit()
        self.txt_server_address.setText(sender.conf.server)
        
        self.sld_brightness = QSlider()
        self.sld_brightness.setMaximum(100)
        self.sld_brightness.setMinimum(0)
        self.sld_brightness.setOrientation(Qt.Orientation.Horizontal)
        self.sld_brightness.setTickInterval(10)
        self.sld_brightness.setSingleStep(10)
        self.sld_brightness.setPageStep(20)
        self.sld_brightness.setTickPosition(QSlider.TickPosition.TicksBelow)
        self.sld_brightness.valueChanged.connect(self.on_sld_brightness_value_changed)
        self.sld_brightness.sliderPressed.connect(self.on_sld_brightness_slider_pressed)
        self.sld_brightness.sliderReleased.connect(self.on_sld_brightness_slider_released)

        layout.addWidget(QLabel("Status"), 0, 0)
        layout.addWidget(self.lbl_status, 0, 1)
        layout.addWidget(QLabel("Server"), 1, 0)
        layout.addWidget(self.txt_server_address, 1, 1)
        layout.addWidget(QLabel("Brightness"), 2, 0)
        layout.addWidget(self.sld_brightness, 2, 1)

        self.sld_brightness.setFocus(Qt.FocusReason.NoFocusReason)
        self.txt_server_address.editingFinished.connect(self.on_txt_server_changed)

        self.metric_sender = sender
        self.metric_sender.set_listener(self)
        self.metric_sender.get_brightness() 

        self.timer = QTimer()
        self.timer.timeout.connect(self.on_timer_tick)
        self.timer.start()
    
    def __del__(self):
        self.metric_sender.set_listener(None)

    def on_txt_server_changed(self):
        conf = Config(self.txt_server_address.text())
        conf.save()
        self.metric_sender.change_conf(conf)

    def on_sld_brightness_slider_pressed(self):
        self.sender().valueChanged.disconnect()

    def on_sld_brightness_slider_released(self):
        self.sender().valueChanged.connect(self.on_sld_brightness_value_changed)
        self.sender().valueChanged.emit(self.sender().value())

    def on_sld_brightness_value_changed(self):
        self.metric_sender.change_brightness(self.sender().value())

    def brightness_received(self, brightness):
        self.sld_brightness.valueChanged.disconnect()
        self.sld_brightness.setValue(brightness)
        self.sld_brightness.valueChanged.connect(self.on_sld_brightness_value_changed)

    def on_timer_tick(self):
        status = self.metric_sender.get_status()
        if status == ConnectionStatus.DISCONNECTED:
            text = "Disconnected"
            color = Qt.GlobalColor.red
        elif status == ConnectionStatus.CONNECTING:
            text = "Connecting"
            color = Qt.GlobalColor.darkYellow
        elif status == ConnectionStatus.CONNECTED:
            text = "Connected"
            color = Qt.GlobalColor.green
        
        self.lbl_status.setText(text)
        palette = QPalette()
        palette.setColor(QPalette.ColorRole.WindowText, color)
        self.lbl_status.setPalette(palette)
