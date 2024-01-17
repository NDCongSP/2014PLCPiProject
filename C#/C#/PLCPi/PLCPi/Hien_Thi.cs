using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PLCPiProject
{
    public class Hien_Thi
    {
        byte[] HienThi_Array = { 0, 0, 0, 0, 0, 0, 0, 0 };// mang chứa các số hiển thị cho từng con led 7. HienThi_Array[0] dữ liệu của con led7 thứ 8
        Int32 Temp = 0, So = 0; //Temp chứa giá trị nhiệt độ hiển thị
        string Temp_Str; //chứa giá trị nhiệt độ dạng chuỗi
        Int16 DauCham1 = 3, DauCham2 = 3; //So và DauCham dùng để điều khiển dấu chấm động
        IC595_HienThi myIC595_HienThi = new IC595_HienThi();
        double Data; //biến lưu giá trị chuyển đổi từ chuỗi SoCanHienThi

        /// <summary>
        /// Method hiển thị led 7 đoạn. Khi ta muốn hiển thị kênh nào thì ta truyền vào đối số SoCanHienThi 1 giá trị kiểu string và số kênh tương ứng, 
        /// còn muốn tắt kênh nào thì ta truyền vào đối số SoCanHienThi chuỗi "Tat" và số kênh tương ứng.
        /// </summary>
        /// <param name="SoCanHienThi">giá trị cần hiển thị hoặc "Tăt"</param>
        /// <param name="KenhSo">kênh muốn hiển thị. 1 hoặc 2</param>
        public void HienThi(string SoCanHienThi, Int16 KenhSo)
        {
            if (SoCanHienThi == "Tat") //tat hien thi
            {
                if (KenhSo == 1)
                    TatKenh1();
                else if (KenhSo == 2)
                    TatKenh2();
            }
            else
            {
                Data = Convert.ToDouble(SoCanHienThi);
                if (Data > 9999)
                {
                    Console.WriteLine("SoCanHienThi vuot qua gioi han");
                    if (KenhSo == 1)
                        QuaGioiHan1();
                    else if (KenhSo == 2)
                        QuaGioiHan2();
                }
                else
                {
                    if (KenhSo == 1)
                    {
                        Kenh1(Data);
                    }
                    else if (KenhSo == 2)
                    {
                        Kenh2(Data);
                    }
                }
            }
        }
        /// <summary>
        /// hiển thị 1 kênh nhiệt độ
        /// </summary>
        /// <param name="Temp0">Giá trị nhiệt độ muốn hiển thị</param>
        private void Kenh1(double Temp0)
        {
            if ((Temp0 > 0) && (Temp0 != 1000)) //nhiệt độ dương
            {
                Temp = Convert.ToInt16(Temp0 * 100);
                Temp_Str = Convert.ToString(Temp);
                So = Temp_Str.Length;
                XuatLed1(Temp, So);
            }
            else if (Temp0 == 0) //nhiệt độ = 0
            {
                Ham_01();
            }
            else if (Temp0 < 0) // nhiệt độ âm
            {
                if ((Temp0 > -0.1) && (Temp0 < 0))
                {
                    Temp = Convert.ToInt16(Temp0 * (-100));
                    So = 21;
                    XuatLed1(Temp, So);
                }
                else if ((Temp0 > -1) && (Temp0 <= -0.1))
                {
                    Temp = Convert.ToInt16(Temp0 * (-100));
                    So = 22;
                    XuatLed1(Temp, So);
                }
                else if ((Temp0 > -10) && (Temp0 <= 1))
                {
                    Temp = Convert.ToInt16(Temp0 * (-100));
                    So = 23;
                    XuatLed1(Temp, So);
                }
                else if (Temp0 <= -10)
                {
                    Temp = Convert.ToInt16(Temp0 * (-100));
                    So = 24;
                    XuatLed1(Temp, So);
                }
            }
            else if (Temp0 == 1000)//hien thi Err khi mất kết nới với DS18B20
            {
                Ham_Loi1();
            }
        }

        private void Kenh2(double Temp0)
        {
            if ((Temp0 > 0) && (Temp0 != 1000)) //nhiệt độ dương
            {
                Temp = Convert.ToInt16(Temp0 * 100);
                Temp_Str = Convert.ToString(Temp);
                So = Temp_Str.Length;
                XuatLed2(Temp, So);
            }
            else if (Temp0 == 0) //nhiệt độ = 0
            {
                Ham_02();
            }
            else if (Temp0 < 0) // nhiệt độ âm
            {
                if ((Temp0 > -0.1) && (Temp0 < 0))
                {
                    Temp = Convert.ToInt16(Temp0 * (-100));
                    So = 21;
                    XuatLed2(Temp, So);
                }
                else if ((Temp0 > -1) && (Temp0 <= -0.1))
                {
                    Temp = Convert.ToInt16(Temp0 * (-100));
                    So = 22;
                    XuatLed2(Temp, So);
                }
                else if ((Temp0 > -10) && (Temp0 <= 1))
                {
                    Temp = Convert.ToInt16(Temp0 * (-100));
                    So = 23;
                    XuatLed2(Temp, So);
                }
                else if (Temp0 <= -10)
                {
                    Temp = Convert.ToInt16(Temp0 * (-100));
                    So = 24;
                    XuatLed2(Temp, So);
                }
            }
            else if (Temp0 == 1000)//hien thi Err khi mất kết nới với DS18B20
            {
                Ham_Loi2();
            }
        }

        private void TatKenh1()
        {
            HienThi_Array[7] = 14;
            HienThi_Array[6] = 14;
            HienThi_Array[5] = 14;
            HienThi_Array[4] = 14;
            DauCham1 = 3;
            myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
        }
        private void TatKenh2()
        {
            HienThi_Array[0] = 14;
            HienThi_Array[1] = 14;
            HienThi_Array[2] = 14;
            HienThi_Array[3] = 14;
            DauCham2 = 3;
            myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
        }

        /// <summary>
        /// method hien thị nhiệt độ lên led7
        /// </summary>
        /// <param name="Data">nhiệt độ truyền vào</param>
        /// <param name="So">dùng để điều khiển dấu chấm động</param>
        private void XuatLed1(Int32 Data, Int32 So) 
        {
            //hien thi nhiet do duong
            if (So == 1) //0<nhiet do < 0.1
            {
                HienThi_Array[7] = 0;
                HienThi_Array[6] = 0;
                HienThi_Array[5] = 0;
                HienThi_Array[4] = Convert.ToByte(Data);
                DauCham1 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 2)//0.1 <=nhiet do < 1
            {
                HienThi_Array[7] = 0;
                HienThi_Array[6] = 0;
                HienThi_Array[5] = Convert.ToByte(Data / 10);
                HienThi_Array[4] = Convert.ToByte(Data % 10);
                DauCham1 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 3)//1 <= nhiet do < 10
            {
                HienThi_Array[7] = 0;
                HienThi_Array[6] = Convert.ToByte(Data / 100);
                HienThi_Array[5] = Convert.ToByte((Data / 10) % 10);
                HienThi_Array[4] = Convert.ToByte(Data % 10);
                DauCham1 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 4)// 10 <= nhiet do < 100
            {
                HienThi_Array[7] = Convert.ToByte(Data / 1000);
                HienThi_Array[6] = Convert.ToByte((Data / 100) % 10);
                HienThi_Array[5] = Convert.ToByte((Data / 10) % 10);
                HienThi_Array[4] = Convert.ToByte(Data % 10);
                DauCham1 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 5) //Nhiet do >=100
            {
                HienThi_Array[7] = Convert.ToByte(Data / 10000);
                HienThi_Array[6] = Convert.ToByte((Data / 1000) % 10);
                HienThi_Array[5] = Convert.ToByte((Data / 100) % 10);
                HienThi_Array[4] = Convert.ToByte((Data / 10) % 10);
                DauCham1 = 1;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            //hien thi nhiet do am
            else if (So == 21) ////-0.1 < nhiet do < 0
            {
                HienThi_Array[7] = 11;
                HienThi_Array[6] = 0;
                HienThi_Array[5] = 0;
                HienThi_Array[4] = Convert.ToByte(Data);
                DauCham1 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 22)//-1 < nhiet do <= -0.1
            {
                HienThi_Array[7] = 11;
                HienThi_Array[6] = 0;
                HienThi_Array[5] = Convert.ToByte(Data / 10);
                HienThi_Array[4] = Convert.ToByte(Data % 10);
                DauCham1 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 23)//-10 < nhiet do <= -1
            {
                HienThi_Array[7] = 11;
                HienThi_Array[6] = Convert.ToByte(Data / 100);
                HienThi_Array[5] = Convert.ToByte((Data / 10) % 10);
                HienThi_Array[4] = Convert.ToByte(Data % 10);
                DauCham1 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 24)//nhiet do <= -10
            {
                HienThi_Array[7] = 11;
                HienThi_Array[6] = Convert.ToByte(Data / 1000);
                HienThi_Array[5] = Convert.ToByte((Data / 100) % 10);
                HienThi_Array[4] = Convert.ToByte((Data / 10) % 10);
                DauCham1 = 1;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }

        }
        private void XuatLed2(Int32 Data, Int32 So)
        {
            //hien thi nhiet do duong
            if (So == 1) //0<nhiet do < 0.1
            {
                HienThi_Array[3] = 0;
                HienThi_Array[2] = 0;
                HienThi_Array[1] = 0;
                HienThi_Array[0] = Convert.ToByte(Data);
                DauCham2 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 2)//0.1 <=nhiet do < 1
            {
                HienThi_Array[3] = 0;
                HienThi_Array[2] = 0;
                HienThi_Array[1] = Convert.ToByte(Data / 10);
                HienThi_Array[0] = Convert.ToByte(Data % 10);
                DauCham2 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 3)//1 <= nhiet do < 10
            {
                HienThi_Array[3] = 0;
                HienThi_Array[2] = Convert.ToByte(Data / 100);
                HienThi_Array[1] = Convert.ToByte((Data / 10) % 10);
                HienThi_Array[0] = Convert.ToByte(Data % 10);
                DauCham2 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 4)// 10 <= nhiet do < 100
            {
                HienThi_Array[3] = Convert.ToByte(Data / 1000);
                HienThi_Array[2] = Convert.ToByte((Data / 100) % 10);
                HienThi_Array[1] = Convert.ToByte((Data / 10) % 10);
                HienThi_Array[0] = Convert.ToByte(Data % 10);
                DauCham2 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 5) //Nhiet do >=100
            {
                HienThi_Array[3] = Convert.ToByte(Data / 10000);
                HienThi_Array[2] = Convert.ToByte((Data / 1000) % 10);
                HienThi_Array[1] = Convert.ToByte((Data / 100) % 10);
                HienThi_Array[0] = Convert.ToByte((Data / 10) % 10);
                DauCham2 = 1;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            //hien thi nhiet do am
            else if (So == 21) ////-0.1 < nhiet do < 0
            {
                HienThi_Array[3] = 11;
                HienThi_Array[2] = 0;
                HienThi_Array[1] = 0;
                HienThi_Array[0] = Convert.ToByte(Data);
                DauCham2 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 22)//-1 < nhiet do <= -0.1
            {
                HienThi_Array[3] = 11;
                HienThi_Array[2] = 0;
                HienThi_Array[1] = Convert.ToByte(Data / 10);
                HienThi_Array[0] = Convert.ToByte(Data % 10);
                DauCham2 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 23)//-10 < nhiet do <= -1
            {
                HienThi_Array[3] = 11;
                HienThi_Array[2] = Convert.ToByte(Data / 100);
                HienThi_Array[1] = Convert.ToByte((Data / 10) % 10);
                HienThi_Array[0] = Convert.ToByte(Data % 10);
                DauCham2 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else if (So == 24)//nhiet do <= -10
            {
                HienThi_Array[3] = 11;
                HienThi_Array[2] = Convert.ToByte(Data / 1000);
                HienThi_Array[1] = Convert.ToByte((Data / 100) % 10);
                HienThi_Array[0] = Convert.ToByte((Data / 10) % 10);
                DauCham2 = 1;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }

        }
        /// <summary>
        /// method hiển thị khi nhiet độ = 0
        /// </summary>
        private void Ham_01()
        {
            HienThi_Array[7] = 0;
            HienThi_Array[6] = 0;
            HienThi_Array[5] = 0;
            HienThi_Array[4] = 0;
            DauCham1 = 0;
            myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
        }
        private void Ham_02()
        {
            HienThi_Array[0] = 0;
            HienThi_Array[1] = 0;
            HienThi_Array[2] = 0;
            HienThi_Array[3] = 0;
            DauCham2 = 0;
            myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
        }
        /// <summary>
        /// method hiển thị chữ Err khi cảm biến mất kết nối
        /// </summary>
        private void Ham_Loi1()
        {
            HienThi_Array[7] = 14;
            HienThi_Array[6] = 12;
            HienThi_Array[5] = 13;
            HienThi_Array[4] = 13;
            DauCham1 = 3;
            myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
        }
        private void Ham_Loi2()
        {
            HienThi_Array[3] = 14;
            HienThi_Array[2] = 12;
            HienThi_Array[1] = 13;
            HienThi_Array[0] = 13;
            DauCham2 = 3;
            myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
        }
        /// <summary>
        /// hiển thị dấu "-" khi SoCanHienThi > 9999
        /// </summary>
        private void QuaGioiHan1()
        {
            HienThi_Array[7] = 11;
            HienThi_Array[6] = 11;
            HienThi_Array[5] = 11;
            HienThi_Array[4] = 11;
            DauCham1 = 3;
            myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
        }
        private void QuaGioiHan2()
        {
            HienThi_Array[3] = 11;
            HienThi_Array[2] = 11;
            HienThi_Array[1] = 11;
            HienThi_Array[0] = 11;
            DauCham2 = 3;
            myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
        }

    }
}
