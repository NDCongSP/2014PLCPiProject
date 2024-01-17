#!/usr/bin/env python
#---------------------------------------------------------------------------# 
# import the various server implementations
#---------------------------------------------------------------------------# 
#from pymodbus.client.sync import ModbusTcpClient as ModbusClient
#from pymodbus.client.sync import ModbusUdpClient as ModbusClient
from pymodbus.client.sync import ModbusSerialClient as ModbusClient

#---------------------------------------------------------------------------# 
# configure the client logging
#---------------------------------------------------------------------------# 
import logging
logging.basicConfig()
log = logging.getLogger()
log.setLevel(logging.DEBUG)

#---------------------------------------------------------------------------# 
#client = ModbusClient('localhost', port=502)
#client = ModbusClient(method='ascii', port='/dev/pts/2', timeout=1)
client = ModbusClient(method='rtu', port='/dev/ttyUSB0',stopbits=1, bytesize=8, parity='N',baudrate=9600, timeout=1)
client.connect()
while 1:
    
#rq = client.write_register(1, 10)
    rr = client.read_holding_registers(0,2,unit=0x01)
#assert(rq.function_code < 0x80)     # test that we are not an error
#assert(rr.registers[0] == 10)       # test the expected value
    print(rr.registers)

#---------------------------------------------------------------------------# 
# close the client
#---------------------------------------------------------------------------# 
client.close()
