using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLCPiProject;
using System.Threading;

namespace PLCPiTestSnap7
{
    class Program
    {
        static byte[] data = { 150, 190, 180, 170, 160,150,140,120,110,100,90,80,60,50,40,30,20,10 }; // du lieu de ghi xuong cac vung nho
        static byte[] data1 = { 0, 1, 2, 3, 4, 5,6,7,8,9,10,11 };
        static byte[] data2 = { 9,8,7,6, 5, 4, 3, 2, 1, 0 };
        static byte[] data3 = { 10, 11, 12, 13, 14, 15, 16, 17,18,19};

        static string error;
        static byte[] mang = new byte[5];//chua du lieu doc tu cac vung nho len

        static PLCPi myPLCPi = new PLCPi();

        static void Main(string[] args)
        {
            //Snap7_Client();
            Snap7_Server();

        }
        //Snap7 Server
        static void Snap7_Server()
        {
            string error = myPLCPi.Snap7.Server.KetNoi(); //chay server
            if (error == "GOOD")
            {
                Console.ReadKey();
                myPLCPi.NgoVao.DocNgoVao("I0");
                Console.ReadKey();
                myPLCPi.NgoVao.DocNgoVao("I0");
                myPLCPi.NgoVao.DocNgoVao("I1");
                Console.ReadKey();
                myPLCPi.NgoVao.DocNgoVao("I0");
                myPLCPi.NgoVao.DocNgoVao("I1");
                myPLCPi.NgoVao.DocNgoVao("I2");
                Console.ReadKey();
                myPLCPi.NgoVao.DocNgoVao("I0");
                myPLCPi.NgoVao.DocNgoVao("I1");
                myPLCPi.NgoVao.DocNgoVao("I2");
                myPLCPi.NgoVao.DocNgoVao("I3");
                Console.ReadKey();
                myPLCPi.NgoVao.DocNgoVao("I0");
                myPLCPi.NgoVao.DocNgoVao("I1");
                myPLCPi.NgoVao.DocNgoVao("I2");
                myPLCPi.NgoVao.DocNgoVao("I3");
                myPLCPi.NgoVao.DocNgoVao("I4");
                Console.ReadKey();
                
                myPLCPi.NgoVao.DocNgoVao("I0");
                Console.ReadKey();
                myPLCPi.NgoVao.DocNgoVao("I1");
                Console.ReadKey();
                myPLCPi.NgoVao.DocNgoVao("I2");
                Console.ReadKey();
                myPLCPi.NgoVao.DocNgoVao("I3");
                Console.ReadKey();
                myPLCPi.NgoVao.DocNgoVao("I4");
                Console.ReadKey();
                //myPLCPi.NgoRa.XuatNgoRa("Q0", 2);
                //Console.ReadKey();
                //myPLCPi.NgoRa.XuatNgoRa("Q1", 4);
                //Console.ReadKey();
                //myPLCPi.NgoRa.XuatNgoRa("Q2", 8);
                //Console.ReadKey();
                //myPLCPi.NgoRa.XuatNgoRa("Q3", 16);
                //Console.ReadKey();
                //myPLCPi.NgoRa.XuatNgoRa("Q4", 32);
                //Console.ReadKey();
                //myPLCPi.NgoRa.XuatNgoRa("Q5", 64);
                Console.ReadKey();
                
            }
            else
            {
                Console.WriteLine(error);
            }
        }

        //Snap7 Client
        static void Snap7_Client()
        {
            //string ip = "192.168.1.105";
            Console.WriteLine("IP Server");
            string ip = Console.ReadLine();
            

            error = myPLCPi.Snap7.Client.KetNoi(ip);

            while (true)
            {
                Console.WriteLine("doc");
                Int16 a = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("ghi");
                Int16 b = Convert.ToInt16(Console.ReadLine());
                Console.ReadKey();
                Doc(a);

                Console.ReadKey();
                GHI(b);
                Console.ReadKey();

                Doc(a);
                Console.ReadKey();
            }
        }
        //doc vung nho
        static void Doc(Int16 x)
        {
            if (x == 0)
            {
                mang = myPLCPi.Snap7.Client.DocDB(1, 0, 5);
            }
            else if (x == 1)
            {
                mang = myPLCPi.Snap7.Client.DocNgoVao(1,5);
            }
            else if (x == 2)
            {
                mang = myPLCPi.Snap7.Client.DocNgoRa(2, 5);
            }
            else if (x == 3)
            {
                mang = myPLCPi.Snap7.Client.DocMB(3, 5);
            }
            Console.WriteLine(mang[0]);
            Console.WriteLine(mang[1]);
            Console.WriteLine(mang[2]);
            Console.WriteLine(mang[3]);
            Console.WriteLine(mang[4]);
            //Console.WriteLine(mang[5]);
            //Console.WriteLine(mang[6]);
            //Console.WriteLine(mang[7]);
            //Console.WriteLine(mang[8]);
            //Console.WriteLine(mang[9]);
            //Console.WriteLine(mang[10]);
            //Console.WriteLine(mang[11]);
            //Console.WriteLine(mang[12]);
            //Console.WriteLine(mang[13]);
            //Console.WriteLine(mang[14]);
            //Console.WriteLine(mang[15]);
            //Console.WriteLine(mang[16]);
            //Console.WriteLine(mang[17]);
            //Console.WriteLine(mang[18]);
            //Console.WriteLine(mang[19]);
        }

        //ghi vung nho
        static void GHI(Int16 x)
        {
            
            if (x == 0)
            {
                error = myPLCPi.Snap7.Client.GhiDB(1, 0, 18, data);
            }
            else if (x == 1)
            {
                error = myPLCPi.Snap7.Client.GhiNgoVao(1, 5, data1);
            }
            else if (x == 2)
            {
                error = myPLCPi.Snap7.Client.GhiNgoRa(2, 5, data2);
            }
            else if (x == 3)
            {
                error = myPLCPi.Snap7.Client.GhiMB(3, 5, data3);
            }
        }
        
    }
}
