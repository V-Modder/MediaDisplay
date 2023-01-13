import platform

class Platform:
    @staticmethod
    def is_raspberry_pi() -> bool:
        return platform.machine() == 'armv7l'