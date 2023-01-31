using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Raspberry.IO.GeneralPurpose;
using System.IO;
 
namespace PLCPiProject
{
    /// <summary>
    /// Đối tượng tương tác với các ngõ vào DI của PLCPi
    /// </summary>
    public class Ngo_Vao
    {
        ProcessorPin Add10 = ConnectorPin.P1Pin29.ToProcessor();
        ProcessorPin Add11 = ConnectorPin.P1Pin31.ToProcessor();
        ProcessorPin Add12 = ConnectorPin.P1Pin33.ToProcessor();

        ProcessorPin Add20 = ConnectorPin.P1Pin36.ToProcessor();
        ProcessorPin Add21 = ConnectorPin.P1Pin38.ToProcessor();
        ProcessorPin Add22 = ConnectorPin.P1Pin40.ToProcessor();

        ProcessorPin EnableV0 = ConnectorPin.P1Pin23.ToProcessor();
        ProcessorPin EnableV1 = ConnectorPin.P1Pin19.ToProcessor();
        ProcessorPin EnableV2 = ConnectorPin.P1Pin21.ToProcessor();

        ProcessorPin EnableV3 = ConnectorPin.P1Pin26.ToProcessor();
        ProcessorPin EnableV4 = ConnectorPin.P1Pin24.ToProcessor();

        ProcessorPin In_151 = ConnectorPin.P1Pin35.ToProcessor();
        ProcessorPin In_152 = ConnectorPin.P1Pin32.ToProcessor();

        IGpioConnectionDriver Driver = GpioConnectionSettings.DefaultDriver;

        string File_Path = "/media/NgoVao.txt";

