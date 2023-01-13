import os 
if os.name == 'nt': import clr
import GPUtil
import platform
from pathlib import Path
import psutil

from metric.metric import Metric, CPU, GPU, Network

class MetricBuilder():
    if os.name == 'nt':
        HWTYPES = ['Mainboard','SuperIO','CPU','RAM','GpuNvidia','GpuAti','TBalancer','Heatmaster','HDD']
        SENSORTYPES = ['Voltage','Clock','Temperature','Load','Fan','Flow','Control','Level','Factor','Power','Data','SmallData']

        def __init__(self) -> None:
            self.computer = self.initialize_cputhermometer()
        
        def __del__(self) -> None:
            if self.computer is not None:
                self.computer.Close()

        def initialize_cputhermometer(self) -> Hardware.Computer:
            file = str(Path(__file__).parent.absolute()) + '\\OpenHardwareMonitorLib.dll'
            clr.AddReference(str(file))

            from OpenHardwareMonitor import Hardware

            computer = Hardware.Computer()
            computer.CPUEnabled = True
            computer.GPUEnabled = True
            computer.Open()
            return computer

    def build_metric(self, interval: float) -> Metric:
        met = Metric()
        met.cpus = []

        net_io = psutil.net_io_counters()
        
        perc = 0
        cpu_ratio = psutil.cpu_count(True) / psutil.cpu_count(False)
        percentage = 0
        
        for i, percentage in enumerate(psutil.cpu_percent(percpu=True, interval=interval)): # type: ignore 
            if i % cpu_ratio == 0:
                met.cpus.append(CPU((percentage + perc) / (100 * cpu_ratio) * 100))
            else:
                perc = percentage
        
        index = 0
        for temp in self.fetch_temperatures():
            met.cpus[index].temperature = temp
            index += 1

        svmem = psutil.virtual_memory()
        met.memory_load = int(svmem.percent)

        net_io_2 = psutil.net_io_counters()
        met.network = Network(int((net_io_2.bytes_sent - net_io.bytes_sent) * (1.0 / interval)), int((net_io_2.bytes_recv - net_io.bytes_recv) * (1.0 / interval)))

        met.gpu = self.fetch_gpu()

        met.hostname = platform.node()

        return met


    if os.name == 'nt':
        def fetch_temperatures(self) -> list[float]:
            result = []
            for h in self.computer.Hardware:
                if int(h.HardwareType) == MetricBuilder.HWTYPES.index('CPU'):
                    h.Update()
                    for sensor in h.Sensors:
                        if int(sensor.SensorType) == MetricBuilder.SENSORTYPES.index('Temperature') \
                            and sensor.Name.startswith("CPU Core #"):
                            result.append(sensor.Value)

            return result
    else:
        def fetch_temperatures(self) -> list[float]:
            result = []
            for temp in psutil.sensors_temperatures()["coretemp"]:
                if temp.label.startswith("Core"):
                    result.append(temp.current)
            
            return result

    if os.name == 'nt':
        def fetch_gpu(self) -> GPU:
            for h in self.computer.Hardware:
                if int(h.HardwareType) == MetricBuilder.HWTYPES.index('GpuNvidia'):
                    h.Update()
                    gpu = GPU()
                    for sensor in h.Sensors:
                        if int(sensor.SensorType) == MetricBuilder.SENSORTYPES.index('Temperature') \
                            and sensor.Name.startswith("GPU Core"):
                            gpu.temperature = sensor.Value
                        elif int(sensor.SensorType) == MetricBuilder.SENSORTYPES.index('Load') \
                              and sensor.Name.startswith("GPU Core"):
                            gpu.load = sensor.Value
                        elif int(sensor.SensorType) == MetricBuilder.SENSORTYPES.index('Load') \
                              and sensor.Name.startswith("GPU Memory"):
                            gpu.memory_load = sensor.Value
                    return gpu
            return None
    else:
        def fetch_gpu(self) -> GPU:
            gpus = GPUtil.getGPUs()
            for gpu in gpus:
                return GPU(gpu.load * 100, gpu.memoryUsed / gpu.memoryTotal * 100, gpu.temperature)
            return GPU(38, 46, 60)

    @staticmethod
    def cpu_core_count() -> int:
        return psutil.cpu_count(False)
