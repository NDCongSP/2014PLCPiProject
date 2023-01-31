using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace PLCPiProject
{
    /// <summary>
    /// Đọc nhiệt độ từ cảm biến truyền thông chuẩn 1 dây (1 wire) DS18b20
    /// </summary>
    public class DS18B20
    {
        string Path_List = "/sys/bus/w1/devices/"; //đường dẫn đến thư mục chứa các cảm biến ds18b20 dc raspi nhận dạng
        string Path_Error = "/media/Error.txt"; // đường dẫn này dc sử dụng khi ko có cảm biến nào dc nhận dạng
        string Path_Temp; //đường dẫn sau cùng dùng để đọc file
        string Line; //đọc dữ liệu trong file text chứa giá trị nhiệt độ của ds18b20
        string KiemTra;//dung để kiểm tra xem cảm biến có được kết nối hay không
        string[] Line_Spl1; // Split từ chuỗi Line
        string[] Line_Spl2; // Split từ chuỗi Line_Spl1[20]
        double NhietDo = 0;//, NhietdoOld = 0;// chứa giá trị nhiệt độ
        string NhietDo_Return = "BAD"; //chứa giá trị nhiệt độ trả về kiểu string


        /// <summary>
        /// Đoc nhiệt độ từ cảm biến DS18B20.
        /// </summary>
        /// <param name="Id_DS18B20">ID của DS18B20.</param>
        /// <returns>Trả về giá trị nhiệt độ kiểu string. nếu trả về là "BAD" nghĩa là bị lỗi khi đọc cảm biến.</returns>
        public string DocNhietDo(string Id_DS18B20)
        {
            try
            {
                try
                {
                    Path_Temp = Path_List + Id_DS18B20 + "/w1_slave";
                    StreamReader file_T = new StreamReader(Path_Temp);
                    Line = file_T.ReadToEnd();
                    file_T.Dispose();
                    //Line = "b0 01 55 00 7f ff 0c 10 a6 : crc=a6 YES
                    //        b0 01 55 00 7f ff 0c 10 a6 t=27000"
                }
                catch
                {
                    Path_Temp = Path_Error;
                    StreamReader file_T = new StreamReader(Path_Temp);
                    Line = file_T.ReadToEnd();
                    file_T.Dispose();
                    //Line = "00 00 00 00 00 00 00 00 00 : crc=00 NOO"
                }
                Line_Spl1 = Line.Split(' ');
                KiemTra = Line_Spl1[11].Substring(0, 3);
                Console.WriteLine(KiemTra);
                if (KiemTra == "YES") //khi kết nối với cảm biến ok thì vào đọc giá trị nhiệt độ
                {
                    Line_Spl2 = Line_Spl1[20].Split('=');
                    //Console.WriteLine(Line_Spl2[1]);
                    NhietDo = Convert.ToDouble(Line_Spl2[1]) / 1000; //lấy giá trị đọc đc chia 1000 để có nhiệt độ thực tế
                    //if (NhietdoOld == 0)
                    //{
                    //    NhietdoOld = NhietDo;
                    //}
                    //if ((NhietDo - NhietdoOld > 4) || (NhietDo - NhietdoOld < -4))
                    //    NhietDo_Return = NhietdoOld.ToString();
                    //else
                    //{
                    //    NhietDo_Return = NhietDo.ToString();
                    //    NhietdoOld = NhietDo;
                    //}
                    if ((NhietDo != 85) && (NhietDo != -35) && NhietDo < 150 && NhietDo > -40)
                    {
                        NhietDo_Return = NhietDo.ToString();
                    }
                }
                else  //khi mất kết nối với cảm biến thì gửi vê "BAD"
                {
                    NhietDo_Return = "BAD";
                }
                return NhietDo_Return;
            }
            catch { return "BAD"; }
        }
    }
}
