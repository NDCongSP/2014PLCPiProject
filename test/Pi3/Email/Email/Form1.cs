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
using System.Diagnostics;

namespace Email
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        PLCPi myPLC = new PLCPi();
        //Email myPLC = new Email();
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
            try
            {
                myPLC.Email.CredentialEmail = textBox3.Text;// "giamsat.canhbao@gmail.com";
                myPLC.Email.CredentialPass = textBox4.Text;// "ujgknqghwcjqxfvq";
                myPLC.Email.Port = 587;

                myPLC.Email.emailTo = textBox1.Text;

                myPLC.Email.subjectEmail = "Alarm";
                //string strAlarm = "Location : " + location.Name + Environment.NewLine
                //     + "Alarm: " + alarm + Environment.NewLine
                //     + "Value: " + location.Value + Environment.NewLine
                //     + "Low Level: " + (location.LowLevel.HasValue ? location.LowLevel.Value.ToString() : "") + Environment.NewLine
                //     + "High Level: " + (location.HighLevel.HasValue ? location.HighLevel.Value.ToString() : "");
                string strAlarm = $"{DateTime.Now} - TEST EMAIL.";
                Debug.WriteLine($"EMAIL {strAlarm}");
                myPLC.Email.bodyEmail = strAlarm;

                myPLC.Email.SendEmail();

                if (myPLC.Email.Error == false)
                {
                    Console.WriteLine($"Gui Email Good");
                }
                else
                {
                    Console.WriteLine($"Gui Email Fail");
                }


                //myPLC.CredentialEmail = "giamsat.canhbao@gmail.com";
                //myPLC.CredentialPass = "ujgknqghwcjqxfvq";
                //myPLC.Port = 587;

                //myPLC.emailTo = textBox1.Text;
                ////myPLC.emailTo = "ndcong08cddv02@gmail.com,phucthinh.automations@gmail.com";
                //myPLC.subjectEmail = "TEST EMAIL.";

                //Console.WriteLine($"EMAIL {textBox2.Text}");
                //myPLC.bodyEmail = textBox2.Text;

                //myPLC.TimeOut = 2000;
                //myPLC.SendEmail();
                //if (myPLC.Error == false)
                //{
                //    label3.Text = "Good";
                //}
                //else
                //{
                //    label3.Text = "Bad";
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi gửi Email : {ex.Message}");
            }

        }

        public string tex { get; set; }
    }
}
