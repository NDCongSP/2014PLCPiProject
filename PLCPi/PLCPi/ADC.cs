using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace PLCPiProject
{
    /// <summary>
    /// Đối tượng tương tác với module AI
    /// để đọc giá trị analog từ 5 kênh áp (0-4) 0-10VDC
    /// và 8 kênh dòng (5-12) 4-20mA
    /// </summary>
    public class ADC
    {
        SerialPort mySerial = new SerialPort("/dev/ttyAMA0", 9600, Parity.None, 8, StopBits.One);
        string[] ADC_Array = { "I0", "I1", "I2", "I3", "I4", "I5", "I6", "I7", "I8", "I9", "I10", "I11", "I12" };
        Int16 Begin_Index = 0, End_Index = 0;
        string Serial_Data = null, Str_x = null, Serial_Data_Buffer = null;//chuoi doc tu serial port co dinh dang "-I0:0I1:0I2:0I3:0I4:0I5:0I6:0I7:0I8:0I9:0I10:0I11:0I12:0-#"
        //Serial_Data = "I0:0I1:0I2:0I3:0I4:0I5:0I6:0I7:0I8:0I9:0I10:0I11:0I12:0" . tách ra từ Serial_Data_Buffer bởi "-"
        string[] Slipt_STR = new string[14];
        Int16 BatDau = 0, KetThuc = 0;
        double a = 0, b = 0, Y = 0;
        string ADC_Return = "BAD"; //gia tri adc tra ve khi doc 1 kenh
        double x = 0; //giá trị ADC dọc lên
        string[] ADC_mang = { "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD" };//mang ADC tra ve khi doc tat ca cac kenh AI
        static bool _WorkingFlagADC = false;
        /// <summary>
        ///Đọc giá trị ADC từ 1 kênh trên module AI và biến đổi sang giá trị đại lượng đo. Có 13 kênh AI, trong đó: kênh 0-4: 0-10VDC; 5-12: 4-20 mA. 
        /// Giá trị trả về kiểu string là giá trị Analog
        /// </summary>
        /// <param name="Channel">kênh muốn đọc Channel = 0,1,2,....,12</param>
        /// <param name="X0">Giá trị phân giải ADC nhỏ nhất (kênh 0-10VDC: X0 = 0; kênh 4-20mA: X0 = 204)</param>
        /// <param name="Y0">Giá trị nhỏ nhất của đại lượng đo</param>
        /// <param name="X1">Giá trị phân giải ADC lớn nhất, trường hợp module AI 10 bit thì X1 = 1024</param>
        /// <param name="Y1">Giá trị lớn nhất của đại lượng đo</param>
        /// <returns></returns>
       public string DocAI1Kenh(Int16 Channel, double X0, double Y0, double X1, double Y1)
       {
           byte KiemTra = 0;
           try
           {
               if (_WorkingFlagADC == false)
               {
                   _WorkingFlagADC = true;
                   mySerial.Open();
                   Serial_Data_Buffer = mySerial.ReadLine();
                   while (KiemTra < 100)
                   {
                       if ((Serial_Data_Buffer.Contains("ABC-ABC-ABCI0") == false) || (Serial_Data_Buffer.Contains("ABC-ABC-ABC#") == false)
                           || (Serial_Data_Buffer.Contains("I1") == false) || (Serial_Data_Buffer.Contains("I2") == false)
                           || (Serial_Data_Buffer.Contains("I3") == false) || (Serial_Data_Buffer.Contains("I4") == false)
                           || (Serial_Data_Buffer.Contains("I5") == false) || (Serial_Data_Buffer.Contains("I6") == false)
                           || (Serial_Data_Buffer.Contains("I7") == false) || (Serial_Data_Buffer.Contains("I8") == false)
                           || (Serial_Data_Buffer.Contains("I9") == false) || (Serial_Data_Buffer.Contains("I10") == false)
                           || (Serial_Data_Buffer.Contains("I11") == false) || (Serial_Data_Buffer.Contains("I12") == false))
                       {
                           Serial_Data_Buffer = mySerial.ReadLine();
                           KiemTra++;
                       }
                       else
                       {
                           KiemTra = 200;
                       }
                   }
                   if (KiemTra >= 100 && KiemTra < 200)
                   {
                       return ADC_Return;
                   }

                   BatDau = Convert.ToInt16(Serial_Data_Buffer.IndexOf("ABC-ABC-ABCI0") + 11);
                   KetThuc = Convert.ToInt16(Serial_Data_Buffer.IndexOf("ABC-ABC-ABC#"));
                   Serial_Data = Serial_Data_Buffer.Substring(BatDau, (KetThuc - BatDau));

                   if (Channel < 10)
                   {
                       Begin_Index = Convert.ToInt16(Serial_Data.IndexOf(ADC_Array[Channel]) + 3);
                       End_Index = Convert.ToInt16(Serial_Data.IndexOf(ADC_Array[Channel + 1]));
                       Str_x = Serial_Data.Substring(Begin_Index, (End_Index - Begin_Index)); //giá trị ADC dạng chuỗi
                       try
                       {
                           x = Convert.ToDouble(Str_x);
                           ADC_Return = Scale(X0, Y0, X1, Y1, x);//gọi hàm Scale
                       }
                       catch { ADC_Return = "BAD"; }
                   }
                   else if ((Channel >= 10) && (Channel < 12))
                   {
                       Begin_Index = Convert.ToInt16(Serial_Data.IndexOf(ADC_Array[Channel]) + 4);
                       End_Index = Convert.ToInt16(Serial_Data.IndexOf(ADC_Array[Channel + 1]));
                       Str_x = Serial_Data.Substring(Begin_Index, (End_Index - Begin_Index));
                       try
                       {
                           x = Convert.ToDouble(Str_x);
                           ADC_Return = Scale(X0, Y0, X1, Y1, x);//gọi hàm Scale
                       }
                       catch { ADC_Return = "BAD"; }
                   }
                   else if (Channel == 12)
                   {
                       Slipt_STR = Serial_Data.Split(':');
                       try
                       {
                           x = Convert.ToDouble(Slipt_STR[13]);
                           ADC_Return = Scale(X0, Y0, X1, Y1, x);//gọi hàm Scale
                       }
                       catch { ADC_Return = "BAD"; }
                   }
                   mySerial.Close();
                   _WorkingFlagADC = false;
               }
               return ADC_Return ;
           }
           catch { mySerial.Close(); _WorkingFlagADC = false; return "BAD"; }
       }
        /// <summary>
       /// Đọc giá trị đồng loạt 13 kênh từ module AI (chưa biến đổi sang đại lượng đo). Có 13 kênh AI, trong đó: kênh 0-4: 0-10VDC; 5-12: 4-20 mA.
       ///Trả về mảng kiểu double 13 phần tử, chứa giá trị ADC. Phần tử 0-4: là giá trị ADC của 5 kênh AI 0-10VDC; phần tử 5-12: là giá trị ADC của 8 kênh AI 4-20mA
        /// </summary>
        /// <returns></returns>
       public string[] DocAI()
       {
           byte KiemTra = 0;
           try
           {
               if (_WorkingFlagADC == false)
               {
                   _WorkingFlagADC = true;
                   mySerial.Open();
                   Serial_Data_Buffer = mySerial.ReadLine();
                   while (KiemTra < 100)
                   {
                       if ((Serial_Data_Buffer.Contains("ABC-ABC-ABCI0") == false) || (Serial_Data_Buffer.Contains("ABC-ABC-ABC#") == false)
                           || (Serial_Data_Buffer.Contains("I1") == false) || (Serial_Data_Buffer.Contains("I2") == false)
                           || (Serial_Data_Buffer.Contains("I3") == false) || (Serial_Data_Buffer.Contains("I4") == false)
                           || (Serial_Data_Buffer.Contains("I5") == false) || (Serial_Data_Buffer.Contains("I6") == false)
                           || (Serial_Data_Buffer.Contains("I7") == false) || (Serial_Data_Buffer.Contains("I8") == false)
                           || (Serial_Data_Buffer.Contains("I9") == false) || (Serial_Data_Buffer.Contains("I10") == false)
                           || (Serial_Data_Buffer.Contains("I11") == false) || (Serial_Data_Buffer.Contains("I12") == false))
                       {
                           Serial_Data_Buffer = mySerial.ReadLine();
                           KiemTra++;
                       }
                       else
                       {
                           KiemTra = 200;
                       }
                   }
                   if (KiemTra >= 100 && KiemTra < 200)
                   {
                       return ADC_mang;
                   }

                   BatDau = Convert.ToInt16(Serial_Data_Buffer.IndexOf("ABC-ABC-ABCI0") + 11);
                   KetThuc = Convert.ToInt16(Serial_Data_Buffer.IndexOf("ABC-ABC-ABC#"));
                   Serial_Data = Serial_Data_Buffer.Substring(BatDau, (KetThuc - BatDau));

                   for (Int16 Channel = 0; Channel < 13; Channel++)
                   {
                       if (Channel < 10)
                       {
                           Begin_Index = Convert.ToInt16(Serial_Data.IndexOf(ADC_Array[Channel]) + 3);
                           End_Index = Convert.ToInt16(Serial_Data.IndexOf(ADC_Array[Channel + 1]));
                           Str_x = Serial_Data.Substring(Begin_Index, (End_Index - Begin_Index));
                           try
                           {
                               ADC_mang[Channel] = Convert.ToDouble(Str_x).ToString();
                           }
                           catch { ADC_mang[Channel] = "BAD"; }
                       }
                       else if ((Channel >= 10) && (Channel < 12))
                       {
                           Begin_Index = Convert.ToInt16(Serial_Data.IndexOf(ADC_Array[Channel]) + 4);
                           End_Index = Convert.ToInt16(Serial_Data.IndexOf(ADC_Array[Channel + 1]));
                           Str_x = Serial_Data.Substring(Begin_Index, (End_Index - Begin_Index));
                           try
                           {
                               ADC_mang[Channel] = Convert.ToDouble(Str_x).ToString();
                           }
                           catch { ADC_mang[Channel] = "BAD"; }
                       }
                       else if (Channel == 12)
                       {
                           Slipt_STR = Serial_Data.Split(':');
                           try
                           {
                               ADC_mang[12] = Convert.ToDouble(Slipt_STR[13]).ToString();
                           }
                           catch { ADC_mang[12] = "BAD"; }
                       }
                   }
                   mySerial.Close();
                   _WorkingFlagADC = false;
               }  
               return ADC_mang;
           }
           catch { mySerial.Close(); _WorkingFlagADC = false; return ADC_mang; }
       }
        /// <summary>
        /// Biến đổi giá trị ADC đọc về sang đại lượng đo bằng phương pháp biến đổi tuyến tính
        /// </summary>
        /// <param name="X0">giá trị MỐC ADC thứ nhất</param>
        /// <param name="Y0">giá trị quy ra đại lượng đo tương ứng MỐC thứ nhất</param>
        /// <param name="X1">giá trị MỐC ADC thứ hai</param>
        /// <param name="Y1">giá trị quy ra đại lượng đo tương ứng MỐC thứ hai</param>
        /// <param name="x">giá trị ADC cần chuyển đổi</param>
        /// <returns>giá trị đại lượng đo trả về tương ứng với giá trị ADC x</returns>
       public string Scale(double X0, double Y0, double X1, double Y1, double x)
        {
            try
            {
                a = (Y1 - Y0) / (X1 - X0);
                b = Y0 - (X0 * a);
                Y = (a * x) + b;

                return Y.ToString() ;
            }
            catch { return "BAD"; }

        }
    }
}
