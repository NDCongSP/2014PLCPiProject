import time
import os
import PLCPi
MyPLC = PLCPi.PLCPi('192.168.1.11',502)
#MyPLC.Modbus_TCP_Client(10,32,[1,1,1])#
#MyPLC.Modbus_TCP_Client(30,10,[10,9,8,7,6,5,4,3,2,1])
while 1:
    print('V2.0 = %d'%MyPLC.Input('V2.0'))#doc ngo vao bit)
    time.sleep(1)
    print('V2.1 = %d'%MyPLC.Input('V2.1'))#doc ngo vao bit)
    time.sleep(1)
    print('V2.2 = %d'%MyPLC.Input('V2.2'))#doc ngo vao bit)
    time.sleep(1)
    print('V2.3 = %d'%MyPLC.Input('V2.3'))#doc ngo vao bit)
    time.sleep(1)
    print('V2.4 = %d'%MyPLC.Input('V2.4'))#doc ngo vao bit)
    time.sleep(1)
    print('V2.5 = %d'%MyPLC.Input('V2.5'))#doc ngo vao bit)
    time.sleep(1)
    print('V2.6 = %d'%MyPLC.Input('V2.6'))#doc ngo vao bit)
    time.sleep(1)
    print('V2.7 = %d'%MyPLC.Input('V2.7'))#doc ngo vao bit)
    time.sleep(1)
    #MyPLC.Output('R1',0xff)#xuat ngo ra
    '''
    try:
        print(MyPLC.Modbus_TCP_Client(11,0,5))
    except:
        pass
    '''
    #print(MyPLC.Modbus_TCP_Client(2,0,5))
    #print(MyPLC.Modbus_TCP_Client(31,0,5))
    
    #print(MyPLC.Modbus_TCP_Client(4,0,5))

#print(MyPLC.Doc_NhietDo())

