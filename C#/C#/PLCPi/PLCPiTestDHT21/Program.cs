using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLCPiProject;

namespace PLCPiTestDHT21
{
    class Program
    {
        static PLCPi myPLCPi = new PLCPi();
        static string Data;
        static string[] mang;
        static void Main(string[] args)
        {
            while (true)
            {
                mang = myPLCPi.DHT21.DocNhietDoDoAm();
                Console.WriteLine("Nhiet do = {0} Do am= {1}", mang[0],mang[1]);
                myPLCPi.HienThiLed7.HienThi(mang[0], 2);
                myPLCPi.HienThiLed7.HienThi(mang[1], 1);
                
            }
        }

    }
}
