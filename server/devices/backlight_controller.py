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
            self.backlight = Backlight(next(iglob("/sys/class/backlight/*-0045/"), "/sys/class/backlight/rpi_backlight"))
        except:
            try:
                logger.info("backlight couldn't find /sys/class/backlight, using dummy path")
                self.fakeBacklightSysfs = FakeBacklightSysfs()
                self.fakeBacklightSysfs.__enter__()
                self.backlight = Backlight(backlight_sysfs_path=self.fakeBacklightSysfs.path)
            except:
                self.backlight = None

    def get_brightness(self):
        if self.backlight != None:
            self.backlight.brightness
        else:
            return 0
    
    def set_brightness(self, brightness:int):
        if brightness is not None and brightness >= 0 and brightness <= 100:
            self.backlight.brightness = brightness
