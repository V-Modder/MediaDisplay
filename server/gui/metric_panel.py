import logging

from PyQt5.QtCore import Qt
from PyQt5.QtGui import QPalette
from PyQt5.QtWidgets import QHBoxLayout, QProgressBar, QVBoxLayout, QWidget

from metric.metric import Metric
from server.gui.cpu_panel import CpuPanel
from server.gui.gpu_panel import GpuPanel
from server.gui.gui_helper import GuiHelper
from server.gui.network_panel import NetworkPanel
from server.gui.stack_panel import StackPanel

logger = logging.getLogger(__name__)

class MetricPanel(StackPanel):
    cpu_panel : CpuPanel
    gpu_panel : GpuPanel
    network_panel : NetworkPanel
    progress_mem_load : QProgressBar

    def __init__(self, name, cpu_count:int) -> None:
        super().__init__()
        self.set_panel_name(name)
        pal = QPalette()
        pal.setColor(QPalette.ColorRole.Background, Qt.GlobalColor.black)
        self.setAutoFillBackground(True)
        self.setPalette(pal)

        main_layout = QVBoxLayout()
        main_layout.setContentsMargins(0, 0, 0, 0)
        self.setLayout(main_layout)

        self.cpu_panel = CpuPanel()
        self.cpu_panel.create_cpus(cpu_count)
        main_layout.addWidget(self.cpu_panel, 729166667)

        bottom_panel = QWidget()
        bottom_panel.setObjectName("panel_metric_bottom")
        bottom_panel.setStyleSheet("""#panel_metric_bottom {
            border-image: url(server/resource/panel_metric_bottom.jpg) 0 0 0 0 stretch stretch;
        }""")
        bottom_panel_layout = QHBoxLayout()
        bottom_panel_layout.setContentsMargins(5, 20, 0, 0)
        bottom_panel.setLayout(bottom_panel_layout)
        main_layout.addWidget(bottom_panel, 104166667)

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
        if data.cpus is None:
            self.cpu_panel.reset_all()
        else:
            for cpu in data.cpus:
                self.cpu_panel.update_value(i, cpu)
                i += 1

        self.progress_mem_load.setValue(0 if data.memory_load is None else data.memory_load)
        
        if data.gpu is not None:
            self.gpu_panel.show_gui(True)
            self.gpu_panel.update_value(data.gpu)
        else:
            self.gpu_panel.show_gui(False)
        
        if data.network is not None:
            self.network_panel.update_values(data.network)
        else:
            self.network_panel.reset()
