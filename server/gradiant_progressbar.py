from PyQt5.QtCore import Qt
from PyQt5.QtWidgets import QProgressBar
from PyQt5.QtGui import QPainter, QLinearGradient, QBrush


class GradiantProgressBar(QProgressBar):
    def __init__(self, parent=None):
        super().__init__(parent=parent)
        self.background_color = Qt.GlobalColor. gray
        self.gradian_colors = [[.00, Qt.GlobalColor.cyan],
                                [.699, Qt.GlobalColor.cyan],
                                [.7, Qt.GlobalColor.yellow],
                                [.87, Qt.GlobalColor.red]
                                ]

    def paintEvent(self, event):
        painter = QPainter(self)
        painter.setRenderHint(QPainter.RenderHint.Antialiasing)

        painter.setBrush(QBrush(self.background_color))
        painter.drawRect(0,0, self.width(), self.height())

        grad = QLinearGradient(0, 0, self.width(), self.height())

        for eachcolor in self.gradian_colors:
            grad.setColorAt(eachcolor[0], eachcolor[1])
        
        painter.setBrush(QBrush(grad))
        painter.drawRect(0, 0, int(self.value() / 100 * self.width()), self.height())