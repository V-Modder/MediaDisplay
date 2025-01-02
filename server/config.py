from os.path import exists

import jsonpickle

FILENAME = "config.json"
FILENAME_2 = "config2.json"

class Buttons
    usb_1_name: str
    usb_2_name: str

    def __init__(self) -> None:
        self.usb_1_name = "1"
        self.usb_2_name = "2"

    def __str__(self) -> str:
        return f'Buttons(usb_1_name = {self.usb_1_name}, usb_2_name = {self.usb_2_name})'

class Config:
    buttons: Buttons
    __CREATE_KEY = object()
    __CONFIG = None

    def __init__(self, create_key) -> None:
        assert(create_key == Config.__CREATE_KEY), "Config objects must be created using Config.get()"
        self.buttons = Buttons()

    def save(self) -> None:
        with open(FILENAME, "w") as file:
            file.write(jsonpickle.encode(self))

    def __str__(self) -> str:
        return f'Config(buttons = {self.buttons})'

    @staticmethod
    def get():
        if Config.__CONFIG is None:
            Config.__CONFIG = Config.load_or_create()

        return Config.__CONFIG

    @staticmethod
    def load_or_create(): 
        conf = Config.load()
        if conf is None:
            conf = Config(Config.__CREATE_KEY)
            conf.save()

        return conf

    @staticmethod
    def load():
        filename = Config.get_filename()
        if filename:
            with open(filename) as file:
                data = file.read()
                return jsonpickle.decode(data, classes=Config)
        else:
            return None
    
    @staticmethod
    def get_filename() -> str|None:
        if exists(FILENAME_2):
            return FILENAME_2
        elif exists(FILENAME):
            return FILENAME
        else:
            return None
