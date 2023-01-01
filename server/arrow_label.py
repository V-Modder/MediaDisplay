from enum import Enum
import typing

from PyQt5.QtCore import Qt, QPoint
from PyQt5.QtGui import QPaintEvent, QPainter, QPainterPath, QPolygon, QColor, QBrush, QGradient
from PyQt5.QtWidgets import QLabel, QWidget

class Direction(Enum):
    TOP = 0
    DOWN = 180
    RIGHT = 90
    LEFT = 270

class ArrowLabel(QLabel) : 
    __arrow_color : typing.Union[QBrush, QColor, Qt.GlobalColor, QGradient]
    __arrow_direction : Direction

    def __init__(self, parent: typing.Optional[QWidget] = ..., color: typing.Union[QBrush, QColor, Qt.GlobalColor, QGradient] = None, direction: Direction = None, flags: typing.Union[Qt.WindowFlags, Qt.WindowType] = ...) -> None:
        super.__init__(self, parent=parent, flags=flags)
    
    def paintEvent(self, a0: QPaintEvent) -> None:
        painter = QPainter(self)

        painter.save()
        painter.translate(self.width / 2, self.height / 2)
        painter.rotate(self.__arrow_direction)
        painter.translate(0, 0)

        polygon = QPolygon()
        # start at tip
        polygon.append(QPoint(self.width / 2, 0))
        # go right
        polygon.append(QPoint(self.width, self.height * 0.33))
        # go to middle
        polygon.append(QPoint(self.width - (self.width * 0.25), self.height * 0.33))
        # go down
        polygon.append(QPoint(self.width - (self.width * 0.25), self.height))
        # go down
        polygon.append(QPoint(self.width * 0.25, self.height))
        # go up
        polygon.append(QPoint(self.width * 0.25, self.height * 0.33))
        # go left
        polygon.append(QPoint(0, self.height * 0.33))
        # go to start
        polygon.append(QPoint(self.width / 2, 0))

        path = QPainterPath()
        path.addPolygon(polygon)

        painter.fillPath(path, self.__arrow_color)
        
        
        painter.restore()


        return super().paintEvent(a0)