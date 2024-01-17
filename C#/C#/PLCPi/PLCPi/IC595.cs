using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Raspberry.IO.GeneralPurpose;

namespace PLCPiProject
{
    public class IC595
    {
        ProcessorPin data = ConnectorPin.P1Pin11.ToProcessor();
        ProcessorPin dich = ConnectorPin.P1Pin15.ToProcessor();
        ProcessorPin xuat = ConnectorPin.P1Pin13.ToProcessor();
        IGpioConnectionDriver driver = GpioConnectionSettings.DefaultDriver;

        byte[] buffer_array = new byte[6];
       /// <summary>
       /// dịch dữ liệu vào 595 để xuất ra ngõ ra
        /// buffer_array[0] sẽ được dịch đầu tiên. tương ứng với R5
       /// </summary>
       /// <param name="DuLieu">mảng 6 phần tử, chứa dữ liệu cần xuất ra</param>
        public void DichDuLieu(byte[] DuLieu)
        {
            Int16 bb = 5;
            foreach (byte item in DuLieu)
            {
                buffer_array[bb] = item;
                bb -= 1;
            }    
            for (int j = 0; j < 6; j++)
            {  
                for (int i = 0; i < 8; i++)
                {
                    if ((buffer_array[j] & 0x80) == 0x80)
                    {
                        driver.Write(data, true);
                    }
                    else
                    {
                        driver.Write(data, false);
                    }
                    driver.Write(dich, false);
                    driver.Write(dich, true);
                    buffer_array[j] <<= 1;
                }
            }
            driver.Write(xuat, false);
            driver.Write(xuat, true);
        } 
    //constructor
        public IC595()
        {
            driver.Allocate(data, PinDirection.Input);
            driver.Allocate(dich, PinDirection.Input);
            driver.Allocate(xuat, PinDirection.Input );

            //System.Threading.Thread.Sleep(1);
 
            driver.Allocate(data, PinDirection.Output);
            driver.Allocate(dich, PinDirection.Output);
            driver.Allocate(xuat, PinDirection.Output);
        }
    }
}
