import eventlet
import eventlet.wsgi
from flask import Flask, request
from flask_socketio import SocketIO, Namespace
from threading import Thread

from metric.metric import Metric

class MetricServer(Namespace, Thread):
    _app : Flask

    def __init__(self, receiver):
        super(Namespace, self).__init__()
        Thread.__init__(self)
        self.__app = Flask(__name__)
        socketio = SocketIO(self.__app)
        socketio.on_namespace(self)
        self.__receiver = receiver

    def run(self):
        print("Starting server at port 5001")
        self.socketio.run(self.__app, host="127.0.0.1", port=5001)

    def stop(self):
        eventlet.wsgi.is_accepting = False
        eventlet.kill()

    def on_connect(self):
        print("socket connected, ", request.sid)
        print("Headers:", request.headers["Cpu-Count"])
        self.__receiver.restore(int(request.headers["Cpu-Count"]))

    def on_disconnect(self):
        print("socket disconnected")
        self.__receiver.reset()

    def on_get_brightness(self):
        print('received get_brightness: ')
        self.emit('receive_brightness', self.__receiver.get_brightness())

    def on_set_brightness(self, message):
        print('received set_brightness: ' + message)
        self.__receiver.set_brightness(message)

    def on_metric(self, message):
        metric = Metric.deserialize(message)
        #print('received metric: ')
        self.__receiver.receive(metric)