        string In_Str = null;
        string MSB, LSB;//dùng để chuyển đổi về 1byte để trả về
        byte Byte_H, Byte_L;
        //byte Bit0 = 1, Bit1 = 2, Bit2 = 4, Bit3 = 8, Bit4 = 16, Bit5 = 32, Bit6 = 64, Bit7 = 128;
        byte Data = 0; //chứa dữ liệu để trả về
        byte[] MangNgoVaoTam = { 0, 0, 0, 0, 0, 0, 0, 0 };// phan tu MangNgoVaoTam[0] la phần tử chứa giá trị đọc về của 
                                                            //bit thap nhat( "Ix.0"). Dùng để đọc các bít ngõ vào. 
        /// <summary>
        ///5 byte ngõ vào tương ứng với IB0-IB4
        /// </summary>
        public byte[] MangNgoVao = { 0, 0, 0, 0, 0};//Mảng lưu trạng thái của các ngõ vào theo từng byte. MangNgoVao[0] :ngõ vào I0
        //public byte KhoaChinh = 0, KhoaPhu = 0;//xử lý đọc ngõ vào truyền thông
        /// <summary>
        /// Method đọc trạng thái của các ngõ vào. Có 2 cách đọc: theo Bit hoặc theo Byte. 
        /// Trả về dữ liệu kiểu Byte.. Nếu đọc theo Bit thì dữ liệu trả về bằng 0 hoặc 1
        /// </summary>
        /// <param name="Channel">Ngõ vào muốn đọc. Đọc theo Bit: "Ix.x"; đọc theo Byte: "Ix". Ví dụ: theo bit("I0.0"); theo Byte("I0")</param>
        /// <returns></returns>
        public byte DocNgoVao(string Channel)
        {
            byte Data_Return = 0;
            #region xử lý doc ngõ vào truyền thông
            //if (KhoaPhu == 0)
            //{
            //    //Console.WriteLine("Server");
            //    KhoaChinh = 1;
            #endregion
            try
            {
                if (Channel.Substring(0, 2) == "I0")
                {
                    Data_Return = I0(Channel);
                }
                else if (Channel.Substring(0, 2) == "I1")
                {
                    Data_Return = I1(Channel);
                }
                else if (Channel.Substring(0, 2) == "I2")
                {
                    Data_Return = I2(Channel);
                }
                else if (Channel.Substring(0, 2) == "I3")
                {
                    Data_Return = I3(Channel);
                }
                else if (Channel.Substring(0, 2) == "I4")
                {
                    Data_Return = I4(Channel);
                }
                #region xử lý doc ngõ vào truyền thông
                //KhoaChinh = 0;

                //}
                //else
                //{
                //    //Doc lên server thì cái này ko liên quan đúng ko
                //    //
                //    byte _KquaTam = MangNgoVao[Convert.ToInt16 (  Channel.Substring(1 , 1))];

                //    if (Channel.Contains("."))
                //    {
                //        byte _TtuBit = Convert.ToByte(Channel.Split('.')[1]);
                //        Data_Return = Convert.ToByte((_KquaTam >> _TtuBit) & 1);
                //    }
                //    else
                //        Data_Return = _KquaTam;

                //}
                #endregion
                return Data_Return;
            }
            catch (Exception ex) { throw ex; }
        }
        #region xử lý doc ngõ vào truyền thông
        ///// <summary>
        ///// Method xử lý truyền thông
        ///// </summary>
        ///// <param name="BienChon"></param>
        //public void DocNgoVao(bool BienChon)
        //{
        //    Console.WriteLine("Client");
        //    if (BienChon == true)
        //    {
        //        KhoaPhu = 1;
        //        I0("I0");
        //        I1("I1");
        //        I2("I2");
        //        I3("I3");
        //        I4("I4");
        //        KhoaPhu = 0;
        //    }
        //    else { }
        //}
        #endregion
        //các method dùng để đọc ngõ vào
        #region I0
        private byte I0(string Channel)
        {
            try
            {
                Driver.Write(EnableV0, false);
                Driver.Write(EnableV1, true);
                Driver.Write(EnableV2, true);
                if (Channel.Length == 4)  //đọc theo bit
                {
                    In_Str = "0";
                    if (Channel.Substring(3, 1) == "0")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[0];
                    }

                    else if (Channel.Substring(3, 1) == "1")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[1];
                    }
                    else if (Channel.Substring(3, 1) == "2")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[2];
                    }
                    else if (Channel.Substring(3, 1) == "3")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[3];
                    }
                    else if (Channel.Substring(3, 1) == "4")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[4];
                    }
                    else if (Channel.Substring(3, 1) == "5")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[5];
                    }
                    else if (Channel.Substring(3, 1) == "6")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[6];
                    }
                    else if (Channel.Substring(3, 1) == "7")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[7] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[7];
                    }
                    foreach (byte item in MangNgoVaoTam)
                    {
                        In_Str = In_Str + Convert.ToString(item);
                    }
                    MSB = In_Str.Substring(1, 4); //4bit thap
                    LSB = In_Str.Substring(5, 4); //4bit cao

                    //Byte cao
                    if (LSB == "0000")
                    {
                        Byte_H = 0;
                    }
                    else if (LSB == "1000")
                    {
                        Byte_H = 1;
                    }
                    else if (LSB == "0100")
                    {
                        Byte_H = 2;
                    }
                    else if (LSB == "1100")
                    {
                        Byte_H = 3;
                    }
                    else if (LSB == "0010")
                    {
                        Byte_H = 4;
                    }
                    else if (LSB == "1010")
                    {
                        Byte_H = 5;
                    }
                    else if (LSB == "0110")
                    {
                        Byte_H = 6;
                    }
                    else if (LSB == "1110")
                    {
                        Byte_H = 7;
                    }
                    else if (LSB == "0001")
                    {
                        Byte_H = 8;
                    }
                    else if (LSB == "1001")
                    {
                        Byte_H = 9;
                    }
                    else if (LSB == "0101")
                    {
                        Byte_H = 10;
                    }
                    else if (LSB == "1101")
                    {
                        Byte_H = 11;
                    }
                    else if (LSB == "0011")
                    {
                        Byte_H = 12;
                    }
                    else if (LSB == "1011")
                    {
                        Byte_H = 13;
                    }
                    else if (LSB == "0111")
                    {
                        Byte_H = 14;
                    }
                    else if (LSB == "1111")
                    {
                        Byte_H = 15;
                    }

                    //byte thap
                    if (MSB == "0000")
                    {
                        Byte_L = 0;
                    }
                    else if (MSB == "1000")
                    {
                        Byte_L = 1;
                    }
                    else if (MSB == "0100")
                    {
                        Byte_L = 2;
                    }
                    else if (MSB == "1100")
                    {
                        Byte_L = 3;
                    }
                    else if (MSB == "0010")
                    {
                        Byte_L = 4;
                    }
                    else if (MSB == "1010")
                    {
                        Byte_L = 5;
                    }
                    else if (MSB == "0110")
                    {
                        Byte_L = 6;
                    }
                    else if (MSB == "1110")
                    {
                        Byte_L = 7;
                    }
                    else if (MSB == "0001")
                    {
                        Byte_L = 8;
                    }
                    else if (MSB == "1001")
                    {
                        Byte_L = 9;
                    }
                    else if (MSB == "0101")
                    {
                        Byte_L = 10;
                    }
                    else if (MSB == "1101")
                    {
                        Byte_L = 11;
                    }
                    else if (MSB == "0011")
                    {
                        Byte_L = 12;
                    }
                    else if (MSB == "1011")
                    {
                        Byte_L = 13;
                    }
                    else if (MSB == "0111")
                    {
                        Byte_L = 14;
                    }
                    else if (MSB == "1111")
                    {
                        Byte_L = 15;
                    }
                    MangNgoVao[0] = Convert.ToByte(Byte_H * 16 + Byte_L);
                }

                else if (Channel.Length == 2) //đọc theo byte
                {
                    In_Str = "0";
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    Thread.Sleep(1);
                    MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    Thread.Sleep(1);
                    MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[7] = Convert.ToByte(Driver.Read(In_151));


                    foreach (byte item in MangNgoVaoTam)
                    {
                        In_Str = In_Str + Convert.ToString(item);
                    }
                    MSB = In_Str.Substring(1, 4);
                    LSB = In_Str.Substring(5, 4);

                    //Byte cao
                    if (LSB == "0000")
                    {
                        Byte_H = 0;
                    }
                    else if (LSB == "1000")
                    {
                        Byte_H = 1;
                    }
                    else if (LSB == "0100")
                    {
                        Byte_H = 2;
                    }
                    else if (LSB == "1100")
                    {
                        Byte_H = 3;
                    }
                    else if (LSB == "0010")
                    {
                        Byte_H = 4;
                    }
                    else if (LSB == "1010")
                    {
                        Byte_H = 5;
                    }
                    else if (LSB == "0110")
                    {
                        Byte_H = 6;
                    }
                    else if (LSB == "1110")
                    {
                        Byte_H = 7;
                    }
                    else if (LSB == "0001")
                    {
                        Byte_H = 8;
                    }
                    else if (LSB == "1001")
                    {
                        Byte_H = 9;
                    }
                    else if (LSB == "0101")
                    {
                        Byte_H = 10;
                    }
                    else if (LSB == "1101")
                    {
                        Byte_H = 11;
                    }
                    else if (LSB == "0011")
                    {
                        Byte_H = 12;
                    }
                    else if (LSB == "1011")
                    {
                        Byte_H = 13;
                    }
                    else if (LSB == "0111")
                    {
                        Byte_H = 14;
                    }
                    else if (LSB == "1111")
                    {
                        Byte_H = 15;
                    }

                    //byte thap
                    if (MSB == "0000")
                    {
                        Byte_L = 0;
                    }
                    else if (MSB == "1000")
                    {
                        Byte_L = 1;
                    }
                    else if (MSB == "0100")
                    {
                        Byte_L = 2;
                    }
                    else if (MSB == "1100")
                    {
                        Byte_L = 3;
                    }
                    else if (MSB == "0010")
                    {
                        Byte_L = 4;
                    }
                    else if (MSB == "1010")
                    {
                        Byte_L = 5;
                    }
                    else if (MSB == "0110")
                    {
                        Byte_L = 6;
                    }
                    else if (MSB == "1110")
                    {
                        Byte_L = 7;
                    }
                    else if (MSB == "0001")
                    {
                        Byte_L = 8;
                    }
                    else if (MSB == "1001")
                    {
                        Byte_L = 9;
                    }
                    else if (MSB == "0101")
                    {
                        Byte_L = 10;
                    }
                    else if (MSB == "1101")
                    {
                        Byte_L = 11;
                    }
                    else if (MSB == "0011")
                    {
                        Byte_L = 12;
                    }
                    else if (MSB == "1011")
                    {
                        Byte_L = 13;
                    }
                    else if (MSB == "0111")
                    {
                        Byte_L = 14;
                    }
                    else if (MSB == "1111")
                    {
                        Byte_L = 15;
                    }
                    MangNgoVao[0] = Convert.ToByte(Byte_H * 16 + Byte_L);
                    Data = MangNgoVao[0];
                }
                return Data;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region I1
        private byte I1(string Channel)
        {
            try
            {
                Driver.Write(EnableV0, true);
                Driver.Write(EnableV1, false);
                Driver.Write(EnableV2, true);

                if (Channel.Length == 4)  //đọc theo bit
                {
                    In_Str = "0";
                    if (Channel.Substring(3, 1) == "0")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[0];
                    }

                    else if (Channel.Substring(3, 1) == "1")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[1];
                    }
                    else if (Channel.Substring(3, 1) == "2")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[2];
                    }
                    else if (Channel.Substring(3, 1) == "3")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[3];
                    }
                    else if (Channel.Substring(3, 1) == "4")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[4];
                    }
                    else if (Channel.Substring(3, 1) == "5")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[5];
                    }
                    else if (Channel.Substring(3, 1) == "6")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[6];
                    }
                    else if (Channel.Substring(3, 1) == "7")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[7] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[7];
                    }
                    foreach (byte item in MangNgoVaoTam)
                    {
                        In_Str = In_Str + Convert.ToString(item);
                    }
                    MSB = In_Str.Substring(1, 4);
                    LSB = In_Str.Substring(5, 4);

                    //Byte cao
                    if (LSB == "0000")
                    {
                        Byte_H = 0;
                    }
                    else if (LSB == "1000")
                    {
                        Byte_H = 1;
                    }
                    else if (LSB == "0100")
                    {
                        Byte_H = 2;
                    }
                    else if (LSB == "1100")
                    {
                        Byte_H = 3;
                    }
                    else if (LSB == "0010")
                    {
                        Byte_H = 4;
                    }
                    else if (LSB == "1010")
                    {
                        Byte_H = 5;
                    }
                    else if (LSB == "0110")
                    {
                        Byte_H = 6;
                    }
                    else if (LSB == "1110")
                    {
                        Byte_H = 7;
                    }
                    else if (LSB == "0001")
                    {
                        Byte_H = 8;
                    }
                    else if (LSB == "1001")
                    {
                        Byte_H = 9;
                    }
                    else if (LSB == "0101")
                    {
                        Byte_H = 10;
                    }
                    else if (LSB == "1101")
                    {
                        Byte_H = 11;
                    }
                    else if (LSB == "0011")
                    {
                        Byte_H = 12;
                    }
                    else if (LSB == "1011")
                    {
                        Byte_H = 13;
                    }
                    else if (LSB == "0111")
                    {
                        Byte_H = 14;
                    }
                    else if (LSB == "1111")
                    {
                        Byte_H = 15;
                    }

                    //byte thap
                    if (MSB == "0000")
                    {
                        Byte_L = 0;
                    }
                    else if (MSB == "1000")
                    {
                        Byte_L = 1;
                    }
                    else if (MSB == "0100")
                    {
                        Byte_L = 2;
                    }
                    else if (MSB == "1100")
                    {
                        Byte_L = 3;
                    }
                    else if (MSB == "0010")
                    {
                        Byte_L = 4;
                    }
                    else if (MSB == "1010")
                    {
                        Byte_L = 5;
                    }
                    else if (MSB == "0110")
                    {
                        Byte_L = 6;
                    }
                    else if (MSB == "1110")
                    {
                        Byte_L = 7;
                    }
                    else if (MSB == "0001")
                    {
                        Byte_L = 8;
                    }
                    else if (MSB == "1001")
                    {
                        Byte_L = 9;
                    }
                    else if (MSB == "0101")
                    {
                        Byte_L = 10;
                    }
                    else if (MSB == "1101")
                    {
                        Byte_L = 11;
                    }
                    else if (MSB == "0011")
                    {
                        Byte_L = 12;
                    }
                    else if (MSB == "1011")
                    {
                        Byte_L = 13;
                    }
                    else if (MSB == "0111")
                    {
                        Byte_L = 14;
                    }
                    else if (MSB == "1111")
                    {
                        Byte_L = 15;
                    }
                    MangNgoVao[1] = Convert.ToByte(Byte_H * 16 + Byte_L);
                }

                else if (Channel.Length == 2) //đọc theo byte
                {
                    In_Str = "0";
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    Thread.Sleep(1);
                    MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    Thread.Sleep(1);
                    MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[7] = Convert.ToByte(Driver.Read(In_151));


                    foreach (byte item in MangNgoVaoTam)
                    {
                        In_Str = In_Str + Convert.ToString(item);
                    }
                    MSB = In_Str.Substring(1, 4);
                    LSB = In_Str.Substring(5, 4);

                    //Byte cao
                    if (LSB == "0000")
                    {
                        Byte_H = 0;
                    }
                    else if (LSB == "1000")
                    {
                        Byte_H = 1;
                    }
                    else if (LSB == "0100")
                    {
                        Byte_H = 2;
                    }
                    else if (LSB == "1100")
                    {
                        Byte_H = 3;
                    }
                    else if (LSB == "0010")
                    {
                        Byte_H = 4;
                    }
                    else if (LSB == "1010")
                    {
                        Byte_H = 5;
                    }
                    else if (LSB == "0110")
                    {
                        Byte_H = 6;
                    }
                    else if (LSB == "1110")
                    {
                        Byte_H = 7;
                    }
                    else if (LSB == "0001")
                    {
                        Byte_H = 8;
                    }
                    else if (LSB == "1001")
                    {
                        Byte_H = 9;
                    }
                    else if (LSB == "0101")
                    {
                        Byte_H = 10;
                    }
                    else if (LSB == "1101")
                    {
                        Byte_H = 11;
                    }
                    else if (LSB == "0011")
                    {
                        Byte_H = 12;
                    }
                    else if (LSB == "1011")
                    {
                        Byte_H = 13;
                    }
                    else if (LSB == "0111")
                    {
                        Byte_H = 14;
                    }
                    else if (LSB == "1111")
                    {
                        Byte_H = 15;
                    }

                    //byte thap
                    if (MSB == "0000")
                    {
                        Byte_L = 0;
                    }
                    else if (MSB == "1000")
                    {
                        Byte_L = 1;
                    }
                    else if (MSB == "0100")
                    {
                        Byte_L = 2;
                    }
                    else if (MSB == "1100")
                    {
                        Byte_L = 3;
                    }
                    else if (MSB == "0010")
                    {
                        Byte_L = 4;
                    }
                    else if (MSB == "1010")
                    {
                        Byte_L = 5;
                    }
                    else if (MSB == "0110")
                    {
                        Byte_L = 6;
                    }
                    else if (MSB == "1110")
                    {
                        Byte_L = 7;
                    }
                    else if (MSB == "0001")
                    {
                        Byte_L = 8;
                    }
                    else if (MSB == "1001")
                    {
                        Byte_L = 9;
                    }
                    else if (MSB == "0101")
                    {
                        Byte_L = 10;
                    }
                    else if (MSB == "1101")
                    {
                        Byte_L = 11;
                    }
                    else if (MSB == "0011")
                    {
                        Byte_L = 12;
                    }
                    else if (MSB == "1011")
                    {
                        Byte_L = 13;
                    }
                    else if (MSB == "0111")
                    {
                        Byte_L = 14;
                    }
                    else if (MSB == "1111")
                    {
                        Byte_L = 15;
                    }
                    MangNgoVao[1] = Convert.ToByte(Byte_H * 16 + Byte_L);
                    Data = MangNgoVao[1];
                }
                return Data;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region I2
        private byte I2(string Channel)
        {
            try
            {
                Driver.Write(EnableV0, true);
                Driver.Write(EnableV1, true);
                Driver.Write(EnableV2, false);

                if (Channel.Length == 4)  //đọc theo bit
                {
                    In_Str = "0";
                    if (Channel.Substring(3, 1) == "0")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[0];
                    }

                    else if (Channel.Substring(3, 1) == "1")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[1];
                    }
                    else if (Channel.Substring(3, 1) == "2")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[2];
                    }
                    else if (Channel.Substring(3, 1) == "3")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[3];
                    }
                    else if (Channel.Substring(3, 1) == "4")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[4];
                    }
                    else if (Channel.Substring(3, 1) == "5")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, false);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[5];
                    }
                    else if (Channel.Substring(3, 1) == "6")
                    {
                        Driver.Write(Add10, false);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[6];
                    }
                    else if (Channel.Substring(3, 1) == "7")
                    {
                        Driver.Write(Add10, true);
                        Driver.Write(Add11, true);
                        Driver.Write(Add12, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[7] = Convert.ToByte(Driver.Read(In_151));
                        Data = MangNgoVaoTam[7];
                    }
                    foreach (byte item in MangNgoVaoTam)
                    {
                        In_Str = In_Str + Convert.ToString(item);
                    }
                    MSB = In_Str.Substring(1, 4);
                    LSB = In_Str.Substring(5, 4);

                    //Byte cao
                    if (LSB == "0000")
                    {
                        Byte_H = 0;
                    }
                    else if (LSB == "1000")
                    {
                        Byte_H = 1;
                    }
                    else if (LSB == "0100")
                    {
                        Byte_H = 2;
                    }
                    else if (LSB == "1100")
                    {
                        Byte_H = 3;
                    }
                    else if (LSB == "0010")
                    {
                        Byte_H = 4;
                    }
                    else if (LSB == "1010")
                    {
                        Byte_H = 5;
                    }
                    else if (LSB == "0110")
                    {
                        Byte_H = 6;
                    }
                    else if (LSB == "1110")
                    {
                        Byte_H = 7;
                    }
                    else if (LSB == "0001")
                    {
                        Byte_H = 8;
                    }
                    else if (LSB == "1001")
                    {
                        Byte_H = 9;
                    }
                    else if (LSB == "0101")
                    {
                        Byte_H = 10;
                    }
                    else if (LSB == "1101")
                    {
                        Byte_H = 11;
                    }
                    else if (LSB == "0011")
                    {
                        Byte_H = 12;
                    }
                    else if (LSB == "1011")
                    {
                        Byte_H = 13;
                    }
                    else if (LSB == "0111")
                    {
                        Byte_H = 14;
                    }
                    else if (LSB == "1111")
                    {
                        Byte_H = 15;
                    }

                    //byte thap
                    if (MSB == "0000")
                    {
                        Byte_L = 0;
                    }
                    else if (MSB == "1000")
                    {
                        Byte_L = 1;
                    }
                    else if (MSB == "0100")
                    {
                        Byte_L = 2;
                    }
                    else if (MSB == "1100")
                    {
                        Byte_L = 3;
                    }
                    else if (MSB == "0010")
                    {
                        Byte_L = 4;
                    }
                    else if (MSB == "1010")
                    {
                        Byte_L = 5;
                    }
                    else if (MSB == "0110")
                    {
                        Byte_L = 6;
                    }
                    else if (MSB == "1110")
                    {
                        Byte_L = 7;
                    }
                    else if (MSB == "0001")
                    {
                        Byte_L = 8;
                    }
                    else if (MSB == "1001")
                    {
                        Byte_L = 9;
                    }
                    else if (MSB == "0101")
                    {
                        Byte_L = 10;
                    }
                    else if (MSB == "1101")
                    {
                        Byte_L = 11;
                    }
                    else if (MSB == "0011")
                    {
                        Byte_L = 12;
                    }
                    else if (MSB == "1011")
                    {
                        Byte_L = 13;
                    }
                    else if (MSB == "0111")
                    {
                        Byte_L = 14;
                    }
                    else if (MSB == "1111")
                    {
                        Byte_L = 15;
                    }
                    MangNgoVao[2] = Convert.ToByte(Byte_H * 16 + Byte_L);
                }

                else if (Channel.Length == 2) //đọc theo byte
                {
                    In_Str = "0";
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    Thread.Sleep(1);
                    MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    Thread.Sleep(1);
                    MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_151));

                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[7] = Convert.ToByte(Driver.Read(In_151));


                    foreach (byte item in MangNgoVaoTam)
                    {
                        In_Str = In_Str + Convert.ToString(item);
                    }
                    MSB = In_Str.Substring(1, 4);
                    LSB = In_Str.Substring(5, 4);

                    //Byte cao
                    if (LSB == "0000")
                    {
                        Byte_H = 0;
                    }
                    else if (LSB == "1000")
                    {
                        Byte_H = 1;
                    }
                    else if (LSB == "0100")
                    {
                        Byte_H = 2;
                    }
                    else if (LSB == "1100")
                    {
                        Byte_H = 3;
                    }
                    else if (LSB == "0010")
                    {
                        Byte_H = 4;
                    }
                    else if (LSB == "1010")
                    {
                        Byte_H = 5;
                    }
                    else if (LSB == "0110")
                    {
                        Byte_H = 6;
                    }
                    else if (LSB == "1110")
                    {
                        Byte_H = 7;
                    }
                    else if (LSB == "0001")
                    {
                        Byte_H = 8;
                    }
                    else if (LSB == "1001")
                    {
                        Byte_H = 9;
                    }
                    else if (LSB == "0101")
                    {
                        Byte_H = 10;
                    }
                    else if (LSB == "1101")
                    {
                        Byte_H = 11;
                    }
                    else if (LSB == "0011")
                    {
                        Byte_H = 12;
                    }
                    else if (LSB == "1011")
                    {
                        Byte_H = 13;
                    }
                    else if (LSB == "0111")
                    {
                        Byte_H = 14;
                    }
                    else if (LSB == "1111")
                    {
                        Byte_H = 15;
                    }

                    //byte thap
                    if (MSB == "0000")
                    {
                        Byte_L = 0;
                    }
                    else if (MSB == "1000")
                    {
                        Byte_L = 1;
                    }
                    else if (MSB == "0100")
                    {
                        Byte_L = 2;
                    }
                    else if (MSB == "1100")
                    {
                        Byte_L = 3;
                    }
                    else if (MSB == "0010")
                    {
                        Byte_L = 4;
                    }
                    else if (MSB == "1010")
                    {
                        Byte_L = 5;
                    }
                    else if (MSB == "0110")
                    {
                        Byte_L = 6;
                    }
                    else if (MSB == "1110")
                    {
                        Byte_L = 7;
                    }
                    else if (MSB == "0001")
                    {
                        Byte_L = 8;
                    }
                    else if (MSB == "1001")
                    {
                        Byte_L = 9;
                    }
                    else if (MSB == "0101")
                    {
                        Byte_L = 10;
                    }
                    else if (MSB == "1101")
                    {
                        Byte_L = 11;
                    }
                    else if (MSB == "0011")
                    {
                        Byte_L = 12;
                    }
                    else if (MSB == "1011")
                    {
                        Byte_L = 13;
                    }
                    else if (MSB == "0111")
                    {
                        Byte_L = 14;
                    }
                    else if (MSB == "1111")
                    {
                        Byte_L = 15;
                    }
                    MangNgoVao[2] = Convert.ToByte(Byte_H * 16 + Byte_L);
                    Data = MangNgoVao[2];
                }
                return Data;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region I3
        private byte I3(string Channel)
        {
            try
            {
                Driver.Write(EnableV3, false);
                Driver.Write(EnableV4, true);

                if (Channel.Length == 4)
                {
                    In_Str = "0";
                    if (Channel.Substring(3, 1) == "0")
                    {
                        Driver.Write(Add20, false);
                        Driver.Write(Add21, false);
                        Driver.Write(Add22, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[0];
                    }

                    else if (Channel.Substring(3, 1) == "1")
                    {
                        Driver.Write(Add20, true);
                        Driver.Write(Add21, false);
                        Driver.Write(Add22, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[1];
                    }
                    else if (Channel.Substring(3, 1) == "2")
                    {
                        Driver.Write(Add20, false);
                        Driver.Write(Add21, true);
                        Driver.Write(Add22, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[2];
                    }
                    else if (Channel.Substring(3, 1) == "3")
                    {
                        Driver.Write(Add20, true);
                        Driver.Write(Add21, true);
                        Driver.Write(Add22, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[3];
                    }
                    else if (Channel.Substring(3, 1) == "4")
                    {
                        Driver.Write(Add20, false);
                        Driver.Write(Add21, false);
                        Driver.Write(Add22, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[4];
                    }
                    else if (Channel.Substring(3, 1) == "5")
                    {
                        Driver.Write(Add20, true);
                        Driver.Write(Add21, false);
                        Driver.Write(Add22, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[5];
                    }
                    else if (Channel.Substring(3, 1) == "6")
                    {
                        Driver.Write(Add20, false);
                        Driver.Write(Add21, true);
                        Driver.Write(Add22, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[6];
                    }
                    else if (Channel.Substring(3, 1) == "7")
                    {
                        Driver.Write(Add20, true);
                        Driver.Write(Add21, true);
                        Driver.Write(Add22, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[7] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[7];
                    }
                    foreach (byte item in MangNgoVaoTam)
                    {
                        In_Str = In_Str + Convert.ToString(item);
                    }
                    MSB = In_Str.Substring(1, 4);
                    LSB = In_Str.Substring(5, 4);

                    //Byte cao
                    if (LSB == "0000")
                    {
                        Byte_H = 0;
                    }
                    else if (LSB == "1000")
                    {
                        Byte_H = 1;
                    }
                    else if (LSB == "0100")
                    {
                        Byte_H = 2;
                    }
                    else if (LSB == "1100")
                    {
                        Byte_H = 3;
                    }
                    else if (LSB == "0010")
                    {
                        Byte_H = 4;
                    }
                    else if (LSB == "1010")
                    {
                        Byte_H = 5;
                    }
                    else if (LSB == "0110")
                    {
                        Byte_H = 6;
                    }
                    else if (LSB == "1110")
                    {
                        Byte_H = 7;
                    }
                    else if (LSB == "0001")
                    {
                        Byte_H = 8;
                    }
                    else if (LSB == "1001")
                    {
                        Byte_H = 9;
                    }
                    else if (LSB == "0101")
                    {
                        Byte_H = 10;
                    }
                    else if (LSB == "1101")
                    {
                        Byte_H = 11;
                    }
                    else if (LSB == "0011")
                    {
                        Byte_H = 12;
                    }
                    else if (LSB == "1011")
                    {
                        Byte_H = 13;
                    }
                    else if (LSB == "0111")
                    {
                        Byte_H = 14;
                    }
                    else if (LSB == "1111")
                    {
                        Byte_H = 15;
                    }

                    //byte thap
                    if (MSB == "0000")
                    {
                        Byte_L = 0;
                    }
                    else if (MSB == "1000")
                    {
                        Byte_L = 1;
                    }
                    else if (MSB == "0100")
                    {
                        Byte_L = 2;
                    }
                    else if (MSB == "1100")
                    {
                        Byte_L = 3;
                    }
                    else if (MSB == "0010")
                    {
                        Byte_L = 4;
                    }
                    else if (MSB == "1010")
                    {
                        Byte_L = 5;
                    }
                    else if (MSB == "0110")
                    {
                        Byte_L = 6;
                    }
                    else if (MSB == "1110")
                    {
                        Byte_L = 7;
                    }
                    else if (MSB == "0001")
                    {
                        Byte_L = 8;
                    }
                    else if (MSB == "1001")
                    {
                        Byte_L = 9;
                    }
                    else if (MSB == "0101")
                    {
                        Byte_L = 10;
                    }
                    else if (MSB == "1101")
                    {
                        Byte_L = 11;
                    }
                    else if (MSB == "0011")
                    {
                        Byte_L = 12;
                    }
                    else if (MSB == "1011")
                    {
                        Byte_L = 13;
                    }
                    else if (MSB == "0111")
                    {
                        Byte_L = 14;
                    }
                    else if (MSB == "1111")
                    {
                        Byte_L = 15;
                    }
                    MangNgoVao[3] = Convert.ToByte(Byte_H * 16 + Byte_L);
                }
                else if (Channel.Length == 2)
                {
                    In_Str = "0";
                    Driver.Write(Add20, false);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, false);
                    Thread.Sleep(1);
                    MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, true);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, false);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, true);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, false);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, true);
                    Thread.Sleep(1);
                    MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, true);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, false);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, true);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[7] = Convert.ToByte(Driver.Read(In_152));


                    foreach (byte item in MangNgoVaoTam)
                    {
                        In_Str = In_Str + Convert.ToString(item);
                    }
                    MSB = In_Str.Substring(1, 4);
                    LSB = In_Str.Substring(5, 4);

                    //Byte cao
                    if (LSB == "0000")
                    {
                        Byte_H = 0;
                    }
                    else if (LSB == "1000")
                    {
                        Byte_H = 1;
                    }
                    else if (LSB == "0100")
                    {
                        Byte_H = 2;
                    }
                    else if (LSB == "1100")
                    {
                        Byte_H = 3;
                    }
                    else if (LSB == "0010")
                    {
                        Byte_H = 4;
                    }
                    else if (LSB == "1010")
                    {
                        Byte_H = 5;
                    }
                    else if (LSB == "0110")
                    {
                        Byte_H = 6;
                    }
                    else if (LSB == "1110")
                    {
                        Byte_H = 7;
                    }
                    else if (LSB == "0001")
                    {
                        Byte_H = 8;
                    }
                    else if (LSB == "1001")
                    {
                        Byte_H = 9;
                    }
                    else if (LSB == "0101")
                    {
                        Byte_H = 10;
                    }
                    else if (LSB == "1101")
                    {
                        Byte_H = 11;
                    }
                    else if (LSB == "0011")
                    {
                        Byte_H = 12;
                    }
                    else if (LSB == "1011")
                    {
                        Byte_H = 13;
                    }
                    else if (LSB == "0111")
                    {
                        Byte_H = 14;
                    }
                    else if (LSB == "1111")
                    {
                        Byte_H = 15;
                    }

                    //byte thap
                    if (MSB == "0000")
                    {
                        Byte_L = 0;
                    }
                    else if (MSB == "1000")
                    {
                        Byte_L = 1;
                    }
                    else if (MSB == "0100")
                    {
                        Byte_L = 2;
                    }
                    else if (MSB == "1100")
                    {
                        Byte_L = 3;
                    }
                    else if (MSB == "0010")
                    {
                        Byte_L = 4;
                    }
                    else if (MSB == "1010")
                    {
                        Byte_L = 5;
                    }
                    else if (MSB == "0110")
                    {
                        Byte_L = 6;
                    }
                    else if (MSB == "1110")
                    {
                        Byte_L = 7;
                    }
                    else if (MSB == "0001")
                    {
                        Byte_L = 8;
                    }
                    else if (MSB == "1001")
                    {
                        Byte_L = 9;
                    }
                    else if (MSB == "0101")
                    {
                        Byte_L = 10;
                    }
                    else if (MSB == "1101")
                    {
                        Byte_L = 11;
                    }
                    else if (MSB == "0011")
                    {
                        Byte_L = 12;
                    }
                    else if (MSB == "1011")
                    {
                        Byte_L = 13;
                    }
                    else if (MSB == "0111")
                    {
                        Byte_L = 14;
                    }
                    else if (MSB == "1111")
                    {
                        Byte_L = 15;
                    }
                    MangNgoVao[3] = Convert.ToByte(Byte_H * 16 + Byte_L);
                    Data = MangNgoVao[3];
                }
                return Data;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region I4
        private byte I4(string Channel)
        {
            try
            {
                Driver.Write(EnableV3, true);
                Driver.Write(EnableV4, false);

                if (Channel.Length == 4)
                {
                    In_Str = "0";
                    if (Channel.Substring(3, 1) == "0")
                    {
                        Driver.Write(Add20, false);
                        Driver.Write(Add21, false);
                        Driver.Write(Add22, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[0];
                    }

                    else if (Channel.Substring(3, 1) == "1")
                    {
                        Driver.Write(Add20, true);
                        Driver.Write(Add21, false);
                        Driver.Write(Add22, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[1];
                    }
                    else if (Channel.Substring(3, 1) == "2")
                    {
                        Driver.Write(Add20, false);
                        Driver.Write(Add21, true);
                        Driver.Write(Add22, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[2];
                    }
                    else if (Channel.Substring(3, 1) == "3")
                    {
                        Driver.Write(Add20, true);
                        Driver.Write(Add21, true);
                        Driver.Write(Add22, false);
                        Thread.Sleep(1);
                        MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[3];
                    }
                    else if (Channel.Substring(3, 1) == "4")
                    {
                        Driver.Write(Add20, false);
                        Driver.Write(Add21, false);
                        Driver.Write(Add22, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[4];
                    }
                    else if (Channel.Substring(3, 1) == "5")
                    {
                        Driver.Write(Add20, true);
                        Driver.Write(Add21, false);
                        Driver.Write(Add22, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[5];
                    }
                    else if (Channel.Substring(3, 1) == "6")
                    {
                        Driver.Write(Add20, false);
                        Driver.Write(Add21, true);
                        Driver.Write(Add22, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[6];
                    }
                    else if (Channel.Substring(3, 1) == "7")
                    {
                        Driver.Write(Add20, true);
                        Driver.Write(Add21, true);
                        Driver.Write(Add22, true);
                        Thread.Sleep(1);
                        MangNgoVaoTam[7] = Convert.ToByte(Driver.Read(In_152));
                        Data = MangNgoVaoTam[7];
                    }
                    foreach (byte item in MangNgoVaoTam)
                    {
                        In_Str = In_Str + Convert.ToString(item);
                    }
                    MSB = In_Str.Substring(1, 4);
                    LSB = In_Str.Substring(5, 4);

                    //Byte cao
                    if (LSB == "0000")
                    {
                        Byte_H = 0;
                    }
                    else if (LSB == "1000")
                    {
                        Byte_H = 1;
                    }
                    else if (LSB == "0100")
                    {
                        Byte_H = 2;
                    }
                    else if (LSB == "1100")
                    {
                        Byte_H = 3;
                    }
                    else if (LSB == "0010")
                    {
                        Byte_H = 4;
                    }
                    else if (LSB == "1010")
                    {
                        Byte_H = 5;
                    }
                    else if (LSB == "0110")
                    {
                        Byte_H = 6;
                    }
                    else if (LSB == "1110")
                    {
                        Byte_H = 7;
                    }
                    else if (LSB == "0001")
                    {
                        Byte_H = 8;
                    }
                    else if (LSB == "1001")
                    {
                        Byte_H = 9;
                    }
                    else if (LSB == "0101")
                    {
                        Byte_H = 10;
                    }
                    else if (LSB == "1101")
                    {
                        Byte_H = 11;
                    }
                    else if (LSB == "0011")
                    {
                        Byte_H = 12;
                    }
                    else if (LSB == "1011")
                    {
                        Byte_H = 13;
                    }
                    else if (LSB == "0111")
                    {
                        Byte_H = 14;
                    }
                    else if (LSB == "1111")
                    {
                        Byte_H = 15;
                    }

                    //byte thap
                    if (MSB == "0000")
                    {
                        Byte_L = 0;
                    }
                    else if (MSB == "1000")
                    {
                        Byte_L = 1;
                    }
                    else if (MSB == "0100")
                    {
                        Byte_L = 2;
                    }
                    else if (MSB == "1100")
                    {
                        Byte_L = 3;
                    }
                    else if (MSB == "0010")
                    {
                        Byte_L = 4;
                    }
                    else if (MSB == "1010")
                    {
                        Byte_L = 5;
                    }
                    else if (MSB == "0110")
                    {
                        Byte_L = 6;
                    }
                    else if (MSB == "1110")
                    {
                        Byte_L = 7;
                    }
                    else if (MSB == "0001")
                    {
                        Byte_L = 8;
                    }
                    else if (MSB == "1001")
                    {
                        Byte_L = 9;
                    }
                    else if (MSB == "0101")
                    {
                        Byte_L = 10;
                    }
                    else if (MSB == "1101")
                    {
                        Byte_L = 11;
                    }
                    else if (MSB == "0011")
                    {
                        Byte_L = 12;
                    }
                    else if (MSB == "1011")
                    {
                        Byte_L = 13;
                    }
                    else if (MSB == "0111")
                    {
                        Byte_L = 14;
                    }
                    else if (MSB == "1111")
                    {
                        Byte_L = 15;
                    }
                    MangNgoVao[4] = Convert.ToByte(Byte_H * 16 + Byte_L);
                }
                else if (Channel.Length == 2)
                {
                    In_Str = "0";
                    Driver.Write(Add20, false);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, false);
                    Thread.Sleep(1);
                    MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, true);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, false);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, true);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, false);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, true);
                    Thread.Sleep(1);
                    MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, true);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, false);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_152));

                    Driver.Write(Add20, true);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[7] = Convert.ToByte(Driver.Read(In_152));


                    foreach (byte item in MangNgoVaoTam)
                    {
                        In_Str = In_Str + Convert.ToString(item);
                    }
                    MSB = In_Str.Substring(1, 4);
                    LSB = In_Str.Substring(5, 4);

                    //Byte cao
                    if (LSB == "0000")
                    {
                        Byte_H = 0;
                    }
                    else if (LSB == "1000")
                    {
                        Byte_H = 1;
                    }
                    else if (LSB == "0100")
                    {
                        Byte_H = 2;
                    }
                    else if (LSB == "1100")
                    {
                        Byte_H = 3;
                    }
                    else if (LSB == "0010")
                    {
                        Byte_H = 4;
                    }
                    else if (LSB == "1010")
                    {
                        Byte_H = 5;
                    }
                    else if (LSB == "0110")
                    {
                        Byte_H = 6;
                    }
                    else if (LSB == "1110")
                    {
                        Byte_H = 7;
                    }
                    else if (LSB == "0001")
                    {
                        Byte_H = 8;
                    }
                    else if (LSB == "1001")
                    {
                        Byte_H = 9;
                    }
                    else if (LSB == "0101")
                    {
                        Byte_H = 10;
                    }
                    else if (LSB == "1101")
                    {
                        Byte_H = 11;
                    }
                    else if (LSB == "0011")
                    {
                        Byte_H = 12;
                    }
                    else if (LSB == "1011")
                    {
                        Byte_H = 13;
                    }
                    else if (LSB == "0111")
                    {
                        Byte_H = 14;
                    }
                    else if (LSB == "1111")
                    {
                        Byte_H = 15;
                    }

                    //byte thap
                    if (MSB == "0000")
                    {
                        Byte_L = 0;
                    }
                    else if (MSB == "1000")
                    {
                        Byte_L = 1;
                    }
                    else if (MSB == "0100")
                    {
                        Byte_L = 2;
                    }
                    else if (MSB == "1100")
                    {
                        Byte_L = 3;
                    }
                    else if (MSB == "0010")
                    {
                        Byte_L = 4;
                    }
                    else if (MSB == "1010")
                    {
                        Byte_L = 5;
                    }
                    else if (MSB == "0110")
                    {
                        Byte_L = 6;
                    }
                    else if (MSB == "1110")
                    {
                        Byte_L = 7;
                    }
                    else if (MSB == "0001")
                    {
                        Byte_L = 8;
                    }
                    else if (MSB == "1001")
                    {
                        Byte_L = 9;
                    }
                    else if (MSB == "0101")
                    {
                        Byte_L = 10;
                    }
                    else if (MSB == "1101")
                    {
                        Byte_L = 11;
                    }
                    else if (MSB == "0011")
                    {
                        Byte_L = 12;
                    }
                    else if (MSB == "1011")
                    {
                        Byte_L = 13;
                    }
                    else if (MSB == "0111")
                    {
                        Byte_L = 14;
                    }
                    else if (MSB == "1111")
                    {
                        Byte_L = 15;
                    }
                    MangNgoVao[4] = Convert.ToByte(Byte_H * 16 + Byte_L);
                    Data = MangNgoVao[4];
                }
                return Data;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public Ngo_Vao()
        {
            Driver.Allocate(Add10, PinDirection.Input);
            Driver.Allocate(Add11, PinDirection.Input);
            Driver.Allocate(Add12, PinDirection.Input);
            Driver.Allocate(EnableV0, PinDirection.Input);
            Driver.Allocate(EnableV1, PinDirection.Input);
            Driver.Allocate(EnableV2, PinDirection.Input);

            Driver.Allocate(Add20, PinDirection.Input);
            Driver.Allocate(Add21, PinDirection.Input);
            Driver.Allocate(Add22, PinDirection.Input);
            Driver.Allocate(EnableV3, PinDirection.Input);
            Driver.Allocate(EnableV4, PinDirection.Input);

            Driver.Allocate(In_152, PinDirection.Input);
            Driver.Allocate(In_151, PinDirection.Input);

            Driver.Allocate(Add10, PinDirection.Output);
            Driver.Allocate(Add11, PinDirection.Output);
            Driver.Allocate(Add12, PinDirection.Output);
            Driver.Allocate(EnableV0, PinDirection.Output);
            Driver.Allocate(EnableV1, PinDirection.Output);
            Driver.Allocate(EnableV2, PinDirection.Output);

            Driver.Allocate(Add20, PinDirection.Output);
            Driver.Allocate(Add21, PinDirection.Output);
            Driver.Allocate(Add22, PinDirection.Output);
            Driver.Allocate(EnableV3, PinDirection.Output);
            Driver.Allocate(EnableV4, PinDirection.Output);

            //Driver.Write(Add10, false);
            //Driver.Write(Add11, false);
            //Driver.Write(Add12, false);

            //Driver.Write(Add20, false);
            //Driver.Write(Add21, false);
            //Driver.Write(Add22, false);

            Driver.Write(EnableV0, true);
            Driver.Write(EnableV1, true);
            Driver.Write(EnableV2, true);
            Driver.Write(EnableV3, true);
            Driver.Write(EnableV4, true);
            Thread.Sleep(1);

            //Doc trang thái cũ
            try
            {
                string _s = "";

                _s = File.ReadAllText(File_Path);
                string[] _array = _s.Split('|');

                for (int i = 0; i < MangNgoVao.Length; i++)
                {
                    MangNgoVao[i] = Convert.ToByte(_array[i]);
                }
            }
            catch (Exception ex) { }
        }

        public void Dispose()
        {
            try
            {
                string _s = "";

                //lưu trạng thái vào file
                _s = MangNgoVao[0].ToString();
                for (int i = 1; i < MangNgoVao.Length; i++)
                {
                    _s = _s + "|" + MangNgoVao[i].ToString();
                }

                File.WriteAllText(File_Path, _s);
            }
            catch (Exception ex) { }

        }
    }
}
