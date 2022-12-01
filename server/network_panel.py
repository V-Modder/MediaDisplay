from PyQt5.QtCore import Qt
from PyQt5.QtWidgets import QGridLayout, QWidget

from metric.metric import Network
from server.gui_helper import GuiHelper

class NetworkPanel(QWidget):

    def __init__(self, parent) -> None:
        super().__init__(parent)

        grid = QGridLayout()
        grid.setContentsMargins(0, 0, 0, 0)
        
        grid.addWidget(GuiHelper.create_label(None, text="Down"), 0, 0)
        grid.addWidget(GuiHelper.create_label(None, text="Up"), 1, 0)
        self.label_net_down = GuiHelper.create_label(None, text="0")
        self.label_net_up = GuiHelper.create_label(None, text="0")

        grid.addWidget(self.label_net_down, 0, 1, Qt.AlignmentFlag.AlignRight)
        grid.addWidget(self.label_net_up, 1, 1, Qt.AlignmentFlag.AlignRight)

        self.setLayout(grid)
    
    def update_values(self, network: Network) -> None:
        self.label_net_down.setText(network.get_down_str())
        self.label_net_up.setText(network.get_up_str())
    
    def reset(self):
        self.label_net_down.setText("0B")
        self.label_net_up.setText("0B")
