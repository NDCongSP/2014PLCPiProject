import time
import os
import PLCPi
MyPLC = PLCPi.PLCPi('192.168.1.11',502)
MyPLC.Output('R0',0)
MyPLC.Output('R1',0)
MyPLC.Output('R2',0)
MyPLC.Output('R3',0)
'''while 1:
    #du ieu tra ve kieu int
    print(MyPLC.Doc_RTC('F'))# doc Full_Time. dinh dang[D_W,D_M,M,Y,H,Mi,S]
    time.sleep(1)
    
    print(MyPLC.Doc_RTC('D'))# doc ngay trong tuan
    time.sleep(1)
    print(MyPLC.Doc_RTC('D_M'))# ngay trong thang
    time.sleep(1)
    print(MyPLC.Doc_RTC('Y'))# nam
    time.sleep(1)
    print(MyPLC.Doc_RTC('H'))# gio
    time.sleep(1)
    print(MyPLC.Doc_RTC('Mi'))# phut
    time.sleep(1)
    print(MyPLC.Doc_RTC('S'))# giay
    time.sleep(1)
    
'''
MyPLC.Doc_RTC('13 JUN 2015 20:22:00')# cai dat thoi gian cho RTC

