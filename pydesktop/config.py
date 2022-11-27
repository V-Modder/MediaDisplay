import jsonpickle
from os.path import exists

FILENAME = "config.json"
DEFAULT_SERVER = "http://localhost:5001"

class Config:
    server : str

    def __init__(self, server : str = None):
        self.server = server

    def save(self):
        with open(FILENAME, "w") as file:
            file.write(jsonpickle.encode(self))

    def load_or_create():
        conf = Config.load()
        if conf is None:
            conf = Config()
            conf.server = DEFAULT_SERVER

        return conf        

    def load():
        if exists(FILENAME):
            with open(FILENAME) as file:
                data = file.read()
                return jsonpickle.decode(data)
        else:
            return None