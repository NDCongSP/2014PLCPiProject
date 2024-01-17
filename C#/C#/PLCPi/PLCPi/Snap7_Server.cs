using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Snap7;

namespace PLCPiProject
{
    public class Snap7_Server
    {
        S7Server Server;
        public Ngo_Ra NgoRa;
        public Ngo_Vao NgoVao;
        public byte[] DB_Server = new byte[1024];//vung nho Data Block cua Snap7 server

        /// <summary>
        /// Chạy Server. trả về trạng thái kết nối kiểu String. 
        /// Nếu String trả vê là "GOOD" là kết nối thành công. 
        /// Còn String trả về khác "GOOD" là kết nối bị lỗi
        /// </summary>
        /// <returns></returns>
        public string KetNoi()
        {
            string Status;
            Server = new S7Server();
            string Server_IP = "";
            IPHostEntry ip = new IPHostEntry();
            string hostname = Dns.GetHostName();
            ip = Dns.GetHostByName(hostname);

            foreach (IPAddress listip in ip.AddressList)
            {
                Server_IP = listip.ToString();
                Console.WriteLine(Server_IP);
            }
            
            // Share some resources with our virtual PLC

            Server.RegisterArea(S7Server.srvAreaDB,  // We are registering a DB
                                1,                   // Its number is 1 (DB1)
                                DB_Server,                 // Our buffer for DB1
                                DB_Server.Length);         // Its size
            // Do the same for DB2 and DB3

            Server.RegisterArea(S7Server.srvAreaPE, 0, NgoVao.MangNgoVao, NgoVao.MangNgoVao.Length); //ngo vao
            Server.RegisterArea(S7Server.srvAreaPA, 0, NgoRa.MangNgoRa, NgoRa.MangNgoRa.Length);//ngo ra
            //Server.RegisterArea(S7Server.srvAreaMK, 0, MB, MB.Length);
;

            int Error = Server.StartTo(Server_IP);
            if (Error == 0)
            {
                Console.WriteLine("Complete");
                Status = "GOOD";
            }
            else
            {
                Status = Server.ErrorText(Error);
                Console.WriteLine(Status);
            }
            return Status;
        }
    }
}
