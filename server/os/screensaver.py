import logging
import subprocess
import time
from typing import List

import pyautogui

from server.os.platform import Platform

logger = logging.getLogger(__name__)

class Screensaver:
    SCREENSAVER_TIMEOUT = 60

    @staticmethod
    def enable_screensaver() -> None:
        if Platform.is_raspberry_pi():
            Screensaver.disable_screensaver()
            try:
                subprocess.run("swayidle -w timeout {} 'wlopm --off \\*' resume 'wlopm --on \\*' &".format(Screensaver.SCREENSAVER_TIMEOUT))
            except:
                logger.error("Error running screensaver")
    
    @staticmethod
    def disable_screensaver() -> None:
        if Platform.is_raspberry_pi():
            Screensaver.__enable_screen()
            pids = Screensaver.__get_running_screensavers()
            for pid in pids:
                subprocess.run(["kill", pid])

    @staticmethod
    def __enable_screen() -> None:
        step = 1
        if pyautogui.position().x >= 800:
            step *= -1
        pyautogui.moveRel(step, 0)
        time.sleep(0.5)
        pyautogui.moveRel(-step, 0)

    @staticmethod
    def __get_running_screensavers() -> List[str]:
        output = subprocess.check_output("ps axf | grep {} | grep -v grep | awk '{print $1}'".format("swayidle"))
        result = []
        for line in output.splitlines():
            result.append(line.strip())
        
        return result
        