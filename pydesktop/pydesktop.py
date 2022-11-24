import sys

from PyQt6.QtGui import QIcon
from PyQt6.QtWidgets import QApplication, QMenu, QSystemTrayIcon

from pydesktop.config import Config
from pydesktop.metric_sender import MetricSender

class PyDesktop:
    def __init__(self) -> None:
        conf = Config.load_or_create()
        self.sender = MetricSender(conf)
        self.app = QApplication(sys.argv)
        self.app.setQuitOnLastWindowClosed(False)

    def show(self):
        if self.window is None:
            self.window = PyDesktop()
            self.window.show()

    def exit(self):
        self.app.quit()
        self.sender.stop()

    def start(self):
        tray_icon = QSystemTrayIcon(QIcon('pydesktop/resource/pydesktop.png'), parent=self.app)
        tray_icon.setToolTip('Media-Display')
        tray_icon.activated.connect(self.show)
        tray_icon.show()

        menu = QMenu()
        show_action = menu.addAction('Show')
        show_action.triggered.connect(self.show)

        exit_action = menu.addAction('Exit')
        exit_action.triggered.connect(self.exit)

        tray_icon.setContextMenu(menu)

        sys.exit(self.app.exec())

    def main(self):
        self.sender.start()
        self.start()
