import time
import os
import RPi.GPIO as GPIO
import exp_595 #module dich 595 de xuat output
GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)   
#=============================================================================
#xuat theo bit
#=============================================================================
#--------------------------------
#R0(CPU)
#--------------------------------
def R0_Bit(Chanel):
    if Chanel == 0:
        Out_Array[5] = 1
    if Chanel == 1:
        Out_Array[5] = 2
    if Chanel == 2:
        Out_Array[5] = 4
    if Chanel == 3:
        Out_Array[5] = 8
    if Chanel == 4:
        Out_Array[5] = 16
    if Chanel == 5:
        Out_Array[5] = 32
    if Chanel == 6:
        Out_Array[5] = 64
    if Chanel == 7:
        Out_Array[5] = 128
    if Chanel == 8:
        Out_Array[5] = 0
    exp_595.export_595(Out_Array)
#--------------------------------------------------
#R1(DI/DO RELAY)
#--------------------------------------------------
def R1_Bit(Chanel):
    if Chanel == 0:
        Out_Array[4] = 1
    if Chanel == 1:
        Out_Array[4] = 2
    if Chanel == 2:
        Out_Array[4] = 4
    if Chanel == 3:
        Out_Array[4] = 8
    if Chanel == 4:
        Out_Array[4] = 16
    if Chanel == 5:
        Out_Array[4] = 32
    if Chanel == 6:
        Out_Array[4] = 64
    if Chanel == 7:
        Out_Array[4] = 0x80
    if Chanel == 8:
        Out_Array[4] = 0
    exp_595.export_595(Out_Array)
#--------------------------------------------------
#R2(DI/DO_BJT)
#--------------------------------------------------
def R2_Bit(Chanel):
    if Chanel == 0:
        Out_Array[3] = 1
    if Chanel == 1:
        Out_Array[3] = 2
    if Chanel == 2:
        Out_Array[3] = 4
    if Chanel == 3:
        Out_Array[3] = 8
    if Chanel == 4:
        Out_Array[3] = 16
    if Chanel == 5:
        Out_Array[3] = 32
    if Chanel == 6:
        Out_Array[3] = 64
    if Chanel == 7:
        Out_Array[3] = 0x80
    if Chanel == 8:
        Out_Array[3] = 0
    exp_595.export_595(Out_Array)
#--------------------------------------------------
#R3(DI/DO BJT)
#--------------------------------------------------
def R3_Bit(Chanel):
    if Chanel == 0:
        Out_Array[2] = 1
    if Chanel == 1:
        Out_Array[2] = 2
    if Chanel == 2:
        Out_Array[2] = 4
    if Chanel == 3:
        Out_Array[2] = 8
    if Chanel == 4:
        Out_Array[2] = 16
    if Chanel == 5:
        Out_Array[2] = 32
    if Chanel == 6:
        Out_Array[2] = 64
    if Chanel == 7:
        Out_Array[2] = 0x80
    if Chanel == 8:
        Out_Array[2] = 0
    exp_595.export_595(Out_Array)
#--------------------------------------------------
#R4( MO RONG DI/DO)
#--------------------------------------------------
def R4_Bit(Chanel):
    if Chanel == 0:
        Out_Array[1] = 1
    if Chanel == 1:
        Out_Array[1] = 2
    if Chanel == 2:
        Out_Array[1] = 4
    if Chanel == 3:
        Out_Array[1] = 8
    if Chanel == 4:
        Out_Array[1] = 16
    if Chanel == 5:
        Out_Array[1] = 32
    if Chanel == 6:
        Out_Array[1] = 64
    if Chanel == 7:
        Out_Array[1] = 0x80
    if Chanel == 8:
        Out_Array[1] = 0
    exp_595.export_595(Out_Array)
#--------------------------------------------------
#R5( MO RONG DI/DO)
#--------------------------------------------------
def R5_Bit(Chanel):
    if Chanel == 0:
        Out_Array[0] = 1
    if Chanel == 1:
        Out_Array[0] = 2
    if Chanel == 2:
        Out_Array[0] = 4
    if Chanel == 3:
        Out_Array[0] = 8
    if Chanel == 4:
        Out_Array[0] = 16
    if Chanel == 5:
        Out_Array[0] = 32
    if Chanel == 6:
        Out_Array[0] = 64
    if Chanel == 7:
        Out_Array[0] = 0x80
    if Chanel == 8:
        Out_Array[0] = 0
    exp_595.export_595(Out_Array)
#=============================================================================
#xuat theo BYTE
#=============================================================================
#-----------------
#R0 (CPU)
#-----------------
def R0_Byte(Data):
    Out_Array[5] = Data
    exp_595.export_595(Out_Array)
#-----------------
#R1 (DI/DO_RELAY)
#-----------------
def R1_Byte(Data):
    Out_Array[4] = Data
    exp_595.export_595(Out_Array)
#-----------------
#R2 (DI/DO_BJT)
#-----------------
def R2_Byte(Data):
    Out_Array[3] = Data
    exp_595.export_595(Out_Array)
#-----------------
#R3 (DI/DO_BJT)
#-----------------
def R3_Byte(Data):
    Out_Array[2] = Data
    exp_595.export_595(Out_Array)
#-----------------
#R4 (MO RONG DI/DO)
#-----------------
def R4_Byte(Data):
    Out_Array[1] = Data
    exp_595.export_595(Out_Array)
#-----------------
#R5 (MO RONG DI/DO)
#-----------------
def R5_Byte(Data):
    Out_Array[0] = Data
    exp_595.export_595(Out_Array)
