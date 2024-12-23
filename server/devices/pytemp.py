import glob
import os
import time
from typing import List

from PyQt5.QtCore import QThread


class PyTemp(QThread):
    temperature : float

    def __init__(self):
        super().__init__()
        os.system('modprobe w1-gpio')
        os.system('modprobe w1-therm')
        
        devices = glob.glob('/sys/bus/w1/devices/28*')
        if len(devices) > 0:
            device = devices[0]
            self.__device_file = device + '/w1_slave'
        else:
            self.__device_file = None   

        self.temperature = 0

    def run(self) -> None:
        while True:
            try:
                self.temperature = self.__read_temp()
            except:
                pass
            time.sleep(1)

    def __read_temp(self) -> float:
        lines = self.__read_temp_raw()
        if lines[0].strip()[-3:] == 'YES':
            return self.__convertTemp(lines[1])
        else:
            return 0

    def __read_temp_raw(self) -> List[str]:
        f = open(self.__device_file, 'r')
        lines = f.readlines()
        f.close()
        return lines

    def __convertTemp(self, line) -> float:
        equals_pos = line.find('t=')
        if equals_pos != -1:
            temp_string = line[equals_pos + 2:]
            return float(temp_string) / 1000.0
        else:
            raise Exception("No number")
