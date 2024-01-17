import serial
import time
import os
import glob
import RPi.GPIO as GPIO
import IN #module doc ngo vao
import OUT#modue xuat ngo ra
import DS18B20#module doc nhiet do cam bien ds18b20
import A_D_Module#module A/D
import RTC #module Real Time
#import modbusTCP_Client_Syn #module modbus TCP/IP Client
#from pymodbus.client.sync import ModbusTcpClient as ModbusClient
class PLCPi(object):
    def __init__(self,IP_Client,Port_Client):
        GPIO.setmode(GPIO.BCM)
        GPIO.setwarnings(False)
        #-------------------------------------------------------
        #tao cac bien qui dinh chan vao ra
        #-------------------------------------------------------
        OUT.Chanel_Out = [17,22,27]
        IN.Chanel_Add1 = [13,6,5]     #3 chan address de dieu khien doc ngo vao 74ls151(module CPU) a2-13
        IN.Chanel_Add2 = [21,20,16]     #3 chan address de dieu khien doc ngo vao 74ls151(module DI/DO) a2- 21
        IN.Chanel_Enable1 = [11,10,9] #chon ngo vao 11- v0(cpu) ; 10-v1(relay); 9-v2(BJT)
        IN.Chanel_Enable2 = [7,8]      #chon ngo vao modue mo rong DI/DO
        IN.Chanel_In = [19,12]        #2 chan out cua 74ls151. 19 cua CPU - 12 cua mo rong DI/DO
        GPIO.setup(IN.Chanel_Add1, GPIO.OUT, initial=GPIO.LOW) 
        GPIO.setup(IN.Chanel_Add2, GPIO.OUT, initial=GPIO.LOW) 
        GPIO.setup(IN.Chanel_Enable1, GPIO.OUT, initial=GPIO.HIGH)
        GPIO.setup(IN.Chanel_Enable2,GPIO.OUT, initial=GPIO.HIGH)
        GPIO.setup(OUT.Chanel_Out, GPIO.OUT, initial=GPIO.LOW)
        GPIO.setup(IN.Chanel_In,GPIO.IN, pull_up_down=GPIO.PUD_DOWN)  # chan Out cua 74LS151(pin 6)
        IN.In_Array = [0,0,0,0,0,0,0,0]#mang chua gia tri doc duoc tu ngo vao. In_Array_0[0] la bit LSB(bit cao)
        OUT.Out_Array = [0,0,0,0,0,0] #tao mang chua gia tri xuat ra output cho modue OUT.Out_Array[5] = R0
        #---------------------------------------------------------
        #mang de luu gia tri nhiet do hien thi led7
        DS18B20.Display_Array = [0,0,0,0,0,0,0,0]#self.Display_Array[0] se dc day di truoc
        RTC.Time_Array = [0,0,0,0,0,0,0] #mang chua gia tri khi doc Full_time. dinh dang[D_W,D_M,M,Y,H,Mi,S]
        #khai bao cho modbusTCP_Client
        #modbusTCP_Client_Syn.client = ModbusClient(IP_Client, port=Port_Client)
        #modbusTCP_Client_Syn.client.connect()
#=================================================================================================================
#Doc ngo vao 
#=================================================================================================================
    def Input(self,Chanel):
        #--------------------------------------
        #DOC THEO BIT
        #--------------------------------------
        if (len(Chanel) == 4):
            if (Chanel.strip()[0:2] == 'V0'): #lay 2 ky tu dau cha Chanel so sanh voi'V0'
                return(IN.V0_Bit(int(Chanel.strip()[-1])))#int(Chanel.strip()[-1]):lay ky tu cuoi cung cua Chanel roichuyen sang int. Roi truyen vao ham V0_Bit
            #cua Module IN. tra ve trang thai cua ngo vao tuong ung
            if (Chanel.strip()[0:2] == 'V1'):
                return(IN.V1_Bit(int(Chanel.strip()[-1])))
            if (Chanel.strip()[0:2] == 'V2'):
                return(IN.V2_Bit(int(Chanel.strip()[-1])))
            if (Chanel.strip()[0:2] == 'V3'):
                return(IN.V3_Bit(int(Chanel.strip()[-1])))
            if (Chanel.strip()[0:2] == 'V4'):
                return(IN.V4_Bit(int(Chanel.strip()[-1])))

        #--------------------------------------
        #DOC THEO BYTE
        #--------------------------------------
        if(len(Chanel) == 2):
            if (Chanel == 'V0'):
                return IN.V0_Byte()
            if (Chanel == 'V1'):
                return IN.V1_Byte()
            if (Chanel == 'V2'):
                return IN.V2_Byte()
            if (Chanel == 'V3'):
                return IN.V3_Byte()
            if (Chanel == 'V4'):
                return IN.V4_Byte()
        GPIO.cleanup(19)

