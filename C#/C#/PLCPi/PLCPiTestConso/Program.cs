using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PLCPiProject;

namespace PLCPiTestConso
{
    class Program
    {
        static PLCPi myPLCPi = new PLCPi();
        public static void Main()
        {
            Tat();
            while (true)
            {
                test();
                //test_byte();
                //test_bit();
                //Thread.Sleep(100);
            }
           
        }
        public static void test()
        {
            myPLCPi.NgoRa.XuatNgoRa("Q5", 100);
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q5"));
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(1000);
            myPLCPi.NgoRa.XuatNgoRa("Q5", 254);
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q5"));
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(1000);

            
        }
        public static void test_byte()
        {
            myPLCPi.NgoRa.XuatNgoRa("Q0", 2);
            myPLCPi.NgoRa.XuatNgoRa("Q1", 4);
            myPLCPi.NgoRa.XuatNgoRa("Q2", 16);
            myPLCPi.NgoRa.XuatNgoRa("Q3", 32);
            myPLCPi.NgoRa.XuatNgoRa("Q4", 64);
            myPLCPi.NgoRa.XuatNgoRa("Q5", 128);
            Console.WriteLine( myPLCPi.NgoRa.DocNgoRa("Q0"));
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q1"));
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q2"));
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q3"));
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q4"));
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q5"));
            Thread.Sleep(500);
            myPLCPi.NgoRa.XuatNgoRa("Q0", 36);
            myPLCPi.NgoRa.XuatNgoRa("Q1", 100);
            myPLCPi.NgoRa.XuatNgoRa("Q2", 200);
            myPLCPi.NgoRa.XuatNgoRa("Q3", 154);
            myPLCPi.NgoRa.XuatNgoRa("Q4", 234);
            myPLCPi.NgoRa.XuatNgoRa("Q5", 12);
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q0"));
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q1"));
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q2"));
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q3"));
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q4"));
            Console.WriteLine(myPLCPi.NgoRa.DocNgoRa("Q5"));
            Thread.Sleep(500);

        }
        public static void test_bit()
        {
            myPLCPi.NgoRa.XuatNgoRa("Q0.0", 1);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.1", 1);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.2", 1);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.3", 1);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.4", 1);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.5", 1);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.6", 1);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.7", 1);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            //q1
            myPLCPi.NgoRa.XuatNgoRa("Q1.0", 1);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.1", 1);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.2", 1);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.3", 1);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.4", 1);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.5", 1);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.6", 1);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.7", 1);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            //q2
            myPLCPi.NgoRa.XuatNgoRa("Q2.0", 1);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.1", 1);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.2", 1);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.3", 1);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.4", 1);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.5", 1);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.6", 1);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.7", 1);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            //q3
            myPLCPi.NgoRa.XuatNgoRa("Q3.0", 1);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.1", 1);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.2", 1);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.3", 1);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.4", 1);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.5", 1);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.6", 1);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.7", 1);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            //q4
            myPLCPi.NgoRa.XuatNgoRa("Q4.0", 1);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.1", 1);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.2", 1);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.3", 1);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.4", 1);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.5", 1);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.6", 1);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.7", 1);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            //q5
            myPLCPi.NgoRa.XuatNgoRa("Q5.0", 1);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.1", 1);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.2", 1);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.3", 1);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.4", 1);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.5", 1);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.6", 1);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.7", 1);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);

            //off
            //q0
            myPLCPi.NgoRa.XuatNgoRa("Q0.0", 0);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.1", 0);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.2", 0);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.3", 0);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.4", 0);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.5", 0);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.6", 0);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q0.7", 0);
            Console.WriteLine("Q0: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q0.0"), myPLCPi.NgoRa.DocNgoRa("Q0.1"), myPLCPi.NgoRa.DocNgoRa("Q0.2")
                , myPLCPi.NgoRa.DocNgoRa("Q0.3"), myPLCPi.NgoRa.DocNgoRa("Q0.4"),
                myPLCPi.NgoRa.DocNgoRa("Q0.5"), myPLCPi.NgoRa.DocNgoRa("Q0.6"), myPLCPi.NgoRa.DocNgoRa("Q0.7"));
            Thread.Sleep(300);
            //q1
            myPLCPi.NgoRa.XuatNgoRa("Q1.0", 0);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.1", 0);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.2", 0);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.3", 0);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.4", 0);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.5", 0);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.6", 0);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q1.7", 0);
            Console.WriteLine("Q1: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q1.0"), myPLCPi.NgoRa.DocNgoRa("Q1.1"), myPLCPi.NgoRa.DocNgoRa("Q1.2")
                , myPLCPi.NgoRa.DocNgoRa("Q1.3"), myPLCPi.NgoRa.DocNgoRa("Q1.4"),
                myPLCPi.NgoRa.DocNgoRa("Q1.5"), myPLCPi.NgoRa.DocNgoRa("Q1.6"), myPLCPi.NgoRa.DocNgoRa("Q1.7"));
            Thread.Sleep(300);
            //q2
            myPLCPi.NgoRa.XuatNgoRa("Q2.0", 0);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.1", 0);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.2", 0);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.3", 0);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.4", 0);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.5", 0);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.6", 0);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q2.7", 0);
            Console.WriteLine("Q2: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q2.0"), myPLCPi.NgoRa.DocNgoRa("Q2.1"), myPLCPi.NgoRa.DocNgoRa("Q2.2")
                , myPLCPi.NgoRa.DocNgoRa("Q2.3"), myPLCPi.NgoRa.DocNgoRa("Q2.4"),
                myPLCPi.NgoRa.DocNgoRa("Q2.5"), myPLCPi.NgoRa.DocNgoRa("Q2.6"), myPLCPi.NgoRa.DocNgoRa("Q2.7"));
            Thread.Sleep(300);
            //q3
            myPLCPi.NgoRa.XuatNgoRa("Q3.0", 0);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.1", 0);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.2", 0);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.3", 0);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.4", 0);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.5", 0);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.6", 0);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q3.7", 0);
            Console.WriteLine("Q3: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q3.0"), myPLCPi.NgoRa.DocNgoRa("Q3.1"), myPLCPi.NgoRa.DocNgoRa("Q3.2")
                , myPLCPi.NgoRa.DocNgoRa("Q3.3"), myPLCPi.NgoRa.DocNgoRa("Q3.4"),
                myPLCPi.NgoRa.DocNgoRa("Q3.5"), myPLCPi.NgoRa.DocNgoRa("Q3.6"), myPLCPi.NgoRa.DocNgoRa("Q3.7"));
            Thread.Sleep(300);
            //q4
            myPLCPi.NgoRa.XuatNgoRa("Q4.0", 0);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.1", 0);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.2", 0);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.3", 0);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.4", 0);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.5", 0);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.6", 0);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q4.7", 0);
            Console.WriteLine("Q4: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q4.0"), myPLCPi.NgoRa.DocNgoRa("Q4.1"), myPLCPi.NgoRa.DocNgoRa("Q4.2")
                , myPLCPi.NgoRa.DocNgoRa("Q4.3"), myPLCPi.NgoRa.DocNgoRa("Q4.4"),
                myPLCPi.NgoRa.DocNgoRa("Q4.5"), myPLCPi.NgoRa.DocNgoRa("Q4.6"), myPLCPi.NgoRa.DocNgoRa("Q4.7"));
            Thread.Sleep(300);
            //q5
            myPLCPi.NgoRa.XuatNgoRa("Q5.0", 0);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.1", 0);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.2", 0);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.3", 0);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.4", 0);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            myPLCPi.NgoRa.XuatNgoRa("Q5.5", 0);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.6", 0);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);
            myPLCPi.NgoRa.XuatNgoRa("Q5.7", 0);
            Console.WriteLine("Q5: {0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", myPLCPi.NgoRa.DocNgoRa("Q5.0"), myPLCPi.NgoRa.DocNgoRa("Q5.1"), myPLCPi.NgoRa.DocNgoRa("Q5.2")
                , myPLCPi.NgoRa.DocNgoRa("Q5.3"), myPLCPi.NgoRa.DocNgoRa("Q5.4"),
                myPLCPi.NgoRa.DocNgoRa("Q5.5"), myPLCPi.NgoRa.DocNgoRa("Q5.6"), myPLCPi.NgoRa.DocNgoRa("Q5.7"));
            Thread.Sleep(300);

        }
        public static void Tat()
        {
            myPLCPi.NgoRa.XuatNgoRa("Q0", 0);
            myPLCPi.NgoRa.XuatNgoRa("Q1", 0);
            myPLCPi.NgoRa.XuatNgoRa("Q2", 0);
            myPLCPi.NgoRa.XuatNgoRa("Q3", 0);
            myPLCPi.NgoRa.XuatNgoRa("Q4", 0);
            myPLCPi.NgoRa.XuatNgoRa("Q5", 0);

        }
    }
}
