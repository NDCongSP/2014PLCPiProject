import time
import os
import PLCPi
MyPLC = PLCPi.PLCPi('192.168.1.11',502)

while 1:
    
    print('V0 = %d'%MyPLC.Input('V0'))
    time.sleep(1)
    print('V1 = %d'%MyPLC.Input('V1'))
    time.sleep(1)
    
    print('V2 = %d'%MyPLC.Input('V2'))
    time.sleep(1)
    
    print('V3 = %d'%MyPLC.Input('V3'))
    time.sleep(1)
    print('V4 = %d'%MyPLC.Input('V4'))
    time.sleep(1)
    '''
    
    print('V0.0 = %d'%MyPLC.Input('V0.0'))#doc ngo vao bit)
    time.sleep(1)
    print('V1.0 = %d'%MyPLC.Input('V1.0'))#doc ngo vao bit)
    time.sleep(1)
    
    print('V2.0 = %d'%MyPLC.Input('V2.0'))#doc ngo vao bit)
    time.sleep(1)
    
    print('V3.0 = %d'%MyPLC.Input('V3.0'))#doc ngo vao bit)
    time.sleep(1)
    print('V4.0 = %d'%MyPLC.Input('V4.0'))#doc ngo vao bit)
    time.sleep(1)
    
    print('V3.5 = %d'%MyPLC.Input('V2.5'))#doc ngo vao bit)
    time.sleep(1)
    print('V3.6 = %d'%MyPLC.Input('V2.6'))#doc ngo vao bit)
    time.sleep(1)
    print('V3.7 = %d'%MyPLC.Input('V2.7'))#doc ngo vao bit)
    time.sleep(1)
    '''
