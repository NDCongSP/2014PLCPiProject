using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLCPiProject;

namespace hello
{
    class Program
    {
        static PLCPi myPLCPi = new PLCPi();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            Console.WriteLine("IP Server");
            string IP = Console.ReadLine();
            myPLCPi.Snap7.Client.Ket_Noi(IP);

            Console.ReadKey();
        }
    }
}
