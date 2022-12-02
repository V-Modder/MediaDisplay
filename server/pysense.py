class PySenseBase:
    INPUT_1 = 24
    INPUT_2 = 23

    def check_state(self, input_pin) -> bool:
        pass

try:
    import RPi.GPIO as GPIO
    class PySense(PySenseBase):
        def __init__(self):
            GPIO.setmode(GPIO.BCM)
            GPIO.setup(self.INPUT_1, GPIO.IN, pull_up_down=GPIO.PUD_UP)
            GPIO.setup(self.INPUT_2, GPIO.IN, pull_up_down=GPIO.PUD_UP)

        def check_state(self, input_pin) -> bool:
            return GPIO.input(input_pin) == 0
except:
    print("RPi.GPIO couldn't be imported, using dummy sense")
    import random
    class PySense(PySenseBase):
        def check_state(self, input_pin) -> bool:
            random.seed(input_pin)
            return random.randint(0, 100) % 2 == 0 
