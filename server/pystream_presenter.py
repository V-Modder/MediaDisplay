from typing import Protocol

from metric.metric import Metric
#https://github.com/ArjanCodes/2022-gui/blob/main/mvp

class PyStreamProtocol(Protocol):
    def init_ui(self, presenter:PyStreamPresenter) -> None:
        ...

    def mainloop(self) -> None:
        ...

    def receive(self, client_id:str, data:Metric) -> None:
        ...
    
    def restore(self, client_id:str, cpu_count:int) -> None:
        ...
    
    def reset(self, client_id:str) -> None:
        ...

class ServerProtocol(Protocol):
    def init_server(self, presenter:PyStreamPresenter) -> None:
        ...

    def start(self) -> None:
        ...

    def stop(self) -> None:
        ...

class PyStreamPresenter:
    view:PyStreamProtocol
    model:ServerProtocol

    def __init__(self, view:PyStreamProtocol, model:ServerProtocol) -> None:
        self.view = view
        self.model = model

    def run(self) -> None:
        self.view.init_ui(self)
        self.model.init_server(self)

        self.model.start()
        self.view.mainloop()
