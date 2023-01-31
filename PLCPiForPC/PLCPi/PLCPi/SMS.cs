using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using MySql.Data.MySqlClient;

namespace PLCPiProject
{
    /// <summary>
    /// Đối tượng tương tác với GSM modem của USB 3G
    /// </summary>
    public class SMS
    {
        /// <summary>
        /// Cổng USB 3G
        /// </summary>
        public string Port_USB3G = "/dev/ttyUSB0"; //port GMS modem               
        static SerialPort mySMS;
        string Bussy_Flag = "0";//Cờ báo cổng serial đang bận(Bussy_Flag="1")

        double bd = 0, kt = 0, kq = 0;
        string chot_bd = "0";

        static string ChuoiKetnoiMySQL = "Server = localhost ; Uid =root ;Pwd= 100100;Database=DB_PLCPi";
        static MySqlConnection connectmysql;
        static string ttkn = "";
        static string chay_ct_resetusb3g = "0";


        public string Khoitao()
        {
            try
            {
                mySMS = new SerialPort(Port_USB3G, 115200, Parity.None, 8);
                mySMS.Open();
                mySMS.DtrEnable = true;
                mySMS.RtsEnable = true;
                mySMS.WriteLine("AT+CMGF=1" + "\r");
                return "Good";
            }
            catch
            {
                return "Bad";
            }
        }

        public string Ngatketnoi()
        {
            //System.Diagnostics.Process proc1 = new System.Diagnostics.Process();
            try
            {
                mySMS.Close();
                mySMS.Dispose();
                //proc1.StartInfo.FileName = "sudo";
                //proc1.StartInfo.Arguments = "pkill -f restart_usb3g.exe";
                //proc1.Start();
                //proc1.Close();
                //proc1.Dispose();
                //Thread.Sleep(1000); 
                return "Good";
            }
            catch
            {
                return "Bad";
                //proc1.Close();
                //proc1.Dispose();
            }
        }

        /// <summary>
        /// Method gửi SMS từ GSM modem của USB 3G. Trả về trạng thái gửi. Nếu chuỗi trả vê là "Good"  là thành công, "Bad" thất bại.
        /// </summary>
        /// <param name="SDT">Chuỗi chứa các số điện thoại cần gửi SMS. Mỗi số cách nhau bởi dấu','. VD: 0909123456,0983456789,01686666777, ...</param>
        /// <param name="NoiDung">Nội dung của SMS.</param>
        /// <returns></returns>
        public string GuiSMS(String SDT, String NoiDung)
        {
            if (Bussy_Flag == "0")
            {
                Bussy_Flag = "1";
                try
                {
                    //int DelayTimes = 5000;
                    string[] MangSDT = SDT.Split(',');
                    //if (NoiDung.Length > 100) DelayTimes = 10000;

                    for (int i = 0; i < MangSDT.Length; i++)
                    {
                        mySMS.WriteLine("AT+CMGS=\"" + MangSDT[i].Replace(" ", "") + "\"" + "\r");//"\r" ký tự Enter
                        Thread.Sleep(200);
                        mySMS.WriteLine(NoiDung + "\x1a");// "\x1a" ký tự của tổ hợp phím Ctrl+z
                        Thread.Sleep(15000);//neu noi dung tin nhan cang dai thi phai tang delay len
                    }
                    Bussy_Flag = "0";
                    return "Good";
                }
                catch
                {
                    Bussy_Flag = "0";
                    return "Bad";
                }
            }
            else return "ban";
        }

        /// <summary>
        /// Đọc SMS.Trả về mảng string[] chứa nội dung của các tin nhắn. Nếu mảng trả về !=null và phần tử đầu tiên của mảng !="Rong",.
        /// nghĩa là có SMS đọc về. Nếu mảng trả về !=null và phần tử đầu tiên của mảng =="Rong", nghĩa là không có SMS đọc về.
        ///  Nếu mảng trả về ==null  nghĩa là bị Lỗi.
        /// </summary>
        /// <returns>Trả về mảng string[] chứa nội dung của các tin nhắn. Nếu mảng trả về = null nghĩa là bị lỗi.</returns>

        public string[] DocSMS()
        {
            ttkn = CapnhatDulieu("1", "chot");
            if (ttkn == "Bad") KetnoiMySQL();
            if (Bussy_Flag == "0")
            {
                Bussy_Flag = "1";
                string data1 = string.Empty;
                string[] Noidung_SMS = null;
                try
                {
                    data1 = ATCommand("AT+CMGL=\"ALL\"");

                    Console.WriteLine(data1.Split('\n').Length);
                    Console.WriteLine(data1);
                    //chuoi ket qua tra ve khi goi lenh doc sms di phai co tren 3 dong thi moi co sms, con no == 3 dong nghia laf ko co sms nao trong sim
                    if (data1.EndsWith("\r\nOK\r\n") && data1.Split('\n').Length > 3 && data1.Contains("GSM") == false)
                    {
                        Noidung_SMS = Lay_Noidung(data1.Split('\n'));
                        mySMS.WriteLine("AT+CMGD=1,1\r");
                    }
                    else
                    {
                        Noidung_SMS = "Rong|Rong".Split('|');
                    }

                    Bussy_Flag = "0";
                    chot_bd = "0";
                    kq = 0; bd = 0; kt = 0;
                    ttkn = CapnhatDulieu("0", "chot");
                    return Noidung_SMS;
                }
                catch
                {
                    try
                    {
                        Noidung_SMS = "ban|ban".Split('|');
                        if (chot_bd == "0")
                        {
                            bd = DateTime.Now.TimeOfDay.TotalSeconds;
                            kq = 0;
                            chot_bd = "1";
                        }
                        kt = DateTime.Now.TimeOfDay.TotalSeconds;
                        kq = kt - bd;
                        if ((kq > 25))
                        {
                            Noidung_SMS = null;
                            chot_bd = "0";
                            kq = 0; bd = 0; kt = 0;
                        }
                        else if (kq < 0) { chot_bd = "0"; kq = 0; }
                    }
                    catch
                    {
                        Noidung_SMS = null;
                        chot_bd = "0";
                        kq = 0; bd = 0; kt = 0;
                    }
                    Console.WriteLine(kq);
                    Bussy_Flag = "0";
                    return Noidung_SMS;
                }
            }
            else return "ban|ban".Split('|');
        }

