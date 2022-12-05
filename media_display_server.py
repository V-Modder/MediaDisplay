import os
from pathlib import Path

from server.pystream import main

os.chdir(Path(__file__).parent.absolute())
main()