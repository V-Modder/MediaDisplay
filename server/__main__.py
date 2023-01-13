import sys

from PyQt5.QtWidgets import QApplication

from server.pystream import PyStream
from server.server import MetricServer
from server.pystream_presenter import PyStreamPresenter

def main() -> None:
    app = QApplication(sys.argv)
    model = MetricServer()
    view = PyStream()
    presenter = PyStreamPresenter(view, model)
    presenter.run(app)

main()
