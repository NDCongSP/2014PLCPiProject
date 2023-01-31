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
        PLCPi myPLC = new PLCPi();
        string temp, hum;
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
            temp = myPLC.DHT21.DocNhietDo();
            hum = myPLC.DHT21.DocDoAm();
            //myPLC.HienThiLed.HienThi(temp, 1);
            //myPLC.HienThiLed.HienThi(hum, 2);
            textBox1.Text = temp;
            textBox2.Text = hum;
        }

        
    }
}
