import Adafruit_DHT as dht
#import Adafruit_DHT
Path_Link = '/media/DHT21.txt'
humidity, temperature = dht.read(dht.AM2302, 4)
f = open(Path_Link, 'w')
if ((humidity != 'NO') and (temperature != 'NO')):
    f.write('%f;%f'%(temperature, humidity))
    #print (temperature, humidity)

elif ((humidity == 'NO') and (temperature == 'NO')):
    humidity, temperature = dht.read(dht.AM2302, 4)
    if ((humidity != 'NO') and (temperature != 'NO')):
        f.write('%f;%f'%(temperature, humidity))

    elif ((humidity == 'NO') and (temperature == 'NO')):
        humidity, temperature = dht.read(dht.AM2302, 4)
        if ((humidity != 'NO') and (temperature != 'NO')):
            f.write('%f;%f'%(temperature, humidity))

        elif ((humidity == 'NO') and (temperature == 'NO')):
            humidity, temperature = dht.read(dht.AM2302, 4)
            if ((humidity != 'NO') and (temperature != 'NO')):
                f.write('%f;%f'%(temperature, humidity))
        
            elif ((humidity == 'NO') and (temperature == 'NO')):
                f.write('%s;%s'%('1000', '1000'))
                #print (temperature, humidity)
f.close()

