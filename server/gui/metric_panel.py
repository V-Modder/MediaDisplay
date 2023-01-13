import logging

from PyQt5.QtCore import Qt
from PyQt5.QtWidgets import QHBoxLayout, QLabel, QProgressBar, QVBoxLayout, QWidget

from metric.metric import Metric
from server.gui.cpu_panel import CpuPanel
from server.gui.gpu_panel import GpuPanel
from server.gui.gui_helper import GuiHelper
from server.gui.network_panel import NetworkPanel

logger = logging.getLogger(__name__)

class MetricPanel(QWidget):
    cpu_panel : CpuPanel
    gpu_panel : GpuPanel
    network_panel : NetworkPanel
    progress_mem_load : QProgressBar

    def __init__(self, cpu_count:int) -> None:
        super().__init__()
        background_1 = QLabel(self)
        background_1.setGeometry(0, 0, 800, 480)
        background_1.setStyleSheet("background-image: url(server/resource/page_1.jpg);")

        self.lbl_hostname = GuiHelper.create_label(self, 0, 0, self.width(), self.height(), text="Hostname", font_size=10)
        self.lbl_hostname.setAlignment(Qt.AlignmentFlag.AlignHCenter | Qt.AlignmentFlag.AlignTop) # type: ignore
 
        self.cpu_panel = CpuPanel(self)
        self.cpu_panel.setGeometry(26, 25, 748, 350)
        self.cpu_panel.create_cpus(cpu_count)

        bottom_panel = QWidget(self)
        #bottom_panel.setStyleSheet("QWidget { border-color: red; border-width: 2px; border-style: solid;}")
        bottom_panel.setGeometry(35, 390, 730, 50)
        bottom_panel_layout = QHBoxLayout()
        bottom_panel_layout.setContentsMargins(0, 0, 0, 0)
        bottom_panel.setLayout(bottom_panel_layout)

        self.gpu_panel = GpuPanel()
        bottom_panel_layout.addWidget(self.gpu_panel, 31)

        self.network_panel = NetworkPanel()
        bottom_panel_layout.addWidget(self.network_panel, 38)

        memory_panel = QWidget()
        memory_panel_layout = QVBoxLayout()
        memory_panel_layout.setContentsMargins(0, 0, 9, 0)
        memory_panel.setLayout(memory_panel_layout)
        
        memory_panel_layout.addWidget(GuiHelper.create_label(memory_panel, text="Memory", font_size=18), 60)

        self.progress_mem_load = GuiHelper.create_progressbar()
        memory_panel_layout.addWidget(self.progress_mem_load, 30)

        bottom_panel_layout.addWidget(memory_panel, 31)
    
    def receive(self, data:Metric) -> None:
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
            self.network_panel.update_values(data.network)
        else:
            self.network_panel.reset()

        self.lbl_hostname.setText(data.hostname)
