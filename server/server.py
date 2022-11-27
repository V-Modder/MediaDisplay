import eventlet
import eventlet.wsgi
from flask import Flask, request
from flask_socketio import SocketIO, Namespace
from threading import Thread
import traceback

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
        print("Starting server at 0.0.0.0:5001")
        self.socketio.run(self.__app, host="0.0.0.0", port=5001)

    def stop(self):
        eventlet.wsgi.is_accepting = False
        eventlet.kill()

    def on_connect(self):
        print("socket connected, ", request.sid)
        print("Headers:", request.headers["Cpu-Count"])
        try:
            self.__receiver.restore(int(request.headers["Cpu-Count"]))
        except Exception as e:
            traceback.print_exception(e)

    def on_disconnect(self):
        print("socket disconnected")
        try:
            self.__receiver.reset()
        except Exception as e:
            traceback.print_exception(e)

    def on_get_brightness(self):
        print('received get_brightness: ')
        value = 0
        try:
            value = self.__receiver.get_brightness()
        except Exception as e:
            traceback.print_exception(e)
        self.emit('receive_brightness', value)

    def on_set_brightness(self, message):
        print('received set_brightness: ', message)
        try:
            self.__receiver.set_brightness(message)
        except Exception as e:
            traceback.print_exception(e)

    def on_metric(self, message):
        #print('received metric: ')
        try:
            metric = Metric.deserialize(message)
            self.__receiver.receive(metric)
        except Exception as e:
            traceback.print_exception(e)
