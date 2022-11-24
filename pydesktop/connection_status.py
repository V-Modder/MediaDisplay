from enum import Enum

class ConnectionStatus(Enum):
    DISCONNECTED = 1
    CONNECTING = 2
    CONNECTED = 3