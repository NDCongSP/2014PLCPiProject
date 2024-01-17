import time
import os
import PLCPi
MyPLC = PLCPi.PLCPi('192.168.1.11',502)
MyPLC.Output('R0',0)
MyPLC.Output('R1',0)
MyPLC.Output('R2',0)
MyPLC.Output('R3',0)
while 1:
    print(MyPLC.Doc_NhietDo())
    time.sleep(1)

