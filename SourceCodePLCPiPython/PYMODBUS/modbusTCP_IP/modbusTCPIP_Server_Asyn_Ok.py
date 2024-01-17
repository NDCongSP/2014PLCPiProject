#---------------------------------------------------------------------------# 
# import the various server implementations
#---------------------------------------------------------------------------# 
from pymodbus.server.async import StartTcpServer
from pymodbus.server.async import StartUdpServer
from pymodbus.server.async import StartSerialServer

from pymodbus.device import ModbusDeviceIdentification
from pymodbus.datastore import ModbusSequentialDataBlock
from pymodbus.datastore import ModbusSlaveContext, ModbusServerContext
from pymodbus.transaction import ModbusRtuFramer, ModbusAsciiFramer
import detect_IP_Address
#---------------------------------------------------------------------------# 
# configure the service logging
#---------------------------------------------------------------------------# 
import logging
logging.basicConfig()
log = logging.getLogger()
log.setLevel(logging.DEBUG)

#---------------------------------------------------------------------------# \
#khoi tao cac vung nho
store = ModbusSlaveContext(
    di = ModbusSequentialDataBlock(0, [0]*1000),
    co = ModbusSequentialDataBlock(0, [0]*1000),
    hr = ModbusSequentialDataBlock(0, [11]*1000),
    ir = ModbusSequentialDataBlock(0, [0]*1000))
context = ModbusServerContext(slaves=store, single= 1)# single=True thi se tu nhan ID, single=False thi ta phai dinh ID

#---------------------------------------------------------------------------# 
# initialize the server information
#---------------------------------------------------------------------------# 
# If you don't set this or any fields, they are defaulted to empty strings.
#---------------------------------------------------------------------------# 
identity = ModbusDeviceIdentification()
identity.VendorName  = 'Pymodbus'
identity.ProductCode = 'PM'
identity.VendorUrl   = 'http://github.com/bashwork/pymodbus/'
identity.ProductName = 'Pymodbus Server'
identity.ModelName   = 'Pymodbus Server'
identity.MajorMinorRevision = '1.0'

#---------------------------------------------------------------------------# 
# run the server you want
#---------------------------------------------------------------------------#
def updating_writer(self):
    #self.Context[self.slave_id].setValues(self.register, 0x01, [self.Port_Num]) #ghi gia tri vao thanh ghi 40002
    #log.debug("new values: " + str([self.TempThread.temp_c]))		#hien thi gia tri nhiet do ra man hinh
    Context[0].setValues(3, 0x00, [111,555])#ghi Gtri nhiet do vao hoding
    Context[0].setValues(1, 0, [1,1,1,1])#ghi trang thai in_put vao coil
    IOThread.OutRaspi = self.Context[0].getValues(1, 10,10)# lay gia tri cua coil dua ra Output Rasbperry
    print(self.Context[0].getValues(3, 0x01,1)) #lay gia tri Port
            
StartTcpServer(context, identity=identity, address=(str(detect_IP_Address.get_lan_ip()), 501))
#StartUdpServer(context, identity=identity, address=("localhost", 502))
#StartSerialServer(context, identity=identity, port='/dev/pts/3', framer=ModbusRtuFramer)
#StartSerialServer(context, identity=identity, port='/dev/pts/3', framer=ModbusAsciiFramer)
