using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace PLCPiProject
{
    public class ModbusRTUMaster
    {
        private SerialPort sp = new SerialPort();
        public string modbusStatus;
        private  Int32 _ResponseTimeout=300;

        /// <summary>
        /// Cài đặt thời gian TimeOut 
        /// </summary>
        public Int32 ResponseTimeout
        {
            get { return _ResponseTimeout; }
            set 
            {
                _ResponseTimeout = value;               
            }
        }

        #region Constructor / Deconstructor
        public ModbusRTUMaster()
        {
        }
        ~ModbusRTUMaster()
        {
            sp.Dispose();              
        }
        #endregion

        #region Open / Close Procedures
        /// <summary>
        /// Tạo kết nối đếm modbus RTU Slave.
        /// </summary>
        /// <param name="portName">Port Serial của modbus.</param>
        /// <param name="baudRate">Tốc độ Baud.</param>
        /// <param name="databits">Số bít dữ liệu.</param>
        /// <param name="parity">Chẵn lẻ.</param>
        /// <param name="stopBits">stopBits.</param>
        /// <returns>Trả về trạng thái kiểu bool, nếu true là thành công, false thất bại.</returns>
        public bool KetNoi(string portName, int baudRate, int databits, Parity parity, StopBits stopBits)
        {
            try
            {
                sp.PortName = portName;
                sp.BaudRate = baudRate;
                sp.DataBits = databits;
                sp.Parity = parity;
                sp.StopBits = stopBits;
                //Ensure port isn't already opened:
                if (!sp.IsOpen)
                {
                    //These timeouts are default and cannot be editted through the class at this point:
                    sp.ReadTimeout = ResponseTimeout;
                    sp.WriteTimeout = ResponseTimeout;

                    try
                    {
                        sp.Open();
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error opening " + sp.PortName + ": " + err.Message;
                        return false;
                    }
                    modbusStatus = sp.PortName + " opened successfully";
                    return true;
                }
                else
                {
                    modbusStatus = sp.PortName + " already opened";
                    return false;
                }
            }
            catch { return false; }
        }
        /// <summary>
        /// Ngắt kết nối đến Slave.
        /// </summary>
        /// <returns>Trả về trạng thái kiểu bool, nếu true là thành công, false thất bại.</returns>
        public bool NgatKetNoi()
        {
            try
            {
                //Ensure port is opened before attempting to close:
                if (sp.IsOpen)
                {
                    try
                    {
                        sp.Close();
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error closing " + sp.PortName + ": " + err.Message;
                        return false;
                    }
                    modbusStatus = sp.PortName + " closed successfully";
                    return true;
                }
                else
                {
                    modbusStatus = sp.PortName + " is not open";
                    return false;
                }
            }
            catch { return false; }
        }
        #endregion

        #region CRC Computation
        private void GetCRC(byte[] message, ref byte[] CRC)
        {

            //SEE CRC.XLS DOCUMENTATION//

            //Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
            //return the CRC values:

            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                //XOR CRCfull with 16bits message to compute it's CRC
                CRCFull = (ushort)(CRCFull ^ message[i]);

                for (int j = 0; j < 8; j++)
                {
                    //get LSB
                    CRCLSB = (char)(CRCFull & 0x0001);

                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
        }
        #endregion

        #region Build Message
        private void BuildMessage(byte address, byte type, ushort start, ushort registers, ref byte[] message)
        {
            //Array to receive CRC bytes:
            byte[] CRC = new byte[2];

            message[0] = address;
            message[1] = type;
            message[2] = (byte)(start >> 8);
            message[3] = (byte)start;
            message[4] = (byte)(registers >> 8);
            message[5] = (byte)registers;

            GetCRC(message, ref CRC);
            message[message.Length - 2] = CRC[0];
            message[message.Length - 1] = CRC[1];

        }

        private void BuildMessageSingleRegister(byte address, byte type, ushort start, ref byte[] message)
        {
            //Array to receive CRC bytes:
            byte[] CRC = new byte[2];

            message[0] = address;
            message[1] = type;
            message[2] = (byte)(start >> 8);
            message[3] = (byte)start;

            GetCRC(message, ref CRC);
            message[message.Length - 2] = CRC[0];
            message[message.Length - 1] = CRC[1];

        }
        #endregion

        #region Check Response
        private bool CheckResponse(byte[] response)
        {
            //Perform a basic CRC check:
            byte[] CRC = new byte[2];
            GetCRC(response, ref CRC);
            if (CRC[0] == response[response.Length - 2] && CRC[1] == response[response.Length - 1])
                return true;
            else
                return false;
        }
        #endregion

        #region Get Response
        private void GetResponse(ref byte[] response)
        {
            //There is a bug in .Net 2.0 DataReceived Event that prevents people from using this
            //event as an interrupt to handle data (it doesn't fire all of the time).  Therefore
            //we have to use the ReadByte command for a fixed length as it's been shown to be reliable.
            for (int i = 0; i < response.Length; i++)
            {
                response[i] = (byte)(sp.ReadByte());
            }
        }
        #endregion

        #region Function 16 - Write Multiple Registers
        /// <summary>
        /// Ghi giá trị xuống 1 thanh ghi holding.
        /// </summary>
        /// <param name="address">ID của Slave.</param>
        /// <param name="start">Vị trí thanh ghi bắt đầu ghi.</param>
        /// <param name="registers">Số thanh ghi muốn ghi.</param>
        /// <param name="values">Mảng kiểu byte chứa giá trị để ghi xuống holding register.</param>
        /// <returns>Trả về trạng thái kiểu bool, nếu true là thành công, false thất bại.</returns>
        public bool WriteHoldingRegister(byte address, ushort start, byte[] values)
        {
            try
            {
                //Ensure port is open:
                if (sp.IsOpen)
                {
                    //Clear in/out buffers:
                    sp.DiscardOutBuffer();
                    sp.DiscardInBuffer();
                    //Message is 1 addr + 1 fcn + 2 start + 2 reg + 1 count + 2 * reg vals + 2 CRC
                    byte[] message = new byte[8]; //02 06 00 08 00 01 + 2byte CRC  =10byte
                    //Function 16 response is fixed at 8 bytes
                    byte[] response = new byte[8];

                    //Add bytecount to message:
                    //message[6] = (byte)(registers * 2);
                    //Put write values into message prior to sending:


                    message[4] = values[0];
                    message[5] = values[1];

                    //Build outgoing message:
                    BuildMessageSingleRegister(address, (byte)06, start, ref message);

                    //Send Modbus message to Serial Port:
                    try
                    {
                        sp.Write(message, 0, message.Length);
                        GetResponse(ref response);
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error in write event: " + err.Message;
                        return false;
                    }
                    //Evaluate message:
                    if (CheckResponse(response))
                    {
                        modbusStatus = "Good";
                        return true;
                    }
                    else
                    {
                        modbusStatus = "CRC error";
                        return false;
                    }
                }
                else
                {
                    modbusStatus = "Serial port not open";
                    return false;
                }
            }
            catch { return false; }
        }
        #endregion

        #region Function 16 - Write Multiple Registers
        /// <summary>
        /// Ghi giá trị xuống các thanh ghi holding.
        /// </summary>
        /// <param name="address">ID của Slave.</param>
        /// <param name="start">Vị trí thanh ghi bắt đầu ghi.</param>
        /// <param name="registers">Số thanh ghi muốn ghi.</param>
        /// <param name="values">Mảng kiểu byte chứa giá trị để ghi xuống holding register.</param>
        /// <returns>Trả về trạng thái kiểu bool, nếu true là thành công, false thất bại.</returns>
        public bool WriteHoldingRegisters(byte address, ushort start, ushort registers, byte[] values)
        {
            try
            {
                //Ensure port is open:
                if (sp.IsOpen)
                {
                    //Clear in/out buffers:
                    sp.DiscardOutBuffer();
                    sp.DiscardInBuffer();
                    //Message is 1 addr + 1 fcn + 2 start + 2 reg + 1 count + 2 * reg vals + 2 CRC
                    byte[] message = new byte[9 + 2 * registers];
                    //Function 16 response is fixed at 8 bytes
                    byte[] response = new byte[8];

                    //Add bytecount to message:
                    message[6] = (byte)(registers * 2);
                    //Put write values into message prior to sending:
                    for (int i = 0; i < registers; i++)
                    {
                        message[7 + 2 * i] = values[2 * i];
                        message[8 + 2 * i] = values[2 * i + 1];
                    }
                    //Build outgoing message:
                    BuildMessage(address, (byte)16, start, registers, ref message);

                    //Send Modbus message to Serial Port:
                    try
                    {
                        sp.Write(message, 0, message.Length);
                        GetResponse(ref response);
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error in write event: " + err.Message;
                        return false;
                    }
                    //Evaluate message:
                    if (CheckResponse(response))
                    {
                        modbusStatus = "Good";
                        return true;
                    }
                    else
                    {
                        modbusStatus = "CRC error";
                        return false;
                    }
                }
                else
                {
                    modbusStatus = "Serial port not open";
                    return false;
                }
            }
            catch { return false; }
        }
        #endregion

        #region Function 3 - Read Holding Registers
        /// <summary>
        /// Đọc thanh ghi holding.
        /// </summary>
        /// <param name="address">ID của Slave.</param>
        /// <param name="start">Vị trí thanh ghi bắt đầu đọc.</param>
        /// <param name="registers">Số thanh ghi muốn đọc.</param>
        /// <param name="values">Mảng kiểu byte, truyền vào để chứa các giá trị đọc về</param>
        /// <returns>Trả về trạng thái kiểu bool, nếu true là thành công, false thất bại.</returns>
        public bool ReadHoldingRegisters(byte address, ushort start, ushort registers, ref byte[] values)
        {
            try
            {
                //Ensure port is open:
                if (sp.IsOpen)
                {
                    //Clear in/out buffers:
                    sp.DiscardOutBuffer();
                    sp.DiscardInBuffer();
                    //Function 3 request is always 8 bytes:
                    byte[] message = new byte[8];
                    //Function 3 response buffer:
                    byte[] response = new byte[5 + 2 * registers];
                    //Build outgoing modbus message:
                    BuildMessage(address, (byte)3, start, registers, ref message);
                    //Send modbus message to Serial Port:
                    try
                    {
                        sp.Write(message, 0, message.Length);
                        GetResponse(ref response);
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error in read event: " + err.Message;
                        return false;
                    }
                    //Evaluate message:
                    if (CheckResponse(response))
                    {
                        //Return requested register values:
                        for (int i = 0; i < (response.Length - 5) / 2; i++)
                        {
                            values[2 * i] = response[2 * i + 3];
                            //values[i] <<= 8;
                            values[2 * i + 1] = response[2 * i + 4];
                        }
                        modbusStatus = "Good";
                        return true;
                    }
                    else
                    {
                        modbusStatus = "CRC error";
                        return false;
                    }
                }
                else
                {
                    modbusStatus = "Serial port not open";
                    return false;
                }
            }
            catch { return false; }
        }
        #endregion

        #region Function 05 - Write Single Coil
        /// <summary>
        /// Ghi giá trị xuống 1 coil của Slave.
        /// </summary>
        /// <param name="address">ID của Slave.</param>
        /// <param name="start">Vị trí coil muốn ghi.</param>
        /// <param name="value">Giá trị muốn ghi ( true hoặc false).</param>
        /// <returns>Trả về trạng thái kiểu bool, nếu true là thành công, false thất bại.</returns>
        public bool WriteSingleCoil(byte address, ushort start, bool value)
        {
            try
            {
                //Ensure port is open:
                if (sp.IsOpen)
                {
                    //Clear in/out buffers:
                    sp.DiscardOutBuffer();
                    sp.DiscardInBuffer();
                    //Message is 1 addr + 1 fcn + 2 start + 2 status to write + 2 CRC
                    byte[] message = new byte[8];
                    //Function 16 response is fixed at 8 bytes
                    byte[] response = new byte[8];

                    //Array to receive CRC bytes:
                    byte[] CRC = new byte[2];

                    message[0] = address;
                    message[1] = 5;
                    message[2] = (byte)(start >> 8);
                    message[3] = (byte)start;

                    if (value == true)
                        message[4] = (byte)(0xFF);
                    else
                        message[4] = 0;

                    message[5] = 0;

                    GetCRC(message, ref CRC);
                    message[message.Length - 2] = CRC[0];
                    message[message.Length - 1] = CRC[1];

                    //Send Modbus message to Serial Port:
                    try
                    {
                        sp.Write(message, 0, message.Length);
                        GetResponse(ref response);
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error in write event: " + err.Message;
                        return false;
                    }
                    //Evaluate message:
                    if (CheckResponse(response))
                    {
                        for (int i = 0; i < response.Length; i++)
                        {
                            if (response[i] != message[i])
                            {
                                modbusStatus = "Wrong response";
                                return false;
                            }
                        }
                        modbusStatus = "Good";
                        return true;
                    }
                    else
                    {
                        modbusStatus = "CRC error";
                        return false;
                    }
                }
                else
                {
                    modbusStatus = "Serial port not open";
                    return false;
                }
            }
            catch { return false; }
        }
        #endregion

        #region Function 15 - Write Multiple Coils
        /// <summary>
        /// Ghi giá trị xuống nhiều coil.
        /// </summary>
        /// <param name="address">ID của Slave.</param>
        /// <param name="start">Vị trí Coil bắt đầu ghi.</param>
        /// <param name="coils">số coil muốn ghi tính từ vị trí bắt đầu.</param>
        /// <param name="values">Mảng giá trị kiểu Bool, để ghi xuống coil</param>
        /// <returns>Trả về trạng thái kiểu bool, nếu true là thành công, false thất bại.</returns>
        public bool WriteMultipleCoils(byte address, ushort start, ushort coils, bool[] values)
        {
            try
            {
                //Ensure port is open:
                if (sp.IsOpen)
                {
                    //Clear in/out buffers:
                    sp.DiscardOutBuffer();
                    sp.DiscardInBuffer();

                    //Message is 1 addr + 1 fcn + 2 startcoil + 2 coilsnum  + 1 dbytes + dbytes * bytevals + 2 CRC

                    int dbytes = 0;
                    if ((coils % 8) > 0)
                        dbytes = coils / 8 + 1;
                    else
                        dbytes = coils / 8;

                    byte[] message = new byte[9 + dbytes];
                    //Function 15 response is fixed at 8 bytes
                    byte[] response = new byte[8];

                    //Array to receive CRC bytes:
                    byte[] CRC = new byte[2];

                    message[0] = address;
                    message[1] = 15;
                    message[2] = (byte)(start >> 8);
                    message[3] = (byte)start;
                    message[4] = (byte)(coils >> 8);
                    message[5] = (byte)coils;
                    message[6] = (byte)dbytes;

                    int k = 0;
                    if ((coils / 8) < dbytes)
                        k = dbytes * 8 - coils;


                    //lay byte chan truoc		

                    for (int i = 0; i < coils / 8; i++)
                    {
                        message[7 + i] = 0;
                        for (int j = 0; j < 8; j++)
                        {
                            message[7 + i] = Convert.ToByte(Convert.ToByte(values[8 * i + 7 - j]) | (message[7 + i] << 1));
                        }
                    }

                    //xu ly le
                    for (int i = coils / 8; i < dbytes; i++)
                    {

                        message[7 + i] = 0;

                        for (int j = k; j < 8; j++)
                        {
                            message[7 + i] = Convert.ToByte(Convert.ToByte(values[8 * i + 7 - j]) | (message[7 + i] << 1));
                        }
                    }

                    GetCRC(message, ref CRC);
                    message[message.Length - 2] = CRC[0];
                    message[message.Length - 1] = CRC[1];


                    //Send Modbus message to Serial Port:
                    try
                    {
                        sp.Write(message, 0, message.Length);
                        GetResponse(ref response);
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error in write event: " + err.Message;
                        return false;
                    }
                    //Evaluate message:
                    if (CheckResponse(response))
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if (response[i] != message[i])
                            {
                                modbusStatus = "Bad";
                                return false;
                            }
                        }
                        modbusStatus = "Good";
                        return true;
                    }
                    else
                    {
                        modbusStatus = "CRC error";
                        return false;
                    }
                }
                else
                {
                    modbusStatus = "Serial port not open";
                    return false;
                }
            }
            catch { return false; }
        }
        #endregion

        #region Function 1 - Read Coil Status
        /// <summary>
        /// Đọc cuộn coil.
        /// </summary>
        /// <param name="address">ID của Slave.</param>
        /// <param name="start">Vị trí bắt đầu đọc.</param>
        /// <param name="coils">Số cuộn coil muốn đọc.</param>
        /// <param name="values">Mảng kiểu bool, truyền vào để chứa các giá trị đọc về.</param>
        /// <returns>Trả về trạng thái kiểu bool, nếu true là thành công, false thất bại.</returns>
        public bool ReadCoils(byte address, ushort start, ushort coils, ref bool[] values)
        {
            try
            {
                //Ensure port is open:
                if (sp.IsOpen)
                {
                    //Clear in/out buffers:
                    sp.DiscardOutBuffer();
                    sp.DiscardInBuffer();
                    //Function 1 request is always 8 bytes:
                    byte[] message = new byte[8];

                    //Array to receive CRC bytes:
                    byte[] CRC = new byte[2];

                    message[0] = address;
                    message[1] = 1;
                    message[2] = (byte)(start >> 8);
                    message[3] = (byte)start;
                    message[4] = (byte)(coils >> 8);
                    message[5] = (byte)coils;

                    GetCRC(message, ref CRC);
                    message[message.Length - 2] = CRC[0];
                    message[message.Length - 1] = CRC[1];

                    //Function 1 response buffer:
                    //Frame: 1add + 1func + 1dbytes + dbytes*data + 2CRC

                    int dbytes = 0;
                    if ((coils % 8) > 0)
                        dbytes = coils / 8 + 1;
                    else
                        dbytes = coils / 8;
                    byte[] response = new byte[5 + dbytes];
                    //Send modbus message to Serial Port:
                    try
                    {
                        sp.Write(message, 0, message.Length);
                        GetResponse(ref response);
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error in read event: " + err.Message;
                        return false;
                    }
                    //Evaluate message:
                    if (CheckResponse(response))
                    {
                        //Return requested register values:
                        for (int i = 0; i < (response.Length - 5); i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if ((8 * i + j) < coils)
                                    values[8 * i + j] = Convert.ToBoolean((response[3 + i] >> j) & 0x01);
                            }
                        }
                        modbusStatus = "Good";
                        return true;
                    }
                    else
                    {
                        modbusStatus = "CRC error";
                        return false;
                    }
                }
                else
                {
                    modbusStatus = "Serial port not open";
                    return false;
                }
            }
            catch { return false; }
        }
        #endregion

        #region Function 2 - Read Discrete Input Contacts 1xxxx
        /// <summary>
        /// Đọc các ngõ vào rời rạc.
        /// </summary>
        /// <param name="address">ID của Slave.</param>
        /// <param name="start">Vị trí bắt đầu đọc.</param>
        /// <param name="inputs">Số ngõ vào rời rạc muốn đọc.</param>
        /// <param name="values">Mảng kiểu bool, truyền vào để chứa các giá trị đọc về.</param>
        /// <returns>Trả về trạng thái kiểu bool, nếu true là thành công, false thất bại.</returns>
        public bool ReadDiscreteInputContact(byte address, ushort start, ushort inputs, ref bool[] values)
        {
            try
            {
                //Ensure port is open:
                if (sp.IsOpen)
                {
                    //Clear in/out buffers:
                    sp.DiscardOutBuffer();
                    sp.DiscardInBuffer();
                    //Function 2 request is always 8 bytes:
                    byte[] message = new byte[8];

                    //Array to receive CRC bytes:
                    byte[] CRC = new byte[2];

                    message[0] = address;
                    message[1] = 2;
                    message[2] = (byte)(start >> 8);
                    message[3] = (byte)start;
                    message[4] = (byte)(inputs >> 8);
                    message[5] = (byte)inputs;

                    GetCRC(message, ref CRC);
                    message[message.Length - 2] = CRC[0];
                    message[message.Length - 1] = CRC[1];

                    //Function 2 response buffer:
                    //Frame: 1add + 1func + 1dbytes + dbytes*data + 2CRC

                    int dbytes = 0;
                    if ((inputs % 8) > 0)
                        dbytes = inputs / 8 + 1;
                    else
                        dbytes = inputs / 8;
                    byte[] response = new byte[5 + dbytes];
                    //Send modbus message to Serial Port:
                    try
                    {
                        sp.Write(message, 0, message.Length);
                        GetResponse(ref response);
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error in read event: " + err.Message;
                        return false;
                    }
                    //Evaluate message:
                    if (CheckResponse(response))
                    {
                        //Return requested register values:
                        for (int i = 0; i < (response.Length - 5); i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if ((8 * i + j) < inputs)
                                    values[8 * i + j] = Convert.ToBoolean((response[3 + i] >> j) & 0x01);
                            }
                        }
                        modbusStatus = "Good";
                        return true;
                    }
                    else
                    {
                        modbusStatus = "CRC error";
                        return false;
                    }
                }
                else
                {
                    modbusStatus = "Serial port not open";
                    return false;
                }
            }
            catch { return false; }
        }
        #endregion

        #region Function 4 - Read Input Register 3xxxx
        /// <summary>
        /// Đọc các thanh ghi ngõ vào.
        /// </summary>
        /// <param name="address">ID của Slave.</param>
        /// <param name="start">Vị trí bắt đầu đọc.</param>
        /// <param name="registers">Số thanh ghi ngõ vào muốn đọc.</param>
        /// <param name="values">Mảng kiểu byte, truyền vào để chứa các giá trị đọc về.</param>
        /// <returns>Trả về trạng thái kiểu bool, nếu true là thành công, false thất bại.</returns>
        public bool ReadInputRegisters(byte address, ushort start, ushort registers, ref byte[] values)
        {
            try
            {
                //Ensure port is open:
                if (sp.IsOpen)
                {
                    //Clear in/out buffers:
                    sp.DiscardOutBuffer();
                    sp.DiscardInBuffer();
                    //Function 4 request is always 8 bytes:
                    byte[] message = new byte[8];
                    //Function 3 response buffer:
                    byte[] response = new byte[5 + 2 * registers];
                    //Build outgoing modbus message:
                    BuildMessage(address, (byte)4, start, registers, ref message);
                    //Send modbus message to Serial Port:
                    try
                    {
                        sp.Write(message, 0, message.Length);
                        GetResponse(ref response);
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error in read event: " + err.Message;
                        return false;
                    }

                    //Evaluate message:
                    if (CheckResponse(response))
                    {
                        //Return requested register values:
                        for (int i = 0; i < (response.Length - 5) / 2; i++)
                        {
                            values[2 * i] = response[2 * i + 3];
                            //values[i] <<= 8;
                            values[2 * i + 1] = response[2 * i + 4];
                        }
                        modbusStatus = "Good";
                        return true;
                    }
                    else
                    {
                        modbusStatus = "CRC error";
                        return false;
                    }
                }
                else
                {
                    modbusStatus = "Serial port not open";
                    return false;
                }
            }
            catch { return false; }
        }
        #endregion
    }
}
