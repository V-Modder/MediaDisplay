from socketio import Client, ClientNamespace
from threading import Thread
from time import sleep
import traceback

from metric.metric import Metric
from metric.metric_builder import MetricBuilder
from pydesktop.config import Config
from pydesktop.connection_status import ConnectionStatus
#from pydesktop.pydesktop import PyDesktop

class MetricSender(ClientNamespace):
    __sending_thread : Thread
    __client_thread : Thread
    __run_connecting : bool
    __run_sending : bool
    conf : Config
    __metric_builder : MetricBuilder

    def __init__(self, conf : Config) -> None:
        super().__init__(None)
        self.conf = conf
        socket = Client(reconnection_attempts=20)
        socket.register_namespace(self)
        self.__sending_thread = Thread(target=self.run_sending)
        self.__client_thread = Thread(target=self.run_client)
        self.__listener = None
        self.__run_connecting = False
        self.__run_sending = False
        self.__metric_builder = MetricBuilder()

    def start(self):
        self.__run_connecting = True
        self.__run_sending = True
        self.__client_thread.start()

    def stop(self):
        self.__run_connecting = False
        self.__run_sending = False
        self.client.disconnect()

    def on_connect(self):
        self.get_brightness()
        self.__run_sending = True
        if not self.__sending_thread.is_alive():
            self.__sending_thread.start()
        else:
            print("problem")
        print('connection established')

    def on_disconnect(self):
        self.__run_sending = False
        self.__sending_thread.join()
        self.__sending_thread = Thread(target=self.run_sending)
        print('disconnected from server')

    def on_receive_brightness(self, data):
        print('receive_brightness received with ', data)
        try:
            if self.__listener is not None:            
                self.__listener.brightness_received(data)
        except Exception:
            traceback.print_exc()

    def change_brightness(self, brightness):
        if self.client.connected:
            self.client.call("set_brightness", brightness)
    
    def get_brightness(self):
        if self.client.connected:
            self.client.call("get_brightness")

    def create_metric(self) -> Metric:
        return self.__metric_builder.build_metric(0.5)

    def run_sending(self):
        while self.__run_sending:
            try:
                if self.client.connected:
                    metric = self.create_metric()
                    if self.client.connected:
                        self.call("metric", metric.serialize(), timeout=3)
                else:
                    sleep(0.5)
            except Exception:
                traceback.print_exc()

    def run_client(self):
        while self.__run_connecting:
            try:
                self.client.connect(self.conf.server, headers={"Cpu-Count": str(MetricBuilder.cpu_core_count())})
                self.client.wait()
            except Exception:
                traceback.print_exc()
                sleep(0.5)

    def set_listener(self, listener):
        self.__listener = listener

    def change_conf(self, conf : Config):
        try:
            self.conf = conf
            self.stop()
            self.start()
        except Exception:
            traceback.print_exc()
    
    def get_status(self) -> ConnectionStatus:
        if not self.__run_connecting:
            return ConnectionStatus.DISCONNECTED
        elif not self.client.connected:
            return ConnectionStatus.CONNECTING
        else:
            return ConnectionStatus.CONNECTED
