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

namespace Email
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        PLCPi myPLC = new PLCPi();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SendButton.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myPLC.Email.CredentialEmail = textBox3.Text;
            myPLC.Email.CredentialPass = textBox4.Text;
            myPLC.Email.Message.From = new System.Net.Mail.MailAddress(textBox3.Text);
            myPLC.Email.Message.To.Clear();
            myPLC.Email.Message.To.Add(textBox1.Text);
            myPLC.Email.Message.Subject = "test";
            myPLC.Email.Message.Body = textBox2.Text;
            myPLC.Email.TimeOut = 2000;
            myPLC.Email.SendEmail();
            if (myPLC.Email.Error == false)
                label3.Text = "Good";
            else
                label3.Text = "Bad";
        }

        public string tex { get; set; }
    }
}
