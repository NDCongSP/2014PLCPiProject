using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace PLCPiProject
{      
    public class PhoneCall
    {
        public string Port_USB3G1 = "ttyUSB0"; //port GMS modem 
        /// <summary>
        /// method gọi điện thoại từ GSM modem của USB 3G.
        /// </summary>
        /// <param name="STD">Số điện thoại muốn gọi tới.</param>
        /// <returns>Trả về trạng thái, nếu là "GOOD" thành công, "BAD" thất bại.</returns>
        public string GoiDienThoai(string STD)
        {
            SerialPort myPhoneCall = new SerialPort("/dev/" + Port_USB3G1, 115200, Parity.None, 8);
            try
            {                
                myPhoneCall.Open();
                //goi dien thoai
                myPhoneCall.WriteLine("ATDT" + STD + ";" + "\r");//"\r" ký tự Enter
                //Thread.Sleep(10);
                myPhoneCall.Close();
                return "GOOD";
            }
            catch { myPhoneCall.Close(); return "BAD"; }
        }
    }
}
