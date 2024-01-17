using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Raspberry.IO.GeneralPurpose;

namespace PLCPiProject
{
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

        string MSB, LSB;//dùng để chuyển đổi về 1byte để trả về
        byte Byte_H, Byte_L;
        byte Data = 0, Data_Return =0; //chứa dữ liệu để trả về
        byte[] MangNgoVaoTam = { 0, 0, 0, 0, 0, 0, 0, 0 };// phan tu MangNgoVaoTam[0] la phần tử chứa giá trị đọc về của 
                                                            //bit thap nhat( "Ix.0"). Dùng để đọc các bít ngõ vào. 
        public byte[] MangNgoVao = { 0,0,0,0,0,0 };//Mảng lưu trạng thái của các ngõ vào theo từng byte. MangNgoVao[0] :ngõ vào I0

        /// <summary>
        /// Method đọc trạng thái của các ngõ vào. Có 2 cách đọc: theo Bit hoặc theo Byte. 
        /// Trả về dữ liệu kiểu Byte.. Nếu đọc theo Bit thì dữ liệu trả về bằng 0 hoặc 1
        /// </summary>
        /// <param name="Chanel">Ngõ vào muốn đọc. Đọc theo Bit: "Ix.x"; đọc theo Byte: "Ix". Ví dụ: theo bit("I0.0"); theo Byte("I0")</param>
        /// <returns></returns>
        public byte DocNgoVao(string Chanel)
        {
            if (Chanel.Substring(0, 2) == "I0")
            {
                Data_Return = I0(Chanel);
            }
            else if (Chanel.Substring(0, 2) == "I1")
            {
                Data_Return = I1(Chanel);
            }
            else if (Chanel.Substring(0, 2) == "I2")
            {
                Data_Return = I2(Chanel);
            }
            else if (Chanel.Substring(0, 2) == "I3")
            {
                Data_Return = I3(Chanel);
            }
            else if (Chanel.Substring(0, 2) == "I4")
            {
                Data_Return = I4(Chanel);
            }
            return Data_Return;
        }

