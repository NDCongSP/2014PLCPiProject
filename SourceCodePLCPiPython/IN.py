import time
import os
import RPi.GPIO as GPIO
GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
#==============================================================================
#DOC THEO BIT
#==============================================================================
#----------------------------------------------------------------
#Doc ngo vao Module CPU (V0)
#----------------------------------------------------------------
def V0_Bit(Chanel):
    GPIO.output(Chanel_Enable1,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
    if Chanel == 0:
        GPIO.output(Chanel_Add1,GPIO.LOW)
        In_Array[7] = GPIO.input(19)
        return(In_Array[7])
    #doc ngo vao V0.1
    if Chanel == 1:
        GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.LOW,GPIO.HIGH))
        In_Array[6] = GPIO.input(19)
        return(In_Array[6])
    #doc ngo vao V0.2
    if Chanel == 2:
        GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.LOW))
        In_Array[5] = GPIO.input(19)
        return(In_Array[5])
    #doc ngo vao V0.3
    if Chanel == 3:
        GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
        In_Array[4] = GPIO.input(19)
        return(In_Array[4])
    #doc ngo vao V0.4
    if Chanel == 4:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.LOW))
        In_Array[3] = GPIO.input(19)
        return(In_Array[3])
    #doc ngo vao V0.5
    if Chanel == 5:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
        In_Array[2] = GPIO.input(19)
        return(In_Array[2])
    #doc ngo vao V0.6
    if Chanel == 6:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
        In_Array[1] = GPIO.input(19)
        return(In_Array[1])
    #doc ngo vao V0.7
    if Chanel == 7:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.HIGH))
        In_Array[0] = GPIO.input(19)
        return(In_Array[0])
#------------------------------------------------------------------------------------
#Doc ngo vao Module DI/DO_Reay (V1)
#------------------------------------------------------------------------------------
def V1_Bit(Chanel):
    GPIO.output(Chanel_Enable1,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
    if Chanel == 0:
        GPIO.output(Chanel_Add1,GPIO.LOW)
        In_Array[7] = GPIO.input(19)
        return(In_Array[7])
    #doc ngo vao V0.1
    if Chanel == 1:
        GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.LOW,GPIO.HIGH))
        In_Array[6] = GPIO.input(19)
        return(In_Array[6])
    #doc ngo vao V0.2
    if Chanel == 2:
        GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.LOW))
        In_Array[5] = GPIO.input(19)
        return(In_Array[5])
    #doc ngo vao V0.3
    if Chanel == 3:
        GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
        In_Array[4] = GPIO.input(19)
        return(In_Array[4])
    #doc ngo vao V0.4
    if Chanel == 4:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.LOW))
        In_Array[3] = GPIO.input(19)
        return(In_Array[3])
    #doc ngo vao V0.5
    if Chanel == 5:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
        In_Array[2] = GPIO.input(19)
        return(In_Array[2])
    #doc ngo vao V0.6
    if Chanel == 6:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
        In_Array[1] = GPIO.input(19)
        return(In_Array[1])
    #doc ngo vao V0.7
    if Chanel == 7:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.HIGH))
        In_Array[0] = GPIO.input(19)
        return(In_Array[0])
#------------------------------------------------------------------------------------
#Doc ngo vao Module DI/DO_bjt (V2)
#------------------------------------------------------------------------------------
def V2_Bit(Chanel):
    GPIO.output(Chanel_Enable1,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
    if Chanel == 0:
        GPIO.output(Chanel_Add1,GPIO.LOW)
        In_Array[7] = GPIO.input(19)
        return(In_Array[7])
    #doc ngo vao V0.1
    if Chanel == 1:
        GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.LOW,GPIO.HIGH))
        In_Array[6] = GPIO.input(19)
        return(In_Array[6])
    #doc ngo vao V0.2
    if Chanel == 2:
        GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.LOW))
        In_Array[5] = GPIO.input(19)
        return(In_Array[5])
    #doc ngo vao V0.3
    if Chanel == 3:
        GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
        In_Array[4] = GPIO.input(19)
        return(In_Array[4])
    #doc ngo vao V0.4
    if Chanel == 4:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.LOW))
        In_Array[3] = GPIO.input(19)
        return(In_Array[3])
    #doc ngo vao V0.5
    if Chanel == 5:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
        In_Array[2] = GPIO.input(19)
        return(In_Array[2])
    #doc ngo vao V0.6
    if Chanel == 6:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
        In_Array[1] = GPIO.input(19)
        return(In_Array[1])
    #doc ngo vao V0.7
    if Chanel == 7:
        GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.HIGH))
        In_Array[0] = GPIO.input(19)
        return(In_Array[0])
