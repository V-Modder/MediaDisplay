from typing import Protocol
import eventlet
import eventlet.wsgi
from flask import Flask, request
from flask_socketio import Namespace, SocketIO 
import logging
from threading import Thread

from metric.metric import Metric
from server.devices.backlight_controller import BacklightController

logger = logging.getLogger(__name__)

class PyStreamPresenterProtocol(Protocol):
    def receive(self, client_id:str, data:Metric) -> None:
        ...
    
    def restore(self, client_id:str, cpu_count:int) -> None:
        ...
    
    def reset(self, client_id:str) -> None:
        ...

class MetricServer(Namespace, Thread):
    _app : Flask
    __presenter : PyStreamPresenterProtocol
    
    def __init__(self) -> None:
        super(Namespace, self).__init__()
        Thread.__init__(self)
        self.__app = Flask(__name__)
        socketio = SocketIO(self.__app)
        socketio.on_namespace(self)
        self.backlight = BacklightController()

    def init_server(self, presenter:PyStreamPresenterProtocol) -> None:
        self.__presenter = presenter

    def run(self) -> None:
        logger.info("Starting server at 0.0.0.0:5001")
        self.socketio.run(self.__app, host="0.0.0.0", port=5001)

    def stop(self) -> None:
        eventlet.wsgi.is_accepting = False
        eventlet.kill(Exception())

    def on_connect(self) -> None:
        logger.info("socket connected, " + str(request.sid))
        logger.debug("Headers: " + str(request.headers))
        #if len(self._connected_clients) > 0:
        #    self.disconnect()
        #else:
        #self._connected_clients.append(request.sid)
        try:
            self.__presenter.restore(str(request.sid), int(request.headers["Cpu-Count"]))
        except Exception as e:
            logger.error("Error connecting", exc_info=True)

    def on_disconnect(self) -> None:
        logger.info("socket disconnected")
        #if request.sid in self._connected_clients:    
        #    logger.info("Disconnecting, already in use")
        #    self._connected_clients.remove(request.sid)
        #else:
        try:
            self.__presenter.reset(str(request.sid))
        except Exception as e:
            logger.error("Error disconnecting", exc_info=True)

    def on_get_brightness(self) -> None:
        logger.info('Received get_brightness: ')
        value = 0
        try:
            value = self.backlight.get_brightness()
        except Exception as e:
            logger.error("Error getting brightness", exc_info=True)
        self.emit('receive_brightness', value)

    def on_set_brightness(self, message) -> None:
        logger.info('Received set_brightness: '+ str(message))
        try:
            self.backlight.set_brightness(message)
        except Exception as e:
            logger.error("Error setting brightness", exc_info=True)

    def on_metric(self, message) -> None:
        logger.debug('Received Metric')
        try:
            metric = Metric.deserialize(message)
            self.__presenter.receive(str(request.sid), metric)
        except Exception as e:
            logger.error("Error receiving metric", exc_info=True)
