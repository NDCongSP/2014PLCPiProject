using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using PLCPiProject;

namespace DHT21
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        SHT10 myPLC = new SHT10();
        string temp, hum;
        string[] GiatriArr = { "0", "0" };
        string[] GiatriArr1 = { "0", "0" };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //temp = myPLC.DocNhietDo();
            //hum = myPLC.DocDoAm();

            GiatriArr = myPLC.DocNhietDoDoAm();
            //myPLC.HienThiLed.HienThi(temp, 1);
            //myPLC.HienThiLed.HienThi(hum, 2);
            textBox1.Text = GiatriArr[0];
            textBox2.Text = GiatriArr[1];
            Console.WriteLine(GiatriArr[0] + "|" + GiatriArr[1] + "|" + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));
            timer1.Enabled = true;
        }
    }
}
