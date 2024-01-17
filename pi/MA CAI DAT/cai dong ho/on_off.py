import time
import os
import RPi.GPIO as GPIO
GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
GPIO.setup(21,GPIO.OUT)
while 1:
    GPIO.output(21,True)
    time.sleep(1)
    GPIO.output(21,False)
    time.sleep(1)