#------------------------------------------------------------------------------------
#Doc ngo vao Module MO RONG DI/DO (V3-V4)
#------------------------------------------------------------------------------------
def V3_Bit(Chanel):
    GPIO.output(Chanel_Enable2,(GPIO.LOW,GPIO.HIGH))
    if Chanel == 0:
        GPIO.output(Chanel_Add2,GPIO.LOW)
        In_Array[7] = GPIO.input(12)
        return(In_Array[7])
    #doc ngo vao V0.1
    if Chanel == 1:
        GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.LOW,GPIO.HIGH))
        In_Array[6] = GPIO.input(12)
        return(In_Array[6])
    #doc ngo vao V0.2
    if Chanel == 2:
        GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.HIGH,GPIO.LOW))
        In_Array[5] = GPIO.input(12)
        return(In_Array[5])
    #doc ngo vao V0.3
    if Chanel == 3:
        GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
        In_Array[4] = GPIO.input(12)
        return(In_Array[4])
    #doc ngo vao V0.4
    if Chanel == 4:
        GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.LOW,GPIO.LOW))
        In_Array[3] = GPIO.input(12)
        return(In_Array[3])
    #doc ngo vao V0.5
    if Chanel == 5:
        GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
        In_Array[2] = GPIO.input(12)
        return(In_Array[2])
    #doc ngo vao V0.6
    if Chanel == 6:
        GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
        In_Array[1] = GPIO.input(12)
        return(In_Array[1])
    #doc ngo vao V0.7
    if Chanel == 7:
        GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.HIGH,GPIO.HIGH))
        In_Array[0] = GPIO.input(12)
        return(In_Array[0])
def V4_Bit(Chanel):
    GPIO.output(Chanel_Enable2,(GPIO.HIGH,GPIO.LOW))
    if Chanel == 0:
        GPIO.output(Chanel_Add2,GPIO.LOW)
        In_Array[7] = GPIO.input(12)
        return(In_Array[7])
    #doc ngo vao V0.1
    if Chanel == 1:
        GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.LOW,GPIO.HIGH))
        In_Array[6] = GPIO.input(12)
        return(In_Array[6])
    #doc ngo vao V0.2
    if Chanel == 2:
        GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.HIGH,GPIO.LOW))
        In_Array[5] = GPIO.input(12)
        return(In_Array[5])
    #doc ngo vao V0.3
    if Chanel == 3:
        GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
        In_Array[4] = GPIO.input(12)
        return(In_Array[4])
    #doc ngo vao V0.4
    if Chanel == 4:
        GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.LOW,GPIO.LOW))
        In_Array[3] = GPIO.input(12)
        return(In_Array[3])
    #doc ngo vao V0.5
    if Chanel == 5:
        GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
        In_Array[2] = GPIO.input(12)
        return(In_Array[2])
    #doc ngo vao V0.6
    if Chanel == 6:
        GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
        In_Array[1] = GPIO.input(12)
        return(In_Array[1])
    #doc ngo vao V0.7
    if Chanel == 7:
        GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.HIGH,GPIO.HIGH))
        In_Array[0] = GPIO.input(12)
        return(In_Array[0])
