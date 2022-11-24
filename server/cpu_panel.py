from typing import List

from PyQt6.QtWidgets import QVBoxLayout, QHBoxLayout, QWidget

from server.cpu_gauge import CpuGauge

class CpuPanel(QWidget):
    cpus: List[CpuGauge]

    def __init__(self, parent) -> None:
        super().__init__(parent)

        layout = QVBoxLayout()
        layout.setSpacing(12)
        self.top_row = QHBoxLayout()
        self.bottom_row = QHBoxLayout()
        layout.addLayout(self.top_row)
        layout.addLayout(self.bottom_row)
        self.setLayout(layout)

        self.cpus = []

    def add(self, cpu: CpuGauge):
        self.cpus.append(cpu)
        if len(self.cpus) % 2 == 0:
            self.bottom_row.addWidget(cpu)
        else:
            self.top_row.addWidget(cpu)

    def create_cpus(self, count: int):
        for i in range(0, count):
            self.add(CpuGauge(None))

    def clear(self):
        i = 0
        for cpu in self.cpus:
            if i % 2 == 0:
                self.top_row.removeWidget(cpu)
            else:
                self.bottom_row.removeWidget(cpu)
            i += 1
        
        self.cpus = []

    def update_value(self, index, value):
        self.cpus[index].set_value(value)
