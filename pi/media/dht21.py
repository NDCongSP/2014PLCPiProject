import Adafruit_DHT
#import Adafruit_DHT as dht
#import Adafruit_DHT
Path_Link = '/media/DHT21.txt'
sensor = Adafruit_DHT.DHT22
pin = 4

try:
    humidity, temperature = Adafruit_DHT.read_retry(sensor, pin)
    f = open(Path_Link, 'w')

#######################################################
    if humidity is not None and temperature is not None:
        #print 'Temp={0:0.1f}*C  Humidity={1:0.1f}%'.format(temperature, humidity)
        f.write('%f;%f'%(temperature, humidity))
    else:
        #print 'Failed to get reading. Try again!'
        f.write('%s;%s'%('BAD', 'BAD'))
    f.close()
except:    
    pass
#######################################################

