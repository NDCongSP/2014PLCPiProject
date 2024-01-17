import serial
import time
ser = serial.Serial(port='/dev/ttyAMA0',baudrate=9600,
                    bytesize=8, parity='N', stopbits=1,timeout=1)

def value(Chanel):
    AI_str=['I0:','I1:','I2:','I3:','I4:','I5:','I6:',
                    'I7:','I8:','I9:','I10:','I11:','I12:']
    ser.readline()
    string_rcv=ser.readline()       
    if (Chanel<10):
        location1 = string_rcv.find(AI_str[Chanel])
        location2 = string_rcv.find(AI_str[Chanel+1])
        return (int(string_rcv[location1+3:location2]))
    elif (Chanel!=12):
        location1 = string_rcv.find(AI_str[Chanel])
        location2 = string_rcv.find(AI_str[Chanel+1])
        return (int(string_rcv[location1+4:location2]))
    else:
        location1 = string_rcv.find(AI_str[Chanel])
        return (int(string_rcv[location1+4:]))
