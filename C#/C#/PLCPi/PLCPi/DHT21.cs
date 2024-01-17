using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PLCPiProject
{
    public class DHT21
    {
        string Line, Path_Temp = "/media/DHT21.txt"; //đường dẫn đến file lưu giá trị nhiệt độ, độ ẩm
        string[] Data_Array; // mang chứa giá trị nhiệt độ, độ ẩm đọc từ file text 'DHT21.txt'
        System.Diagnostics.Process proc = new System.Diagnostics.Process();//tạo đối tượng proc dùng để chạy lệnh trong linux( run terminal command)

        /// <summary>
        /// method đọc nhiệt độ từ cảm biến DHT21. Trả về dữ liệu kiểu string. 
        /// Nếu dữ liệu trả về là '1000' nghĩa là lõi kết nối với cảm biến
        /// </summary>
        /// <returns></returns>
        public string DocNhietDo()
        {
            StreamReader file_T = new StreamReader(Path_Temp);
            //chạy chương trình đọc DHT21 Sensor
            Run_Python();
            Line = file_T.ReadToEnd();
            file_T.Dispose();
            Data_Array = Line.Split(';');
            return Data_Array[0];
        }
        /// <summary>
        /// method đọc độ ẩm từ cảm biến DHT21. Trả về dữ liệu kiểu string. 
        /// Nếu dữ liệu trả về là '1000' nghĩa là lõi kết nối với cảm biến
        /// </summary>
        /// <returns></returns>
        public string DocDoAm()
        {
            StreamReader file_T = new StreamReader(Path_Temp);
            //chạy chương trình đọc DHT21 Sensor
            Run_Python();
            Line = file_T.ReadToEnd();
            file_T.Dispose();
            Data_Array = Line.Split(';');
            return Data_Array[1];
        }
        /// <summary>
        /// method đọc nhiệt từ cảm biến DHT21. Trả về mảng dữ liệu kiểu string[]. 
        /// Nếu dữ liệu trả về là {'1000','1000'} nghĩa là lõi kết nối với cảm biến
        /// </summary>
        /// <returns></returns>
        public string[] DocNhietDoDoAm()
        {
            StreamReader file_T = new StreamReader(Path_Temp);
            //chạy chương trình đọc DHT21 Sensor
            Run_Python();
            Line = file_T.ReadToEnd();
            file_T.Dispose();
            Data_Array = Line.Split(';');
            return Data_Array;
        }

        /// <summary>
        /// method chạy chương trình python đọc nhiệt độ độ ẩm từ DHT21
        /// </summary>
        private void Run_Python()
        {
            proc.StartInfo.FileName = "sudo";
            proc.StartInfo.Arguments = "python /media/dht21.py";
            proc.Start();
            proc.WaitForExit();
        }
    }
}
