import time
import os
import RPi.GPIO as GPIO
GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
GPIO.setup(26,GPIO.OUT)
GPIO.setup(18,GPIO.OUT)
GPIO.output(18,False)
while 1:
    GPIO.output(18,False)
    GPIO.output(26,True)
    time.sleep(1)
    GPIO.output(26,False)
    GPIO.output(18,False)
    time.sleep(1)
