import struct
i1 = 17255
i2 = 26771
a = i1 & 0xFF
b = (i1 >> 8) & 0xFF
c = i2 & 0xFF
d = (i2 >> 8) & 0xFF;
res = (b << 24) | (a << 16) | (d << 8) | c
rep = struct.pack('>I', res)
myfloat = struct.unpack('>f', rep)[0]
print(res)
print(myfloat)
