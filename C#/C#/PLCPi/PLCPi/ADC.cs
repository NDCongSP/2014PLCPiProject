using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace PLCPiProject
{
    public class ADC
    {
        SerialPort mySerial = new SerialPort("/dev/ttyAMA0", 9600, Parity.None, 8, StopBits.One);
        string[] ADC_Array = { "I0", "I1", "I2", "I3", "I4", "I5", "I6", "I7", "I8", "I9", "I10", "I11", "I12" };
        Int32 Begin_Index = 0, End_Index = 0;
        string Serial_Data;//chuoi doc tu serial port co dinh dang "I0:0I1:0I2:0I3:0I4:0I5:0I6:0I7:0I8:0I9:0I10:0I11:0I12:0"
        string[] Slipt_STR;
        double a, b, Y, ADC_Return;

        /// <summary>
        ///Đọc giá trị ADC từ Module ADC gửi lên, qua Serial Port. Có 13 kênh AI, trong đó: kênh 0-4: 0-5VDC; 5-12: 4-20 mA. 
        /// Giá trị trả về kiểu double. Các đối số truyền vào phải thỏa điều kiện sau: (X0 < X1) và (Y0 < Y1)
        /// </summary>
        /// <param name="Chanel">kênh muốn đọc Chanel = 0,1,2,....,12</param>
        /// <param name="X0">Giá trị ADC nhỏ nhất. 0-5VDC: X0 = 0; 4-20mA: X0 = 204</param>
        /// <param name="Y0">Giá trị Analog nhỏ nhất</param>
        /// <param name="X1">Giá trị ADC lớn nhất</param>
        /// <param name="Y1">Giá trị Analog lớn nhất</param>
        /// <returns></returns>
        public double DocAI(Int16 Chanel, double X0, double Y0, double X1, double Y1)
        {
            mySerial.Open();
            Serial_Data = mySerial.ReadLine();
            if (Chanel < 10)
            {
                Begin_Index = Serial_Data.IndexOf(ADC_Array[Chanel]) + 3;
                End_Index = Serial_Data.IndexOf(ADC_Array[Chanel + 1]);
                ADC_Return = Scale(X0, Y0, X1, Y1, Convert.ToDouble(Serial_Data.Substring(Begin_Index, (End_Index - Begin_Index))));//gọi hàm Scale
            }
            else if ((Chanel >= 10) && (Chanel < 12))
            {
                Begin_Index = Serial_Data.IndexOf(ADC_Array[Chanel]) + 4;
                End_Index = Serial_Data.IndexOf(ADC_Array[Chanel + 1]);
                ADC_Return = Scale(X0, Y0, X1, Y1, Convert.ToDouble(Serial_Data.Substring(Begin_Index, (End_Index - Begin_Index))));
            }
            else if (Chanel == 12)
            {
                Slipt_STR = Serial_Data.Split(':');
                ADC_Return = Scale(X0, Y0, X1, Y1, Convert.ToDouble(Slipt_STR[13]));

            }
            mySerial.Close();
            return ADC_Return;
        }

        /// <summary>
        /// Method chuyển đổi giá trị ADC về lại giá trị thực(ví dụ như: nhiệt độ)
        /// </summary>
        /// <param name="X0"></param>
        /// <param name="Y0"></param>
        /// <param name="X1"></param>
        /// <param name="Y1"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private double Scale(double X0, double Y0, double X1, double Y1, double x)
        {
            if ((x < X0) || (x > X1) || (X0 > X1) || (Y0 > Y1))
            {
                Console.WriteLine("Các đối số truyền vào không thỏa điều kiện");
                return 0;
            }
            else
            {
                a = (Y1 - Y0) / (X1 - X0);
                b = Y0 - (X0 * a);
                Y = a * x + b;
            }
            return Y;
        }
    }
}
