import logging
import time

from gpiozero import Device, OutputDevice
from gpiozero.pins.mock import MockFactory

from server.os.platform import Platform

logger = logging.getLogger(__name__)

class PyRelay:
    SMALL_1 = 19
    SMALL_2 = 26
    BIG_1 = 13
    BIG_2 = 6

    def __init__(self) -> None:
        if not Platform.is_raspberry_pi():
            logger.info("gpiozero couldn't find gpio's, using dummy relay")
            Device.pin_factory = MockFactory()

        self.__relays = {
            PyRelay.SMALL_1: OutputDevice(PyRelay.SMALL_1, active_high=False, initial_value=False),
            PyRelay.SMALL_2: OutputDevice(PyRelay.SMALL_2, active_high=False, initial_value=False),
            PyRelay.BIG_1: OutputDevice(PyRelay.BIG_1, active_high=True, initial_value=False),
            PyRelay.BIG_2: OutputDevice(PyRelay.BIG_2, active_high=True, initial_value=False)
        }

        self.activasion = {}

    def _validate_input(self, input) -> bool:
        return input in self.__relays
    
    def activate_relay(self, relay_number) -> None:
        if not self._validate_input(relay_number):
            return
        
        self.activasion[relay_number] = time.time()
        logger.info("relay on {}", relay_number)
        dev = self.__relays.get(relay_number)
        dev.on()

    def deactivate_relay(self, relay_number) -> None:
        if not self._validate_input(relay_number):
            return
        
        logger.info("relay off")
        dev = self.__relays.get(relay_number)
        dev.off()

        if relay_number in self.activasion:
            pressed_time = time.time() - self.activasion[relay_number]
            if pressed_time < 0.5:
                time.sleep(0.5 - pressed_time)
            self.activasion.pop(relay_number)
        
    def toggle_relay(self, relay_number) -> None:
        if not self._validate_input(relay_number):
            return

        dev = self.__relays.get(relay_number)
        dev.toggle()
