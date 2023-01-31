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
                ADC_Mang = myPLC.AI.DocAI();
                textBox1.Text = "AI0 = " + ADC_Mang[0] + " | " + "AI1 = " + ADC_Mang[1] + " | " + " = " + ADC_Mang[2] + " | " + "AI3 = " + ADC_Mang[3] + " | " +
                    "AI4 = " + ADC_Mang[4] + " | " + "AI5 = " + ADC_Mang[5] + " | " + "AI6 = " + ADC_Mang[6] + " | " + "AI7 = " + ADC_Mang[7] + " | " +
                    "AI8 = " + ADC_Mang[8] + " | " + "AI9 = " + ADC_Mang[9] + " | " + "AI10 = " + ADC_Mang[10] + " | " + "AI11 = " + ADC_Mang[11] + " | " +
                    "AI12 = " + ADC_Mang[12];
                textBox2.Text = myPLC.AI.Scale(0, 0, 1024, 10, Convert.ToDouble(ADC_Mang[4]));
                textBox3.Text = myPLC.AI.Scale(204, 0, 1024, 400, Convert.ToDouble(ADC_Mang[12]));
            }
            catch { }
        }
    }
}
