import time
import os
import RPi.GPIO as GPIO
import exp_595
GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
GPIO.setup(18,GPIO.OUT)#GPIO dieu khien chan 13(enable) cua ic595
GPIO.setup(26,GPIO.OUT)#led status


Out_Array = [0,0,0,0,0,0]#mang tat het cac ngo ra
GPIO.output(18,True)#cho chan enable cua ic595 ve muc"1" de khong cho phep xuat ngo ra
exp_595.export_595(Out_Array)#goi ham xuat ra ngo ra

GPIO.output(18,False)#cho chan enable cua ic595 ve muc"0" de cho phep xuat ngo ra
while 1:
    GPIO.output(18,False)
    GPIO.output(26,True)
    time.sleep(1)
    GPIO.output(26,False)
    GPIO.output(18,False)
    time.sleep(1)
