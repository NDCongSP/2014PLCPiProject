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

namespace ADC
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        PLCPi myPLC = new PLCPi();
        string[] ADC_Mang = new string[13];
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
            
            try
            {
                //ADC_Mang = myPLC.AI.DocAI();
                textBox1.Text = "AI0 = " + myPLC.AI.DocAI1Kenh(0, 0, 0, 1024, 10) + " | " + "AI1 = " + myPLC.AI.DocAI1Kenh(1, 0, 0, 1024, 10) + " | "
                    + "AI5 = " + myPLC.AI.DocAI1Kenh(5, 204, 0, 1024, 10) + " | " + "AI6 = " + myPLC.AI.DocAI1Kenh(6, 204, 0, 1024, 100);
                //textBox2.Text = myPLC.AI.Scale(0, 0, 1024, 10, Convert.ToDouble(ADC_Mang[4]));
                //textBox3.Text = myPLC.AI.Scale(204, 0, 1024, 400, Convert.ToDouble(ADC_Mang[12]));
            }
            catch { }
        }
    }
}
