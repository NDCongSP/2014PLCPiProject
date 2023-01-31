using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using Snap7;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace PLCPiProject
{
    /// <summary>
    /// Đối tượng S7 Ethernet TCPIP/ Profinet Client
    /// </summary>
    public class Snap7_Client
    {
        //S7Client MyClient = new S7Client(); 
        S7Client MyClient;
        string Status ="BAD";
        
        string IP_Server = null;
        /// <summary>
        /// Số lần thực hiện 1 lệnh đọc, mục đích để chống nhiễu trên đường truyền kém
        /// </summary>
        public byte SoLanDoc = 1;
        /// <summary>
        /// khởi động Snap7 Client. Trả về trạng thái kết nối kiểu string. 
        /// Nếu dữ liệu trả về = "GOOD": thành công. Nếu dữ liệu trả về khác "GOOD": thất bại
        /// </summary>
        /// <param name="ip">địa chỉ IP của Server "0.0.0.0"</param>
        /// <returns></returns>
        public string KetNoi(string ip)
        {
            try
            {
                MyClient = new S7Client();
                IP_Server = ip;
                Ping Ping_IPAdd = new Ping(); //tạo đối tượng để kiểm tra kết nối internet của server và client
                PingReply pingresult; //thuộc tính chứa các thông số khi ping. trong đó có thông số status(trạng thái kết nối internet)

                pingresult = Ping_IPAdd.Send(ip);

                if (pingresult.Status.ToString() == "Success")
                {                 
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
                }
                return Status;
            }
            catch { return "BAD"; }
        }
        /// <summary>
        /// Dừng Snap7 Client. Trả về trạng thái kết nối kiểu string. 
        /// Nếu dữ liệu trả về = "GOOD": thành công. Nếu dữ liệu trả về khác "GOOD": thất bại
        /// </summary>
        /// <returns></returns>
        public string NgatKetNoi()
        {
            try
            {
                Ping Ping_IPAdd = new Ping(); //tạo đối tượng để kiểm tra kết nối internet của server và client
                PingReply pingresult; //thuộc tính chứa các thông số khi ping. trong đó có thông số status(trạng thái kết nối internet)

                pingresult = Ping_IPAdd.Send(IP_Server);

                if (pingresult.Status.ToString() == "Success")
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
                }
                return Status;
            }
            catch { return "BAD"; }
        }
        /// <summary>
        /// Đọc vùng nhớ DB. Trả về đối tượng gồm: 1.MangGiaTri kiểu byte: chứa các giá trị đọc được từ vùng nhớ DB; 2.TrangThai kiểu string: để ta biết được việc 
        /// đọc vùng nhớ thành công hay thất bại, nếu TrangThai = "GOOD" thành công, TrangThai != "GOOD" thất bại
        /// </summary>
        /// <param name="DB_Num">vị trí của vùng nhớ DB mà ta muốn đọc, là một số kiểu Int. Bắt đầu từ 1,2,3,.... VD: truyền vào số 1 thì sẽ đọc vùng DB1</param>
        /// <param name="BatDau">Vị trí Byte bắt đầu đọc</param>
        /// <param name="SoByte">Số Byte muốn đọc</param>
        /// <returns></returns>
        public DocS7 DocDB(int DB_Num, int BatDau, int SoByte)
        {
            try
            {
                //Tao doi tuong nhan data doc ve
                DocS7 _myDocS7 = new DocS7();
                _myDocS7.MangGiaTri = new byte[SoByte];
                Ping Ping_IPAdd = new Ping(); //tạo đối tượng để kiểm tra kết nối internet của server và client
                PingReply pingresult; //thuộc tính chứa các thông số khi ping. trong đó có thông số status(trạng thái kết nối internet)

                pingresult = Ping_IPAdd.Send(IP_Server);
                if (pingresult.Status.ToString() == "Success")
                {
                    for (int b = 0; b < SoLanDoc; b++)
                    {
                        if (MyClient.Connected())
                        {
                            int error = MyClient.DBRead(DB_Num, BatDau, SoByte, _myDocS7.MangGiaTri);
                            if (error == 0)
                            {
                                _myDocS7.TrangThai = "GOOD";
                            }
                            else
                            {
                                _myDocS7.TrangThai = "BAD";
                                break;
                            }
                        }
                        else
                        {
                            _myDocS7.TrangThai = "BAD";
                            break;
                        }
                    }
                }
                else
                    _myDocS7.TrangThai = "BAD";
                //Thread.Sleep(300);
                return _myDocS7;
            }
            catch (Exception ex) { return null; }//luc nay neu doi tuong tra ve la null co nghia la roi vao catch
        }
        /// <summary>
        /// Đọc vùng nhớ AB(vùng nhớ ngõ ra). Trả về đối tượng gồm: 1.MangGiaTri kiểu byte: chứa các giá trị đọc được từ vùng nhớ AB; 2.TrangThai kiểu string: để ta biết được việc 
        /// đọc vùng nhớ thành công hay thất bại, nếu TrangThai = "GOOD" thành công, TrangThai != "GOOD" thất bại
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu đọc</param>
        /// <param name="SoByte">Số Byte muốn đọc</param>
        /// <returns></returns>
        public DocS7 DocQB(int BatDau, int SoByte)
        {
            try
            {
                DocS7 _myDocS7 = new DocS7();
                _myDocS7.MangGiaTri = new byte[SoByte];//mảng để lưu các giá trị đọc về từ vùng nhớ ngõ vào của server
                Ping Ping_IPAdd = new Ping(); //tạo đối tượng để kiểm tra kết nối internet của server và client
                PingReply pingresult; //thuộc tính chứa các thông số khi ping. trong đó có thông số status(trạng thái kết nối internet)

                pingresult = Ping_IPAdd.Send(IP_Server);
                if (pingresult.Status.ToString() == "Success")
                {
                    for (int b = 0; b < SoLanDoc; b++)
                    {
                        if (MyClient.Connected())
                        {
                            int error = MyClient.ABRead(BatDau, SoByte, _myDocS7.MangGiaTri);
                            if (error == 0)
                            {
                                _myDocS7.TrangThai = "GOOD";
                            }
                            else
                            {
                                _myDocS7.TrangThai = "BAD";
                                break;
                            }
                        }
                        else
                        {
                            _myDocS7.TrangThai = "BAD";
                            break;
                        }
                    }
                }
                else
                    _myDocS7.TrangThai = "BAD";
                //Thread.Sleep(300);
                return _myDocS7;
            }
            catch (Exception ex) { return null; }
        }
        /// <summary>
        /// Đọc vùng nhớ EB(vùng nhớ ngõ vào). Trả về đối tượng gồm: 1.MangGiaTri kiểu byte: chứa các giá trị đọc được từ vùng nhớ EB; 2.TrangThai kiểu string: để ta biết được việc 
        /// đọc vùng nhớ thành công hay thất bại, nếu TrangThai = "GOOD" thành công, TrangThai != "GOOD" thất bại
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu đọc</param>
        /// <param name="SoByte">Số Byte muốn đọc</param>
        /// <returns></returns>
        public DocS7 DocIB(int BatDau, int SoByte)
        {
            try
            {
                //Tao doi tuong nhan data doc ve
                DocS7 _myDocS7 = new DocS7();
                _myDocS7.MangGiaTri = new byte[SoByte];  //mảng để lưu các giá trị đọc về từ vùng nhớ ngõ ra của server
                Ping Ping_IPAdd = new Ping(); //tạo đối tượng để kiểm tra kết nối internet của server và client
                PingReply pingresult; //thuộc tính chứa các thông số khi ping. trong đó có thông số status(trạng thái kết nối internet)

                pingresult = Ping_IPAdd.Send(IP_Server);
                if (pingresult.Status.ToString() == "Success")
                {
                    for (int b = 0; b < SoLanDoc; b++)
                    {
                        if (MyClient.Connected())
                        {
                            int error = MyClient.EBRead(BatDau, SoByte, _myDocS7.MangGiaTri);
                            if (error == 0)
                            {
                                _myDocS7.TrangThai = "GOOD";
                            }
                            else
                            {
                                _myDocS7.TrangThai = "BAD";
                                break;
                            }
                        }
                        else
                        {
                            _myDocS7.TrangThai = "BAD";
                            break;
                        }
                    }
                }
                else
                    _myDocS7.TrangThai = "BAD";
                //Thread.Sleep(300);
                return _myDocS7;
            }
            catch (Exception ex) { return null; }
        }
        /// <summary>
        /// Đọc vùng nhớ MB. Trả về đối tượng gồm: 1.MangGiaTri kiểu byte: chứa các giá trị đọc được từ vùng nhớ MB; 2.TrangThai kiểu string: để ta biết được việc 
        /// đọc vùng nhớ thành công hay thất bại, nếu TrangThai = "GOOD" thành công, TrangThai != "GOOD" thất bại
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu đọc</param>
        /// <param name="SoByte">Số Byte muốn đọc</param>
        /// <returns></returns>
        public DocS7 DocMB(int BatDau, int SoByte)
        {
            try
            {                
                //Tao doi tuong nhan data doc ve
                DocS7 _myDocS7 = new DocS7();
                _myDocS7.MangGiaTri = new byte[SoByte]; //mảng để lưu các giá trị đọc về từ vùng nhớ MB của server
                Ping Ping_IPAdd = new Ping(); //tạo đối tượng để kiểm tra kết nối internet của server và client
                PingReply pingresult; //thuộc tính chứa các thông số khi ping. trong đó có thông số status(trạng thái kết nối internet)

                pingresult = Ping_IPAdd.Send(IP_Server);
                if (pingresult.Status.ToString() == "Success")
                {
                    for (int b = 0; b < SoLanDoc; b++)
                    {
                        if (MyClient.Connected())
                        {
                            int error = MyClient.MBRead(BatDau, SoByte, _myDocS7.MangGiaTri);
                            if (error == 0)
                            {
                                _myDocS7.TrangThai = "GOOD";
                            }
                            else
                            {
                                _myDocS7.TrangThai = "BAD";
                                break;
                            }
                        }
                        else
                        {
                            _myDocS7.TrangThai = "BAD";
                            break;
                        }
                    }
                }
                else
                    _myDocS7.TrangThai = "BAD";
                //Thread.Sleep(300);
                return _myDocS7;
            }
            catch (Exception ex) { return null; }
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
            try
            {
                Ping Ping_IPAdd = new Ping(); //tạo đối tượng để kiểm tra kết nối internet của server và client
                PingReply pingresult; //thuộc tính chứa các thông số khi ping. trong đó có thông số status(trạng thái kết nối internet)

                pingresult = Ping_IPAdd.Send(IP_Server);

                if (pingresult.Status.ToString() == "Success")
                {
                    for (int b = 0; b < SoLanDoc; b++)
                    {
                        if (MyClient.Connected())
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
                        }
                        else
                        {
                            Status = "BAD";
                            break;
                        }
                    }
                }
                else
                    Status = "BAD";
                //Thread.Sleep(300);
                return Status;
            }
            catch (Exception ex) { return "BAD"; }
        }
        /// <summary>
        /// Ghi dữ liệu xuống Ngõ ra
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu ghi</param>
        /// <param name="SoByte">Số Byte muốn ghi</param>
        /// <param name="Value">mảng chứa dữ liệu ghi xuống ngõ ra, kiểu Byte</param>
        public string GhiQB(int BatDau, int SoByte, byte[] Value)
        {
            try
            {
                Ping Ping_IPAdd = new Ping(); //tạo đối tượng để kiểm tra kết nối internet của server và client
                PingReply pingresult; //thuộc tính chứa các thông số khi ping. trong đó có thông số status(trạng thái kết nối internet)

                pingresult = Ping_IPAdd.Send(IP_Server);

                if (pingresult.Status.ToString() == "Success")
                {
                    for (int b = 0; b < SoLanDoc; b++)
                    {
                        if (MyClient.Connected())
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
                        }
                        else
                        {
                            Status = "BAD";
                            break;
                        }
                    }
                }
                else
                    Status = "BAD";
                //Thread.Sleep(300);
                return Status;
            }
            catch (Exception ex) { return "BAD"; }
        }
        /// <summary>
        /// Ghi dữ liệu xuống Ngõ vào
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu ghi</param>
        /// <param name="SoByte">Số Byte muốn ghi</param>
        /// <param name="Value">mảng chứ dữ liệu ghi xuống ngõ vào, kiểu Byte</param>
        public string GhiIB(int BatDau, int SoByte, byte[] Value)
        {
            try
            {
                Ping Ping_IPAdd = new Ping(); //tạo đối tượng để kiểm tra kết nối internet của server và client
                PingReply pingresult; //thuộc tính chứa các thông số khi ping. trong đó có thông số status(trạng thái kết nối internet)

                pingresult = Ping_IPAdd.Send(IP_Server);

                if (pingresult.Status.ToString() == "Success")
                {
                    for (int b = 0; b < SoLanDoc; b++)
                    {
                        if (MyClient.Connected())
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
                        }
                        else
                        {
                            Status = "BAD";
                            break;
                        }
                    }
                }
                else
                    Status = "BAD";
                //Thread.Sleep(300);
                return Status;
            }
            catch (Exception ex) { return "BAD"; }
        }
        /// <summary>
        /// Ghi dữ liệu xuống MB.
        /// </summary>
        /// <param name="BatDau">Vị trí Byte bắt đầu ghi.</param>
        /// <param name="SoByte">Số Byte muốn ghi.</param>
        /// <param name="Value">mảng chứa dữ liệu ghi xuống MB, kiểu Byte.</param>
        public string GhiMB(int BatDau, int SoByte, byte[] Value)
        {
            try
            {

                Ping Ping_IPAdd = new Ping(); //tạo đối tượng để kiểm tra kết nối internet của server và client
                PingReply pingresult; //thuộc tính chứa các thông số khi ping. trong đó có thông số status(trạng thái kết nối internet)

                pingresult = Ping_IPAdd.Send(IP_Server);

                if (pingresult.Status.ToString() == "Success")
                {
                    for (int b = 0; b < SoLanDoc; b++)
                    {
                        if (MyClient.Connected())
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
                        }
                        else
                        {
                            Status = "BAD";
                            break;
                        }
                    }
                }
                else
                    Status = "BAD";
                //Thread.Sleep(300);
                return Status;
            }
            catch (Exception ex) { return "BAD"; }
        }
    }

    /// <summary>
    /// Dinh nghia lop tra ve cua doc DB
    /// </summary>

    public class DocS7
    {
        public byte[] MangGiaTri { get; set; }
        public string TrangThai { get; set; }
    }
}
