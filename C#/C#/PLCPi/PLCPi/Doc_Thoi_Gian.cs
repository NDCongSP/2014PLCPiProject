using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCPiProject
{
    public class Doc_Thoi_Gian
    {
        Int16[] Time_Array = { 0, 0, 0, 0, 0, 0 }; // mảng chứa các giá trị thời gian đọc được
        string BienTam; // chuỗi chứa dữ liệu thời gian đọc về
        string[] Buffer_Array1, Buffer_Array2; // các mảng tạm để chuyển đổi chuổi thời gian đọc được sang mảng Int16 Time_Array
        string[] Buffer_Array, MangNgay, MangGio;// các mảng dùng để chuyển đổi định dạng ngày tháng
        string Ngay, Thang, Nam, Gio, Phut, Giay, ThoiGian; //chuỗi chứa thời gian sau khi chuyển đổi đinh dạng, để ghi xuống PLCPi
        System.Diagnostics.Process proc = new System.Diagnostics.Process();

        /// <summary>
        /// Đọc thời gian từ hệ thống. Trả về mảng 6 phần tử kiểu Int16
        /// Time_Array = {ngày, tháng, năm, giờ, phút, giây}
        /// </summary>
        /// <returns></returns>
        public Int16[] DocThoiGian()
        {
            BienTam = DateTime.Now.ToString("dd MM yyy HH:mm:ss");
            Buffer_Array1 = BienTam.Split(' ');
            Buffer_Array2 = Buffer_Array1[3].Split(':');

            Time_Array[0] = Convert.ToInt16(Buffer_Array1[0]);//ngay
            Time_Array[1] = Convert.ToInt16(Buffer_Array1[1]);//thang
            Time_Array[2] = Convert.ToInt16(Buffer_Array1[2]);//nam

            Time_Array[3] = Convert.ToInt16(Buffer_Array2[0]);//gio
            Time_Array[4] = Convert.ToInt16(Buffer_Array2[1]);//phut
            Time_Array[5] = Convert.ToInt16(Buffer_Array2[2]);//giay

            return Time_Array;
        }
        /// <summary>
        /// Method cài đặt thời gian cho hệ thống
        /// </summary>
        /// <param name="Time">thời gian muốn cài đặt, Có định dạng như sau: "dd-MM-yyyy HH:mm:ss" . Ví dụ: "25-03-2015 21:50:00"</param>
        public void CaiDat(string DateTime)
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
                /*proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;*/

                //var output = proc.StandardOutput.ReadToEnd();
                //Console.WriteLine("stdout: {0}", output);
            }
        }
        /// <summary>
        /// Method cài đặt thời gian cho Module thời gian thực (RTC_DS1307)
        /// </summary>
        public void CaiDatRTC()
        {
            proc.StartInfo.FileName = "sudo";
            proc.StartInfo.Arguments = "hwclock -w";
            proc.Start();
            proc.WaitForExit();
        }
    }
}
