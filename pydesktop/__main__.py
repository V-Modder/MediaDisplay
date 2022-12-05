import logging
from logging.handlers import RotatingFileHandler

from pydesktop.pydesktop import PyDesktop

log_format = ('[%(asctime)s] %(levelname)-8s %(name)-12s %(message)s')

def main():
	rotatingHandler = RotatingFileHandler(filename='pydesktop.log', maxBytes=1000)
	rotatingHandler.setLevel(logging.DEBUG)
	
	logging.basicConfig(
		level=logging.INFO,
		format=log_format
  	)

	logging.getLogger('').addHandler(rotatingHandler)

	desk = PyDesktop()
	desk.main()

main()