        //các method dùng để đọc ngõ vào
        private byte I0(string Chanel)
        {
            Driver.Write(Add10, false);
            Driver.Write(Add11, false);
            Driver.Write(Add12, false);

            Driver.Write(EnableV0, false);
            Driver.Write(EnableV1, true);
            Driver.Write(EnableV2, true);
            if (Chanel.Length == 4)  //đọc theo bit
            {
                string In_Str = "0";
                if (Chanel.Substring(3,1) == "0")
                { 
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[0];
                }

                else if (Chanel.Substring(3, 1) == "1")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[1];
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[2];
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[3];
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[4];
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[5];
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[6];
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
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
                MangNgoVao[0] = Convert.ToByte(Byte_H * 16 + Byte_L);
            }

            else if (Chanel.Length == 2) //đọc theo byte
            {
                string In_Str = "0";
                Driver.Write(Add10, false);
                Driver.Write(Add11, false);
                Driver.Write(Add12, false);
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
                MangNgoVao[0] =  Convert.ToByte(Byte_H * 16 + Byte_L);
                Data = MangNgoVao[0];
            }
            return Data;
        }
        private byte I1(string Chanel)
        {
            Driver.Write(Add10, false);
            Driver.Write(Add11, false);
            Driver.Write(Add12, false);

            Driver.Write(EnableV0, true);
            Driver.Write(EnableV1, false);
            Driver.Write(EnableV2, true);

            if (Chanel.Length == 4)  //đọc theo bit
            {
                string In_Str = "0";
                if (Chanel.Substring(3, 1) == "0")
                {
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[0];
                }

                else if (Chanel.Substring(3, 1) == "1")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[1];
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[2];
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[3];
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[4];
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[5];
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[6];
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
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

            else if (Chanel.Length == 2) //đọc theo byte
            {
                string In_Str = "0";
                Driver.Write(Add10, false);
                Driver.Write(Add11, false);
                Driver.Write(Add12, false);
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
        private byte I2(string Chanel)
        {
            Driver.Write(Add10, false);
            Driver.Write(Add11, false);
            Driver.Write(Add12, false);

            Driver.Write(EnableV0, true);
            Driver.Write(EnableV1, true);
            Driver.Write(EnableV2, false);

            if (Chanel.Length == 4)  //đọc theo bit
            {
                string In_Str = "0";
                if (Chanel.Substring(3, 1) == "0")
                {
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[0];
                }

                else if (Chanel.Substring(3, 1) == "1")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[1];
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[2];
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, false);
                    MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[3];
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[4];
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, false);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[5];
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    Driver.Write(Add10, false);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
                    MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_151));
                    Data = MangNgoVaoTam[6];
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    Driver.Write(Add10, true);
                    Driver.Write(Add11, true);
                    Driver.Write(Add12, true);
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

            else if (Chanel.Length == 2) //đọc theo byte
            {
                string In_Str = "0";
                Driver.Write(Add10, false);
                Driver.Write(Add11, false);
                Driver.Write(Add12, false);
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
        private byte I3(string Chanel)
        {
            Driver.Write(Add20, false);
            Driver.Write(Add21, false);
            Driver.Write(Add22, false);

            Driver.Write(EnableV3, false);
            Driver.Write(EnableV4, true);

            if (Chanel.Length == 4)
            {
                string In_Str = "0";
                if (Chanel.Substring(3, 1) == "0")
                {
                    Driver.Write(Add20, false);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[0];
                }

                else if (Chanel.Substring(3, 1) == "1")
                {
                    Driver.Write(Add20, true);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[1];
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    Driver.Write(Add20, false);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[2];
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    Driver.Write(Add20, true);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[3];
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    Driver.Write(Add20, false);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[4];
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    Driver.Write(Add20, true);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[5];
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    Driver.Write(Add20, false);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[6];
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    Driver.Write(Add20, true);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, true);
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
            else if (Chanel.Length == 2)
            {
                string In_Str = "0";
                Driver.Write(Add20, false);
                Driver.Write(Add21, false);
                Driver.Write(Add22, false);
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
        private byte I4(string Chanel)
        {
            Driver.Write(Add20, false);
            Driver.Write(Add21, false);
            Driver.Write(Add22, false);

            Driver.Write(EnableV3, true);
            Driver.Write(EnableV4, false);

            if (Chanel.Length == 4)
            {
                string In_Str = "0";
                if (Chanel.Substring(3, 1) == "0")
                {
                    Driver.Write(Add20, false);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[0] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[0];
                }

                else if (Chanel.Substring(3, 1) == "1")
                {
                    Driver.Write(Add20, true);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[1] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[1];
                }
                else if (Chanel.Substring(3, 1) == "2")
                {
                    Driver.Write(Add20, false);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[2] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[2];
                }
                else if (Chanel.Substring(3, 1) == "3")
                {
                    Driver.Write(Add20, true);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, false);
                    MangNgoVaoTam[3] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[3];
                }
                else if (Chanel.Substring(3, 1) == "4")
                {
                    Driver.Write(Add20, false);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[4] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[4];
                }
                else if (Chanel.Substring(3, 1) == "5")
                {
                    Driver.Write(Add20, true);
                    Driver.Write(Add21, false);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[5] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[5];
                }
                else if (Chanel.Substring(3, 1) == "6")
                {
                    Driver.Write(Add20, false);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, true);
                    MangNgoVaoTam[6] = Convert.ToByte(Driver.Read(In_152));
                    Data = MangNgoVaoTam[6];
                }
                else if (Chanel.Substring(3, 1) == "7")
                {
                    Driver.Write(Add20, true);
                    Driver.Write(Add21, true);
                    Driver.Write(Add22, true);
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
            else if (Chanel.Length == 2)
            {
                string In_Str = "0";
                Driver.Write(Add20, false);
                Driver.Write(Add21, false);
                Driver.Write(Add22, false);
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

        }
    }
}
