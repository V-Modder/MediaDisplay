from enum import IntEnum
import random
import sys
import typing

from PyQt5.QtCore import Qt, QPoint
from PyQt5.QtGui import QPaintEvent, QPainter, QPainterPath, QPolygonF, QColor, QBrush, QGradient
from PyQt5.QtWidgets import QWidget, QApplication, QPushButton, QVBoxLayout, QGridLayout

class Direction(IntEnum):
    UP = 0
    DOWN = 180
    RIGHT = 90
    LEFT = 270

class ArrowLabel(QWidget) : 
    arrow_color : typing.Union[QBrush, QColor, Qt.GlobalColor, QGradient]
    arrow_direction : Direction

    def __init__(self, parent: QWidget, color: typing.Union[QBrush, QColor, Qt.GlobalColor, QGradient], direction: Direction) -> None:
        super().__init__(parent)
        self.arrow_color = color
        self.arrow_direction = direction
    
    def set_color(self, color: typing.Union[QBrush, QColor, Qt.GlobalColor, QGradient]) -> None:
        self.arrow_color = color
        self.repaint()

    def set_direction(self, direction: Direction) -> None:
        self.arrow_direction = direction
        self.repaint()

    def paintEvent(self, a0: QPaintEvent) -> None:
        painter = QPainter(self)

        painter.save()
        painter.translate(self.width() // 2, self.height() // 2)
        painter.rotate(int(self.arrow_direction))
        

        if self.arrow_direction == Direction.UP or self.arrow_direction == Direction.DOWN:
            width = self.width()
            height = self.height()
            painter.translate(self.width() // -2, self.height() // -2)
        else:
            width = self.height()
            height = self.width()
            painter.translate(self.height() // -2, self.width() // -2)
            
        polygon = QPolygonF()
        # start at tip
        polygon.append(QPoint(width // 2, 0))
        # go right
        polygon.append(QPoint(width, round(height * 0.33)))
        # go to middle
        polygon.append(QPoint(width - round(width * 0.25), round(height * 0.33)))
        # go down
        polygon.append(QPoint(width - round(width * 0.25), height))
        # go down
        polygon.append(QPoint(round(width * 0.25), height))
        # go up
        polygon.append(QPoint(round(width * 0.25), round(height * 0.33)))
        # go left
        polygon.append(QPoint(0, round(height * 0.33)))
        # go to start
        polygon.append(QPoint(width // 2, 0))

        path = QPainterPath()
        path.addPolygon(polygon)

        painter.fillPath(path, self.arrow_color)
        
        painter.restore()

        return super().paintEvent(a0)

if __name__ == "__main__":
    app = QApplication(sys.argv)
    w = QWidget()
    main_layout = QVBoxLayout()
    w.setLayout(main_layout)

    btnw = QWidget()
    btn_layout = QGridLayout()
    btnw.setLayout(btn_layout)

    b1 = QPushButton("up")
    b1.clicked.connect(lambda:arrow.set_direction(Direction.UP))
    btn_layout.addWidget(b1, 0, 0)

    b2 = QPushButton("Down")
    b2.clicked.connect(lambda:arrow.set_direction(Direction.DOWN))
    btn_layout.addWidget(b2, 0, 1)

    b3 = QPushButton("Left")
    b3.clicked.connect(lambda:arrow.set_direction(Direction.LEFT))
    btn_layout.addWidget(b3, 1, 0)

    b4 = QPushButton("Right")
    b4.clicked.connect(lambda:arrow.set_direction(Direction.RIGHT))
    btn_layout.addWidget(b4, 1, 1)

    random.seed()
    b5 = QPushButton("Random color")
    b5.clicked.connect(lambda:arrow.set_color(QColor(random.randint(0, 255), random.randint(0, 255), random.randint(0, 255), random.randint(0, 255))))
    btn_layout.addWidget(b5, 2, 0, 1, 2)

    main_layout.addWidget(btnw)

    arrow = ArrowLabel(w, Qt.GlobalColor.red, Direction.UP)
    main_layout.addWidget(arrow)

    w.setFixedSize(400, 400)
    w.setWindowTitle("PyQt5")
    w.show()
    sys.exit(app.exec_())