#=================================================================================================================
#Xuat ngo ra
#=================================================================================================================
    def Output(self,Chanel,Data):
        if (len(Chanel) == 4):
            if (Chanel.strip()[0:2] == 'R0'):
                OUT.R0_Bit(int(Chanel.strip()[-1]))
            if (Chanel.strip()[0:2] == 'R1'):
                OUT.R1_Bit(int(Chanel.strip()[-1]))
            if (Chanel.strip()[0:2] == 'R2'):
                OUT.R2_Bit(int(Chanel.strip()[-1]))
            if (Chanel.strip()[0:2] == 'R3'):
                OUT.R3_Bit(int(Chanel.strip()[-1]))
            if (Chanel.strip()[0:2] == 'R4'):
                OUT.R4_Bit(int(Chanel.strip()[-1]))
            if (Chanel.strip()[0:2] == 'R5'):
                OUT.R5_Bit(int(Chanel.strip()[-1]))
        if (len(Chanel) == 2):
            if (Chanel == 'R0'):
                OUT.R0_Byte(Data)
            if (Chanel == 'R1'):
                OUT.R1_Byte(Data)
            if (Chanel == 'R2'):
                OUT.R2_Byte(Data)
            if (Chanel == 'R3'):
                OUT.R3_Byte(Data)
            if (Chanel == 'R4'):
                OUT.R4_Byte(Data)
            if (Chanel == 'R5'):
                OUT.R5_Byte(Data)
            
#==========================================================================================
#doc nhiet do DS18B20
#==========================================================================================
    def Doc_NhietDo(self):
        return (DS18B20.Doc_NhietDo())
#==========================================================================================
#Doc thoi gian thuc tu he thong
#==========================================================================================
    def Doc_RTC(self,Data):
        if Data == 'F': # doc toan bo thoi gian tu he thong
            return(RTC.Full_Time())
        elif Data == 'D': #doc ngay trong tuan
            return(RTC.Day_Of_Week())
        elif Data == 'D_M': #doc ngay trong thang
            return(RTC.Day_Of_Month())
        elif Data == 'M': #doc thang
            return(RTC.Month())
        elif Data == 'Y': #doc nam
            return(RTC.Year())
        elif Data == 'H': #doc gio
            return(RTC.Hour())
        elif Data == 'Mi': #doc phut
            return(RTC.Minute())
        elif Data == 'S': #doc giay
            return(RTC.Second())
        else : # cai dat thoi gian cho he thong
            RTC.Set_RTC(('sudo date -s"'+ Data +'"'))
#==========================================================================================
#doc Analog
#==========================================================================================
    def DocAnalog(self,Chanel):
        #-----------------------------------
        #ngo vao ap 0-10VDC
        #-----------------------------------
        if Chanel == 'V0':
            return(A_D_Module.value(0))
        if Chanel == 'V1':
            return(A_D_Module.value(1))
        if Chanel == 'V2':
            return(A_D_Module.value(2))
        if Chanel == 'V3':
            return(A_D_Module.value(3))
        if Chanel == 'V4':
            return(A_D_Module.value(4))
        #-----------------------------------
        #ngo vao dong 4-20mA
        #-----------------------------------
        if Chanel == 'I0':
            return(A_D_Module.value(5))
        if Chanel == 'I1':
            return(A_D_Module.value(6))
        if Chanel == 'I2':
            return(A_D_Module.value(7))
        if Chanel == 'I3':
            return(A_D_Module.value(8))
        if Chanel == 'I4':
            return(A_D_Module.value(9))
        if Chanel == 'I5':
            return(A_D_Module.value(10))
        if Chanel == 'I6':
            return(A_D_Module.value(11))
        if Chanel == 'I7':
            return(A_D_Module.value(12))
#===========================================================================================
#MODBUS
#===========================================================================================
    #------------------------------------
    #Modbus TCP/IP Client Synchronous
    #------------------------------------
    def Modbus_TCP_Client(self,Chuc_Nang,Add,Data):
        #coil(1-9999)
        if Chuc_Nang == 10:
            modbusTCP_Client_Syn.Write_Coil(Add,Data)
        if Chuc_Nang == 11:
            return(modbusTCP_Client_Syn.Read_Coil(Add,Data))
        #dau vao roi rac (10001-19999)
        if Chuc_Nang == 2:
            return(modbusTCP_Client_Syn.Read_Discrete_Input(Add,Data))
        #hoding register (40001-49999)    
        if Chuc_Nang == 30:
            modbusTCP_Client_Syn.Write_Holding(Add,Data)
        if Chuc_Nang == 31:
            return(modbusTCP_Client_Syn.Read_Holding(Add,Data))
        #Input Register (30001-39999)
        if Chuc_Nang == 4:
            return(modbusTCP_Client_Syn.Read_Input_Register(Add,Data))
