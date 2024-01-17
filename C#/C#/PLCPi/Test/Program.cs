using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

class Program
{
    static string Line, Path_Temp = "/media/DHT21.txt";
    static string[] Data_Array, mang;
    static System.Diagnostics.Process proc = new System.Diagnostics.Process();
    static double a, b, Y, data;
    static void Main(string[] args)
    {
        byte[] mang = {1,2,3,4,5,6,7,8,9,10};
        Console.WriteLine(mang.Length);
        Console.ReadKey();
    }

    
    static double Scale(Int32 X0, Int32 Y0, Int32 X1, Int32 Y1, Int32 x)
    {
        a = Convert.ToDouble(Y1 - Y0) / Convert.ToDouble(X1 - X0);
        b = Convert.ToDouble(Y0 - Convert.ToDouble(X0 * a));
        Y = a * x + b;
        return Y;
    }
    static string[] Doc_DHT21()
    {
        proc.StartInfo.FileName = "sudo";
        proc.StartInfo.Arguments = "python /root/dht21.py";
        proc.Start();
        proc.WaitForExit();
        StreamReader file_T = new StreamReader(Path_Temp);
        Line = file_T.ReadToEnd();
        Console.WriteLine(Line);
        file_T.Dispose();
        Data_Array = Line.Split(';');
        return Data_Array;
    }
    static void Chay_Lenh_SYS()
    {
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = "sudo";
        proc.StartInfo.Arguments = "date -s\"10 DEC 2015 12:00:00\"";
        proc.Start();
        proc.WaitForExit();
        /*proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.RedirectStandardError = true;
        proc.StartInfo.RedirectStandardInput = true;
        proc.StartInfo.RedirectStandardOutput = true;*/
        //proc.StartInfo.FileName = "sudo";
        proc.StartInfo.Arguments = "hwclock -w";
        proc.Start();
        proc.WaitForExit();
        //var output = proc.StandardOutput.ReadToEnd();
        //Console.WriteLine("stdout: {0}", output);
    }
}
