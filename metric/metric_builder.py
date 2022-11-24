import GPUtil
import psutil

from metric.metric import Metric, CPU, GPU, Network

class MetricBuilder():
    def build_metric(interval: float) -> Metric:
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
        for temp in psutil.sensors_temperatures()["coretemp"]:
            if temp.label.startswith("Core"):
                met.cpus[index].temperature = temp.current
                index += 1

        svmem = psutil.virtual_memory()
        met.memory_load = svmem.percent

        net_io_2 = psutil.net_io_counters()
        met.network = Network((net_io_2.bytes_sent - net_io.bytes_sent) * (1.0 / interval), (net_io_2.bytes_recv - net_io.bytes_recv) * (1.0 / interval))

        gpus = GPUtil.getGPUs()
        for gpu in gpus:
            met.gpu = GPU(gpu.load * 100, gpu.memoryUsed / gpu.memoryTotal * 100, gpu.temperature)

        return met
    
    def cpu_core_count():
        return psutil.cpu_count(False)
