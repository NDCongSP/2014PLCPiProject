using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.IO; 
using Snap7;

namespace PLCPiProject
{
    /// <summary>
    /// Đối tượng tổng thể, quản lý lập trình điều khiển giám sát và thu thập dữ liệu của PLCPi
    /// </summary>
    public class PLCPi: Component, IDisposable
    {

        #region AI
        /// <summary>
        /// Đối tượng tương tác ngõ vào AI của PLCPi
        /// </summary>
        public ADC AI
        {
            get { return _AI; }
        }
        static ADC _AI = new ADC();
        #endregion

        #region Đọc cảm biến DS18B20
        /// <summary>
        /// Đối tượng tương tác cảm biến nhiệt độ DS18b20
        /// </summary>
        public DS18B20 DS18B20
        {
            get { return _DS18B20; }
        }
        static DS18B20 _DS18B20 = new DS18B20();
        #endregion


        #region RTC
        /// <summary>
        /// Đối tượng làm việc với thời gian thực
        /// </summary>
        public Doc_Thoi_Gian ThoiGian
        {
            get { return _ThoiGian; }
        }
        static Doc_Thoi_Gian _ThoiGian = new Doc_Thoi_Gian();
        #endregion

        #region Đọc cảm biến DHT21
        /// <summary>
        /// Đối tượng tương tác cảm biến nhiệt độ độ ẩm DHT21
        /// </summary>
        public DHT21 DHT21
        {
            get { return _DHT21; }
        }
        static DHT21 _DHT21 = new DHT21();
        #endregion

        #region truyền thông profinet
        /// <summary>
        /// Đối tượng quản lý truyền thông S7 Ethernet TCPIP / Profinet
        /// </summary>
        public S7_Ethernet S7Ethernet
        {
            get { return _S7Ethernet; }
        }
        static S7_Ethernet _S7Ethernet = new S7_Ethernet();
        #endregion

        #region MySQL Logger
        /// <summary>
        /// Đối tượng quản lý tính năng lưu trữ dữ liệu vào mySQL và truy vấn báo cáo
        /// </summary>
        public mySQLLogger mySQLLogger
        {
            get { return _mySQLLogger; }
        }
        static mySQLLogger _mySQLLogger = new mySQLLogger();
        #endregion

        #region SQLServer Logger
        /// <summary>
        /// Đối tượng quản lý tính năng lưu trữ dữ liệu vào SQL Server và truy vấn báo cáo
        /// </summary>
        public SQLLogger SQLLogger
        {
            get { return _SQLLogger; }
        }
        static SQLLogger _SQLLogger = new SQLLogger();
        #endregion

        #region Goi điện thoại từ GSM modem của USB 3G
        /// <summary>
        /// Đối tượng quản lý tính năng gọi điện thoại từ GSM modem của USB 3G
        /// </summary>
        public PhoneCall PhoneCall
        {
            get { return _PhoneCall; }
        }
        static PhoneCall _PhoneCall = new PhoneCall();
        #endregion

        #region Gửi SMS từ GSM modem của USB 3G
        /// <summary>
        /// Đối tượng quản lý tính năng gửi SMS từ GSM modem của USB 3G
        /// </summary>
        public SMS SMS
        {
            get { return _SMS; }
        }
        static SMS _SMS = new SMS();
        #endregion
      

        #region Gửi Email
        /// <summary>
        /// Đối tượng quản lý tính năng gửi Email
        /// </summary>
        public Email Email
        {
            get { return _Email; }
        }
        static Email _Email = new Email();
        #endregion

        #region Modbus RTU Master
        /// <summary>
        /// Đối tượng quản lý tính năng modbus RTU Master
        /// </summary>
        public ModbusRTUMaster ModbusRTUMaster
        {
            get { return _ModbusRTUMaster; }
        }
        static ModbusRTUMaster _ModbusRTUMaster = new ModbusRTUMaster();
        #endregion

        #region Modbus TCP/IP Client
        /// <summary>
        /// Đối tượng quản lý tính năng modbus TCP Client
        /// </summary>
        public ModbusTCPClient ModbusTCPClient
        {
            get { return _ModbusTCPClient; }
        }
        static ModbusTCPClient _ModbusTCPClient = new ModbusTCPClient();
        #endregion

