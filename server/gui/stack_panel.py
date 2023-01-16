from PyQt5.QtGui import QPaintEvent, QPainter
from PyQt5.QtWidgets import QStyle, QStyleOption, QWidget

class StackPanel(QWidget):
    __panel_name:str

    def __init__(self) -> None:
        super().__init__()

    def get_panel_name(self) -> str:
        return self.__panel_name

    def set_panel_name(self, name) -> None:
        self.__panel_name = name

    def paintEvent(self, a0: QPaintEvent) -> None:
        opt = QStyleOption()
        opt.initFrom(self)
        p = QPainter(self)
        self.style().drawPrimitive(QStyle.PrimitiveElement.PE_Widget, opt, p, self)
        return super().paintEvent(a0)
