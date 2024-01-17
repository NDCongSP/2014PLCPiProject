using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;
using System.Threading;

namespace s7client
{
    class Program
    {
        static byte[] MyDB1 = new byte[5];
        static byte[] MyAB1 = new byte[5];
        static byte[] MyEB1 = new byte[5];
        static byte[] MyMB1 = new byte[5];
        static ushort[] MyTM1 = new ushort[5];
        static ushort[] MyCT1 = new ushort[5];
        static ushort[] data1 = {100,200};
        static byte[] data = { 10, 20, 30, 40, 50, 60 };
        static S7Client MyClient;

        static void Main(string[] args)
        {
            Console.WriteLine("nhap IP server");
            string ip = Console.ReadLine();
            MyClient = new S7Client();
            int error = MyClient.ConnectTo(ip, 0, 0);
            if (error == 0)
            {
                Console.WriteLine("GOOD");
            }
            else
            {
                Console.WriteLine("ERROR");
            }
            Vung_DB();
            //Vung_EB();
            //Vung_AB();
            //Vung_MB();
            MyClient = null;
            Console.ReadKey();
        }


        //test
        static void Vung_DB()
        {
            Doc_DB();
            Console.WriteLine(MyDB1[0]);
            Console.WriteLine(MyDB1[1]);
            Console.WriteLine(MyDB1[2]);
            Console.WriteLine(MyDB1[3]);
            Console.WriteLine(MyDB1[4]);
            Thread.Sleep(2000);
            Ghi_DB(data);
            Doc_DB();
            Console.WriteLine(MyDB1[0]);
            Console.WriteLine(MyDB1[1]);
            Console.WriteLine(MyDB1[2]);
            Console.WriteLine(MyDB1[3]);
            Console.WriteLine(MyDB1[4]);
        }

        static void Vung_MB()
        {
            Doc_MB();
            Console.WriteLine(MyMB1[0]);
            Console.WriteLine(MyMB1[1]);
            Console.WriteLine(MyMB1[2]);
            Console.WriteLine(MyMB1[3]);
            Console.WriteLine(MyMB1[4]);
            Thread.Sleep(2000);
            Ghi_MB(data);
            Doc_MB();
            Console.WriteLine(MyMB1[0]);
            Console.WriteLine(MyMB1[1]);
            Console.WriteLine(MyMB1[2]);
            Console.WriteLine(MyMB1[3]);
            Console.WriteLine(MyMB1[4]);
        }

        static void Vung_AB()
        {
            Doc_AB();
            Console.WriteLine(MyAB1[0]);
            Console.WriteLine(MyAB1[1]);
            Console.WriteLine(MyAB1[2]);
            Console.WriteLine(MyAB1[3]);
            Console.WriteLine(MyAB1[4]);
            Thread.Sleep(2000);
            Ghi_AB(data);
            Doc_AB();
            Console.WriteLine(MyAB1[0]);
            Console.WriteLine(MyAB1[1]);
            Console.WriteLine(MyAB1[2]);
            Console.WriteLine(MyAB1[3]);
            Console.WriteLine(MyAB1[4]);
        }

        static void Vung_EB()
        {
            Doc_EB();
            Console.WriteLine(MyEB1[0]);
            Console.WriteLine(MyEB1[1]);
            Console.WriteLine(MyEB1[2]);
            Console.WriteLine(MyEB1[3]);
            Console.WriteLine(MyEB1[4]);
            Thread.Sleep(2000);
            Ghi_EB(data);
            Doc_EB();
            Console.WriteLine(MyEB1[0]);
            Console.WriteLine(MyEB1[1]);
            Console.WriteLine(MyEB1[2]);
            Console.WriteLine(MyEB1[3]);
            Console.WriteLine(MyEB1[4]);
        }
        //doc ghi DB
        static byte[] Doc_DB()
        {
            MyClient.DBRead(1, 0, 5, MyDB1);
            return MyDB1;
        }
        static void Ghi_DB(byte[] Value)
        {
            MyClient.DBWrite(1, 0, 6, Value);
        }
        //doc ghi AB - ngo ra
        static byte[] Doc_AB()
        {
            MyClient.ABRead(0, 5, MyAB1);
            return MyAB1;
        }
        static void Ghi_AB(byte[] Value)
        {
            MyClient.ABWrite(0, 3, Value);
        }
        //doc ghi EB-ngo vao
        static byte[] Doc_EB()
        {
            MyClient.EBRead(0, 5, MyEB1);
            return MyEB1;
        }
        static void Ghi_EB(byte[] Value)
        {
            MyClient.EBWrite(0, 3, Value);
        }
        //doc ghi MB - vung nho tam
        static byte[] Doc_MB()
        {
            MyClient.MBRead(0, 5, MyMB1);
            return MyMB1;
        }
        static void Ghi_MB(byte[] Value)
        {
            MyClient.MBWrite(0, 3, Value);
        }
    }
}
