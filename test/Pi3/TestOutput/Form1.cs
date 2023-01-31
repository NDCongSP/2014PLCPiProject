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

namespace TestOutput
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        PLCPi myPLC = new PLCPi();
        string[] MangNgoRa = {"Q0","Q1","Q2","Q3","Q4","Q5"};
        byte[] MangGiaTri1 = { 0, 1, 2, 4, 8, 16, 32, 64, 128, 0 };      
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myPLC.NgoRa.XuatNgoRa("Q0", 0);
            myPLC.NgoRa.XuatNgoRa("Q1", 0);
            myPLC.NgoRa.XuatNgoRa("Q2", 0);
            myPLC.NgoRa.XuatNgoRa("Q3", 0);
            myPLC.NgoRa.XuatNgoRa("Q4", 0);
            myPLC.NgoRa.XuatNgoRa("Q5", 0);
            
                   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {           
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    myPLC.NgoRa.XuatNgoRa(MangNgoRa[i], MangGiaTri1[j]);
                    System.Threading.Thread.Sleep(500);           
                }             
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
