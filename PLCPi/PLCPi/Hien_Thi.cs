using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PLCPiProject
{
    /// <summary>
    /// Đối tượng tương tác với module Display để hiển thị 2 kênh 7 đoạn
    /// </summary>
    public class Hien_Thi
    {
        byte[] HienThi_Array = { 0, 0, 0, 0, 0, 0, 0, 0 };// mang chứa các số hiển thị cho từng con led 7. HienThi_Array[0] dữ liệu của con led7 thứ 8
        Int32 Temp = 0, So = 0; //Temp chứa giá trị nhiệt độ hiển thị
        string Temp_Str; //chứa giá trị nhiệt độ dạng chuỗi
        Int16 DauCham1 = 3, DauCham2 = 3; //So và DauCham dùng để điều khiển dấu chấm động
        IC595_HienThi myIC595_HienThi = new IC595_HienThi();
        double Data1; //biến lưu giá trị chuyển đổi từ chuỗi SoCanHienThi(so thuc)
        Int32 Data2 = 0; //biến lưu giá trị chuyển đổi từ chuỗi SoCanHienThi(so nguyen)

        /// <summary>
        /// Gọi hiển thị ra module Display led 7 đoạn.
        /// </summary>
        /// <param name="SoCanHienThi">số cần hiển thị hoặc "Tat" để tắt/xóa hiển thị</param>
        /// <param name="KenhSo">kênh cần hiển thị. Chọn 1 cho kênh 1 (hàng trên) hoặc 2 cho kênh 2 (hàng dưới)</param>
        public void HienThi(string SoCanHienThi, Int16 KenhSo)
        {
            try
            {
                #region tắt hiển thị
                if (SoCanHienThi == "Tat") //tat hien thi
                {
                    if (KenhSo == 1)
                        TatKenh1();
                    else if (KenhSo == 2)
                        TatKenh2();
                }
                #endregion

                #region số thực
                else if ((SoCanHienThi != "BAD") && (SoCanHienThi != "Tat")&&(SoCanHienThi.Contains(".") == true))
                {
                    Data1 = Convert.ToDouble(SoCanHienThi);
                    if ((Data1 <= 999.9) && (Data1 >= -99.9))
                    {
                        if (KenhSo == 1)
                        {
                            Kenh1(Data1);
                        }
                        else if (KenhSo == 2)
                        {
                            Kenh2(Data1);
                        }
                    }
                    else
                    {
                        if (KenhSo == 1)
                            QuaGioiHan1();
                        else if (KenhSo == 2)
                            QuaGioiHan2();                        
                    }
                }
                #endregion

                #region so nguyen
                else if ((SoCanHienThi != "BAD") && (SoCanHienThi != "Tat") && (SoCanHienThi.Contains(".") == false))
                {
                    Data2 = Convert.ToInt32(SoCanHienThi);
                    if((Data2 <= 9999) && (Data2 >= -999))
                    {
                        if(KenhSo == 1)
                            Kenh1_Songuyen(Data2);
                        else if(KenhSo == 2)
                            Kenh2_Songuyen(Data2);
                    }
                    else
                    {
                        if (KenhSo == 1)
                            QuaGioiHan1();
                        else if (KenhSo == 2)
                            QuaGioiHan2();
                    }
                }
                #endregion

                #region khi đọc cảm biến ds18b20 và dht 21 bị lỗi
                else if (SoCanHienThi == "BAD")
                {
                    if (KenhSo == 1)
                        Ham_Loi1();
                    else if (KenhSo == 2)
                        Ham_Loi2();
                }
                #endregion
            }
            catch (Exception ex) { }//throw ex; }
        }

////////////////////////////////////////////////////////////////////////////////////
//cac chuong trinh con
////////////////////////////////////////////////////////////////////////////////////        
        #region hiển thị số thực 2 kênh
        /// <summary>
        /// hiển thị 1 kênh nhiệt độ
        /// </summary>
        /// <param name="Temp0">Giá trị nhiệt độ muốn hiển thị</param>
        private void Kenh1(double Temp0)
        {
            try
            {
                if (Temp0 > 0) //nhiệt độ dương
                {
                    Temp = Convert.ToInt32(Temp0 * 100);
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
                        Temp = Convert.ToInt32(Temp0 * (-100));
                        So = 21;
                        XuatLed1(Temp, So);
                    }
                    else if ((Temp0 > -1) && (Temp0 <= -0.1))
                    {
                        Temp = Convert.ToInt32(Temp0 * (-100));
                        So = 22;
                        XuatLed1(Temp, So);
                    }
                    else if ((Temp0 > -10) && (Temp0 <= 1))
                    {
                        Temp = Convert.ToInt32(Temp0 * (-100));
                        So = 23;
                        XuatLed1(Temp, So);
                    }
                    else if (Temp0 <= -10)
                    {
                        Temp = Convert.ToInt32(Temp0 * (-100));
                        So = 24;
                        XuatLed1(Temp, So);
                    }
                }
            }
            catch (Exception ex) { }
        }
        private void Kenh2(double Temp0)
        {
            try
            {
                if (Temp0 > 0) //nhiệt độ dương
                {
                    Temp = Convert.ToInt32(Temp0 * 100);
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
                        Temp = Convert.ToInt32(Temp0 * (-100));
                        So = 21;
                        XuatLed2(Temp, So);
                    }
                    else if ((Temp0 > -1) && (Temp0 <= -0.1))
                    {
                        Temp = Convert.ToInt32(Temp0 * (-100));
                        So = 22;
                        XuatLed2(Temp, So);
                    }
                    else if ((Temp0 > -10) && (Temp0 <= 1))
                    {
                        Temp = Convert.ToInt32(Temp0 * (-100));
                        So = 23;
                        XuatLed2(Temp, So);
                    }
                    else if (Temp0 <= -10)
                    {
                        Temp = Convert.ToInt32(Temp0 * (-100));
                        So = 24;
                        XuatLed2(Temp, So);
                    }
                }
            }
            catch (Exception ex) { }
        }
        #endregion

        #region hiển thị số nguyên 2 kênh
        private void Kenh1_Songuyen(Int32 Value)
        {
            if (Value >= 0)
            {
                HienThi_Array[7] = Convert.ToByte(Value / 1000);
                HienThi_Array[6] = Convert.ToByte((Value / 100) % 10);
                HienThi_Array[5] = Convert.ToByte((Value / 10) % 10);
                HienThi_Array[4] = Convert.ToByte(Value % 10);
                DauCham1 = 2;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else
            {
                HienThi_Array[7] = 11;
                HienThi_Array[6] = Convert.ToByte((Value * -1) / 100);
                HienThi_Array[5] = Convert.ToByte(((Value * -1) / 10) % 10);
                HienThi_Array[4] = Convert.ToByte((Value * -1) % 10);
                DauCham1 = 2;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
        }

        private void Kenh2_Songuyen(Int32 Value)
        {
            if (Value >= 0)
            {
                HienThi_Array[3] = Convert.ToByte(Value / 1000);
                HienThi_Array[2] = Convert.ToByte((Value / 100) % 10);
                HienThi_Array[1] = Convert.ToByte((Value / 10) % 10);
                HienThi_Array[0] = Convert.ToByte(Value % 10);
                DauCham2 = 2;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            else
            {
                HienThi_Array[3] = 11;
                HienThi_Array[2] = Convert.ToByte((Value * -1) / 100);
                HienThi_Array[1] = Convert.ToByte(((Value * -1) / 10) % 10);
                HienThi_Array[0] = Convert.ToByte((Value * -1) % 10);
                DauCham2 = 2;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
        }
        #endregion

        //tat hien thi
        private void TatKenh1()
        {
            try
            {
                HienThi_Array[7] = 14;
                HienThi_Array[6] = 14;
                HienThi_Array[5] = 14;
                HienThi_Array[4] = 14;
                DauCham1 = 3;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            catch { }
        }
        private void TatKenh2()
        {
            try
            {
                HienThi_Array[0] = 14;
                HienThi_Array[1] = 14;
                HienThi_Array[2] = 14;
                HienThi_Array[3] = 14;
                DauCham2 = 3;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// method hien thị nhiệt độ lên led7
        /// </summary>
        /// <param name="Data">nhiệt độ truyền vào</param>
        /// <param name="So">dùng để điều khiển dấu chấm động</param>
        private void XuatLed1(Int32 Data, Int32 So) 
        {
            try
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
            catch (Exception ex) { throw ex; }

        }
        private void XuatLed2(Int32 Data, Int32 So)
        {
            try
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
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// method hiển thị khi nhiet độ = 0
        /// </summary>
        private void Ham_01()
        {
            try
            {
                HienThi_Array[7] = 0;
                HienThi_Array[6] = 0;
                HienThi_Array[5] = 0;
                HienThi_Array[4] = 0;
                DauCham1 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            catch (Exception ex) { throw ex; }
        }
        private void Ham_02()
        {
            try
            {
                HienThi_Array[0] = 0;
                HienThi_Array[1] = 0;
                HienThi_Array[2] = 0;
                HienThi_Array[3] = 0;
                DauCham2 = 0;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// method hiển thị chữ Err khi cảm biến mất kết nối
        /// </summary>
        private void Ham_Loi1()
        {
            try
            {
                HienThi_Array[7] = 14;
                HienThi_Array[6] = 12;
                HienThi_Array[5] = 13;
                HienThi_Array[4] = 13;
                DauCham1 = 3;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            catch (Exception ex) { throw ex; }
        }
        private void Ham_Loi2()
        {
            try
            {
                HienThi_Array[3] = 14;
                HienThi_Array[2] = 12;
                HienThi_Array[1] = 13;
                HienThi_Array[0] = 13;
                DauCham2 = 3;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// hiển thị dấu "-" khi SoCanHienThi > 9999
        /// </summary>
        private void QuaGioiHan1()
        {
            try
            {
                HienThi_Array[7] = 11;
                HienThi_Array[6] = 11;
                HienThi_Array[5] = 11;
                HienThi_Array[4] = 11;
                DauCham1 = 3;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            catch (Exception ex) { throw ex; }
        }
        private void QuaGioiHan2()
        {
            try
            {
                HienThi_Array[3] = 11;
                HienThi_Array[2] = 11;
                HienThi_Array[1] = 11;
                HienThi_Array[0] = 11;
                DauCham2 = 3;
                myIC595_HienThi.DichDuLieu(HienThi_Array, DauCham1, DauCham2);
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
