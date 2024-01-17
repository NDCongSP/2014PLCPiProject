using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCPiProject
{
    public class Snap7
    {
        /// <summary>
        /// Đối tượng để cài đặt PLCPi thành một Snap7 Client để truy cập đến các Snap7 Server khác
        /// </summary>
        public Snap7_Client Client = new Snap7_Client();
        /// <summary>
        /// Đối tượng để cài đặt PLCPi thành một Snap7 Server
        /// </summary>
        public Snap7_Server Server = new Snap7_Server();
    }
}
