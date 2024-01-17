using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiDotNet;

namespace PLCPiProject
{
    class IC595
    {
        GPIOFile data = new GPIOFile(GPIOPins.V2_Pin_P1_11, GPIODirection.Out);
        GPIOFile Xuat = new GPIOFile(GPIOPins.V2_Pin_P1_13, GPIODirection.Out);
        GPIOFile Dich = new GPIOFile(GPIOPins.V2_Pin_P1_15, GPIODirection.Out);
        byte a = 0x80;
        //Method export595
        //Dat lun bang tieng viet di
        //vi đối tượng khách hàng của mình ở đây là người việt
        /// <summary>
        /// Huống dẫn ở đây
        /// </summary>
        public void DichDuLieu(int[] DuLieu)
        {
            
            for (int j = 0; j <6; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    if ((DuLieu[j] & a) == 0x80)
                    {
                        data.Write(PinState.High);
                        Console.WriteLine("11");
                    }
                    else
                    {
                        data.Write(PinState.Low);
                        Console.WriteLine("00");
                    }
                    Dich.Write(PinState.Low);
                    Dich.Write(PinState.High);
                    DuLieu[j] <<= 1;
                }
                Xuat.Write(PinState.Low);
                Xuat.Write(PinState.High);
            }
        }
            
        //còn có thể có nhiều method hoặc properties khác, phù hợp với yêu cầu của em

        //Constructor
        //nếu ko cần khởi tạo gì đầu tiên thì ko cần khai báo method constructor này
        //vì hướng dẫn nên anh cứ tạo nhưng ko bỏ gì vào
        public IC595() { } 
    }
}
