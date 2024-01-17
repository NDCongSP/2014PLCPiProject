from pymodbus.client.sync import ModbusTcpClient as ModbusClient
#from pymodbus.client.sync import ModbusUdpClient as ModbusClient
#from pymodbus.client.sync import ModbusSerialClient as ModbusClient
#---------------------------------------------------------------------------# 
# configure the client logging
#---------------------------------------------------------------------------# 
import logging
logging.basicConfig()
log = logging.getLogger()
log.setLevel(logging.DEBUG)
#---------------------------------------------------------------------------# 
#client = ModbusClient(IP_Add, port=Port_Num)
#client.connect()
#------------------------------
#ghi/doc cuon cam 1-9999
#function_code = 1
#------------------------------
def Write_Coil(Add,Data):
    client.write_coils(Add,Data)
def Read_Coil(Add,Count):
    rr = client.read_coils(Add,Count)
    return rr.bits

#----------------------------------
#Doc dau vao roi rac 10001-19999
#function_code = 2
#----------------------------------
def Read_Discrete_Input(Add,Data):
    rr = client.read_discrete_inputs(Add,Data)#doc dau vao roi rac
    return rr.bits
    #assert(rq.function_code < 0x80)     # test that we are not an error
    #assert(rr.bits == [False]*8)         # test the expected value
#-----------------------------------------
#doc Input register 30001 - 39999
#function_code = 4
#-----------------------------------------
def Read_Input_Register(Add,Data):
    rr = client.read_input_registers(Add,Data)
    return rr.registers

#-----------------------------------------
#ghi/doc hoding register 40001 - 49999
#function_code = 3
#-----------------------------------------
def Write_Holding(Add,Data):
    client.write_registers(Add, Data)
def Read_Holding(Add,Count):
    rr = client.read_holding_registers(Add,Count)
    return rr.registers


