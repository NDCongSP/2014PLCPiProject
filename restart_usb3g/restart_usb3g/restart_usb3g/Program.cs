using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using MySql.Data.MySqlClient;

namespace restart_usb3g
{
    class Program
    {
        static string ChuoiKetnoiMySQL = "Server = localhost ; Uid =root ;Pwd= 100100;Database=DB_PLCPi";
        static MySqlConnection connectmysql;
        static string ttkn = "", giatri = "", newport = "";
        static string port = "ttyUSB0", chot = "0", chot1 = "0";
        static double bd = 0, kt = 0, kq = 0;        

        static void Main(string[] args)
        {
            if (ttkn == "Bad") KetnoiMySQL();
            Thread.Sleep(5000);
            newport = DocMySQL("port");
            if (newport != "0") port = "ttyUSB" + newport.Trim();
            while (true)
            {
                if (ttkn == "Bad") KetnoiMySQL();           
                try
                {
                    giatri = DocMySQL("chot");
                    if (giatri == "1")
                    {
                        if (chot1 == "0")
                        {
                            bd = DateTime.Now.TimeOfDay.TotalSeconds;
                            chot1 = "1";
                        }
                        kt = DateTime.Now.TimeOfDay.TotalSeconds;
                        kq = kt - bd;

                        if (kq > 5)
                        {                            
                            Console.WriteLine(port);
                            SerialPort mySMS = new SerialPort("/dev/" + port, 115200, Parity.None, 8);
                            mySMS.Open();                            
                            mySMS.WriteLine("AT+CFUN=1,1" + "\r");
                            //Console.WriteLine("reset USB");
                            mySMS.Close();
                            mySMS.Dispose();
                            ttkn = CapnhatDulieu("0");
                            kq = 0;
                            Thread.Sleep(40000);
                        }
                        else if (kq < 0)
                        {
                            bd = DateTime.Now.TimeOfDay.TotalSeconds; kq = 0;
                        }
                    }
                    else if (giatri == "0") { kq = 0; chot1 = "0"; }
                    else if (giatri == null)
                    {
                        NgatKetnoiMySQL();
                        ttkn = KetnoiMySQL();
                    }                    
                }
                catch
                {
                    chot1 = "0";
                    //Console.WriteLine("loi");
                    if (newport != "0")
                    {
                        if (chot == "0") { port = "ttyUSB" + newport; chot = "1"; }
                        else if (chot == "1") { port = "ttyUSB" + (Convert.ToByte(newport) + 1).ToString(); chot = "2"; }
                        else if (chot == "2") { port = "ttyUSB" + (Convert.ToByte(newport) + 2).ToString(); chot = "0"; }                        
                    }
                    else if (newport == "0")
                    {
                        if (chot == "0") { port = "ttyUSB0"; chot = "1"; }
                        else if (chot == "1") { port = "ttyUSB1"; chot = "2"; }
                        else if (chot == "2") { port = "ttyUSB2"; chot = "0"; }
                    }
                }
                Console.WriteLine("ttkn={0};  giatri={1}; port={2};kq={3}", ttkn, giatri, port, kq);
                Thread.Sleep(10000);
            }
        }

        static string KetnoiMySQL()
        {
            try
            {
                connectmysql = new MySqlConnection(ChuoiKetnoiMySQL);
                connectmysql.Open();
                return "Good";
            }
            catch { return "Bad"; }

        }
        static string NgatKetnoiMySQL()
        {
            try
            {
                connectmysql.Dispose();
                return "Good";
            }
            catch { return "Bad"; }

        }
        //doc du lieu tu MySQL
        static string DocMySQL(string dieukien)
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
        static string CapnhatDulieu(string giatriupdate)
        {
            try
            {
                string MysqlCmd = "update doc_sms set giatri='" + giatriupdate + "'" + "where ten='chot'";
                MySqlCommand cmd = new MySqlCommand(MysqlCmd, connectmysql);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                return "Good";
            }
            catch { return "Bad"; }

        }

        static Program()
        {
            ttkn = KetnoiMySQL();
        }
    }
}
