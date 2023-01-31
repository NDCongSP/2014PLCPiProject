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

namespace S7Client
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        PLCPi myPLC = new PLCPi();        

        byte[] Data = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] Data1 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        byte[] Data2 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        byte DemLoi=0, BienDemGuiSMS = 0;
        
        public Form1()
        {
            InitializeComponent();
        } 

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox4.Text = myPLC.S7Ethernet.Client.SoLanDoc.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox12.Text = "";
                if (Data2[0] == 255)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Data[i] = 0;
                        Data1[i] = 1;
                        Data2[i] = 2;
                    }
                }


                label20.BackColor = Color.Green;
                BienDemGuiSMS = 0;
                //đọc vùng nhớ ngõ vào
                DocS7 myDocS7 = myPLC.S7Ethernet.Client.DocIB(0, 10);
                if (myDocS7 != null)
                {
                    if (myDocS7.TrangThai == "GOOD")
                    {
                        label20.BackColor = Color.Green;
                        label14.Text = "GOOD";
                        foreach (byte b in myDocS7.MangGiaTri)
                            textBox12.Text = textBox12.Text + "|" + b.ToString();
                        DemLoi = 0;
                    }
                    else
                    {
                        label14.Text = "BAD";
                        label20.BackColor = Color.Red;

                        DemLoi++;
                    }
                }
                else
                {
                    label14.Text = "BAD";
                    label20.BackColor = Color.Red;
                    DemLoi++;
                }
                //đọc vùng nhớ ngõ ra
                DocS7 myDocS71 = myPLC.S7Ethernet.Client.DocQB(0, 10);
                if (myDocS71 != null)
                {
                    if (myDocS71.TrangThai == "GOOD")
                    {
                        label20.BackColor = Color.Green;
                        label14.Text = "GOOD";
                        foreach (byte b in myDocS71.MangGiaTri)
                            textBox1.Text = textBox1.Text + "|" + b.ToString();
                        DemLoi = 0;
                    }
                    else
                    {
                        label14.Text = "BAD";
                        label20.BackColor = Color.Red;
                        DemLoi++;
                    }
                }
                else
                {
                    label14.Text = "BAD";
                    label20.BackColor = Color.Red;
                    DemLoi++;
                }
                //đọc vùng nhớ data block
                DocS7 myDocS72 = myPLC.S7Ethernet.Client.DocDB(1, 0, 10);
                if (myDocS72 != null)
                {
                    if (myDocS72.TrangThai == "GOOD")
                    {
                        label20.BackColor = Color.Green;
                        label14.Text = "GOOD";
                        foreach (byte b in myDocS72.MangGiaTri)
                            textBox2.Text = textBox2.Text + "|" + b.ToString();
                        DemLoi = 0;
                    }
                    else
                    {
                        label14.Text = "BAD";
                        label20.BackColor = Color.Red;
                        DemLoi++;
                    }
                }
                else
                {
                    label14.Text = "BAD";
                    label20.BackColor = Color.Red;
                    DemLoi++;
                }
                //đọc vùng nhớ memory block
                DocS7 myDocS73 = myPLC.S7Ethernet.Client.DocMB(0, 10);
                if (myDocS73 != null)
                {
                    if (myDocS73.TrangThai == "GOOD")
                    {
                        label20.BackColor = Color.Green;
                        label14.Text = "GOOD";
                        foreach (byte b in myDocS73.MangGiaTri)
                            textBox3.Text = textBox3.Text + "|" + b.ToString();
                        DemLoi = 0;
                    }
                    else
                    {
                        label14.Text = "BAD";
                        label20.BackColor = Color.Red;
                        DemLoi++;
                    }
                }
                else
                {
                    label14.Text = "BAD";
                    label20.BackColor = Color.Red;
                    DemLoi++;
                }
                //Ghi
                //ghi vùng nhớ Ngõ ra
                if (myPLC.S7Ethernet.Client.GhiQB(0, 10, Data2) == "GOOD")
                {
                    label20.BackColor = Color.Green;
                    label4.Text = "GOOD";
                    DemLoi = 0;
                }
                else
                {
                    label4.Text = "BAD";
                    label20.BackColor = Color.Red;
                    DemLoi++;
                }
                //ghi vùng nhớ data block
                if (myPLC.S7Ethernet.Client.GhiDB(1, 0, 10, Data) == "GOOD")
                {
                    label20.BackColor = Color.Green;
                    label4.Text = "GOOD";
                    DemLoi = 0;
                }
                else
                {
                    label4.Text = "BAD";
                    label20.BackColor = Color.Red;
                    DemLoi++;
                }
                //ghi vùng nhớ memory block
                if (myPLC.S7Ethernet.Client.GhiMB(0, 10, Data1) == "GOOD")
                {
                    label20.BackColor = Color.Green;
                    label4.Text = "GOOD";
                    DemLoi = 0;
                }

                else
                {
                    label4.Text = "BAD";
                    label20.BackColor = Color.Red;
                    DemLoi++;
                }
                //myPLC.S7Ethernet.Client.NgatKetNoi();


                //tang gia tri de ghi xuong cac vung nho

                for (byte i2 = 0; i2 < 10; i2++)
                {
                    Data[i2] = Convert.ToByte(Data[i2] + 1);
                    Data1[i2] = Convert.ToByte(Data1[i2] + 1);
                    Data2[i2] = Convert.ToByte(Data2[i2] + 1);
                }
                if (DemLoi >= 14)
                {
                    DemLoi = 0;
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = "sudo";
                    proc.StartInfo.Arguments = "ping google.com";
                    proc.Start();

                    Thread.Sleep(2000);

                    if (myPLC.S7Ethernet.Client.KetNoi(textBox5.Text) == "GOOD")
                    {
                        label20.BackColor = Color.Green;
                    }

                    proc.Close();
                    proc.Kill();
                }
                label7.Text = DemLoi.ToString();

                timer1.Start();
            }
            catch { timer1.Start(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (textBox4.Text != myPLC.S7Ethernet.Client.SoLanDoc.ToString())
                myPLC.S7Ethernet.Client.SoLanDoc = Convert.ToByte(textBox4.Text);
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (myPLC.S7Ethernet.Client.KetNoi(textBox5.Text) == "GOOD")
            {
                label20.BackColor = Color.Green;
            }
            else
                label20.BackColor = Color.Red;
            timer1.Enabled = true;
        }
    }
}