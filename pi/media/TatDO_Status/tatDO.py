import time
import os
import RPi.GPIO as GPIO
import exp_595
GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
GPIO.setup(26,GPIO.OUT)#led status

Out_Array = [0,0,0,0,0,0]#mang tat het cac ngo ra ngo ra
exp_595.export_595(Out_Array)#goi ham xuat ra ngo ra
while 1:
    GPIO.output(26,True)
    time.sleep(1)
    GPIO.output(26,False)
    time.sleep(1)
