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

namespace S7Server
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        PLCPi myPLC = new PLCPi();
        string trangthai;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trangthai = myPLC.S7Ethernet.Server.Khoitao();

            if (trangthai == "GOOD")
            {
                panel1.BackColor = Color.Green;

                _txtDB0.KeyDown += (s, o) =>
                {
                    if (o.KeyCode==Keys.Enter)
                    {
                        var t = (TextBox)s;
                        var v = Convert.ToByte(_txtDB0.Text);
                        myPLC.S7Ethernet.Server.DataBlock[0] = (byte)v;
                    }
                };

                _txtDB15.KeyDown += (s, o) =>
                {
                    if (o.KeyCode == Keys.Enter)
                    {
                        var t = (TextBox)s;
                        var v = Convert.ToByte(_txtDB15.Text);
                        myPLC.S7Ethernet.Server.DataBlock[15] = (byte)v;
                    }
                };
            }
            else
            {
                panel1.BackColor = Color.Red;
            }
            timer1.Enabled = true;
            System.Threading.Thread.Sleep(1000);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (this.InvokeRequired)
            {
                label2.Text = Convert.ToString(myPLC.S7Ethernet.Server.DataBlock[1]);
                _txtDB0.Text = Convert.ToString(myPLC.S7Ethernet.Server.DataBlock[0]);
                _txtDB15.Text = Convert.ToString(myPLC.S7Ethernet.Server.DataBlock[15]);
            }
            else
            {
                label2.Text = Convert.ToString(myPLC.S7Ethernet.Server.DataBlock[1]);
                _txtDB0.Text = Convert.ToString(myPLC.S7Ethernet.Server.DataBlock[0]);
                _txtDB15.Text = Convert.ToString(myPLC.S7Ethernet.Server.DataBlock[15]);
            }
            timer1.Enabled = true;
        }


    }
}