#==============================================================================
#DOC THEO BYTE
#==============================================================================
#----------------------------------------------------------------
#Doc ngo vao Module CPU (V0)
#----------------------------------------------------------------
def V0_Byte ():
    GPIO.output(Chanel_Enable1,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
    In_Byte = '0'
    GPIO.output(Chanel_Add1,GPIO.LOW)
    In_Array[7] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.LOW,GPIO.HIGH))
    In_Array[6] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.LOW))
    In_Array[5] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
    In_Array[4] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.LOW))
    In_Array[3] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
    In_Array[2] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
    In_Array[1] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.HIGH))
    In_Array[0] = GPIO.input(19)
    for i in In_Array:
        In_Byte = In_Byte + str(i)
    LSB = In_Byte[1:5]#lay 4 bit cao cua byte
    MSB = In_Byte[5:]#lay 4 bit thap cua byte
    if LSB == '0000' :
        In_Byte_H = 0
    if LSB == '0001' :
        In_Byte_H = 1
    if LSB == '0010' :
        In_Byte_H = 2
    if LSB == '0011' :
        In_Byte_H = 3
    if  LSB == '0100' :
        In_Byte_H = 4
    if LSB == '0101' :
        In_Byte_H = 5
    if LSB == '0110' :
        In_Byte_H = 6
    if LSB == '0111' :
        In_Byte_H = 7
    if LSB == '1000' :
        In_Byte_H = 8
    if LSB == '1001' :
        In_Byte_H = 9
    if LSB == '1010' :
        In_Byte_H = 10    
    if LSB == '1011' :
        In_Byte_H = 11
    if LSB == '1100' :
        In_Byte_H = 12
    if LSB == '1101' :
        In_Byte_H = 13
    if LSB == '1110' :
        In_Byte_H = 14
    if LSB == '1111' :
        In_Byte_H = 15
    #---------------------------
    # 4 bit thap
    #---------------------------
    if MSB == '0000' :
        In_Byte_L = 0
    if MSB == '0001' :
        In_Byte_L = 1
    if MSB == '0010' :
        In_Byte_L = 2
    if MSB == '0011' :
        In_Byte_L = 3
    if MSB == '0100' :
        In_Byte_L = 4
    if MSB == '0101' :
        In_Byte_L = 5
    if MSB == '0110' :
        In_Byte_L = 6
    if MSB == '0111' :
        In_Byte_L = 7
    if MSB == '1000' :
        In_Byte_L = 8
    if MSB == '1001' :
        In_Byte_L = 9
    if MSB == '1010' :
        In_Byte_L = 10    
    if MSB == '1011' :
        In_Byte_L = 11
    if MSB == '1100' :
        In_Byte_L = 12
    if MSB == '1101' :
        In_Byte_L = 13
    if MSB == '1110' :
        In_Byte_L = 14
    if MSB == '1111' :
        In_Byte_L = 15
    return((In_Byte_H * 16) + In_Byte_L)# doi tu so Hex-->Dec
#----------------------------------------------------------------
#Doc ngo vao Module DI/DO_REAY(V1)
#----------------------------------------------------------------
def V1_Byte ():
    GPIO.output(Chanel_Enable1,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
    In_Byte = '0'
    GPIO.output(Chanel_Add1,GPIO.LOW)
    In_Array[7] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.LOW,GPIO.HIGH))
    In_Array[6] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.LOW))
    In_Array[5] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
    In_Array[4] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.LOW))
    In_Array[3] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
    In_Array[2] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
    In_Array[1] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.HIGH))
    In_Array[0] = GPIO.input(19)
    for i in In_Array:
        In_Byte = In_Byte + str(i)
    LSB = In_Byte[1:5]#lay 4 bit cao cua byte
    MSB = In_Byte[5:]#lay 4 bit thap cua byte
    if LSB == '0000' :
        In_Byte_H = 0
    if LSB == '0001' :
        In_Byte_H = 1
    if LSB == '0010' :
        In_Byte_H = 2
    if LSB == '0011' :
        In_Byte_H = 3
    if LSB == '0100' :
        In_Byte_H = 4
    if LSB == '0101' :
        In_Byte_H = 5
    if LSB == '0110' :
        In_Byte_H = 6
    if LSB == '0111' :
        In_Byte_H = 7
    if LSB == '1000' :
        In_Byte_H = 8
    if LSB == '1001' :
        In_Byte_H = 9
    if LSB == '1010' :
        In_Byte_H = 10    
    if LSB == '1011' :
        In_Byte_H = 11
    if LSB == '1100' :
        In_Byte_H = 12
    if LSB == '1101' :
        In_Byte_H = 13
    if LSB == '1110' :
        In_Byte_H = 14
    if LSB == '1111' :
        In_Byte_H = 15
    #---------------------------
    # 4 bit thap
    #---------------------------
    if MSB == '0000' :
        In_Byte_L = 0
    if MSB == '0001' :
        In_Byte_L = 1
    if MSB == '0010' :
        In_Byte_L = 2
    if MSB == '0011' :
        In_Byte_L = 3
    if MSB == '0100' :
        In_Byte_L = 4
    if MSB == '0101' :
        In_Byte_L = 5
    if MSB == '0110' :
        In_Byte_L = 6
    if MSB == '0111' :
        In_Byte_L = 7
    if MSB == '1000' :
        In_Byte_L = 8
    if MSB == '1001' :
        In_Byte_L = 9
    if MSB == '1010' :
        In_Byte_L = 10    
    if MSB == '1011' :
        In_Byte_L = 11
    if MSB == '1100' :
        In_Byte_L = 12
    if MSB == '1101' :
        In_Byte_L = 13
    if MSB == '1110' :
        In_Byte_L = 14
    if MSB == '1111' :
        In_Byte_L = 15
    return((In_Byte_H * 16) + In_Byte_L)# doi tu so Hex-->Dec
