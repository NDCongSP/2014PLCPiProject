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

namespace TestIn
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        PLCPi myPLC = new PLCPi();
        static string[] NgoVao = {"I0.0","I0.1","I0.2","I0.3","I0.4","I0.5","I0.6","I0.7"
                                 ,"I1.0","I1.1","I1.2","I1.3","I1.4","I1.5","I1.6","I1.7"
                                 ,"I2.0","I2.1","I2.2","I2.3","I2.4","I2.5","I2.6","I2.7"
                                 ,"I3.0","I3.1","I3.2","I3.3","I3.4","I3.5","I3.6","I3.7"
                                 ,"I4.0","I4.1","I4.2","I4.3","I4.4","I4.5","I4.6","I4.7"};
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tbChuoiNgoVao0.Text = TestNgoVao(tbChonNgoVao0.Text);
            tbChuoiNgoVao1.Text = TestNgoVao(tbChonNgoVao1.Text);
            tbChuoiNgoVao2.Text = TestNgoVao(tbChonNgoVao2.Text);
            tbChuoiNgoVao3.Text = TestNgoVao(tbChonNgoVao3.Text);
            tbChuoiNgoVao4.Text = TestNgoVao(tbChonNgoVao4.Text);
        }
        string TestNgoVao(string SoNgoVao)
        {
            string chuoingovao = "";
            int j = 8 * Convert.ToInt32(SoNgoVao);
            for (int i = j; i <= j + 7; i++)
            {
                chuoingovao += (myPLC.NgoVao.DocNgoVao(NgoVao[i]).ToString() + " ");
            }
            return chuoingovao;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
