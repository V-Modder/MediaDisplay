
def calc_square(n:int):
    i=1
    while(i ** 2 < n):
        i += 1
    return i

count = 2
square = calc_square(count)
row = -1
for i in range(0, count):
    col = i % square
    if col == 0:
        row += 1
    print("Add: ", col, row)