#----------------------------------------------------------------
#Doc ngo vao Module DI/DO_BJT (V2)
#----------------------------------------------------------------
def V2_Byte ():
    GPIO.output(Chanel_Enable1,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
    In_Byte = '0'
    GPIO.output(Chanel_Add1,GPIO.LOW)
    In_Array[7] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.LOW,GPIO.HIGH))
    In_Array[6] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.LOW))
    In_Array[5] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
    In_Array[4] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.LOW))
    In_Array[3] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
    In_Array[2] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
    In_Array[1] = GPIO.input(19)
    GPIO.output(Chanel_Add1,(GPIO.HIGH,GPIO.HIGH,GPIO.HIGH))
    In_Array[0] = GPIO.input(19)
    for i in In_Array:
        In_Byte = In_Byte + str(i)
    LSB = In_Byte[1:5]#lay 4 bit cao cua byte
    MSB = In_Byte[5:]#lay 4 bit thap cua byte
    if LSB == '0000' :
        In_Byte_H = 0
    if LSB == '0001' :
        In_Byte_H = 1
    if LSB == '0010' :
        In_Byte_H = 2
    if LSB == '0011' :
        In_Byte_H = 3
    if LSB == '0100' :
        In_Byte_H = 4
    if LSB == '0101' :
        In_Byte_H = 5
    if LSB == '0110' :
        In_Byte_H = 6
    if LSB == '0111' :
        In_Byte_H = 7
    if LSB == '1000' :
        In_Byte_H = 8
    if LSB == '1001' :
        In_Byte_H = 9
    if LSB == '1010' :
        In_Byte_H = 10    
    if LSB == '1011' :
        In_Byte_H = 11
    if LSB == '1100' :
        In_Byte_H = 12
    if LSB == '1101' :
        In_Byte_H = 13
    if LSB == '1110' :
        In_Byte_H = 14
    if LSB == '1111' :
        In_Byte_H = 15
    #---------------------------
    # 4 bit thap
    #---------------------------
    if MSB == '0000' :
        In_Byte_L = 0
    if MSB == '0001' :
        In_Byte_L = 1
    if MSB == '0010' :
        In_Byte_L = 2
    if MSB == '0011' :
        In_Byte_L = 3
    if MSB == '0100' :
        In_Byte_L = 4
    if MSB == '0101' :
        In_Byte_L = 5
    if MSB == '0110' :
        In_Byte_L = 6
    if MSB == '0111' :
        In_Byte_L = 7
    if MSB == '1000' :
        In_Byte_L = 8
    if MSB == '1001' :
        In_Byte_L = 9
    if MSB == '1010' :
        In_Byte_L = 10    
    if MSB == '1011' :
        In_Byte_L = 11
    if MSB == '1100' :
        In_Byte_L = 12
    if MSB == '1101' :
        In_Byte_L = 13
    if MSB == '1110' :
        In_Byte_L = 14
    if MSB == '1111' :
        In_Byte_L = 15
    return((In_Byte_H * 16) + In_Byte_L)# doi tu so Hex-->Dec
#----------------------------------------------------------------
#Doc ngo vao Module MO RONG DI/DO (V3-V4)
#----------------------------------------------------------------
def V3_Byte ():
    GPIO.output(Chanel_Enable2,(GPIO.LOW,GPIO.HIGH))
    In_Byte = '0'
    GPIO.output(Chanel_Add2,GPIO.LOW)
    In_Array[7] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.LOW,GPIO.HIGH))
    In_Array[6] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.HIGH,GPIO.LOW))
    In_Array[5] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
    In_Array[4] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.LOW,GPIO.LOW))
    In_Array[3] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
    In_Array[2] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
    In_Array[1] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.HIGH,GPIO.HIGH))
    In_Array[0] = GPIO.input(12)
    for i in In_Array:
        In_Byte = In_Byte + str(i)
    LSB = In_Byte[1:5]#lay 4 bit cao cua byte
    MSB = In_Byte[5:]#lay 4 bit thap cua byte
    if LSB == '0000' :
        In_Byte_H = 0
    if LSB == '0001' :
        In_Byte_H = 1
    if LSB == '0010' :
        In_Byte_H = 2
    if LSB == '0011' :
        In_Byte_H = 3
    if LSB == '0100' :
        In_Byte_H = 4
    if LSB == '0101' :
        In_Byte_H = 5
    if LSB == '0110' :
        In_Byte_H = 6
    if LSB == '0111' :
        In_Byte_H = 7
    if LSB == '1000' :
        In_Byte_H = 8
    if LSB == '1001' :
        In_Byte_H = 9
    if LSB == '1010' :
        In_Byte_H = 10    
    if LSB == '1011' :
        In_Byte_H = 11
    if LSB == '1100' :
        In_Byte_H = 12
    if LSB == '1101' :
        In_Byte_H = 13
    if LSB == '1110' :
        In_Byte_H = 14
    if LSB == '1111' :
        In_Byte_H = 15
    #---------------------------
    # 4 bit thap
    #---------------------------
    if MSB == '0000' :
        In_Byte_L = 0
    if MSB == '0001' :
        In_Byte_L = 1
    if MSB == '0010' :
        In_Byte_L = 2
    if MSB == '0011' :
        In_Byte_L = 3
    if MSB == '0100' :
        In_Byte_L = 4
    if MSB == '0101' :
        In_Byte_L = 5
    if MSB == '0110' :
        In_Byte_L = 6
    if MSB == '0111' :
        In_Byte_L = 7
    if MSB == '1000' :
        In_Byte_L = 8
    if MSB == '1001' :
        In_Byte_L = 9
    if MSB == '1010' :
        In_Byte_L = 10    
    if MSB == '1011' :
        In_Byte_L = 11
    if MSB == '1100' :
        In_Byte_L = 12
    if MSB == '1101' :
        In_Byte_L = 13
    if MSB == '1110' :
        In_Byte_L = 14
    if MSB == '1111' :
        In_Byte_L = 15
    return((In_Byte_H * 16) + In_Byte_L)# doi tu so Hex-->Dec

