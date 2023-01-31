using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Raspberry.IO.GeneralPurpose;

namespace PLCPiProject
{
    public class IC595_HienThi
    {
        ProcessorPin data = ConnectorPin.P1Pin16.ToProcessor();
        ProcessorPin dich = ConnectorPin.P1Pin22.ToProcessor();
        ProcessorPin xuat = ConnectorPin.P1Pin18.ToProcessor();
        IGpioConnectionDriver driver = GpioConnectionSettings.DefaultDriver;

        byte[] ma_led_7 = { 0x14, 0x7E, 0x8C, 0x2C, 0x66, 0x25, 0x05, 0x7C, 0x04, 0x24, 0xFB, 0xEF, 0x85, 0xCF, 0xFF }; //14 phan tu
        byte[] buffer_array = new byte[8];
        /// <summary>
        /// dịch dữ liệu ra 595 hiển thị Led7
        /// </summary>
        /// <param name="DuLieu">mảng dữ liệu cần dịch</param>
        /// <param name="DauCham1">để điều khiển dấu chấm động. </param>
        /// <param name="DauCham2">để điều khiển dấu chấm động. </param>
        public void DichDuLieu(byte[] DuLieu, Int16 DauCham1, Int16 DauCham2)
        {
            try
            {
                Int16 bb = 0;
                foreach (byte item in DuLieu)
                {
                    buffer_array[bb] = ma_led_7[item];
                    bb += 1;
                }
                if (DauCham1 == 0)
                {
                    buffer_array[6] = Convert.ToByte(buffer_array[6] & ma_led_7[10]);
                }
                else if (DauCham1 == 1)
                {
                    buffer_array[5] = Convert.ToByte(buffer_array[5] & ma_led_7[10]);
                }

                if (DauCham2 == 0)
                {
                    buffer_array[2] = Convert.ToByte(buffer_array[2] & ma_led_7[10]);
                }
                else if (DauCham2 == 1)
                {
                    buffer_array[1] = Convert.ToByte(buffer_array[1] & ma_led_7[10]);
                }


                for (int j = 0; j < 8; j++)
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
            catch (Exception ex) { throw ex; }
        }

        //constructor
        public IC595_HienThi()
        {
            try
            {
                driver.Allocate(data, PinDirection.Input);
                driver.Allocate(dich, PinDirection.Input);
                driver.Allocate(xuat, PinDirection.Input);

                driver.Allocate(data, PinDirection.Output);
                driver.Allocate(dich, PinDirection.Output);
                driver.Allocate(xuat, PinDirection.Output);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
