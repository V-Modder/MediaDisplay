import glob
import os

class PyTemp():

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

        self.last_temperature = 0

    def read(self) -> int:
        try:
            temp = self.__read_temp()
            self.last_temperature = temp
            return temp
        except:
            return self.last_temperature

    def __read_temp(self):
        lines = self.__read_temp_raw()
        if lines[0].strip()[-3:] == 'YES':
            return self.__convertTemp(lines[1])

    def __read_temp_raw(self):
        f = open(self.__device_file, 'r')
        lines = f.readlines()
        f.close()
        return lines

    def __convertTemp(self, line):
        equals_pos = line.find('t=')
        if equals_pos != -1:
            temp_string = line[equals_pos + 2:]
            return float(temp_string) / 1000.0
        else:
            raise Exception("No number")
