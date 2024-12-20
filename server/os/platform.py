import platform


class Platform:
    @staticmethod
    def is_raspberry_pi() -> bool:
        return platform.machine() in ['armv7l', 'aarch64']
