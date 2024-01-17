import time
from threading import Thread
import math
import glob
from time import gmtime, strftime
import os
import exp_595
import RPi.GPIO as GPIO
import detect_IP_Address  #detect IP Address
import RTC
#---------------------------------------------------------------------------# 
# import the various server implementations
# su dung modbus slave bat dong bo( tuong ung voi server)
#---------------------------------------------------------------------------# 
from pymodbus.server.async import StartTcpServer
from pymodbus.device import ModbusDeviceIdentification
from pymodbus.datastore import ModbusSequentialDataBlock
from pymodbus.datastore import ModbusSlaveContext, ModbusServerContext
from pymodbus.transaction import ModbusRtuFramer, ModbusAsciiFramer
#---------------------------------------------------------------------------# 
# import the twisted libraries we need
#---------------------------------------------------------------------------# 
from twisted.internet.task import LoopingCall
#---------------------------------------------------------------------------# 
# configure the service logging
#---------------------------------------------------------------------------# 
import logging
logging.basicConfig()
log = logging.getLogger()
log.setLevel(logging.DEBUG)
#-----------------------------------------------------------#
# quy dinh cac chan I/O
#-----------------------------------------------------------#
GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
GPIO.setup(13,GPIO.OUT)# ngo ra de cap nguon 12v cho led 7. de khong che ko cho nhay bay khi boot
exp_595.export_595([11,11,11,11],0)#tat tat ca cac led
GPIO.output(13, True) 
#-----------------------------------------------------------------------------------------------
#40001(0x00):luu gtri dem----40002(0x01):luu gtri t_delay----40003(0x02):luu gtri Slave_ID
# lop mosbus TCP/IP
#-----------------------------------------------------------------------------------------------
class TCP_IP_thread (Thread):
        def __init__(self):
                Thread.__init__(self)
                self.local_host = str(detect_IP_Address.get_lan_ip())
                self.Context = None
                f=open('/media/Port_Num.txt','r')
                self.Port_Num = int (f.readline())
                self.PortBuffer = self.Port_Num
                #print (self.Port_Num)
                self.slave_id = 0
                self.register = 3
                self.Only_Write = 0
                self.Doc_Ho = [0,0,0,0,0,0,0]# bien chua cac gia tri thoi gian lay tu atscada xuong
        #------------------------------------------------------------------
        #------------------------------------------------------------------
        #ghi du lieu dem dc vao bo nho hoding register
        #------------------------------------------------------------------
        def updating_writer(self):
                if self.Only_Write == 0:#ghi gia tri Port khi moi khoi CT chay lan dau tien
                        self.Only_Write = 1
                        try:
                                self.Context[self.slave_id].setValues(self.register, 14, [self.Port_Num])
                        except:
                                pass
                try:
                        self.Doc_Ho = self.Context[self.slave_id].getValues(self.register, 7,8) #lay gia tri thoi gian tu atscada gui xuong
                        print(self.Doc_Ho)
                        if self.Doc_Ho[6] != 19: # neu vung nho 40013!=19 thi cap nhat gio tu raspberry len modbus
                                Time_Ras = RTC.Full_Time()
                                log.debug("new values: " + str(Time_Ras))
                                self.Context[self.slave_id].setValues(self.register, 0x00, Time_Ras) #ghi gia tri thoi gian cua raspi vao cac vung nho modbus
                                if ((Time_Ras[4] == 23)and(Time_Ras[5] == 59)and(Time_Ras[6] == 59)):
                                        os.system('sudo hwclock -w')
                                        time.sleep(3)
                        else:# neu vung nho 40013=19 thi moi cho cap nhat gio xuong raspberry
                                #chuyen doi thang sang dung dinh dang de ghi xuong raspi
                                if self.Doc_Ho[1] == 1:
                                        self.Doc_Ho[1] = 'JAN'
                                elif self.Doc_Ho[1] == 2:
                                        self.Doc_Ho[1] = 'FEB'
                                elif self.Doc_Ho[1] == 3:
                                        self.Doc_Ho[1] = 'MAR'
                                elif self.Doc_Ho[1] == 4:
                                        self.Doc_Ho[1] = 'APR'
                                elif self.Doc_Ho[1] == 5:
                                        self.Doc_Ho[1] = 'MAY'
                                elif self.Doc_Ho[1] == 6:
                                        self.Doc_Ho[1] = 'JUN'
                                elif self.Doc_Ho[1] == 7:
                                        self.Doc_Ho[1] = 'JUL'
                                elif self.Doc_Ho[1] == 8:
                                        self.Doc_Ho[1] = 'AUG'
                                elif self.Doc_Ho[1] == 9:
                                        self.Doc_Ho[1] = 'SEP'
                                elif self.Doc_Ho[1] == 10:
                                        self.Doc_Ho[1] = 'OCT'
                                elif self.Doc_Ho[1] == 11:
                                        self.Doc_Ho[1] = 'NOV'
                                elif self.Doc_Ho[1] == 12:
                                        self.Doc_Ho[1] = 'DEC'
                                #tao bien co dinh dang 'd m y h:m:s'  de ghi xuong raspi
                                Set_Time = str(self.Doc_Ho[0])+' '+self.Doc_Ho[1]+' '+str(self.Doc_Ho[2])+' '+str(self.Doc_Ho[3])+':'+str(self.Doc_Ho[4])+':'+str(self.Doc_Ho[5])
                                print(Set_Time)
                                RTC.Set_RTC(('sudo date -s"'+ Set_Time +'"')) #goi ham ghi thoi gian raspi tu modue RTC
                                time.sleep(2)
                                try:
                                        self.Context[self.slave_id].setValues(self.register, 13, [0]) #ghi gia tri 0 vao thanh ghi cho phep dong bo thoi gian
                                except:
                                        pass
                        
                        if self.Doc_Ho[7] != self.PortBuffer :#kiem tra xem co thay doi Port hay ko
                                self.PortBuffer = self.Doc_Ho[7]
                                g =open('/media/Port_Num.txt','w')
                                g.write('%d\n'%self.Doc_Ho[7])
                except:
                        pass
        #-------------------------------------------------------------------
        # method de goi method updating_writer_TCP va chay modbus TCP server
        #-------------------------------------------------------------------
        def run(self):
                loop = LoopingCall(self.updating_writer)        #toi doi tuong loop de goi method updating_writer_TCP
                loop.start(0)                                   # cho Obj chay       
                StartTcpServer(self.Context, identity=identity, address=(self.local_host, self.Port_Num))#chay TCP server
        
