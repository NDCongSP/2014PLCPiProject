using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PLCPiProject;

namespace PLCPiTestReadOut
{
    class Program
    {
        static PLCPi MyPLCPi = new PLCPi();
        public static void Main()
        {
            while (true)
            {
                MyPLCPi.Ngo_Ra.Q0_Byte(254);
                MyPLCPi.Ngo_Ra.Q1_Byte(4);
                MyPLCPi.Ngo_Ra.Q2_Byte(6);
                MyPLCPi.Ngo_Ra.Q3_Byte(8);
                MyPLCPi.Ngo_Ra.Q4_Byte(16);
                MyPLCPi.Ngo_Ra.Q5_Byte(32);

                test_Bit();
                //test_Byte();
                while (true) { }
            }
        }
        public static void test_Bit()
        {
            Console.WriteLine("R0.0 = {0}", MyPLCPi.Ngo_Ra.RQ0_Bit("0"));
            Console.WriteLine("R0.1 = {0}", MyPLCPi.Ngo_Ra.RQ0_Bit("1"));
            Console.WriteLine("R0.2 = {0}", MyPLCPi.Ngo_Ra.RQ0_Bit("2"));
            Console.WriteLine("R0.3 = {0}", MyPLCPi.Ngo_Ra.RQ0_Bit("3"));
            Console.WriteLine("R0.4 = {0}", MyPLCPi.Ngo_Ra.RQ0_Bit("4"));
            Console.WriteLine("R0.5 = {0}", MyPLCPi.Ngo_Ra.RQ0_Bit("5"));
            Console.WriteLine("R0.6 = {0}", MyPLCPi.Ngo_Ra.RQ0_Bit("6"));
            Console.WriteLine("R0.7 = {0}", MyPLCPi.Ngo_Ra.RQ0_Bit("7"));
            Console.WriteLine(" ");
            Console.WriteLine("R1.0 = {0}", MyPLCPi.Ngo_Ra.RQ1_Bit("0"));
            Console.WriteLine("R1.1 = {0}", MyPLCPi.Ngo_Ra.RQ1_Bit("1"));
            Console.WriteLine("R1.2 = {0}", MyPLCPi.Ngo_Ra.RQ1_Bit("2"));
            Console.WriteLine("R1.3 = {0}", MyPLCPi.Ngo_Ra.RQ1_Bit("3"));
            Console.WriteLine("R1.4 = {0}", MyPLCPi.Ngo_Ra.RQ1_Bit("4"));
            Console.WriteLine("R1.5 = {0}", MyPLCPi.Ngo_Ra.RQ1_Bit("5"));
            Console.WriteLine("R1.6 = {0}", MyPLCPi.Ngo_Ra.RQ1_Bit("6"));
            Console.WriteLine("R1.7 = {0}", MyPLCPi.Ngo_Ra.RQ1_Bit("7"));
            Console.WriteLine(" ");
            Console.WriteLine("R2.0 = {0}", MyPLCPi.Ngo_Ra.RQ2_Bit("0"));
            Console.WriteLine("R2.1 = {0}", MyPLCPi.Ngo_Ra.RQ2_Bit("1"));
            Console.WriteLine("R2.2 = {0}", MyPLCPi.Ngo_Ra.RQ2_Bit("2"));
            Console.WriteLine("R2.3 = {0}", MyPLCPi.Ngo_Ra.RQ2_Bit("3"));
            Console.WriteLine("R2.4 = {0}", MyPLCPi.Ngo_Ra.RQ2_Bit("4"));
            Console.WriteLine("R2.5 = {0}", MyPLCPi.Ngo_Ra.RQ2_Bit("5"));
            Console.WriteLine("R2.6 = {0}", MyPLCPi.Ngo_Ra.RQ2_Bit("6"));
            Console.WriteLine("R2.7 = {0}", MyPLCPi.Ngo_Ra.RQ2_Bit("7"));
            Console.WriteLine("");
            Console.WriteLine("R3.0 = {0}", MyPLCPi.Ngo_Ra.RQ3_Bit("0"));
            Console.WriteLine("R3.1 = {0}", MyPLCPi.Ngo_Ra.RQ3_Bit("1"));
            Console.WriteLine("R3.2 = {0}", MyPLCPi.Ngo_Ra.RQ3_Bit("2"));
            Console.WriteLine("R3.3 = {0}", MyPLCPi.Ngo_Ra.RQ3_Bit("3"));
            Console.WriteLine("R3.4 = {0}", MyPLCPi.Ngo_Ra.RQ3_Bit("4"));
            Console.WriteLine("R3.5 = {0}", MyPLCPi.Ngo_Ra.RQ3_Bit("5"));
            Console.WriteLine("R3.6 = {0}", MyPLCPi.Ngo_Ra.RQ3_Bit("6"));
            Console.WriteLine("R3.7 = {0}", MyPLCPi.Ngo_Ra.RQ3_Bit("7"));
            Console.WriteLine(" ");
            Console.WriteLine("R4.0 = {0}", MyPLCPi.Ngo_Ra.RQ4_Bit("0"));
            Console.WriteLine("R4.1 = {0}", MyPLCPi.Ngo_Ra.RQ4_Bit("1"));
            Console.WriteLine("R4.2 = {0}", MyPLCPi.Ngo_Ra.RQ4_Bit("2"));
            Console.WriteLine("R4.3 = {0}", MyPLCPi.Ngo_Ra.RQ4_Bit("3"));
            Console.WriteLine("R4.4 = {0}", MyPLCPi.Ngo_Ra.RQ4_Bit("4"));
            Console.WriteLine("R4.5 = {0}", MyPLCPi.Ngo_Ra.RQ4_Bit("5"));
            Console.WriteLine("R4.6 = {0}", MyPLCPi.Ngo_Ra.RQ4_Bit("6"));
            Console.WriteLine("R4.7 = {0}", MyPLCPi.Ngo_Ra.RQ4_Bit("7"));
            Console.WriteLine(" ");
            Console.WriteLine("R5.0 = {0}", MyPLCPi.Ngo_Ra.RQ5_Bit("0"));
            Console.WriteLine("R5.1 = {0}", MyPLCPi.Ngo_Ra.RQ5_Bit("1"));
            Console.WriteLine("R5.2 = {0}", MyPLCPi.Ngo_Ra.RQ5_Bit("2"));
            Console.WriteLine("R5.3 = {0}", MyPLCPi.Ngo_Ra.RQ5_Bit("3"));
            Console.WriteLine("R5.4 = {0}", MyPLCPi.Ngo_Ra.RQ5_Bit("4"));
            Console.WriteLine("R5.5 = {0}", MyPLCPi.Ngo_Ra.RQ5_Bit("5"));
            Console.WriteLine("R5.6 = {0}", MyPLCPi.Ngo_Ra.RQ5_Bit("6"));
            Console.WriteLine("R5.7 = {0}", MyPLCPi.Ngo_Ra.RQ5_Bit("7"));
            Console.WriteLine(" ");

        }
        public static void test_Byte()
        {
            Console.WriteLine("R0 = {0}", MyPLCPi.Ngo_Ra.RQ0_Byte());
            Console.WriteLine("R1 = {0}", MyPLCPi.Ngo_Ra.RQ1_Byte());
            Console.WriteLine("R2 = {0}", MyPLCPi.Ngo_Ra.RQ2_Byte());
            Console.WriteLine("R3 = {0}", MyPLCPi.Ngo_Ra.RQ3_Byte());
            Console.WriteLine("R4 = {0}", MyPLCPi.Ngo_Ra.RQ4_Byte());
            Console.WriteLine("R5 = {0}", MyPLCPi.Ngo_Ra.RQ5_Byte());
        }
    }
}