def V4_Byte ():
    GPIO.output(Chanel_Enable2,(GPIO.HIGH,GPIO.LOW))
    In_Byte = '0'
    GPIO.output(Chanel_Add2,GPIO.LOW)
    In_Array[7] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.LOW,GPIO.HIGH))
    In_Array[6] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.HIGH,GPIO.LOW))
    In_Array[5] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.LOW,GPIO.HIGH,GPIO.HIGH))
    In_Array[4] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.LOW,GPIO.LOW))
    In_Array[3] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.LOW,GPIO.HIGH))
    In_Array[2] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.HIGH,GPIO.LOW))
    In_Array[1] = GPIO.input(12)
    GPIO.output(Chanel_Add2,(GPIO.HIGH,GPIO.HIGH,GPIO.HIGH))
    In_Array[0] = GPIO.input(12)
    for i in In_Array:
        In_Byte = In_Byte + str(i)
    LSB = In_Byte[1:5]#lay 4 bit cao cua byte
    MSB = In_Byte[5:]#lay 4 bit thap cua byte
    if LSB == '0000' :
        In_Byte_H = 0
    if LSB == '0001' :
        In_Byte_H = 1
    if LSB == '0010' :
        In_Byte_H = 2
    if LSB == '0011' :
        In_Byte_H = 3
    if LSB == '0100' :
        In_Byte_H = 4
    if LSB == '0101' :
        In_Byte_H = 5
    if LSB == '0110' :
        In_Byte_H = 6
    if LSB == '0111' :
        In_Byte_H = 7
    if LSB == '1000' :
        In_Byte_H = 8
    if LSB == '1001' :
        In_Byte_H = 9
    if LSB == '1010' :
        In_Byte_H = 10    
    if LSB == '1011' :
        In_Byte_H = 11
    if LSB == '1100' :
        In_Byte_H = 12
    if LSB == '1101' :
        In_Byte_H = 13
    if LSB == '1110' :
        In_Byte_H = 14
    if LSB == '1111' :
        In_Byte_H = 15
    #---------------------------
    # 4 bit thap
    #---------------------------
    if MSB == '0000' :
        In_Byte_L = 0
    if MSB == '0001' :
        In_Byte_L = 1
    if MSB == '0010' :
        In_Byte_L = 2
    if MSB == '0011' :
        In_Byte_L = 3
    if MSB == '0100' :
        In_Byte_L = 4
    if MSB == '0101' :
        In_Byte_L = 5
    if MSB == '0110' :
        In_Byte_L = 6
    if MSB == '0111' :
        In_Byte_L = 7
    if MSB == '1000' :
        In_Byte_L = 8
    if MSB == '1001' :
        In_Byte_L = 9
    if MSB == '1010' :
        In_Byte_L = 10    
    if MSB == '1011' :
        In_Byte_L = 11
    if MSB == '1100' :
        In_Byte_L = 12
    if MSB == '1101' :
        In_Byte_L = 13
    if MSB == '1110' :
        In_Byte_L = 14
    if MSB == '1111' :
        In_Byte_L = 15
    return((In_Byte_H * 16) + In_Byte_L)# doi tu so Hex-->Dec
