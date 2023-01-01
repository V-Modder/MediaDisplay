from PyQt5.QtCore import Qt
from PyQt5.QtWidgets import QGridLayout, QWidget

from metric.metric import Network
from server.arrow_label import ArrowLabel, Direction
from server.gui_helper import GuiHelper

class NetworkPanel(QWidget):

    def __init__(self, parent) -> None:
        super().__init__(parent)

        grid = QGridLayout()
        grid.setContentsMargins(0, 0, 0, 0)
        
        arrow_up = ArrowLabel(Qt.GlobalColor.red, Direction.UP)
        arrow_up.setMaximumWidth(30)
        grid.addWidget(arrow_up, 0, 0)

        arrow_down = ArrowLabel(Qt.GlobalColor.green, Direction.DOWN)
        arrow_down.setMaximumWidth(30)
        grid.addWidget(arrow_down, 1, 0)
        
        self.label_net_up = GuiHelper.create_label(text="0B")
        grid.addWidget(self.label_net_up, 0, 1, Qt.AlignmentFlag.AlignRight)
        
        self.label_net_down = GuiHelper.create_label(text="0B")
        grid.addWidget(self.label_net_down, 1, 1, Qt.AlignmentFlag.AlignRight)

        self.setLayout(grid)
        
    def update_values(self, network: Network) -> None:
        self.label_net_down.setText(network.get_down_str())
        self.label_net_up.setText(network.get_up_str())
    
    def reset(self):
        self.label_net_down.setText("0B")
        self.label_net_up.setText("0B")
