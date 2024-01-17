using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PLCPiProject;

namespace PLCPiTestIn
{
    class Program
    {
        public static PLCPi MyPLCPi = new PLCPi();
        public static void Main()
        {
            while (true)
            {
                //V_byte();
                //Thread.Sleep(500);
                //Console.WriteLine("");
                V0_bit();
                Console.WriteLine("V0={0}, V1={1}, V2={2}, V3={3}, V4={4}", MyPLCPi.NgoVao.MangNgoVao[0], MyPLCPi.NgoVao.MangNgoVao[1],
                    MyPLCPi.NgoVao.MangNgoVao[2], MyPLCPi.NgoVao.MangNgoVao[3], MyPLCPi.NgoVao.MangNgoVao[4]);
                Thread.Sleep(500);
                Console.WriteLine("");
                V1_bit();
                Console.WriteLine("V0={0}, V1={1}, V2={2}, V3={3}, V4={4}", MyPLCPi.NgoVao.MangNgoVao[0], MyPLCPi.NgoVao.MangNgoVao[1],
                    MyPLCPi.NgoVao.MangNgoVao[2], MyPLCPi.NgoVao.MangNgoVao[3], MyPLCPi.NgoVao.MangNgoVao[4]);
                Thread.Sleep(500);
                Console.WriteLine("");
                V2_bit();
                Console.WriteLine("V0={0}, V1={1}, V2={2}, V3={3}, V4={4}", MyPLCPi.NgoVao.MangNgoVao[0], MyPLCPi.NgoVao.MangNgoVao[1],
                    MyPLCPi.NgoVao.MangNgoVao[2], MyPLCPi.NgoVao.MangNgoVao[3], MyPLCPi.NgoVao.MangNgoVao[4]);
                Thread.Sleep(500);
                Console.WriteLine("");
                V3_bit();
                Console.WriteLine("V0={0}, V1={1}, V2={2}, V3={3}, V4={4}", MyPLCPi.NgoVao.MangNgoVao[0], MyPLCPi.NgoVao.MangNgoVao[1],
                    MyPLCPi.NgoVao.MangNgoVao[2], MyPLCPi.NgoVao.MangNgoVao[3], MyPLCPi.NgoVao.MangNgoVao[4]);
                Thread.Sleep(500);
                Console.WriteLine("");
                V4_bit();
                Console.WriteLine("V0={0}, V1={1}, V2={2}, V3={3}, V4={4}", MyPLCPi.NgoVao.MangNgoVao[0], MyPLCPi.NgoVao.MangNgoVao[1],
                    MyPLCPi.NgoVao.MangNgoVao[2], MyPLCPi.NgoVao.MangNgoVao[3], MyPLCPi.NgoVao.MangNgoVao[4]);
                Thread.Sleep(500);
                Console.WriteLine("");
                
                
            } 
        }
        //doc ngo vao bit
        public static void V0_bit()
        {
            Console.WriteLine("V0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", MyPLCPi.NgoVao.DocNgoVao("I0.7"), MyPLCPi.NgoVao.DocNgoVao("I0.6"), MyPLCPi.NgoVao.DocNgoVao("I0.5"),
                                                                MyPLCPi.NgoVao.DocNgoVao("I0.4"), MyPLCPi.NgoVao.DocNgoVao("I0.3"), MyPLCPi.NgoVao.DocNgoVao("I0.2"),
                                                                MyPLCPi.NgoVao.DocNgoVao("I0.1"), MyPLCPi.NgoVao.DocNgoVao("I0.0"));
        }
        public static void V1_bit()
        {
            Console.WriteLine("V1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", MyPLCPi.NgoVao.DocNgoVao("I1.7"), MyPLCPi.NgoVao.DocNgoVao("I1.6"), MyPLCPi.NgoVao.DocNgoVao("I1.5"),
                                                                MyPLCPi.NgoVao.DocNgoVao("I1.4"), MyPLCPi.NgoVao.DocNgoVao("I1.3"), MyPLCPi.NgoVao.DocNgoVao("I1.2"),
                                                                MyPLCPi.NgoVao.DocNgoVao("I1.1"), MyPLCPi.NgoVao.DocNgoVao("I1.0"));
        }
        public static void V2_bit()
        {
            Console.WriteLine("V2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", MyPLCPi.NgoVao.DocNgoVao("I2.7"), MyPLCPi.NgoVao.DocNgoVao("I2.6"), MyPLCPi.NgoVao.DocNgoVao("I2.5"),
                                                                MyPLCPi.NgoVao.DocNgoVao("I2.4"), MyPLCPi.NgoVao.DocNgoVao("I2.3"), MyPLCPi.NgoVao.DocNgoVao("I2.2"),
                                                                MyPLCPi.NgoVao.DocNgoVao("I2.1"), MyPLCPi.NgoVao.DocNgoVao("I2.0"));
        }
        public static void V3_bit()
        {
            Console.WriteLine("V3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", MyPLCPi.NgoVao.DocNgoVao("I3.7"), MyPLCPi.NgoVao.DocNgoVao("I3.6"), MyPLCPi.NgoVao.DocNgoVao("I3.5"),
                                                                MyPLCPi.NgoVao.DocNgoVao("I3.4"), MyPLCPi.NgoVao.DocNgoVao("I3.3"), MyPLCPi.NgoVao.DocNgoVao("I3.2"),
                                                                MyPLCPi.NgoVao.DocNgoVao("I3.1"), MyPLCPi.NgoVao.DocNgoVao("I3.0"));
        }
        public static void V4_bit()
        {
            Console.WriteLine("V4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", MyPLCPi.NgoVao.DocNgoVao("I4.7"), MyPLCPi.NgoVao.DocNgoVao("I4.6"), MyPLCPi.NgoVao.DocNgoVao("I4.5"),
                                                                MyPLCPi.NgoVao.DocNgoVao("I4.4"), MyPLCPi.NgoVao.DocNgoVao("I4.3"), MyPLCPi.NgoVao.DocNgoVao("I4.2"),
                                                                MyPLCPi.NgoVao.DocNgoVao("I4.1"), MyPLCPi.NgoVao.DocNgoVao("I4.0"));
        }
        ////doc ngo vao byte
        public static void V_byte()
        {
            Console.WriteLine("V0 = {0}; V1 = {1}; V2 = {2}; V3 = {3}; V4 = {4}", MyPLCPi.NgoVao.DocNgoVao("I0"), MyPLCPi.NgoVao.DocNgoVao("I1")
                , MyPLCPi.NgoVao.DocNgoVao("I2"), MyPLCPi.NgoVao.DocNgoVao("I3"), MyPLCPi.NgoVao.DocNgoVao("I4"));
        }
    }
}
