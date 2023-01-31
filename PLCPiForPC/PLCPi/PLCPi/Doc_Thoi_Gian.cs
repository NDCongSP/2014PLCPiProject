using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCPiProject
{
    /// <summary>
    /// Đối tượng cho phép thao tác với thời gian thực của PLCPi
    /// </summary>
    public class Doc_Thoi_Gian
    {
        string[] Time_Array = { "BAD", "BAD", "BAD", "BAD", "BAD", "BAD" }; // mảng chứa các giá trị thời gian đọc được
        string BienTam; // chuỗi chứa dữ liệu thời gian đọc về
        string[] Buffer_Array1, Buffer_Array2; // các mảng tạm để chuyển đổi chuổi thời gian đọc được sang mảng Int16 Time_Array
        string[] Buffer_Array, MangNgay, MangGio;// các mảng dùng để chuyển đổi định dạng ngày tháng
        string Ngay, Thang, Nam, Gio, Phut, Giay, ThoiGian; //chuỗi chứa thời gian sau khi chuyển đổi đinh dạng, để ghi xuống PLCPi
        System.Diagnostics.Process proc = new System.Diagnostics.Process();

        /// <summary>
        /// Lấy thời gian thực từ hệ điều hành Raspbian. Trả về mảng 6 phần tử kiểu string
        /// Time_Array = {ngày, tháng, năm, giờ, phút, giây}
        /// </summary>
        /// <returns></returns>
        public string[] DocThoiGian()
        {
            try
            {
                BienTam = DateTime.Now.ToString("dd MM yyyy HH:mm:ss");
                Buffer_Array1 = BienTam.Split(' ');
                Buffer_Array2 = Buffer_Array1[3].Split(':');

                Time_Array[0] = Buffer_Array1[0];//ngay
                Time_Array[1] = Buffer_Array1[1];//thang
                Time_Array[2] = Buffer_Array1[2];//nam

                Time_Array[3] = Buffer_Array2[0];//gio
                Time_Array[4] = Buffer_Array2[1];//phut
                Time_Array[5] = Buffer_Array2[2];//giay

                return Time_Array;
            }
            catch (Exception ex) { return Time_Array; }
        }
        
        /// <summary>
        ///  Cài đặt lại thời gian cho hệ điều hành Raspbian.
        /// </summary>
        /// <param name="DateTime">thời gian muốn cài đặt, có định dạng: "dd-MM-yyyy HH:mm:ss" . Ví dụ: "25-03-2015 21:50:00"</param>
        public void CaiDat(string DateTime)
        {
            try
            {
                Buffer_Array = DateTime.Split(' ');
                MangNgay = Buffer_Array[0].Split('-');
                MangGio = Buffer_Array[1].Split(':');
                if ((MangNgay[0].Length != 2) || (MangNgay[1].Length != 2) || (MangNgay[2].Length != 4) ||
                    (MangGio[0].Length != 2) || (MangGio[1].Length != 2) || (MangGio[2].Length != 2))
                    Console.WriteLine("Sai dinh dang DateTime");
                else
                {
                    Ngay = MangNgay[0];
                    Nam = MangNgay[2];
                    Gio = MangGio[0];
                    Phut = MangGio[1];
                    Giay = MangGio[2];
                    if (MangNgay[1] == "01")
                        Thang = "JAN";
                    else if (MangNgay[1] == "02")
                        Thang = "FEB";
                    else if (MangNgay[1] == "03")
                        Thang = "MAR";
                    else if (MangNgay[1] == "04")
                        Thang = "APR";
                    else if (MangNgay[1] == "05")
                        Thang = "MAY";
                    else if (MangNgay[1] == "06")
                        Thang = "JUN";
                    else if (MangNgay[1] == "07")
                        Thang = "JUL";
                    else if (MangNgay[1] == "08")
                        Thang = "AUG";
                    else if (MangNgay[1] == "09")
                        Thang = "SEP";
                    else if (MangNgay[1] == "10")
                        Thang = "OCT";
                    else if (MangNgay[1] == "11")
                        Thang = "NOV";
                    else if (MangNgay[1] == "12")
                        Thang = "DEC";
                    ThoiGian = "\"" + Ngay + " " + Thang + " " + Nam + " " + Gio + ":" + Phut + ":" + Giay + "\"";

                    proc.StartInfo.FileName = "sudo";
                    proc.StartInfo.Arguments = "date -s" + ThoiGian;
                    proc.Start();
                    proc.WaitForExit();
                }
            }
            catch (Exception ex) { }//throw ex; }
        }
        /// <summary>
        /// Cài đặt thời gian cho IC nuôi thời gian DS 1307
        /// IC này giữ thời gian thực cho PLCPi khi PLCPi bị mất điện 
        /// và khi có điện lại hệ điều hành Raspbian của PLCPi sẽ tự động lấy thời gian thực từ Ds1307
        /// nếu PLCPi không kết nối internet.
        ///PLCPi sẽ lấy thời gian từ Network Timer Server nếu PLCPi có kết nối internet khi khởi động lên
        /// </summary>
        public void CaiDatRTC_DS1307()
        {
            try
            {
                proc.StartInfo.FileName = "sudo";
                proc.StartInfo.Arguments = "hwclock -w";
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex) {}// throw ex; }
        }
    }
}
