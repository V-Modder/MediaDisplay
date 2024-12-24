import logging
try:
    from rpi_backlight import Backlight
    from rpi_backlight.utils import FakeBacklightSysfs
    from glob import iglob
except:
    pass

logger = logging.getLogger(__name__)

class BacklightController:

    def __init__(self) -> None:
        try:
            self.backlight = Backlight(next(iglob("/sys/class/backlight/*-0045/")))
        except:
            try:
                logger.info("backlight couldn't find /sys/class/backlight, using dummy path")
                print("test")
                self.fakeBacklightSysfs = FakeBacklightSysfs()
                self.fakeBacklightSysfs.__enter__()
                self.backlight = Backlight(backlight_sysfs_path=self.fakeBacklightSysfs.path)
            except:
                self.backlight = None

    def get_brightness(self):
        return self.backlight.brightness
    
    def set_brightness(self, brightness:int):
        if brightness is not None and brightness >= 0 and brightness <= 100:
            self.backlight.brightness = brightness

if __name__ == "__main__":
    bl = BacklightController()
    print(next(iglob("/sys/class/backlight/*-0045/")))
    print(bl.backlight)
    print(bl.get_brightness())

