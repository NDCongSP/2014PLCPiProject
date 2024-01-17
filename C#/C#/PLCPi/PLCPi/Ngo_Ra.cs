using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCPiProject
{
    /// <summary>
    /// Class Output. 
    /// 1. Set giá trị cho các ngõ ra của PLCPi: Xuất theo Bit hoặc theo Byte
    /// 2. Đọc trạng thái hiện tại của các ngõ ra: Đọc theo Bit hoặc theo Byte
    /// </summary>
    public class Ngo_Ra
    {
        IC595 myIC595_Out = new IC595();
        public byte[] MangNgoRa = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }; //MangNgoRa[0] là phần tử chứa dữ liệu xuất cho ngõ ra "Q0"
        byte Data = 0, Data_Return = 0;//Biến dùng khi sử dụng method ĐỌC NGÕ RA. Dùng để trả về kết quả cho Method đọc trạng thái của các ngõ ra
        ///////////////////////////////////////////////////////////////////////////
        //Xuất giá trị ra các ngõ ra                                             //
        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Method xuất giá trị cho các ngõ ra. Có 2 cách xuất: theo Bit hoặc theo Byte
        /// </summary>
        /// <param name="Chanel">ngõ ra muốn xuất "Qx.x"(xuất theo Bit); "Qx"(xuất theo Byte). Ví dụ: theo bit: "Q0.1"; theo Byte: "Q0"</param>
        /// <param name="Value">nếu xuất theo Bit thì Value = 0 or 1; xuất theo Byte Value = 1Byte</param>
        public void XuatNgoRa(string Chanel, byte Value)
        {
            if (Chanel.Substring(0, 2) == "Q0")
            {
                Q0(Chanel, Value);
            }
            else if (Chanel.Substring(0, 2) == "Q1")
            {
                Q1(Chanel, Value);
            }
            else if (Chanel.Substring(0, 2) == "Q2")
            {
                Q2(Chanel, Value);
            }
            else if (Chanel.Substring(0, 2) == "Q3")
            {
                Q3(Chanel, Value);
            }
            else if (Chanel.Substring(0, 2) == "Q4")
            {
                Q4(Chanel, Value);
            }
            else if (Chanel.Substring(0, 2) == "Q5")
            {
                Q5(Chanel, Value);
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        // method đọc trạng thái của các ngõ ra                                                //
        /////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// method đọc trạng thái của các ngõ ra. Có 2 cách đọc: theo Bit hoặc theo Byte.Dữ liệu trả về kiểu byte.
        /// Nếu đọc theo Bit thì Dữ liệu trả về bằng 0 hoặc 1
        /// </summary>
        /// <param name="Chanel">ngõ ra muốn đọc. "Qx.x": đọc theo Bit; "Qx": đọc theo Byte. Ví dụ: theo Bit: "Q0.0"; theo Byte: "Q0"</param>
        /// <returns></returns>
        public byte DocNgoRa(string Chanel)
        {
            if (Chanel.Substring(0, 2) == "Q0")
            {
                Data_Return = DocQ0(Chanel);
            }
            else if (Chanel.Substring(0, 2) == "Q1")
            {
                Data_Return = DocQ1(Chanel);
            }
            else if (Chanel.Substring(0, 2) == "Q2")
            {
                Data_Return = DocQ2(Chanel);
            }
            else if (Chanel.Substring(0, 2) == "Q3")
            {
                Data_Return = DocQ3(Chanel);
            }
            else if (Chanel.Substring(0, 2) == "Q4")
            {
                Data_Return = DocQ4(Chanel);
            }
            else if (Chanel.Substring(0, 2) == "Q5")
            {
                Data_Return = DocQ5(Chanel);
            }
            return Data_Return;
        }
        
        //các method để xuất ngõ ra
        private void Q0 (string Chanel, byte Value)
        {
            if (Chanel.Length == 4) //Xuất Bit
            {
                if (Chanel.Substring(3,1) == "0")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] | 0x01);
                    }
                    else
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] & 0xFE);
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] | 0x02);
                    }
                    else
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] & 0xFD);
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] | 0x04);
                    }
                    else
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] & 0xFB);
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] | 0x08);
                    }
                    else
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] & 0xF7);
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] | 0x10);
                    }
                    else
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] & 0xEF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] | 0x20);
                    }
                    else
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] & 0xDF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] | 0x40);
                    }
                    else
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] & 0xBF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] | 0x80);
                    }
                    else
                    {
                        MangNgoRa[0] = Convert.ToByte(MangNgoRa[0] & 0x7F);
                    }
                }
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
            else if (Chanel.Length == 2)//Xuất Byte
            {
                MangNgoRa[0] = Value;
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
        }
        private void Q1(string Chanel, byte Value)
        {
            if (Chanel.Length == 4)
            {
                if (Chanel.Substring(3, 1) == "0")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] | 0x01);
                    }
                    else
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] & 0xFE);
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] | 0x02);
                    }
                    else
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] & 0xFD);
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] | 0x04);
                    }
                    else
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] & 0xFB);
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] | 0x08);
                    }
                    else
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] & 0xF7);
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] | 0x10);
                    }
                    else
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] & 0xEF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] | 0x20);
                    }
                    else
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] & 0xDF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] | 0x40);
                    }
                    else
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] & 0xBF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] | 0x80);
                    }
                    else
                    {
                        MangNgoRa[1] = Convert.ToByte(MangNgoRa[1] & 0x7F);
                    }
                }
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
            else if (Chanel.Length == 2)
            {
                MangNgoRa[1] = Value;
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
        }
        private void Q2(string Chanel, byte Value)
        {
            if (Chanel.Length == 4)
            {
                if (Chanel.Substring(3, 1) == "0")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] | 0x01);
                    }
                    else
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] & 0xFE);
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] | 0x02);
                    }
                    else
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] & 0xFD);
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] | 0x04);
                    }
                    else
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] & 0xFB);
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] | 0x08);
                    }
                    else
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] & 0xF7);
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] | 0x10);
                    }
                    else
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] & 0xEF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] | 0x20);
                    }
                    else
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] & 0xDF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] | 0x40);
                    }
                    else
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] & 0xBF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] | 0x80);
                    }
                    else
                    {
                        MangNgoRa[2] = Convert.ToByte(MangNgoRa[2] & 0x7F);
                    }
                }
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
            else if (Chanel.Length == 2)
            {
                MangNgoRa[2] = Value;
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
        }
        private void Q3(string Chanel, byte Value)
        {
            if (Chanel.Length == 4)
            {
                if (Chanel.Substring(3, 1) == "0")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] | 0x01);
                    }
                    else
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] & 0xFE);
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] | 0x02);
                    }
                    else
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] & 0xFD);
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] | 0x04);
                    }
                    else
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] & 0xFB);
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] | 0x08);
                    }
                    else
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] & 0xF7);
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] | 0x10);
                    }
                    else
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] & 0xEF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] | 0x20);
                    }
                    else
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] & 0xDF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] | 0x40);
                    }
                    else
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] & 0xBF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] | 0x80);
                    }
                    else
                    {
                        MangNgoRa[3] = Convert.ToByte(MangNgoRa[3] & 0x7F);
                    }
                }
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
            else if (Chanel.Length == 2)
            {
                MangNgoRa[3] = Value;
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
        }
        private void Q4(string Chanel, byte Value)
        {
            if (Chanel.Length == 4)
            {
                if (Chanel.Substring(3, 1) == "0")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] | 0x01);
                    }
                    else
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] & 0xFE);
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] | 0x02);
                    }
                    else
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] & 0xFD);
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] | 0x04);
                    }
                    else
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] & 0xFB);
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] | 0x08);
                    }
                    else
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] & 0xF7);
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] | 0x10);
                    }
                    else
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] & 0xEF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] | 0x20);
                    }
                    else
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] & 0xDF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] | 0x40);
                    }
                    else
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] & 0xBF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] | 0x80);
                    }
                    else
                    {
                        MangNgoRa[4] = Convert.ToByte(MangNgoRa[4] & 0x7F);
                    }
                }
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
            else if (Chanel.Length == 2)
            {
                MangNgoRa[4] = Value;
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
        }
        private void Q5(string Chanel, byte Value)
        {
            if (Chanel.Length == 4)
            {
                if (Chanel.Substring(3, 1) == "0")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] | 0x01);
                    }
                    else
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] & 0xFE);
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] | 0x02);
                    }
                    else
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] & 0xFD);
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] | 0x04);
                    }
                    else
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] & 0xFB);
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] | 0x08);
                    }
                    else
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] & 0xF7);
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] | 0x10);
                    }
                    else
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] & 0xEF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] | 0x20);
                    }
                    else
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] & 0xDF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] | 0x40);
                    }
                    else
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] & 0xBF);
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if (Value == 1)
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] | 0x80);
                    }
                    else
                    {
                        MangNgoRa[5] = Convert.ToByte(MangNgoRa[5] & 0x7F);
                    }
                }
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
            else if (Chanel.Length == 2)
            {
                MangNgoRa[5] = Value;
                myIC595_Out.DichDuLieu(MangNgoRa);
            }
        }

        //các method để đọc ngõ ra
        private byte DocQ0(string Chanel)
        {
            if (Chanel.Length == 4) //Đọc Bit
            {
                if (Chanel.Substring(3,1) == "0")
                {
                    if ((MangNgoRa[0] & 0x01) == 0x01)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if ((MangNgoRa[0] & 0x02) == 0x02)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if ((MangNgoRa[0] & 0x04) == 0x04)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if ((MangNgoRa[0] & 0x08) == 0x08)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if ((MangNgoRa[0] & 0x10) == 0x10)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if ((MangNgoRa[0] & 0x20) == 0x20)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if ((MangNgoRa[0] & 0x40) == 0x40)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if ((MangNgoRa[0] & 0x80) == 0x80)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
            }
            else if (Chanel.Length == 2) //Đọc Byte
            {
                Data = MangNgoRa[0];
            }
            return Data;
        }
        private byte DocQ1(string Chanel)
        {
            if (Chanel.Length == 4)
            {
                if (Chanel.Substring(3, 1) == "0")
                {
                    if ((MangNgoRa[1] & 0x01) == 0x01)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if ((MangNgoRa[1] & 0x02) == 0x02)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if ((MangNgoRa[1] & 0x04) == 0x04)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if ((MangNgoRa[1] & 0x08) == 0x08)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if ((MangNgoRa[1] & 0x10) == 0x10)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if ((MangNgoRa[1] & 0x20) == 0x20)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if ((MangNgoRa[1] & 0x40) == 0x40)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if ((MangNgoRa[1] & 0x80) == 0x80)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
            }
            else if (Chanel.Length == 2)
            {
                Data = MangNgoRa[1];
            }
            return Data;
        }
        private byte DocQ2(string Chanel)
        {
            if (Chanel.Length == 4)
            {
                if (Chanel.Substring(3, 1) == "0")
                {
                    if ((MangNgoRa[2] & 0x01) == 0x01)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if ((MangNgoRa[2] & 0x02) == 0x02)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if ((MangNgoRa[2] & 0x04) == 0x04)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if ((MangNgoRa[2] & 0x08) == 0x08)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if ((MangNgoRa[2] & 0x10) == 0x10)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if ((MangNgoRa[2] & 0x20) == 0x20)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if ((MangNgoRa[2] & 0x40) == 0x40)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if ((MangNgoRa[2] & 0x80) == 0x80)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
            }
            else if (Chanel.Length == 2)
            {
                Data = MangNgoRa[2];
            }
            return Data;
        }
        private byte DocQ3(string Chanel)
        {
            if (Chanel.Length == 4)
            {
                if (Chanel.Substring(3, 1) == "0")
                {
                    if ((MangNgoRa[3] & 0x01) == 0x01)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if ((MangNgoRa[3] & 0x02) == 0x02)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if ((MangNgoRa[3] & 0x04) == 0x04)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if ((MangNgoRa[3] & 0x08) == 0x08)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if ((MangNgoRa[3] & 0x10) == 0x10)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if ((MangNgoRa[3] & 0x20) == 0x20)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if ((MangNgoRa[3] & 0x40) == 0x40)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if ((MangNgoRa[3] & 0x80) == 0x80)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
            }
            else if (Chanel.Length == 2)
            {
                Data = MangNgoRa[3];
            }
            return Data;
        }
        private byte DocQ4(string Chanel)
        {
            if (Chanel.Length == 4)
            {
                if (Chanel.Substring(3, 1) == "0")
                {
                    if ((MangNgoRa[4] & 0x01) == 0x01)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if ((MangNgoRa[4] & 0x02) == 0x02)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if ((MangNgoRa[4] & 0x04) == 0x04)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if ((MangNgoRa[4] & 0x08) == 0x08)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if ((MangNgoRa[4] & 0x10) == 0x10)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if ((MangNgoRa[4] & 0x20) == 0x20)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if ((MangNgoRa[4] & 0x40) == 0x40)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if ((MangNgoRa[4] & 0x80) == 0x80)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
            }
            else if (Chanel.Length == 2)
            {
                Data = MangNgoRa[4];
            }
            return Data;
        }
        private byte DocQ5(string Chanel)
        {
            if (Chanel.Length == 4)
            {
                if (Chanel.Substring(3, 1) == "0")
                {
                    if ((MangNgoRa[5] & 0x01) == 0x01)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "1")
                {
                    if ((MangNgoRa[5] & 0x02) == 0x02)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    if ((MangNgoRa[5] & 0x04) == 0x04)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    if ((MangNgoRa[5] & 0x08) == 0x08)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    if ((MangNgoRa[5] & 0x10) == 0x10)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    if ((MangNgoRa[5] & 0x20) == 0x20)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    if ((MangNgoRa[5] & 0x40) == 0x40)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    if ((MangNgoRa[5] & 0x80) == 0x80)
                    {
                        Data = 1;
                    }
                    else
                    {
                        Data = 0;
                    }
                }
            }
            else if (Chanel.Length == 2)
            {
                Data = MangNgoRa[5];
            }
            return Data;
        }

    }
}