        #region các method để đọc sms
        /// <summary>
        /// method thực thi các lệnh AT Command.
        /// </summary>
        /// <param name="congcom">Com port.</param>
        /// <param name="command">Lệnh cần thực hiện.</param>
        /// <returns>Trả về kiểu string chứa các dữ liệu kết quả khi thực hiện lệnh.</returns>
        private string ATCommand(string command)
        {
            string serialPortData = string.Empty, data = string.Empty;
            byte Demkytu = 0;
            try
            {
                mySMS.WriteLine(command + "\r");//thực hiện lệnh
                do
                {
                    data = mySMS.ReadExisting();
                    serialPortData += data;
                    Demkytu++;
                }
                while ((Demkytu < 100) && !serialPortData.EndsWith("\r\nOK\r\n") && !serialPortData.EndsWith("\r\n> ") && !serialPortData.EndsWith("\r\nERROR\r\n"));
            }
            catch { serialPortData = null; }
            return serialPortData;
        }

        private string[] Lay_Noidung(string[] Dulieu)
        {
            int batdau = 0, ketthuc = 0;
            string[] Data_return = new string[(Dulieu.Length - 4) / 2];//mảng chứa nội dung của các tin nhắn đọc được

            #region Note
            //tin nhắn đọc được có dang nhu dưới:
            //"""
            //AT+CMGL="REC READ"--------------------------------------------------1
            //+CMGL: 0,"REC READ","+841649667605",,"16/01/04,16:10:26+28"
            //Test doc sms tu plcpi
            //+CMGL: 1,"REC READ","+841649667605",,"16/01/04,16:13:51+28"
            //Test jgj
            //+CMGL: 2,"REC READ","+84909167655",,"16/01/04,16:36:12+28"
            //Test so dt 10 so
            //--------------------------------------------------------------------2
            //OK------------------------------------------------------------------3
            //--------------------------------------------------------------------4
            //"""
            //ví dụ ở đây có 3 sms. mỗi sms có 2 dòng. 1 dong thông tin và 1 dòng nội dung tin nhắn
            //số phần tử của biến Data_return ở đây là 3. tính như sau: (10-4)/2
            #endregion

            byte a = 0;
            if (Data_return.Length > 0)
            {
                try
                {
                    for (int i = 2; i < Dulieu.Length - 3; i += 2)
                    {
                        batdau = Dulieu[i - 1].IndexOf("\",\"") + 3;
                        ketthuc = Dulieu[i - 1].IndexOf("\",,\"");
                        Data_return[a] = Dulieu[i - 1].Substring(batdau, ketthuc - batdau) + "|" + Dulieu[i];
                        a++;
                    }
                }
                catch { }
            }
            else
                Data_return = null;
            a = 0;
            return Data_return;
        }
        #endregion

        #region lam viec voi MySQL
        private string KetnoiMySQL()
        {
            try
            {
                connectmysql = new MySqlConnection(ChuoiKetnoiMySQL);
                connectmysql.Open();
                return "Good";
            }
            catch { return "Bad"; }

        }
        private string NgatKetnoiMySQL()
        {
            try
            {
                connectmysql.Dispose();
                return "Good";
            }
            catch { return "Bad"; }

        }
        //doc du lieu tu MySQL
        private string DocMySQL(string dieukien)
        {
            string giatri = null;
            try
            {
                MySqlCommand command = new MySqlCommand("Select giatri From doc_sms Where ten = '" + dieukien + "'", connectmysql);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    giatri = reader.GetString(0);
                }
                reader.Close();
                command.Dispose();
                return giatri;
            }
            catch { return null; }
        }
        //cap nhat du lieu vao MySQL
        private string CapnhatDulieu(string giatriupdate, string dieukien)
        {
            try
            {
                string MysqlCmd = "update doc_sms set giatri='" + giatriupdate + "'" + "where ten='" + dieukien + "'";
                MySqlCommand cmd = new MySqlCommand(MysqlCmd, connectmysql);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                return "Good";
            }
            catch { return "Bad"; }

        }
        #endregion
        public SMS()
        {
            //System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //ttkn = KetnoiMySQL();
            //try
            //{
            //    proc.StartInfo.FileName = "sudo";
            //    proc.StartInfo.Arguments = "mono /media/restart_usb3g/restart_usb3g.exe";
            //    proc.Start();
            //    proc.Close();
            //    proc.Dispose();
            //}
            //catch
            //{
            //    proc.Close();
            //    proc.Dispose();
            //}
        }
    }
}
