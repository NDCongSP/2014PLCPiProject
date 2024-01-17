using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PLCPiProject;

namespace PLCPiTestDS18B20
{
    class Program
    {
        static PLCPi myPLCPi = new PLCPi();
        static double Temp;
        static void Main()
        {
            while (true)
            {
                Temp = myPLCPi.DS18B20.DocNhietDo("28-00042b536dff");
                Console.WriteLine("Nhiệt Dộ  {0}", Temp);
                //myPLCPi.HienThiLed7.HienThi(Convert.ToString(Temp), 1);
                myPLCPi.HienThiLed7.HienThi(Convert.ToString(Temp), 2);
            }
            
        }
    }
}
