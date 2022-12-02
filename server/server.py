import eventlet
import eventlet.wsgi
from flask import Flask, request
from flask_socketio import SocketIO, Namespace
from threading import Thread
import traceback
import typing

from metric.metric import Metric

class MetricServer(Namespace, Thread):
    _app : Flask
    _connected_clients : typing.List[str]

    def __init__(self, receiver):
        super(Namespace, self).__init__()
        Thread.__init__(self)
        self.__app = Flask(__name__)
        socketio = SocketIO(self.__app)
        socketio.on_namespace(self)
        self.__receiver = receiver
        self._connected_clients = []

    def run(self):
        print("Starting server at 0.0.0.0:5001")
        self.socketio.run(self.__app, host="0.0.0.0", port=5001)

    def stop(self):
        eventlet.wsgi.is_accepting = False
        eventlet.kill(Exception())

    def on_connect(self):
        print("socket connected, ", request.sid)
        print("Headers:", request.headers["Cpu-Count"])
        if len(self._connected_clients) > 0:
            self.disconnect()
        else:
            self._connected_clients.append(request.sid)
            try:
                self.__receiver.restore(int(request.headers["Cpu-Count"]))
            except Exception as e:
                traceback.print_exc()

    def on_disconnect(self):
        print("socket disconnected")
        if request.sid in self._connected_clients:    
            print("Disconnecting, already in use")
            self._connected_clients.remove(request.sid)
        else:
            try:
                self.__receiver.reset()
            except Exception as e:
                traceback.print_exc()

    def on_get_brightness(self):
        print('received get_brightness: ')
        value = 0
        try:
            value = self.__receiver.get_brightness()
        except Exception as e:
            traceback.print_exc()
        self.emit('receive_brightness', value)

    def on_set_brightness(self, message):
        print('received set_brightness: ', message)
        try:
            self.__receiver.set_brightness(message)
        except Exception as e:
            traceback.print_exc()

    def on_metric(self, message):
        try:
            metric = Metric.deserialize(message)
            self.__receiver.receive(metric)
        except Exception as e:
            traceback.print_exc()
