from metric.metric import Metric

class PyStreamProtocol():
    def receive(self, client_id:str, data:Metric) -> None:
        ...
    
    def restore(self, client_id:str, cpu_count:int) -> None:
        ...
    
    def reset(self, client_id:str) -> None:
        ...
