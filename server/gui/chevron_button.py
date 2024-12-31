from enum import IntEnum

from PyQt5.QtCore import QPointF, Qt
from PyQt5.QtGui import QColor, QPainter, QPaintEvent, QPolygonF
from PyQt5.QtWidgets import QPushButton, QWidget


class ChevronButton(QPushButton):
    class Orientation(IntEnum):
        Left = 0
        Up = 90
        Right = 180
        Down = 270

    _buffer: int
    _thickness: int
    _color:QColor
    orientation:Orientation

    def __init__(self, parent: QWidget | None, buffer:int = 1, thickness:int = 10, color:QColor=QColor.fromRgb(26, 246, 248, 255), orientation:Orientation=Orientation.Left) -> None:
        super().__init__(parent)
        self._buffer = buffer
        self._thickness = thickness
        self._color = color
        self.orientation = orientation

    def paintEvent(self, a0: QPaintEvent | None) -> None:
        painter = QPainter(self)
        painter.setPen(self.get_color())
        painter.setBrush(self.get_color())
        painter.setRenderHint(QPainter.RenderHint.HighQualityAntialiasing)
        painter.save()
        xmid = self.width() / 2
        ymid = self.height() / 2
        painter.translate(xmid, ymid)
        painter.rotate(int(self.orientation))
        painter.translate(-xmid, -ymid)
        painter.drawPolygon(self.get_points())
        painter.restore()
        painter.end()
    
    def get_color(self) -> QColor | Qt.GlobalColor:
        if self.isEnabled():
            return self._color
        else: 
            return Qt.GlobalColor.gray

    def get_points(self) -> QPolygonF:
        # Points:
        # - A: x(max) - buffer | buffer
        # - B: x(max) - buffer | buffer + thickness
        # - C: buffer + thickness | y(max) / 2
        # - D: x(max) - buffer | y(max) - (buffer + thickness)
        # - E: x(max) - buffer | y(max) - buffer
        # - F: buffer | y(max) / 2
        points = QPolygonF() 
        points.append(QPointF(self.width() - self._buffer, self._buffer))
        points.append(QPointF(self.width() - self._buffer, self._buffer + self._thickness))
        points.append(QPointF(self._buffer + self._thickness, self.height() / 2))
        points.append(QPointF(self.width() - self._buffer, self.height() - (self._buffer + self._thickness)))
        points.append(QPointF(self.width() - self._buffer, self.height() - self._buffer))
        points.append(QPointF(self._buffer, self.height() / 2))

        return points
