from PyQt5.QtCore import Qt
from PyQt5.QtGui import QPaintEvent, QPainter, QImage
from PyQt5.QtWidgets import QWidget, QGridLayout

from metric.metric import GPU
from server.gui.gui_helper import GuiHelper

class GpuPanel(QWidget):
    __show_label : bool

    def __init__(self, parent = None) -> None:
        super().__init__(parent)

        grid = QGridLayout()
        grid.setContentsMargins(0, 0, 0, 0)

        self.label_gpu = GuiHelper.create_label(text="GPU", font_size=20)
        self.label_gpu_temp = GuiHelper.create_label(width=50, height=25, text="--°C")
        self.progress_gpu_load = GuiHelper.create_progressbar(height=15)
        self.progress_gpu_mem_load = GuiHelper.create_progressbar(height=15)

        grid.addWidget(self.label_gpu, 0, 0)
        grid.addWidget(self.progress_gpu_load, 0, 1)
        grid.addWidget(self.label_gpu_temp, 1, 0, Qt.AlignmentFlag.AlignRight)
        grid.addWidget(self.progress_gpu_mem_load, 1, 1)

        self.setLayout(grid)
        self.show_gui(False)
    
    def paintEvent(self, event: QPaintEvent) -> None:
        if self.__show_label:
            image = QImage()
            image.load("server/resource/gpu.png")
            painter = QPainter(self)
            painter.setRenderHint(QPainter.RenderHint.Antialiasing)
            painter.drawImage(0, 0, image.scaled(self.width(), self.height(), transformMode=Qt.TransformationMode.SmoothTransformation))
            painter.end()
        
        return super().paintEvent(event)

    def update_value(self, value: GPU) -> None:
        self.label_gpu_temp.setText("%1.0f°C" % value.temperature)
        self.progress_gpu_load.setValue(value.load)
        self.progress_gpu_mem_load.setValue(value.memory_load)

    def show_gui(self, value) -> None:
        self.__show_label = not value
        self.label_gpu.setVisible(value)
        self.label_gpu_temp.setVisible(value)
        self.progress_gpu_load.setVisible(value)
        self.progress_gpu_mem_load.setVisible(value)
        self.repaint()
