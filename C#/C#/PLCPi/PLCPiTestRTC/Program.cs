using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PLCPiProject;

namespace PLCPiTestRTC
{
    class Program
    {
        static PLCPi myPLCPi = new PLCPi();
        static Int16[] mang;
        static void Main()
        {
            //while (true)
            //{
            test_docTG();
            test_caidat();
            test_docTG();
            Console.ReadKey();
            //}
        }
        static void test_docTG()
        {
            mang = myPLCPi.ThoiGian.DocThoiGian();// doc thoi gian tu PLC
            Console.WriteLine("ngay {0} thang {1} nam {2} gio {3} phut {4} giay {5}", mang[0], mang[1], mang[2], mang[3], mang[4], mang[5]);
        }

        static void test_caidat()
        {
            myPLCPi.ThoiGian.CaiDat("07-05-2015 15:50:00");//cai dat thoi gian cho PLC
            myPLCPi.ThoiGian.CaiDatRTC();//cai dat thoi gian xuong module RTC ds1307
        }
    }
}
