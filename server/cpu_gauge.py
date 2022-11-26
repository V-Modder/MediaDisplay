from PyQt5.QtCore import Qt
from PyQt5.QtGui import QResizeEvent, QPaintEvent, QPainter, QImage
from PyQt5.QtWidgets import QLabel, QWidget

from metric.metric import CPU
from server.gui_helper import GuiHelper
from server.analoggaugewidget import AnalogGaugeWidget

class CpuGauge(QWidget):
    value : int
    gauge : AnalogGaugeWidget
    label : QLabel

    def __init__(self, parent) -> None:
        super().__init__(parent)

        self.gauge = GuiHelper.create_gauge(self)
        
        self.label = GuiHelper.create_label(self, None, None, None, None, text="--°C", color="#00FFFF")
        self.label.setAlignment(Qt.AlignmentFlag.AlignCenter)

    def set_value(self, value: CPU):
        self.gauge.update_value(value.load)
        self.label.setText("%1.0f°C" % value.temperature)
    
    def resizeEvent(self, event: QResizeEvent) -> None:
        super().resizeEvent(event)
        self.resize()
        self.repaint()
    
    def resize(self):
        self.gauge.setGeometry(0, int(self.height() * 0.15), self.width(), int(self.height() * 0.85))
        self.label.setGeometry(0, int(self.height() * 0.78), self.width(), int(self.height() * 0.22))
    
    def paintEvent(self, event: QPaintEvent) -> None:
        image = QImage()
        image.load("server/resource/cpu_gauge.jpg")
        painter = QPainter(self)
        painter.setRenderHint(QPainter.RenderHint.Antialiasing)
        painter.drawImage(0, 0, image.scaled(self.width(), self.height(), transformMode=Qt.TransformationMode.SmoothTransformation))
        painter.end()
        return super().paintEvent(event)