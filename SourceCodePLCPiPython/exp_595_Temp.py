import os
import time
import RPi.GPIO as GPIO

GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
GPIO.setup(23,GPIO.OUT)#data
GPIO.setup(24,GPIO.OUT)#sck_dich du lieu vao
GPIO.setup(25,GPIO.OUT)#rck_xuat du lieu ra ngo ra
#ma_led_7 = [0x14,0x7E,0x8C,0x2C,0x66,0x25,0x05,0x7C,0x04,0x24,0xFF]
ma_led_7 = [0x14,0x7E,0x8C,0x2C,0x66,0x25,0x05,0x7C,0x04,0x24,0xFB,0xEF,0x85,0xCF,0xFF]#14 pham tu
buffer_dis = [0,0,0,0,0,0,0,0]
def export_595(data_dis,DauCham):        
        a = 0
        #print data_dis
        #so sanh mang du lieu doc dc data_dis[] voi mang ma_led_7[] de lay ma hien thi
        for i in data_dis:
                buffer_dis[a] = ma_led_7[i]
                a = a + 1
        if DauCham == 0:
                buffer_dis[6] = buffer_dis[6] & ma_led_7[10]
        if DauCham == 1:
                buffer_dis[5] = buffer_dis[5] & ma_led_7[10]
        #print buffer_dis
        #gui ma hien thi ra 595
        for i in buffer_dis:
                send_byte(i)
        GPIO.output(24,False)
        GPIO.output(24,True)

#dich du lieu vao 595
def send_byte(bits) :
    if bits&0x80==0x80:
        GPIO.output(23, True)
    else:
        GPIO.output(23,False)
    GPIO.output(25,False)
    GPIO.output(25,True)
    if bits&0x40==0x40:
        GPIO.output(23, True)
    else:
        GPIO.output(23,False)
    GPIO.output(25,False)
    GPIO.output(25,True)
    if bits&0x20==0x20:
        GPIO.output(23, True)
    else:
        GPIO.output(23,False)
    GPIO.output(25,False)
    GPIO.output(25,True)
    if bits&0x10==0x10:
        GPIO.output(23, True)
    else:
        GPIO.output(23,False)
    GPIO.output(25,False)
    GPIO.output(25,True)
    if bits&0x08==0x08:
        GPIO.output(23, True)
    else:
        GPIO.output(23,False)
    GPIO.output(25,False)
    GPIO.output(25,True)
    if bits&0x04==0x04:
        GPIO.output(23, True)
    else:
        GPIO.output(23,False)
    GPIO.output(25,False)
    GPIO.output(25,True)
    if bits&0x02==0x02:
        GPIO.output(23, True)
    else:
        GPIO.output(23,False)
    GPIO.output(25,False)
    GPIO.output(25,True)
    if bits&0x01==0x01:
        GPIO.output(23, True)
    else:
        GPIO.output(23,False)
    GPIO.output(25,False)
    GPIO.output(25,True)
    
            
    
