using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PLCPiProject
{
    public class PLCPi
    {
        /// <summary>
        /// Đối tượng Set giá trị cho các ngõ ra của PLCPi
        /// </summary>
        public Ngo_Ra NgoRa = new Ngo_Ra();
        /// <summary>
        /// Dối tượng đọc trạng thái các ngõ vào của PLCPi
        /// </summary>
        public Ngo_Vao NgoVao = new Ngo_Vao();
        /// <summary>
        /// Đối tượng đọc các ngõ vào Analog
        /// </summary>
        public ADC AI = new ADC();
        /// <summary>
        /// Đối tượng đọc cảm biến nhiệt độ DS18B20
        /// </summary>
        public DS18B20 DS18B20 = new DS18B20();
        /// <summary>
        /// Đối tượng hiển thị cho module hiển thị led7
        /// </summary>
        public Hien_Thi HienThiLed7 = new Hien_Thi();
        /// <summary>
        /// Đối tượng thời gian, dùng để lấy thời gian từ PLCPi, và cài đặt thời gian cho PLCPi
        /// </summary>
        public Doc_Thoi_Gian ThoiGian = new Doc_Thoi_Gian();
        /// <summary>
        /// Đối tượng đọc cảm biến nhiệt độ độ ẩm DHT21
        /// </summary>
        public DHT21 DHT21 = new DHT21();
        /// <summary>
        /// Đối tượng truyền thông 
        /// </summary>
        public Snap7 Snap7 = new Snap7();

        //constructor
        public PLCPi() 
        {
            Snap7.Server.NgoVao = NgoVao;
            Snap7.Server.NgoRa = NgoRa;
        } 
    }
}
