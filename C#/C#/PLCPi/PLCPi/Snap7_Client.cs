using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;

namespace PLCPiProject
{
    public class Snap7_Client
    {
        S7Client MyClient;
        string Status;

        /// <summary>
        /// khởi động Snap7 Client. Trả về trạng thái kết nối kiểu string. 
        /// Nếu dữ liệu trả về = "GOOD": thành công. Nếu dữ liệu trả về khác "GOOD": thất bại
        /// </summary>
        /// <param name="ip">địa chỉ IP của Server "0.0.0.0"</param>
        /// <returns></returns>
        public string KetNoi(string ip)
        {
            MyClient = new S7Client();
            int error = MyClient.ConnectTo(ip, 0, 0);
            if (error == 0)
            {
                Console.WriteLine("Complete");
                Status = "GOOD";
            }
            else
            {
                Status = MyClient.ErrorText(error);
                Console.WriteLine(Status);
            }
            return Status;
        }
        /// <summary>
        /// Dừng Snap7 Client. Trả về trạng thái kết nối kiểu string. 
        /// Nếu dữ liệu trả về = "GOOD": thành công. Nếu dữ liệu trả về khác "GOOD": thất bại
        /// </summary>
        /// <returns></returns>
        public string NgatKetNoi()
        {
            int error = MyClient.Disconnect();
            if (error == 0)
            {
                Console.WriteLine("Complete");
                Status = "GOOD";
            }
            else
            {
                Status = MyClient.ErrorText(error);
                Console.WriteLine(Status);
            }
            return Status;
        }
        /// <summary>
        /// Đọc vùng nhớ DB. Trả về mảng kiểu Byte
        /// </summary>
        /// <param name="DB_Num">vị trí của vùng nhớ DB mà ta muốn đọc, là một số kiểu Int. Bắt đầu từ 1,2,3,.... VD: truyền vào số 1 thì sẽ đọc vùng DB1</param>
        /// <param name="BatDau">Vị trí Byte bắt đầu đọc</param>
        /// <param name="SoByte">Số Byte muốn đọc</param>
        /// <returns></returns>
        public byte[] DocDB(int DB_Num, int BatDau, int SoByte)
        {
            byte[] MyDB = new byte[SoByte];//mảng để lưu các giá trị đọc về từ vùng nhớ Data Block của server
            int error = MyClient.DBRead(DB_Num, BatDau, SoByte, MyDB);
            if (error == 0)
            {
                Console.WriteLine("Complete");
            }
            else
            {
                Console.WriteLine(MyClient.ErrorText(error));
            }
            return MyDB;
        }
        /// <summary>
        /// đọc vùng nhớ ngõ ra. Trả về mảng dữ liệu kiểu Byte
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu đọc</param>
        /// <param name="SoByte">Số Byte muốn đọc</param>
        /// <returns></returns>
        public byte[] DocNgoRa(int BatDau, int SoByte)
        {
            byte[] MyAB = new byte[SoByte];  //mảng để lưu các giá trị đọc về từ vùng nhớ ngõ vào của server
            int error = MyClient.ABRead(BatDau, SoByte, MyAB);
            if (error == 0)
            {
                Console.WriteLine("Complete");
            }
            else
            {
                Console.WriteLine(MyClient.ErrorText(error));
            }
            return MyAB;
        }
        /// <summary>
        /// đọc vùng nhớ Ngõ vào. Trả về mảng dữ liệu kiểu Byte
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu đọc</param>
        /// <param name="SoByte">Số Byte muốn đọc</param>
        /// <returns></returns>
        public byte[] DocNgoVao(int BatDau, int SoByte)
        {
            byte[] MyEB = new byte[SoByte]; //mảng để lưu các giá trị đọc về từ vùng nhớ ngõ ra của server
            int error = MyClient.EBRead(BatDau, SoByte, MyEB);
            if (error == 0)
            {
                Console.WriteLine("Complete");
            }
            else
            {
                Console.WriteLine(MyClient.ErrorText(error));
            }
            return MyEB;
        }
        /// <summary>
        /// đọc vùng nhớ MB. Trả về mảng dữ liệu kiểu Byte
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu đọc</param>
        /// <param name="SoByte">Số Byte muốn đọc</param>
        /// <returns></returns>
        public byte[] DocMB(int BatDau, int SoByte)
        {
            byte[] MyMB = new byte[SoByte]; //mảng để lưu các giá trị đọc về từ vùng nhớ MB của server
            int error = MyClient.MBRead(BatDau, SoByte, MyMB);
            if (error == 0)
            {
                Console.WriteLine("Complete");
            }
            else
            {
                Console.WriteLine(MyClient.ErrorText(error));
            }
            return MyMB;
        }
        /// <summary>
        /// Ghi dữ liệu xuống Data Block
        /// </summary>
        /// <param name="DB_Num">vị trí của vùng nhớ DB mà ta muốn ghi, là một số kiểu Int. Bắt đầu từ 1,2,3,.... VD: truyền vào số 1 thì sẽ ghi vùng DB1</param>
        /// <param name="BatDau">Vị trí Byte bắt đầu ghi</param>
        /// <param name="SoByte">Số Byte muốn ghi</param>
        /// <param name="Value">mảng chứ dữ liệu ghi xuống DB, kiểu Byte</param>
        public string GhiDB(int DB_Num, int BatDau, int SoByte, byte[] Value)
        {
            int error = MyClient.DBWrite(DB_Num, BatDau, SoByte, Value);
            if (error == 0)
            {
                Console.WriteLine("Complete");
                Status = "GOOD";
            }
            else
            {
                Status = MyClient.ErrorText(error);
                Console.WriteLine(Status);
            }
            return Status;
        }
        /// <summary>
        /// Ghi dữ liệu xuống Ngõ ra
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu ghi</param>
        /// <param name="SoByte">Số Byte muốn ghi</param>
        /// <param name="Value">mảng chứa dữ liệu ghi xuống ngõ ra, kiểu Byte</param>
        public string GhiNgoRa(int BatDau, int SoByte, byte[] Value)
        {
            int error = MyClient.ABWrite(BatDau, SoByte, Value);
            if (error == 0)
            {
                Console.WriteLine("Complete");
                Status = "GOOD";
            }
            else
            {
                Status = MyClient.ErrorText(error);
                Console.WriteLine(Status);
            }
            return Status;
        }
        /// <summary>
        /// Ghi dữ liệu xuống Ngõ vào
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu ghi</param>
        /// <param name="SoByte">Số Byte muốn ghi</param>
        /// <param name="Value">mảng chứ dữ liệu ghi xuống ngõ vào, kiểu Byte</param>
        public string GhiNgoVao(int BatDau, int SoByte, byte[] Value)
        {
            int error = MyClient.EBWrite(BatDau, SoByte, Value);
            if (error == 0)
            {
                Console.WriteLine("Complete");
                Status = "GOOD";
            }
            else
            {
                Status = MyClient.ErrorText(error);
                Console.WriteLine(Status);
            }
            return Status;
        }
        /// <summary>
        /// Ghi dữ liệu xuống MB
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu ghi</param>
        /// <param name="SoByte">Số Byte muốn ghi</param>
        /// <param name="Value">mảng chứ dữ liệu ghi xuống MB, kiểu Byte</param>
        public string GhiMB(int BatDau, int SoByte, byte[] Value)
        {
            int error = MyClient.MBWrite(BatDau, SoByte, Value);
            if (error == 0)
            {
                Console.WriteLine("Complete");
                Status = "GOOD";
            }
            else
            {
                Status = MyClient.ErrorText(error);
                Console.WriteLine(Status);
            }
            return Status;
        }
    }
}
