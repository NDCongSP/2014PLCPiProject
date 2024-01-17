from time import sleep
import RPi.GPIO as GPIO
from pi_sht1x import SHT1x
Path_Link = '/media/SHT10.txt'
#2 chan tin hieu GPIO, nhap theo kieu GPIO chu ko phaoi CONECTOR pin
DATA_PIN = 22
SCK_PIN = 27
try:
    f = open(Path_Link, 'w')
    with SHT1x(DATA_PIN, SCK_PIN, gpio_mode=GPIO.BCM) as sensor:
        temp = sensor.read_temperature()
        humidity = sensor.read_humidity(temp)
        #sensor.calculate_dew_point(temp, humidity)
        #print(sensor)
    f.write('%f;%f'%(temp, humidity))
    f.close()
except:    
    f.write('BAD;BAD')
    f.close()