        //////////////////////////////////////////////////////
        #region lấy giá trị  theo kiểu dữ liệu mong muốn từ mảng kiểu byte
        /// <summary
        /// Đọc về giá trị kiểu Bool
        /// </summary>
        /// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        /// <param name="pos">vị trí byte bắt dầu chuyển đổi</param>
        /// <param name="bit">vị trí bit muốn đọc</param>
        /// <returns></returns>
        public bool GetBoolAt(byte[] buffer, int pos, int bit)
        {
            try
            {
                return S7.GetBitAt(buffer, pos, bit);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Đọc về giá trị kiểu Word không dấu(Ushort), giá trị từ 0 – 65535. 2 byte
        /// </summary>
        /// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        /// <param name="pos">vị trí byte bắt dầu chuyển đổi</param>
        /// <returns></returns>
        public ushort GetUshortAt(byte[] buffer, int pos)
        {
            try
            {
                return S7.GetWordAt(buffer, pos);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// Đọc về giá trị kiểu Word có dấu(short), giá trị từ -32768 đến 32767. 2 byte
        /// </summary>
        /// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        /// <param name="pos">vị trí byte bắt dầu chuyển đổi</param>
        /// <returns></returns>
        public short GetShortAt(byte[] buffer, int pos)//int
        {
            try
            {
                return S7.GetIntAt(buffer, pos);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Đọc về giá trị kiểu DWord không dấu(Uint), giá trị từ 0 đến 4.294.967.295, 4 byte
        /// </summary>
        /// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public uint GetUintAt(byte[] buffer, int pos)
        {
            try
            {
                return S7.GetDWordAt(buffer, pos);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// Đọc về giá trị kiểu DWord có dấu(int), giá trị từ –2.147.483.647 đến 2.147.483.647, 4 byte
        /// </summary>
        /// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        /// <param name="pos">vị trí byte bắt dầu chuyển đổi</param>
        /// <returns></returns>
        public int GetIntAt(byte[] buffer, int pos)
        {
            try
            {
                return S7.GetDIntAt(buffer, pos);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Đọc về giá trị kiểu float. 4 byte
        /// </summary>
        /// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        /// <param name="pos">vị trí byte bắt dầu chuyển đổi</param>
        /// <returns></returns>
        public float GetFloatAt(byte[] buffer, int pos)
        {
            try
            {
                return S7.GetRealAt(buffer, pos);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// Đọc về giá trị kiểu DWord không dấu(Uint), giá trị từ 0 đến 4.294.967.295, 4 byte. Dạng first word low.
        /// </summary>
        /// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi.</param>
        /// <param name="pos">vị trí byte bắt dầu chuyển đổi.</param>
        /// <returns>Trả về giá trị là 1 số DWord.</returns>
        public uint GetUint_LSB_At(byte[] buffer, int pos)
        {
            byte[] myBuffer = new byte[4];
            
            try
            {            
                myBuffer[0] = buffer[pos + 2];
                myBuffer[1] = buffer[pos + 3];
                myBuffer[2] = buffer[pos];
                myBuffer[3] = buffer[pos + 1];
             
                return S7.GetDWordAt(myBuffer, 0);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// Đọc về giá trị kiểu DWord có dấu(int), giá trị từ –2.147.483.647 đến 2.147.483.647, 4 byte. Dạng first word low.
        /// </summary>
        /// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi.</param>
        /// <param name="pos">vị trí byte bắt dầu chuyển đổi.</param>
        /// <returns>Trả về giá trị là 1 số Int.</returns>
        public int GetIntAt_LSB_At(byte[] buffer, int pos)
        {
            byte[] myBuffer = new byte[4];
            try
            {
                myBuffer[0] = buffer[pos + 2];
                myBuffer[1] = buffer[pos + 3];
                myBuffer[2] = buffer[pos];
                myBuffer[3] = buffer[pos + 1];

                return S7.GetDIntAt(myBuffer, 0);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// Đọc về giá trị kiểu float. 4 byte. Dạng first word low.
        /// </summary>
        /// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi.</param>
        /// <param name="pos">vị trí byte bắt dầu chuyển đổi.</param>
        /// <returns>Trả về giá trị là 1 số Float.</returns>
        public float GetFloatAt_LSB_At(byte[] buffer, int pos)
        {
            byte[] myBuffer = new byte[4];
            try
            {
                myBuffer[0] = buffer[pos + 2];
                myBuffer[1] = buffer[pos + 3];
                myBuffer[2] = buffer[pos];
                myBuffer[3] = buffer[pos + 1];

                return S7.GetRealAt(myBuffer, 0);
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region set giá trị cho các vùng nhớ của S7 ethernet theo kiểu dữ liệu mong muốn
        /// <summary>
        /// Ghi giá trị kiểu bit xuống mảng byte
        /// </summary>
        /// <param name="Buffer">mảng Byte cần ghi giá trị vào</param>
        /// <param name="Pos">vị trí byte bắt muốn ghi</param>
        /// <param name="Bit">vị trí bit muốn ghi trong byte</param>
        /// <param name="Value">giá trị kiểu int. Co giá trị là 0 hoặc 1</param>
        public void SetBit(byte[] Buffer, int Pos, int Bit, int Value)
        {
            try
            {
                S7.SetBitAt(ref Buffer, Pos, Bit, Convert.ToBoolean(Value));
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// ghi số ushort vào mảng Byte
        /// </summary>
        /// <param name="Buffer">mảng Byte cần ghi giá trị vào</param>
        /// <param name="Pos">vị trí byte bắt đầu ghi</param>
        /// <param name="Value">giá trị kiểu ushort( 0 - 65535)</param>
        public void SetWord(byte[] Buffer, int Pos, ushort Value)
        {
            try
            {
                S7.SetWordAt(Buffer, Pos, Value);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// ghi số short vào mảng Byte
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Pos"></param>
        /// <param name="Value"></param>
        public void SetInt(byte[] Buffer, int Pos, short Value)
        {
            try
            {
                S7.SetIntAt(Buffer, Pos, Value);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// ghi số uint vào mảng Byte
        /// </summary>
        /// <param name="Buffer">mảng Byte cần ghi giá trị vào</param>
        /// <param name="Pos">vị trí byte bắt đầu ghi</param>
        /// <param name="Value">giá trị kiểu Float</param>
        public void SetDWord(byte[] Buffer, int Pos, uint Value)
        {
            try
            {
                S7.SetDWordAt(Buffer, Pos, Value);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// ghi số int vào mảng Byte
        /// </summary>
        /// <param name="Buffer">mảng Byte cần ghi giá trị vào</param>
        /// <param name="Pos">vị trí byte bắt đầu ghi</param>
        /// <param name="Value">giá trị kiểu int</param>
        public void SetDint(byte[] Buffer, int Pos, int Value)
        {
            try
            {
                S7.SetDIntAt(Buffer, Pos, Value);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// ghi số Float vào mảng Byte
        /// </summary>
        /// <param name="Buffer">mảng Byte cần ghi giá trị vào.</param>
        /// <param name="Pos">vị trí byte bắt đầu ghi.</param>
        /// <param name="Value">giá trị kiểu Float.</param>
        public void SetFloat(byte[] Buffer, int Pos, float Value)
        {
            try
            {
                S7.SetRealAt(Buffer, Pos, Value);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// ghi số uint vào mảng Byte. Dạng First word low.
        /// </summary>
        /// <param name="Buffer">mảng Byte cần ghi giá trị vào.</param>
        /// <param name="Pos">vị trí byte bắt đầu ghi.</param>
        /// <param name="Value">giá trị kiểu uint.</param>
        public void SetDWord_LSB(byte[] Buffer, int Pos, uint Value)
        {
            byte[] myBuffer = new byte[4];
            try
            {
                S7.SetDWordAt(myBuffer, 0, Value);

                Buffer[Pos] = myBuffer[2];
                Buffer[Pos + 1] = myBuffer[3];
                Buffer[Pos + 2] = myBuffer[0];
                Buffer[Pos + 3] = myBuffer[1];
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// ghi số int vào mảng Byte. Dạng First word low.
        /// </summary>
        /// <param name="Buffer">mảng Byte cần ghi giá trị vào.</param>
        /// <param name="Pos">vị trí byte bắt đầu ghi.</param>
        /// <param name="Value">giá trị kiểu int.</param>
        public void SetDint_LSB(byte[] Buffer, int Pos, int Value)
        {
            byte[] myBuffer = new byte[4];
            try
            {
                S7.SetDIntAt(myBuffer, 0, Value);

                Buffer[Pos] = myBuffer[2];
                Buffer[Pos + 1] = myBuffer[3];
                Buffer[Pos + 2] = myBuffer[0];
                Buffer[Pos + 3] = myBuffer[1];

            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// ghi số Float vào mảng Byte. Dạng First word low.
        /// </summary>
        /// <param name="Buffer">mảng Byte cần ghi giá trị vào.</param>
        /// <param name="Pos">vị trí byte bắt đầu ghi.</param>
        /// <param name="Value">giá trị kiểu float.</param>
        public void SetFloat_LSB(byte[] Buffer, int Pos, float Value)
        {
            byte[] myBuffer = new byte[4];
            try
            {
                S7.SetRealAt(myBuffer, 0, Value);

                Buffer[Pos] = myBuffer[2];
                Buffer[Pos + 1] = myBuffer[3];
                Buffer[Pos + 2] = myBuffer[0];
                Buffer[Pos + 3] = myBuffer[1];

            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Contruction
        /// <summary>
        /// Khởi tạo đối tượng PLCPi
        /// </summary>
        public PLCPi() 
        {
           
        }
        #endregion

        /// <summary>
        /// Dispose đối tượng
        /// </summary>
        ~ PLCPi()
        {
        }
        
    }
}
