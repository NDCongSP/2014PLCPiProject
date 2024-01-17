using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using PLCPiProject;

namespace PLCPiTestADC
{
    class Program
    {
        static PLCPi myPLCPi = new PLCPi();
        static void Main(string[] args)
        {
            while (true)
            {
                DocToanBo();
            }
            
        }
        static void Doc_Ap()
        {
            Console.WriteLine("AV0 = {0}", myPLCPi.AI.DocAI(0, 0, 0, 1024, 200));
            Console.WriteLine("AV1 = {0}", myPLCPi.AI.DocAI(1, 0, 0, 1024, 200));
            Console.WriteLine("AV2 = {0}", myPLCPi.AI.DocAI(2, 0, 0, 1024, 200));
            Console.WriteLine("AV3 = {0}", myPLCPi.AI.DocAI(3, 0, 0, 1024, 200));
            Console.WriteLine("AV4 = {0}", myPLCPi.AI.DocAI(4, 0, 0, 1024, 200));
            Console.WriteLine("");
        }
        static void Doc_Dong()
        {
            Console.WriteLine("AI0 = {0}", myPLCPi.AI.DocAI(5, 204, 0, 1024, 400));
            Console.WriteLine("AI1 = {0}", myPLCPi.AI.DocAI(6, 204, 0, 1024, 400));
            Console.WriteLine("AI2 = {0}", myPLCPi.AI.DocAI(7, 204, 0, 1024, 400));
            Console.WriteLine("AI3 = {0}", myPLCPi.AI.DocAI(8, 204, 0, 1024, 400));
            Console.WriteLine("AI4 = {0}", myPLCPi.AI.DocAI(9, 204, 0, 1024, 400));
            Console.WriteLine("AI5 = {0}", myPLCPi.AI.DocAI(10, 204, 0, 1024, 400));
            Console.WriteLine("AI6 = {0}", myPLCPi.AI.DocAI(11, 204, 0, 1024, 400));
            Console.WriteLine("AI7 = {0}", myPLCPi.AI.DocAI(12, 204, 0, 1024, 400));
            Console.WriteLine("");
        }
        static void DocToanBo()
        {
            Console.WriteLine("AV0 = {0}", myPLCPi.AI.DocAI(0, 0, 0, 1024, 10));
            Console.WriteLine("AV1 = {0}", myPLCPi.AI.DocAI(1, 0, 0, 1024, 10));
            Console.WriteLine("AV2 = {0}", myPLCPi.AI.DocAI(2, 0, 0, 1024, 10));
            Console.WriteLine("AV3 = {0}", myPLCPi.AI.DocAI(3, 0, 0, 1024, 10));
            Console.WriteLine("AV4 = {0}", myPLCPi.AI.DocAI(4, 0, 0, 1024, 10));
            Console.WriteLine("");

            Console.WriteLine("AI0 = {0}", myPLCPi.AI.DocAI(5, 204, 0, 1024, 400));
            Console.WriteLine("AI1 = {0}", myPLCPi.AI.DocAI(6, 204, 0, 1024, 400));
            Console.WriteLine("AI2 = {0}", myPLCPi.AI.DocAI(7, 204, 0, 1024, 400));
            Console.WriteLine("AI3 = {0}", myPLCPi.AI.DocAI(8, 204, 0, 1024, 400));
            Console.WriteLine("AI4 = {0}", myPLCPi.AI.DocAI(9, 204, 0, 1024, 400));
            Console.WriteLine("AI5 = {0}", myPLCPi.AI.DocAI(10, 204, 0, 1024, 400));
            Console.WriteLine("AI6 = {0}", myPLCPi.AI.DocAI(11, 204, 0, 1024, 400));
            Console.WriteLine("AI7 = {0}", myPLCPi.AI.DocAI(12, 204, 0, 1024, 400));
            Console.WriteLine("");
        }
    }
}
