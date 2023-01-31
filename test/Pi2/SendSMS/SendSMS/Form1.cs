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

namespace SendSMS
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        PLCPi myPLC = new PLCPi();
        string Port = null, TT = "BAD";
        string TT1 = "BAD";
        byte[] mang = { 0, 0, 0, 0, 0 };
        byte a = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Text = myPLC.SMS.Port_USB3G;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != myPLC.SMS.Port_USB3G)
            {
                myPLC.SMS.Port_USB3G = textBox3.Text;
            }
            MessageBox.Show(myPLC.SMS.GuiSMS(textBox1.Text, textBox2.Text));
        }
    }
}
