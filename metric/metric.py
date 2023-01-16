from typing import List, Any, Optional
from dataclasses import dataclass
import json

@dataclass
class Network(object):
    up: Optional[int]
    down: Optional[int]

    def __init__(self, up: Optional[int] = None, down: Optional[int] = None) -> None:
        self.up = up
        self.down = down
    
    def __str__(self) -> str:
        return "[Network: up: {},\ndown: {}]".format(self.up, self.down)

    @staticmethod
    def from_dict(obj: Any) -> 'Network':
        up = int(obj.get("up"))
        down = int(obj.get("down"))
        return Network(up, down)

    def get_up_str(self) -> str:
        return self.__get_size(self.up)

    def get_down_str(self) -> str:
        return self.__get_size(self.down)

    def __get_size(self, bytes, suffix="B") -> str:
        factor = 1024
        for unit in ["", "K", "M", "G", "T", "P"]:
            if bytes < factor:
                return f"{bytes:.2f}{unit}{suffix}"
            bytes /= factor
        
        return bytes

@dataclass
class CPU(object):
    load: Optional[int]
    temperature: Optional[float]

    def __init__(self, load: Optional[int] = None, temperature: Optional[float] = None) -> None:
        self.load = load
        self.temperature = temperature

    def __str__(self) -> str:
        return "[CPU: load: {},\ntemperature: {}]".format(self.load, self.temperature)
    
    @staticmethod
    def from_dict(obj: Any) -> 'CPU':
        load = int(obj.get("load"))
        temperature = float(obj.get("temperature"))
        return CPU(load, temperature)

@dataclass
class GPU(object):
    load: Optional[int]
    memory_load: Optional[int]
    temperature: Optional[float]

    def __init__(self, load: Optional[int] = None, memory_load: Optional[int] = None, temperature: Optional[float] = None) -> None:
        self.load = load
        self.memory_load = memory_load
        self.temperature = temperature

    def __str__(self) -> str:
        return "[GPU: load: {},\nmemory_load: {},\ntemperature: {}]".format(self.load, self.memory_load, self.temperature)
    
    @staticmethod
    def from_dict(obj: Any) -> 'GPU':
        load = int(obj.get("load"))
        memory_load = int(obj.get("memory_load"))
        temperature = float(obj.get("temperature"))
        return GPU(load, memory_load, temperature)

@dataclass
class Metric(object):
    cpus: Optional[List[CPU]]
    gpu: Optional[GPU]
    memory_load: Optional[int]
    network: Optional[Network]

    def __init__(self, cpu: Optional[List[CPU]] = None, gpu: Optional[GPU] = None, memory_load: Optional[int] = None, network: Optional[Network] = None) -> None:
        self.cpus = cpu
        self.gpu = gpu
        self.memory_load = memory_load
        self.network = network

    def __str__(self) -> str:
        return "[Metric: cpus: {},\ngpu: {},\nmemory_load: {},\nnetwork: {}]".format(self.cpus, self.gpu, self.memory_load, self.network)

    @staticmethod
    def from_dict(obj: Any) -> 'Metric':
        if obj.get("cpus") is not None:
            cpus = [CPU.from_dict(y) for y in obj.get("cpus")]
        else:
            cpus = None

        if obj.get("gpu") is not None:
            gpu = GPU.from_dict(obj.get("gpu"))
        else:
            gpu = None

        memory_load = int(obj.get("memory_load"))

        if obj.get("network") is not None:
            network = Network.from_dict(obj.get("network"))
        else:
            network = None

        return Metric(cpus, gpu, memory_load, network)

    @staticmethod
    def deserialize(dump: str):
        return Metric.from_dict(json.loads(dump))

    def serialize(self) -> str:
        return json.dumps(self.__dict__, cls=MetricEncoder)

class MetricEncoder(json.JSONEncoder):
  def default(self, o) -> dict[str, Any] | Any:
    try:
        if isinstance(o, Metric) or isinstance(o, GPU) or isinstance(o, CPU) or isinstance(o, Network):
            return o.__dict__
    except TypeError:
        pass

    return json.JSONEncoder.default(self, o)
