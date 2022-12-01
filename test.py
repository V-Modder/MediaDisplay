import time
from server.pytemp import PyTemp

tmp = PyTemp()
t1 = time.time()
tmp.read()
t2 = time.time()

print(t2 - t1)