#-----------------------------------------------------------------------------
#---------------------------------------------------------------------------#
#thiet lap vung nho modbus
#---------------------------------------------------------------------------#
slaves  = {0: ModbusSlaveContext(co = ModbusSequentialDataBlock(0, [0]*100),hr = ModbusSequentialDataBlock(0, [0]*100))}
context = ModbusServerContext(slaves=slaves, single=False)
#---------------------------------------------------------------------------# 
# initialize the server information
#---------------------------------------------------------------------------# 
# If you don't set this or any fields, they are defaulted to empty strings.
#---------------------------------------------------------------------------# 
identity = ModbusDeviceIdentification()
identity.VendorName  = 'Pymodbus'
identity.ProductCode = 'PM'
identity.VendorUrl   = 'http://github.com/bashwork/pymodbus/'
identity.ProductName = 'Pymodbus Server'
identity.ModelName   = 'Pymodbus Server'
identity.MajorMinorRevision = '1.0'
#---------------------------------------------------------------------------------
# tao cac doi tuong va cho no chay
#---------------------------------------------------------------------------------
modbus_TCP = TCP_IP_thread()#tao Object de lam modbus TCP/IP server
#---------------------------------------------------------------------------------
modbus_TCP.Context = context #truyen context(vung nho modbus) vao proprety Context cua doi tuong modbus_TCP
#---------------------------------------------------------------------------------
#chay obj
modbus_TCP.start()

