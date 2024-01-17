using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCPiProject
{
    public class PLCPi
    {
        //Method Input
        //3 dấu xẹt là để viết ghi chú, để khi người ta dùng đối tượng của lớp này, vừa gọi hàm này 
        //thì nó sẽ xuất hiện nội dung gợi ý ra chính là các ghi chú ở đây, người ta đọc sẽ hiểu là đang làm gì
        //PLCPi thì em ghi chú băng tiếng Việt luôn
        /// <summary>
        /// Phương thức này dùng để đọc giá trị của 1 dạng ngõ vào về        
        /// </summary>
        /// <param name="Type">Tên ngõ vào cần đọc. "V0.0": đọc bit, "V0": đọc byte</param>
        /// <returns></returns>
        public string Input(string Type)
        {
            //Code here
            return null;//trả rỗng về, em lập trình trong này rồi trả về sau
        }

        /// <summary>
        /// Em giải thích cụ thể ở đây, để đỡ viết nhiều lúc hướng dẫn họ, lúc lập trình họ đọc gợi ý này là biết làm liền        
        /// </summary>
        /// <param name="Type">Đây là giải thích cho đối số Type</param>
        /// <param name="Value">Thêm đối số này nữa</param>
        public void Output(string Type, int Value)
        {
            //Code here
            //Chỗ này phải dịch 595 đúng ko
            //nên sẽ triển khai 1 đối tượng của IC595 ở đây
            IC595 myIC595 = new IC595();
            //họi method dịch dữ liệu vì mình cần dịch
            myIC595.DichDuLieu();

            string laydata = myIC595.Hello; 
            //void là ko trả về gì cả
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string Read_Output(string Type)
        {
            //Code here
            return null;//trả rỗng về, em lập trình trong này rồi trả về sau
        }

        //vâng vâng

        //constructor
        public PLCPi() { } 
    }
}
