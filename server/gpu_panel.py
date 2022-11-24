from PyQt6.QtCore import Qt
from PyQt6.QtGui import QPaintEvent, QImage, QPainter
from PyQt6.QtWidgets import QWidget

from metric.metric import GPU
from server.gui_helper import GuiHelper

class GpuPanel(QWidget):
    __show_picture: bool

    def __init__(self, parent) -> None:
        super().__init__(parent)

        self.label_gpu = GuiHelper.create_label(self, 35, 0, text="GPU", font_size=20)
        self.label_gpu_temp = GuiHelper.create_label(self, 37, 36, text="--°C")
        self.progress_gpu_load = GuiHelper.create_progressbar(self, 100, 6, 160, 20)
        self.progress_gpu_mem_load = GuiHelper.create_progressbar(self, 100, 36, 160, 20)

        self.show_gui(False)

    def update_value(self, value: GPU):
        self.label_gpu_temp.setText("%1.0f°C" % value.temperature)
        self.progress_gpu_load.setValue(value.load)
        self.progress_gpu_mem_load.setValue(value.memory_load)

    def show_gui(self, value):
        self.__show_picture = not value
        self.label_gpu.setVisible(value)
        self.label_gpu_temp.setVisible(value)
        self.progress_gpu_load.setVisible(value)
        self.progress_gpu_mem_load.setVisible(value)
    
    def paintEvent(self, event: QPaintEvent) -> None:
        if self.__show_picture:
            image = QImage()
            image.load("server/resource/gpu.png")
            painter = QPainter(self)
            painter.setRenderHint(QPainter.RenderHint.Antialiasing)
            painter.drawImage(35, 0, image.scaled(229, 56, transformMode=Qt.TransformationMode.SmoothTransformation))
            painter.end()
        else:
            super().paintEvent(event)
