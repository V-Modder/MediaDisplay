import logging

from gpiozero import Device, InputDevice
from gpiozero.pins.mock import MockFactory

from server.os.platform import Platform

logger = logging.getLogger(__name__)

class PySense:
    INPUT_1 = 24
    INPUT_2 = 23

    def __init__(self) -> None:
        if not Platform.is_raspberry_pi():
            logger.info("gpiozero couldn't find gpio's, using dummy sensor")
            Device.pin_factory = MockFactory()
        
        self.__senses = {
            PySense.INPUT_1: InputDevice(PySense.INPUT_1, pull_up=True),
            PySense.INPUT_2: InputDevice(PySense.INPUT_2, pull_up=True)
        }
        
    def _validate_input(self, input_pin) -> bool:
        return input_pin in self.__senses

    def check_state(self, input_pin) -> bool:
        if not self._validate_input(input_pin):
            return False
        
        dev = self.__senses.get(input_pin)
        return dev.value == 0
