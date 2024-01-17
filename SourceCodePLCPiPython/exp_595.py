import time
import RPi.GPIO as GPIO
GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)

def export_595(data_dis):        
        for i in data_dis:
                send_byte(i)
        GPIO.output(27,False)
        GPIO.output(27,True)

#dich du lieu vao 595
def send_byte(bits) :
    if bits&0x80==0x80:
        GPIO.output(17, True)
    else:
        GPIO.output(17,False)
    GPIO.output(22,False)
    GPIO.output(22,True)
    if bits&0x40==0x40:
        GPIO.output(17, True)
    else:
        GPIO.output(17,False)
    GPIO.output(22,False)
    GPIO.output(22,True)
    if bits&0x20==0x20:
        GPIO.output(17, True)
    else:
        GPIO.output(17,False)
    GPIO.output(22,False)
    GPIO.output(22,True)
    if bits&0x10==0x10:
        GPIO.output(17, True)
    else:
        GPIO.output(17,False)
    GPIO.output(22,False)
    GPIO.output(22,True)
    if bits&0x08==0x08:
        GPIO.output(17, True)
    else:
        GPIO.output(17,False)
    GPIO.output(22,False)
    GPIO.output(22,True)
    if bits&0x04==0x04:
        GPIO.output(17, True)
    else:
        GPIO.output(17,False)
    GPIO.output(22,False)
    GPIO.output(22,True)
    if bits&0x02==0x02:
        GPIO.output(17, True)
    else:
        GPIO.output(17,False)
    GPIO.output(22,False)
    GPIO.output(22,True)
    if bits&0x01==0x01:
        GPIO.output(17, True)
    else:
        GPIO.output(17,False)
    GPIO.output(22,False)
    GPIO.output(22,True)
    
            
    
