try:
    from rpi_backlight import Backlight
    from rpi_backlight.utils import FakeBacklightSysfs
except:
    pass

class BacklightController:

    def __init__(self) -> None:
        try:
            self.backlight = Backlight()
        except:
            try:
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
