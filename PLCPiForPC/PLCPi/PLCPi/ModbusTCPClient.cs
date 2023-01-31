using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace PLCPiProject
{
    public class ModbusTCPClient
    {
        // ------------------------------------------------------------------------
        // Constants for access
        private const byte fctReadCoil = 1;
        private const byte fctReadDiscreteInputs = 2;
        private const byte fctReadHoldingRegister = 3;
        private const byte fctReadInputRegister = 4;
        private const byte fctWriteSingleCoil = 5;
        private const byte fctWriteSingleRegister = 6;
        private const byte fctWriteMultipleCoils = 15;
        private const byte fctWriteMultipleRegister = 16;
        private const byte fctReadWriteMultipleRegister = 23;

        /// <summary>Constant for exception illegal function.</summary>
        public const byte excIllegalFunction = 1;
        /// <summary>Constant for exception illegal data address.</summary>
        public const byte excIllegalDataAdr = 2;
        /// <summary>Constant for exception illegal data value.</summary>
        public const byte excIllegalDataVal = 3;
        /// <summary>Constant for exception Server device failure.</summary>
        public const byte excServerDeviceFailure = 4;
        /// <summary>Constant for exception acknowledge.</summary>
        public const byte excAck = 5;
        /// <summary>Constant for exception Server is busy/booting up.</summary>
        public const byte excServerIsBusy = 6;
        /// <summary>Constant for exception gate path unavailable.</summary>
        public const byte excGatePathUnavailable = 10;
        /// <summary>Constant for exception not connected.</summary>
        public const byte excExceptionNotConnected = 253;
        /// <summary>Constant for exception connection lost.</summary>
        public const byte excExceptionConnectionLost = 254;
        /// <summary>Constant for exception response timeout.</summary>
        public const byte excExceptionTimeout = 255;
        /// <summary>Constant for exception wrong offset.</summary>
        private const byte excExceptionOffset = 128;
        /// <summary>Constant for exception send failt.</summary>
        private const byte excSendFailt = 100;

        // ------------------------------------------------------------------------
        // Private declarations
        private static ushort _timeout = 10000;
        private static ushort _refresh = 10;
        private static bool _connected = false;

        public string DeviceID;

        public int BadCount=0;

        //private Socket tcpAsyCl;
        //private byte[] tcpAsyClBuffer = new byte[2048];

        private Socket tcpSynCl;
        private byte[] tcpSynClBuffer = new byte[2048];

        // ------------------------------------------------------------------------
        /// <summary>Response data event. This event is called when new data arrives</summary>
        public delegate void ResponseData(ushort id, byte unit, byte function, byte[] data);
        /// <summary>Response data event. This event is called when new data arrives</summary>
        public event ResponseData OnResponseData;
        /// <summary>Exception data event. This event is called when the data is incorrect</summary>
        public delegate void ExceptionData(ushort id, byte unit, byte function, byte exception);
        /// <summary>Exception data event. This event is called when the data is incorrect</summary>
        public event ExceptionData OnException;

        // ------------------------------------------------------------------------
        /// <summary>Response timeout. If the Server didn't answers within in this time an exception is called.</summary>
        /// <value>The default value is 500ms.</value>
        public ushort timeout
        {
            get { return _timeout; }
            set { _timeout = 10000; }
        }

        // ------------------------------------------------------------------------
        /// <summary>Refresh timer for Server answer. The class is polling for answer every X ms.</summary>
        /// <value>The default value is 10ms.</value>
        public ushort refresh
        {
            get { return _refresh; }
            set { _refresh = value; }
        }

        // ------------------------------------------------------------------------
        /// <summary>Shows if a connection is active.</summary>
        public bool connected
        {
            get { return _connected; }
        }

        // ------------------------------------------------------------------------
        /// <summary>Create master instance without parameters.</summary>
        //public ModbusTCPClient()
        //{
        //}

        // ------------------------------------------------------------------------
        /// <summary>
        /// Bắt đầu kết nối tới modbus Server.
        /// </summary>
        /// <param name="ip">IP adress của modbus Server.</param>
        /// <param name="port">Port number của modbus Server. Mặc định sử dụng port 502.</param>
        public void KetNoi(string ip, ushort port)
        {
            try
            {
                IPAddress _ip;
                if (IPAddress.TryParse(ip, out _ip) == false)
                {
                    IPHostEntry hst = Dns.GetHostEntry(ip);
                    ip = hst.AddressList[0].ToString();
                }                
                // Connect synchronous client
                tcpSynCl = new Socket(IPAddress.Parse(ip).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                tcpSynCl.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                tcpSynCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _timeout);
                tcpSynCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
                tcpSynCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
                _connected = true;
            }
            catch
            {
                _connected = false;
                //throw (error); 
            }
        }

        // ------------------------------------------------------------------------
        /// <summary>Ngắt kết nối đến Server.</summary>
        public void NgatKetNoi()
        {
            try
            {
                Dispose();
            }
            catch {  }
        }

        // ------------------------------------------------------------------------
        /// <summary>Destroy master instance.</summary>
        ~ModbusTCPClient()
        {
            Dispose();
        }

        // ------------------------------------------------------------------------
        /// <summary>Destroy master instance</summary>
        public void Dispose()
        {            
            if (tcpSynCl != null)
            {
                if (tcpSynCl.Connected)
                {
                    try { tcpSynCl.Shutdown(SocketShutdown.Both); }
                    catch { }
                    tcpSynCl.Close();
                }
                tcpSynCl = null;
            }
        }

        internal void CallException(ushort id, byte unit, byte function, byte exception)
        {         
            if(OnException != null) OnException(id, unit, function, exception);
        }

        internal static UInt16 SwapUInt16(UInt16 inValue)
        {
            return (UInt16)(((inValue & 0xff00) >> 8) |
                     ((inValue & 0x00ff) << 8));
        }

        
        // ------------------------------------------------------------------------
        //Đọc
        /// <summary>
        /// Đọc coil(1-9999).
        /// </summary>
        /// <param name="id">ID của Server, mặc đinh là 0.</param>
        /// <param name="startAddress">Địa chỉ bắt đầu đọc data.</param>
        /// <param name="numInputs">Số data muốn đọc tính từ địa chỉ bắt đầu đọc.</param>
        /// <param name="values">Mảng kiểu byte, truyền vào để chứa kết quả đọc về, nếu mảng = null nghĩa là đọc không thành công.</param>
        public void ReadCoils(ushort id, ushort startAddress, ushort numInputs, ref byte[] values)
        {
            values = WriteSyncData(CreateReadHeader(id, 0, startAddress, numInputs, fctReadCoil), id);
        }

        /// <summary>
        /// Đọc vùng nhớ discrete inputs từ Server.
        /// </summary>
        /// <param name="id">ID của Server, mặc đinh là 0.</param>
        /// <param name="startAddress">Địa chỉ bắt đầu đọc data.</param>
        /// <param name="numInputs">Số data muốn đọc tính từ địa chỉ bắt đầu đọc.</param>
        /// <param name="values">Mảng kiểu byte, truyền vào để chứa kết quả đọc về, nếu mảng = null nghĩa là đọc không thành công.</param>
        public void ReadDiscreteInputs(ushort id, ushort startAddress, ushort numInputs, ref byte[] values)
        {
            values = WriteSyncData(CreateReadHeader(id, 0, startAddress, numInputs, fctReadDiscreteInputs), id);
        }

        /// <summary>
        /// Đọc vùng nhớ holding register từ Server.
        /// </summary>
        /// <param name="id">ID của Server, mặc đinh là 0.</param>
        /// <param name="startAddress">Địa chỉ bắt đầu đọc data.</param>
        /// <param name="numInputs">Số data muốn đọc tính từ địa chỉ bắt đầu đọc.</param>
        /// <param name="values">Mảng kiểu byte, truyền vào để chứa kết quả đọc về, nếu mảng = null nghĩa là đọc không thành công.</param>

        public void ReadHoldingRegister(ushort id, ushort startAddress, ushort numInputs, ref byte[] values)
        {
            values = WriteSyncData(CreateReadHeader(id, 0, startAddress, numInputs, fctReadHoldingRegister), id);
        }

        /// <summary>
        /// Đọc input registers từ Server.
        /// </summary>
        /// <param name="id">ID của Server, mặc đinh là 0.</param>
        /// <param name="startAddress">Địa chỉ bắt đầu đọc data.</param>
        /// <param name="numInputs">Số data muốn đọc tính từ địa chỉ bắt đầu đọc.</param>
        /// <param name="values">Mảng kiểu byte, truyền vào để chứa kết quả đọc về, nếu mảng = null nghĩa là đọc không thành công.</param>
        public void ReadInputRegister(ushort id, ushort startAddress, ushort numInputs, ref byte[] values)
        {
            values = WriteSyncData(CreateReadHeader(id, 0, startAddress, numInputs, fctReadInputRegister), id);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Ghi
        /// <summary>
        /// Ghi giá trị xuống 1 coil của Server.
        /// </summary>
        /// <param name="id">ID của Server, mặc đinh là 0</param>
        /// <param name="startAddress">Địa chỉ của ciol muốn ghi</param>
        /// <param name="OnOff">giá trị ghi xuống coil( true hoặc false)</param>
        /// <param name="result">Kết quả, nếu mảng = null nghĩa là ghi không thành công.</param>
        public void WriteSingleCoil(ushort id, ushort startAddress, bool OnOff, ref byte[] result)
        {
            byte[] data;
            data = CreateWriteHeader(id, 0, startAddress, 1, 1, fctWriteSingleCoil);
            if (OnOff == true) data[10] = 255;
            else data[10] = 0;
            result = WriteSyncData(data, id);
        }

        /// <summary>Ghi nhiều Coil 1 lúc.</summary>
        /// <param name="id">ID của Server.</param>        
        /// <param name="startAddress">Địa chỉ bắt đầu ghi.</param>
        /// <param name="numBits">Số Coil muốn ghi.</param>
        /// <param name="values">Mảng giá trị để ghi xuống, kiểu byte, mỗi phần tử chứa giá trị của 8 coil.</param>
        /// <param name="result">Kết quả, nếu mảng = null nghĩa là ghi không thành công.</param>
        public void WriteMultipleCoils(ushort id, ushort startAddress, ushort numBits, byte[] values, ref byte[] result)
        {
            byte numBytes = Convert.ToByte(values.Length);
            byte[] data;
            data = CreateWriteHeader(id, 0, startAddress, numBits, (byte)(numBytes + 2), fctWriteMultipleCoils);
            Array.Copy(values, 0, data, 13, numBytes);
            result = WriteSyncData(data, id);
        }

        /// <summary>Ghi 1 thanh ghi.</summary>
        /// <param name="id">ID của Server.</param>       
        /// <param name="startAddress">Địa chỉ bắt đầu ghi.</param>
        /// <param name="values">Mảng giá trị để ghi xuống, kiểu byte.</param>
        /// <param name="result">Kết quả, nếu mảng = null nghĩa là ghi không thành công.</param>
        public void WriteSingleRegister(ushort id, ushort startAddress, byte[] values, ref byte[] result)
        {
            byte[] data;
            data = CreateWriteHeader(id, 0, startAddress, 1, 1, fctWriteSingleRegister);
            data[10] = values[0];
            data[11] = values[1];
            result = WriteSyncData(data, id);
        }

        /// <summary>Ghi nhiều thanh ghi 1 lúc.</summary>
        /// <param name="id">ID của Server.</param>        
        /// <param name="startAddress">Địa chỉ bắt đầu ghi.</param>
        /// <param name="values">Mảng giá trị để ghi xuống, kiểu byte.</param>
        /// <param name="result">Kết quả, nếu mảng = null nghĩa là ghi không thành công.</param>
        public void WriteMultipleRegister(ushort id, ushort startAddress, byte[] values, ref byte[] result)
        {
            ushort numBytes = Convert.ToUInt16(values.Length);
            if (numBytes % 2 > 0) numBytes++;
            byte[] data;

            data = CreateWriteHeader(id, 0, startAddress, Convert.ToUInt16(numBytes / 2), Convert.ToUInt16(numBytes + 2), fctWriteMultipleRegister);
            Array.Copy(values, 0, data, 13, values.Length);
            result = WriteSyncData(data, id);
        }
      
        // Create modbus header for read action
        private byte[] CreateReadHeader(ushort id, byte unit, ushort startAddress, ushort length, byte function)
        {
            byte[] data = new byte[12];
            try
            {                
                byte[] _id = BitConverter.GetBytes((short)id);
                data[0] = _id[1];			    // Server id high byte
                data[1] = _id[0];				// Server id low byte
                data[5] = 6;					// Message size
                data[6] = unit;					// Server address
                data[7] = function;				// Function code
                byte[] _adr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startAddress));
                data[8] = _adr[0];				// Start address
                data[9] = _adr[1];				// Start address
                byte[] _length = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)length));
                data[10] = _length[0];			// Number of data to read
                data[11] = _length[1];			// Number of data to read
                return data;
            }
            catch { return data; }
        }

        // ------------------------------------------------------------------------
        // Create modbus header for write action
        private byte[] CreateWriteHeader(ushort id, byte unit, ushort startAddress, ushort numData, ushort numBytes, byte function)
        {
            byte[] data = new byte[numBytes + 11];
            try
            {
                byte[] _id = BitConverter.GetBytes((short)id);
                data[0] = _id[1];				// Server id high byte
                data[1] = _id[0];				// Server id low byte
                byte[] _size = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)(5 + numBytes)));
                data[4] = _size[0];				// Complete message size in bytes
                data[5] = _size[1];				// Complete message size in bytes
                data[6] = unit;					// Server address
                data[7] = function;				// Function code
                byte[] _adr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startAddress));
                data[8] = _adr[0];				// Start address
                data[9] = _adr[1];				// Start address
                if (function >= fctWriteMultipleCoils)
                {
                    byte[] _cnt = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numData));
                    data[10] = _cnt[0];			// Number of bytes
                    data[11] = _cnt[1];			// Number of bytes
                    data[12] = (byte)(numBytes - 2);
                }
                return data;
            }
            catch { return data; }
        }

        // ------------------------------------------------------------------------
        // Create modbus header for read/write action
        private byte[] CreateReadWriteHeader(ushort id, byte unit, ushort startReadAddress, ushort numRead, ushort startWriteAddress, ushort numWrite)
        {
            byte[] data = new byte[numWrite * 2 + 17];
            try
            {
                byte[] _id = BitConverter.GetBytes((short)id);
                data[0] = _id[1];						// Server id high byte
                data[1] = _id[0];						// Server id low byte
                byte[] _size = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)(11 + numWrite * 2)));
                data[4] = _size[0];						// Complete message size in bytes
                data[5] = _size[1];						// Complete message size in bytes
                data[6] = unit;							// Server address
                data[7] = fctReadWriteMultipleRegister;	// Function code
                byte[] _adr_read = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startReadAddress));
                data[8] = _adr_read[0];					// Start read address
                data[9] = _adr_read[1];					// Start read address
                byte[] _cnt_read = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numRead));
                data[10] = _cnt_read[0];				// Number of bytes to read
                data[11] = _cnt_read[1];				// Number of bytes to read
                byte[] _adr_write = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startWriteAddress));
                data[12] = _adr_write[0];				// Start write address
                data[13] = _adr_write[1];				// Start write address
                byte[] _cnt_write = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numWrite));
                data[14] = _cnt_write[0];				// Number of bytes to write
                data[15] = _cnt_write[1];				// Number of bytes to write
                data[16] = (byte)(numWrite * 2);

                return data;
            }
            catch { return data; }
        }

        // ------------------------------------------------------------------------
        // Write asynchronous data acknowledge
        private void OnSend(System.IAsyncResult result)
        {
            if (result.IsCompleted == false) CallException(0xFFFF, 0xFF, 0xFF, excSendFailt);
        }

        // Write data and and wait for response

        bool _IsBad = false;
        private byte[] WriteSyncData(byte[] write_data, ushort id)
        {
            try
            {
                if (tcpSynCl.Connected)
                {
                    tcpSynCl.Send(write_data, 0, write_data.Length, SocketFlags.None);
                    int result = tcpSynCl.Receive(tcpSynClBuffer, 0, tcpSynClBuffer.Length, SocketFlags.None);

                    byte unit = tcpSynClBuffer[6];
                    byte function = tcpSynClBuffer[7];
                    byte[] data;

                    if (result == 0) CallException(id, unit, write_data[7], excExceptionConnectionLost);

                    // ------------------------------------------------------------
                    // Response data is Server exception
                    if (function > excExceptionOffset)
                    {
                        function -= excExceptionOffset;
                        CallException(id, unit, function, tcpSynClBuffer[8]);
                        _IsBad = true;
                        return null;
                    }
                    // ------------------------------------------------------------
                    // Write response data
                    else if ((function >= fctWriteSingleCoil) && (function != fctReadWriteMultipleRegister))
                    {
                        if (_IsBad == false)
                        {
                            data = new byte[2];
                            Array.Copy(tcpSynClBuffer, 10, data, 0, 2);
                            return data;
                        }
                        else
                            _IsBad = false;

                    }
                    // ------------------------------------------------------------
                    // Read response data
                    else
                    {
                        if (_IsBad == false)
                        {
                            data = new byte[tcpSynClBuffer[8]];
                            Array.Copy(tcpSynClBuffer, 9, data, 0, tcpSynClBuffer[8]);
                            return data;
                        }
                        else
                            _IsBad = false;
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch { return null; }         
        }
    }
}
