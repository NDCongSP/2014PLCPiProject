using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace DHT21
{
    /// <summary>
    /// Đối tượng tương tác với cảm biến nhiệt độ độ ẩm DHT21
    /// </summary>
    public class SHT10
    {
        string Line, Path_Temp = "/media/SHT10.txt"; //đường dẫn đến file lưu giá trị nhiệt độ, độ ẩm
        string[] Data_Array = {"BAD", "BAD"}; // mang chứa giá trị nhiệt độ, độ ẩm đọc từ file text 'DHT21.txt'
        System.Diagnostics.Process proc = new System.Diagnostics.Process();//tạo đối tượng proc dùng để chạy lệnh trong linux( run terminal command)
        static bool _WorkingFlag = false;
        static double Nhietdo = 0, Nhietdo_Old = 0, Doam = 0, Doam_Old = 0;

        /// <summary>
        /// Đọc nhiệt độ từ cảm biến DHT21. Trả về dữ liệu kiểu string. 
        /// </summary>
        /// <returns>trả về giá trị nhiệt độ kiểu string. Nếu dữ liệu trả về là 'BAD' nghĩa là lõi kết nối với cảm biến.</returns>
        public string DocNhietDo()
        {
            try
            {
                if (_WorkingFlag == false)
                {
                    _WorkingFlag = true;

                    StreamReader file_T = new StreamReader(Path_Temp);
                    //chạy chương trình đọc DHT21 Sensor
                    Run_Python();
                    Line = file_T.ReadToEnd();
                    file_T.Dispose();
                    Data_Array = Line.Split(';');
                    if (Data_Array[0] != "BAD")
                    {
                        Nhietdo = Convert.ToDouble(Data_Array[0]);
                        if (Nhietdo_Old == 0)
                        {
                            Nhietdo_Old = Nhietdo;
                        }
                        if ((Nhietdo - Nhietdo_Old > 4) || (Nhietdo - Nhietdo_Old < -4))
                            Data_Array[0] = Nhietdo_Old.ToString();
                        else
                            Nhietdo_Old = Nhietdo;
                    }
                    _WorkingFlag = false;
                    //Console.WriteLine(Nhietdo_Old);
                }
                return Data_Array[0];
            }
            catch { _WorkingFlag = false; return "BAD"; }
        }
        /// <summary>
        /// Đọc độ ẩm từ cảm biến DHT21. Trả về dữ liệu kiểu string. 
        /// </summary>
        /// <returns>trả về giá trị độ ẩm kiểu string. Nếu dữ liệu trả về là 'BAD' nghĩa là lõi kết nối với cảm biến.</returns>
        public string DocDoAm()
        {
            try
            {
                if (_WorkingFlag == false)
                {
                    _WorkingFlag = true;
                    StreamReader file_T = new StreamReader(Path_Temp);
                    //chạy chương trình đọc DHT21 Sensor
                    Run_Python();
                    Line = file_T.ReadToEnd();
                    file_T.Dispose();
                    Data_Array = Line.Split(';');
                    if (Data_Array[1] != "BAD")
                    {
                        Doam = Convert.ToDouble(Data_Array[1]);
                        if (Doam_Old == 0)
                        {
                            Doam_Old = Doam;
                        }
                        if ((Doam - Doam_Old > 4) || (Doam - Doam_Old < -4))
                            Data_Array[1] = Doam_Old.ToString();
                        else
                            Doam_Old = Doam;
                    }
                    _WorkingFlag = false; 
                }
                return Data_Array[1];
            }
            catch { _WorkingFlag = false; return "BAD"; }
        }
        /// <summary>
        /// Đọc nhiệt độ và độ ẩm từ cảm biến DHT21. Trả về mảng dữ liệu kiểu string[].        
        /// </summary>
        /// <returns>trả về mảng giá trị nhiệt độ độ ẩm.Nếu dữ liệu trả về là {'BAD','BAD'} nghĩa là lỗi kết nối với cảm biến.</returns>
        public string[] DocNhietDoDoAm()
        {
            try
            {
                if (_WorkingFlag == false)
                {
                    _WorkingFlag = true;
 
                    StreamReader file_T = new StreamReader(Path_Temp);
                    //chạy chương trình đọc DHT21 Sensor
                    Run_Python();
                    Line = file_T.ReadToEnd();
                    file_T.Dispose();
                    Data_Array = Line.Split(';');
                    if ((Data_Array[0] != "BAD") && (Data_Array[1] != "BAD"))
                    {
                        //nhiet do
                        Nhietdo = Convert.ToDouble(Data_Array[0]);
                        Doam = Convert.ToDouble(Data_Array[1]);
                        if (Nhietdo_Old == 0)
                        {
                            Nhietdo_Old = Nhietdo;
                        }
                        if ((Nhietdo - Nhietdo_Old > 5) || (Nhietdo - Nhietdo_Old < -5))
                            Data_Array[0] = Nhietdo_Old.ToString();
                        else
                            Nhietdo_Old = Nhietdo;
                        //do am
                        if (Doam_Old == 0)
                        {
                            Doam_Old = Doam;
                        }
                        if ((Doam - Doam_Old > 10) || (Doam - Doam_Old < -10))
                            Data_Array[1] = Doam_Old.ToString();
                        else
                            Doam_Old = Doam;
                    }
                    _WorkingFlag = false; 
                }

                return Data_Array;
            }
            catch (Exception ex) { _WorkingFlag = false; return Data_Array; }
        }

        /// <summary>
        /// method chạy chương trình python đọc nhiệt độ độ ẩm từ DHT21
        /// </summary>
        private void Run_Python()
        {
            try
            {
                proc.StartInfo.FileName = "sudo";
                proc.StartInfo.Arguments = "python3 /media/sht10.py";
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
