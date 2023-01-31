using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCPiProject
{
    /// <summary>
    /// Đối tượng quản lý truyền thông S7 Ethernet TCPIP/ Profinet cho PLCPi
    /// Bao gồm S7 Server và S7 Client
    /// </summary>
    public class S7_Ethernet
    {
        /// <summary>
        /// S7 Ethernet / Profinet Client
        /// </summary>
        public Snap7_Client Client = new Snap7_Client();
        
        /// <summary>
        /// S7 Ethernet/ Profinet Server
        /// </summary>
        public Snap7_Server Server = new Snap7_Server();

        #region lấy giá trị các vùng nhớ của S7 ethernet theo kiểu dữ liệu mong muốn
        ///// <summary>
        ///// Đọc về giá trị kiểu Bool
        ///// </summary>
        ///// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        ///// <param name="pos">vị trí byte bắt dầu chuyển đổi</param>
        ///// <param name="bit">vị trí bit muốn đọc</param>
        ///// <returns></returns>
        //public bool GetBoolAt(byte[] buffer, int pos, int bit)
        //{
        //    try
        //    {
        //        return S7.GetBitAt(buffer, pos, bit);
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        
        ///// <summary>
        ///// Đọc về giá trị kiểu Word không dấu(Ushort), giá trị từ 0 – 65535. 2 byte
        ///// </summary>
        ///// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        ///// <param name="pos">vị trí byte bắt dầu chuyển đổi</param>
        ///// <returns></returns>
        //public ushort GetUshortAt(byte[] buffer, int pos)
        //{
        //    try
        //    {
        //        return S7.GetWordAt(buffer, pos);
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        ///// <summary>
        ///// Đọc về giá trị kiểu Word có dấu(short), giá trị từ -32768 đến 32767. 2 byte
        ///// </summary>
        ///// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        ///// <param name="pos">vị trí byte bắt dầu chuyển đổi</param>
        ///// <returns></returns>
        //public short GetShortAt(byte[] buffer, int pos)//int
        //{
        //    try
        //    {
        //        return S7.GetIntAt(buffer, pos);
        //    }
        //    catch (Exception ex) { throw ex; }
        //}

        ///// <summary>
        ///// Đọc về giá trị kiểu DWord không dấu(Uint), giá trị từ 0 đến 4.294.967.295, 4 byte
        ///// </summary>
        ///// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        ///// <param name="pos"></param>
        ///// <returns></returns>
        //public uint GetUintAt(byte[] buffer, int pos)
        //{
        //    try
        //    {
        //        return S7.GetDWordAt(buffer, pos);
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        ///// <summary>
        ///// Đọc về giá trị kiểu DWord có dấu(int), giá trị từ –2.147.483.647 đến 2.147.483.647, 4 byte
        ///// </summary>
        ///// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        ///// <param name="pos">vị trí byte bắt dầu chuyển đổi</param>
        ///// <returns></returns>
        //public int GetIntAt(byte[] buffer, int pos)
        //{
        //    try
        //    {
        //        return S7.GetDIntAt(buffer, pos);
        //    }
        //    catch (Exception ex) { throw ex; }
        //}

        ///// <summary>
        ///// Đọc về giá trị kiểu float. 4 byte
        ///// </summary>
        ///// <param name="buffer">mảng chứa các phần tử kiểu byte đưa vào để chuyển đổi</param>
        ///// <param name="pos">vị trí byte bắt dầu chuyển đổi</param>
        ///// <returns></returns>
        //public float GetFloatAt(byte[] buffer, int pos)
        //{
        //    try
        //    {
        //        return S7.GetRealAt(buffer, pos);
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        #endregion

        #region set giá trị cho các vùng nhớ của S7 ethernet theo kiểu dữ liệu mong muốn
        ///// <summary>
        ///// Ghi giá trị kiểu bit xuống mảng byte
        ///// </summary>
        ///// <param name="Buffer">mảng Byte cần ghi giá trị vào</param>
        ///// <param name="Pos">vị trí byte bắt muốn ghi</param>
        ///// <param name="Bit">vị trí bit muốn ghi trong byte</param>
        ///// <param name="Value">giá trị kiểu int. Co giá trị là 0 hoặc 1</param>
        //public void SetBit(byte[] Buffer, int Pos, int Bit, int Value)
        //{
        //    try
        //    {
        //        S7.SetBitAt(ref Buffer, Pos, Bit, Convert.ToBoolean(Value));
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        ///// <summary>
        ///// ghi số ushort vào mảng Byte
        ///// </summary>
        ///// <param name="Buffer">mảng Byte cần ghi giá trị vào</param>
        ///// <param name="Pos">vị trí byte bắt đầu ghi</param>
        ///// <param name="Value">giá trị kiểu ushort( 0 - 65535)</param>
        //public void SetWord(byte[] Buffer, int Pos, ushort Value)
        //{
        //    try
        //    {
        //        S7.SetWordAt(Buffer, Pos, Value);
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        ///// <summary>
        ///// ghi số short vào mảng Byte
        ///// </summary>
        ///// <param name="Buffer"></param>
        ///// <param name="Pos"></param>
        ///// <param name="Value"></param>
        //public void SetInt(byte[] Buffer, int Pos, short Value)
        //{
        //    try
        //    {
        //        S7.SetIntAt(Buffer, Pos, Value);
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        ///// <summary>
        ///// ghi số uint vào mảng Byte
        ///// </summary>
        ///// <param name="Buffer">mảng Byte cần ghi giá trị vào</param>
        ///// <param name="Pos">vị trí byte bắt đầu ghi</param>
        ///// <param name="Value">giá trị kiểu Float</param>
        //public void SetDWord(byte[] Buffer, int Pos, uint Value)
        //{
        //    try
        //    {
        //        S7.SetDWordAt(Buffer, Pos, Value);
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        ///// <summary>
        ///// ghi số int vào mảng Byte
        ///// </summary>
        ///// <param name="Buffer">mảng Byte cần ghi giá trị vào</param>
        ///// <param name="Pos">vị trí byte bắt đầu ghi</param>
        ///// <param name="Value">giá trị kiểu int</param>
        //public void SetDint(byte[] Buffer, int Pos, int Value)
        //{
        //    try
        //    {
        //        S7.SetDIntAt(Buffer, Pos, Value);
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        ///// <summary>
        ///// ghi số Float vào mảng Byte
        ///// </summary>
        ///// <param name="Buffer">mảng Byte cần ghi giá trị vào</param>
        ///// <param name="Pos">vị trí byte bắt đầu ghi</param>
        ///// <param name="Value">giá trị kiểu Float</param>
        //public void SetFloat(byte[] Buffer, int Pos, float Value)
        //{
        //    try
        //    {
        //        S7.SetRealAt(Buffer, Pos, Value);
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        #endregion

    }
}
