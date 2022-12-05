import logging
from logging.handlers import RotatingFileHandler

log_format = ('[%(asctime)s] %(levelname)-8s %(name)-12s %(message)s')
rotatingHandler = RotatingFileHandler(filename='media_dispaly_server.log', maxBytes=1000)
rotatingHandler.setLevel(logging.DEBUG)

logging.basicConfig(
    level=logging.INFO,
    format=log_format
)

logging.getLogger('').addHandler(rotatingHandler)