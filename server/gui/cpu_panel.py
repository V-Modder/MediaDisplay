from typing import List

from PyQt5.QtWidgets import QGridLayout, QWidget
from metric.metric import CPU

from server.gui.cpu_gauge import CpuGauge

class CpuPanel(QWidget):
    cpus: List[CpuGauge]

    def __init__(self, parent=None) -> None:
        super().__init__(parent)

        layout = QGridLayout()
        layout.setSpacing(8)
        self.setLayout(layout)

        self.cpus = []

    def create_cpus(self, count: int) -> None:
        square = self.__calc_square(count)
        row = -1
        for i in range(0, count):
            col = i % square
            if col == 0:
                row += 1
            self.__add(CpuGauge(None), col, row)

    def clear(self) -> None:
        for cpu in self.cpus:
            self.layout().removeWidget(cpu)
        
        self.cpus = []

    def update_value(self, index, value) -> None:
        if len(self.cpus) > index:
            self.cpus[index].set_value(value)

    def __add(self, cpu: CpuGauge, x:int, y:int) -> None:
        self.cpus.append(cpu)
        self.layout().addWidget(cpu, y, x) # type: ignore
    
    def __calc_square(self, n:int) -> int:
        i=1
        while(i ** 2 < n):
            i += 1
        return i

    def reset_all(self) -> None:
        for cpu in self.cpus:
            cpu.set_value(CPU(0, 0))
