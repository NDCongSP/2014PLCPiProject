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
using MySql.Data.MySqlClient;
using System.IO;

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

        //Tao doi tuong myPLC
        //static PLCPi myPLCpi = new PLCPi();
        static string _path = $"D:\\ATPro\\CodeProject\\GatewayPi\\WeblogMVC\\SourceCode\\";
        //static string _path = $"/home/pi/";
        static string SMSString = "", ServerIpAddress = "", noiDung = "";
        static string ConnectionString = "";
        static DataTable bangSMS = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string temp = ReadText(_path + "enableSMSEmail.txt");
            string[] prams = temp.Split(',');
            ServerIpAddress = prams[4].Trim();

            Debug.WriteLine($"EnableSMS/EnableEmail/LogType/LogRate/ServerIp: {temp}");
            ConnectionString = $"user id=root;password=100100;database=gateway;server={ServerIpAddress};convertzerodatetime=True;port=3306";

            SMSString = ReadText(_path + "sms.txt").Trim();
            Debug.WriteLine($"{ConnectionString}|SDT {SMSString}");

 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != myPLC.SMS.Port_USB3G)
            {
                myPLC.PhoneCall.Port_USB3G1 = textBox3.Text;
            }
            MessageBox.Show(myPLC.PhoneCall.GoiDienThoai(textBox1.Text.Trim()));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myPLC.SMS.Port_USB3G = textBox3.Text.Trim();
            label4.Text = myPLC.SMS.Khoitao() + "|" + myPLC.SMS.Port_USB3G;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //Debug.WriteLine($"tong sdt nhan SMS {textBox1.Text.Trim().Split(',').Length}");
            //Debug.WriteLine($"SDT {textBox1.Text}");
            //string noidung = $"{DateTime.Now}\nLocation: 1\nAlarm: Alarm\nValue: 15.5" +
            //        $"\nLow Level: 10" +
            //        $"\nHigh Level: 2";
            ////for (int i = 0; i < textBox1.Text.Trim().Split(',').Length; i++)
            //{
            //    Debug.WriteLine(myPLC.SMS.GuiSMS(textBox1.Text.Trim(), noidung));
            //    //Thread.Sleep(2000);
            //}

            bangSMS.Clear();
            bangSMS = GetSMS();
            if (bangSMS != null && bangSMS.Rows.Count > 0)
            {
                for (int i = 0; i < bangSMS.Rows.Count; i++)
                {
                    noiDung = $"{DateTime.Now}\nLocation: {bangSMS.Rows[i][1].ToString()}\nAlarm: {bangSMS.Rows[i][2].ToString()}\nValue: {bangSMS.Rows[i][3].ToString()}" +
                 $"\nLow Level: {bangSMS.Rows[i][4].ToString()}" +
                 $"\nHigh Level: {bangSMS.Rows[i][5].ToString()}";
                    Debug.WriteLine($"noi dung sms\n{noiDung}");
                    if (myPLC.SMS.GuiSMS(textBox1.Text.Trim(), noiDung) == "Good")
                    {
                        SetSMS(Convert.ToInt16(bangSMS.Rows[i][0].ToString()));
                        Debug.WriteLine($"gui SMS thanh cong");
                    }

                }
            }

            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Debug.WriteLine($"tong sdt nhan SMS {textBox1.Text.Trim().Split(',').Length}");
            //Debug.WriteLine($"SDT {textBox1.Text}");
            //string noidung = $"{DateTime.Now}\nLocation: 1\nAlarm: Alarm\nValue: 15.5" +
            //        $"\nLow Level: 10" +
            //        $"\nHigh Level: 2";
            ////for (int i = 0; i < textBox1.Text.Trim().Split(',').Length; i++)
            //{
            //    Debug.WriteLine(myPLC.SMS.GuiSMS(textBox1.Text.Trim(), noidung));
            //    //Thread.Sleep(2000);
            //}
            timer1.Enabled = true;
        }

        static string ReadText(string PathFile)
        {
            try
            {
                FileStream fs = new FileStream(PathFile, FileMode.Open, FileAccess.Read, FileShare.None);
                System.IO.StreamReader sr = new System.IO.StreamReader(fs);
                string text = sr.ReadToEnd().Trim();
                sr.Close();
                fs.Close();
                return text;
            }
            catch { return "NULL"; }
        }

        static DataTable GetSMS()
        {
            DataTable userData = new DataTable();
            string query;
            MySqlConnection conn;
            conn = new MySqlConnection(ConnectionString);
            conn.Open();

            query = $"select * from test_sms where Flag=100";
            try
            {
                MySqlDataAdapter Adapter = new MySqlDataAdapter(query, conn);
                Adapter.Fill(userData);
                conn.Close();
                conn.Dispose();
            }
            catch (System.Exception)
            {
                userData = null;
                conn.Close();
                conn.Dispose();
            }
            return userData;
        }

        static int SetSMS(int id)
        {
            int res = 0;
            string query;
            MySqlConnection conn;
            conn = new MySqlConnection(ConnectionString);
            conn.Open();

            query = $"update test_sms set Flag=0 where Id='{id}'";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                res = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (System.Exception)
            {
                conn.Close();
                conn.Dispose();
            }
            return res;
        }
    }
}
