import time

class PyRelayBase:
    SMALL_1 = 19
    SMALL_2 = 26
    BIG_1 = 6
    BIG_2 = 13

    def __init__(self) -> None:
        self.activasion = {}

    def _validate_input(self, input):
        return input in [self.SMALL_1, self.SMALL_2, self.BIG_1, self.BIG_2]
    
    def activate_relay(self, relay_number):
        self.activasion[relay_number] = time.time()
        print("an")

    def deactivate_relay(self, relay_number):
        if relay_number in self.activasion:
            pressed_time = time.time() - self.activasion[relay_number]
            if pressed_time < 0.5:
                time.sleep(0.5 - pressed_time)
            self.activasion.pop(relay_number)
            print("aus")
    
    def toggle_relay(self, relay_number):
        pass

try:
    import RPi.GPIO as GPIO
    class PyRelay(PyRelayBase):
        def __init__(self):
            super().__init__()
            GPIO.setmode(GPIO.BCM)
            GPIO.setup(self.SMALL_1, GPIO.OUT)
            GPIO.setup(self.SMALL_2, GPIO.OUT, initial=GPIO.HIGH)
            GPIO.setup(self.BIG_1, GPIO.OUT, initial=GPIO.LOW)
            GPIO.setup(self.BIG_2, GPIO.OUT, initial=GPIO.LOW)

        def activate_relay(self, relay_number):
            if self._validate_input(relay_number):
                state = GPIO.HIGH
                if relay_number in [self.SMALL_1, self.SMALL_2]:
                    state = GPIO.LOW
                GPIO.output(relay_number, state)
                super().activate_relay(relay_number)

        def deactivate_relay(self, relay_number):
            if self._validate_input(relay_number):
                state = GPIO.LOW
                if relay_number in [self.SMALL_1, self.SMALL_2]:
                    state = GPIO.HIGH
                GPIO.output(relay_number, state)
                super().deactivate_relay(relay_number)
        
        def toggle_relay(self, relay_number):
            if not self._validate_input(relay_number):
                return

            GPIO.output(relay_number, not GPIO.input(relay_number))
except:
    print("RPi.GPIO couldn't be imported, using dummy relay")
    class PyRelay(PyRelayBase):
        def __init__(self) -> None:
            super().__init__()
