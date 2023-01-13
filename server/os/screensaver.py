import pyautogui
import time

from server.os.platform import Platform

try:
    from Xlib import X
    from Xlib import display

    class Screensaver:
        @staticmethod
        def enable_screensaver() -> None:
            if Platform.is_raspberry_pi():
                disp = display.Display()
                screensaver = disp.get_screen_saver()
                if screensaver.timeout != 60:
                    disp.set_screen_saver(60, 60, X.DefaultBlanking, X.AllowExposures)
                    disp.sync()
        
        @staticmethod
        def disable_screensaver() -> None:
            if Platform.is_raspberry_pi():
                disp = display.Display()
                if disp.get_screen_saver().timeout != 0:
                    disp.set_screen_saver(0, 0, X.DontPreferBlanking, X.AllowExposures)
                    disp.sync()
                    Screensaver.__enable_screen(disp.screen()["width_in_pixels"])

        @staticmethod
        def __enable_screen(width:int) -> None:
            step = 1
            if pyautogui.position().x <= width:
                step *= -1
            pyautogui.moveRel(step, 0)
            time.sleep(0.5)
            pyautogui.moveRel(-step, 0)
except:
    class Screensaver:
        @staticmethod
        def enable_screensaver() -> None:
            pass

        @staticmethod
        def disable_screensaver() -> None:
            pass
