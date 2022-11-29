"""
import clr
import os

openhardwaremonitor_hwtypes = ['Mainboard','SuperIO','CPU','RAM','GpuNvidia','GpuAti','TBalancer','Heatmaster','HDD']
openhardwaremonitor_sensortypes = ['Voltage','Clock','Temperature','Load','Fan','Flow','Control','Level','Factor','Power','Data','SmallData']

def initialize_cputhermometer():
    file = 'D:\workspace\MediaDisplay\OpenHardwareMonitorLib.dll'
    clr.AddReference(file)

    from OpenHardwareMonitor import Hardware

    computer = Hardware.Computer()
    computer.CPUEnabled = True
    computer.Open()
    return computer

def fetch_stats(computer):
    for h in computer.Hardware:
        h.Update()
        for sensor in h.Sensors:
            parse_sensor(sensor)
        
        for sh in h.SubHardware:
            sh.Update()
            for subsensor in sh.Sensors:
                parse_sensor(subsensor)

def parse_sensor(sensor):
        if sensor.Value is not None:
            if int(sensor.SensorType) == openhardwaremonitor_sensortypes.index('Temperature'):
                unit = u"\u00B0C"
            elif int(sensor.SensorType) == openhardwaremonitor_sensortypes.index('Clock'):
                unit = "Mhz"
            elif int(sensor.SensorType) == openhardwaremonitor_sensortypes.index('Load'):
                unit = "%"
            elif int(sensor.SensorType) == openhardwaremonitor_sensortypes.index('Power'):
                unit = "W"
            
            print(u"%s %s  Sensor #%i %s %s - %s%s" % (sensor.Hardware.HardwareType, sensor.Hardware.Name, sensor.Index, sensor.Name, sensor.SensorType, sensor.Value, unit))

computer = initialize_cputhermometer()
fetch_stats(computer)
"""

import clr
import GPUtil
import psutil
import os 

from metric.metric import Metric, CPU, GPU, Network

class MetricBuilder():
    if os.name == 'nt':
        HWTYPES = ['Mainboard','SuperIO','CPU','RAM','GpuNvidia','GpuAti','TBalancer','Heatmaster','HDD']
        SENSORTYPES = ['Voltage','Clock','Temperature','Load','Fan','Flow','Control','Level','Factor','Power','Data','SmallData']

        def __init__(self) -> None:
            self.computer = self.initialize_cputhermometer()
        
        def __del__(self):
            if self.computer is not None:
                self.computer.Close()

        def initialize_cputhermometer(self):
            file = 'D:\workspace\MediaDisplay\OpenHardwareMonitorLib.dll'
            clr.AddReference(file)

            from OpenHardwareMonitor import Hardware

            computer = Hardware.Computer()
            computer.CPUEnabled = True
            computer.Open()
            return computer

    def build_metric(self, interval: float) -> Metric:
        met = Metric()
        met.cpus = []

        net_io = psutil.net_io_counters()
        
        perc = 0
        cpu_ratio = psutil.cpu_count(True) / psutil.cpu_count(False)
        percentage = 0

        for i, percentage in enumerate(psutil.cpu_percent(percpu=True, interval=interval)):
            if i % cpu_ratio == 0:
                met.cpus.append(CPU((percentage + perc) / (100 * cpu_ratio) * 100))
            else:
                perc = percentage
        
        index = 0
        for temp in self.fetch_temperatures():
            met.cpus[index].temperature = temp
            index += 1

        svmem = psutil.virtual_memory()
        met.memory_load = svmem.percent

        net_io_2 = psutil.net_io_counters()
        met.network = Network((net_io_2.bytes_sent - net_io.bytes_sent) * (1.0 / interval), (net_io_2.bytes_recv - net_io.bytes_recv) * (1.0 / interval))

        gpus = GPUtil.getGPUs()
        for gpu in gpus:
            met.gpu = GPU(gpu.load * 100, gpu.memoryUsed / gpu.memoryTotal * 100, gpu.temperature)

        return met


    if os.name != 'nt':
        def fetch_temperatures(self):
            result = []
            for temp in psutil.sensors_temperatures()["coretemp"]:
                if temp.label.startswith("Core"):
                    result.append(temp.current)
            
            return result
    else:
        def fetch_temperatures(self):
            result = []
            for h in self.computer.Hardware:
                h.Update()
                for sensor in h.Sensors:
                    if int(sensor.SensorType) == MetricBuilder.SENSORTYPES.index('Temperature') and sensor.Name.startswith("CPU Core #"):
                        result.append(sensor.Value)
            
            return result

    def cpu_core_count():
        return psutil.cpu_count(False)

b = MetricBuilder()
m = b.build_metric(0.5)
print(m)