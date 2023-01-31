using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Snap7;

namespace PLCPiProject
{
    /// <summary>
    /// Đối tượng S7 Ethernet TCPIP/ Profinet Server
    /// </summary>
    public class Snap7_Server
    {
        S7Server Server;
       

        /// <summary>
        /// S7 Server có 1024 khối Datablock (DB) từ 0-1024
        /// </summary>
        public byte[] DataBlock = new byte[1024];//vung nho Data Block cua Snap7 server

        #region Event
        //string[] Event_Read = new string[15];// mảng để điều khiển đọc ngõ vào khi có yêu cầu đọc ngõ vào tuwd S7 client
        //S7Server.TSrvCallback TheEventCallBack; // <== Static var containig the callback
        //S7Server.TSrvCallback TheReadCallBack; // <== Static var containig the callback
        //string BatDau = null, SoByte = null;// vị trí bắt đầu đọc, và số byte đọc
        //IC595 my595 = new IC595();

        //private void EventCallback(IntPtr usrPtr, ref S7Server.USrvEvent Event, int Size)
        //{
        //    string _mystring = Server.EventText(ref Event);
        //    //Console.WriteLine(_mystring);
        //    if (_mystring.Contains("Write") && _mystring.Contains("PA"))
        //    {
        //        if (NgoRa.KhoaRa == 0)
        //        {
        //            my595.DichDuLieu(NgoRa.MangNgoRa);
        //        }
        //        else {}
        //    }
        //}
        //private void ReadEventCallback(IntPtr usrPtr, ref S7Server.USrvEvent Event, int Size)
        //{
        //    #region c2            
        //    string _mystring = Server.EventText(ref Event);
        //    //Console.WriteLine(_mystring);
        //    Event_Read = _mystring.Split(' ');
        //    BatDau = Event_Read[10].Substring(0, 1);
        //    SoByte = Event_Read[13];
        //    //Console.WriteLine("{0}//{1}", BatDau, SoByte);
        //    if (_mystring.Contains("Read") && _mystring.Contains("PE"))
        //    {

        //        if (NgoVao.KhoaChinh == 0)
        //        {
        //            try
        //            {
        //                NgoVao.DocNgoVao(true);
        //            }
        //            catch (Exception ex) { throw ex; }
        //        }
        //    }
        //    #endregion

        //}
        #endregion
        /// <summary>
        /// Chạy Server. trả về trạng thái kết nối kiểu String. 
        /// Nếu String trả vê là "GOOD" là kết nối thành công. 
        /// Còn String trả về khác "GOOD" là kết nối bị lỗi
        /// </summary>
        /// <returns></returns>
        public string Khoitao()
        {
            try
            {
                //
                for (int i = 0; i < 1024; i++)
                {
                    DataBlock[i] = 0;
                }

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
                                     ref DataBlock,                 // Our buffer for DB1
                                    DataBlock.Length);         // Its size
                // Do the same for DB2 and DB3

                
                //Server.RegisterArea(S7Server.srvAreaMK, 0, MB, MB.Length);

                //TheEventCallBack = new S7Server.TSrvCallback(EventCallback);
                //TheReadCallBack = new S7Server.TSrvCallback(ReadEventCallback);

                //Server.EventMask = ~S7Server.evcDataRead;
                //Server.SetEventsCallBack(TheEventCallBack, IntPtr.Zero);
                //Server.SetReadEventsCallBack(TheReadCallBack, IntPtr.Zero);

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
            catch (Exception ex) { return "BAD"; }
        }
    }
}
