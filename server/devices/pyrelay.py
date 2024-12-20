import logging
import time

from gpiozero import Device, OutputDevice
from gpiozero.pins.mock import MockFactory

from server.os.platform import Platform

logger = logging.getLogger(__name__)

class PyRelay:
    SMALL_1 = 19
    SMALL_2 = 26
    BIG_1 = 6
    BIG_2 = 13

    def __init__(self) -> None:
        if not Platform.is_raspberry_pi():
            #relay.off() # switch off
            #relay.on() # switch on
            #print(relay.value) # see if on or off
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
        #GPIO.output(relay_number, not GPIO.input(relay_number))

#try:
#
#    class PyRelay(PyRelayBase):
#        def __init__(self):
#            super().__init__()
#            #GPIO.setmode(GPIO.BCM)
#            #GPIO.setup(self.SMALL_1, GPIO.OUT)
#            #GPIO.setup(self.SMALL_2, GPIO.OUT, initial=GPIO.HIGH)
#            #GPIO.setup(self.BIG_1, GPIO.OUT, initial=GPIO.LOW)
#            #GPIO.setup(self.BIG_2, GPIO.OUT, initial=GPIO.LOW)
#
#        def activate_relay(self, relay_number):
#            if self._validate_input(relay_number):
#                state = GPIO.HIGH
#                if relay_number in [self.SMALL_1, self.SMALL_2]:
#                    state = GPIO.LOW
#                GPIO.output(relay_number, state)
#                super().activate_relay(relay_number)
#
#        def deactivate_relay(self, relay_number):
#            if self._validate_input(relay_number):
#                state = GPIO.LOW
#                if relay_number in [self.SMALL_1, self.SMALL_2]:
#                    state = GPIO.HIGH
#                GPIO.output(relay_number, state)
#                super().deactivate_relay(relay_number)
#        
#        def toggle_relay(self, relay_number):
#            
#except:
#    logger.info("RPi.GPIO couldn't be imported, using dummy relay")
#    class PyRelay(PyRelayBase):
#        def __init__(self) -> None:
#            super().__init__()
