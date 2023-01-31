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
using System.Data;
using MySql.Data.MySqlClient;

namespace DOC_GHI_DATABASE
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        PLCPi myPLC = new PLCPi();
        GhiMySQL mySQL = new GhiMySQL();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    mySQL.ChuoiKetnoiMySQL= "Server = localhost ; Uid =root ;Pwd= 100100;Database=" + textBox3.Text;
                    if (mySQL.KetnoiMySQL() == "GOOD")
                    {
                        string MySqlCmd = "CREATE SCHEMA " + textBox1.Text.Trim();
                        MySqlCommand cmd = new MySqlCommand(MySqlCmd, mySQL.connectmysql);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("LOI KETNOI");
                    }
                    mySQL.NgatKetnoiMySQL();
                }
                catch { MessageBox.Show("LOI"); }
            }
            else
            {
                MessageBox.Show("CHUA NHAP");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "")
            {
                try
                {
                    mySQL.ChuoiKetnoiMySQL= "Server = localhost ; Uid =root ;Pwd= 100100;Database=" + textBox3.Text;
                    if (mySQL.KetnoiMySQL() == "GOOD")
                    {
                        string MySqlCmd = "CREATE TABLE " + textBox2.Text.Trim() + "  (GIATRI VARCHAR(50) NOT NULL, PRIMARY KEY (GIATRI))";
                        MySqlCommand cmd = new MySqlCommand(MySqlCmd, mySQL.connectmysql);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("LOI KETNOI");
                    }
                    mySQL.NgatKetnoiMySQL();
                }
                catch { MessageBox.Show("LOI"); }
            }
            else
            {
                MessageBox.Show("CHUA NHAP");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                try
                {
                    mySQL.ChuoiKetnoiMySQL = "Server = localhost ; Uid =root ;Pwd= 100100;Database=" + textBox5.Text;
                    if (mySQL.KetnoiMySQL() == "GOOD")
                    {
                        string dk2 = "'" + textBox4.Text.Trim()  + "'";
                        string MySqlCmd = "insert into " + textBox6.Text.Trim() + " values(" + dk2 + ")";
                        MySqlCommand cmd = new MySqlCommand(MySqlCmd, mySQL.connectmysql);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("LOI1");
                    }
                    mySQL.NgatKetnoiMySQL();
                }
                catch { MessageBox.Show("LOI"); }
            }
            else
            {
                MessageBox.Show("CHUA NHAP");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "" && textBox8.Text != "" )
            {
                try
                {
                    mySQL.ChuoiKetnoiMySQL = "Server = localhost ; Uid =root ;Pwd= 100100;Database=" + textBox7.Text;
                    if (mySQL.KetnoiMySQL() == "GOOD")
                    {
                        DataTable op_user2 = new DataTable();
                        string conn_str2 = "select GIATRI from " + textBox8.Text ;
                        MySqlDataAdapter ad2 = new MySqlDataAdapter(conn_str2, mySQL.connectmysql);

                        ad2.Fill(op_user2);
                        ad2.Dispose();
                        comboBox1.DataSource = op_user2;
                        comboBox1.DisplayMember = "GIATRI";
                        // .DataSource = op_user2;
                        //DropDownList2.DataBind();

                        
                    }
                    else
                    {
                        MessageBox.Show("LOI1");
                    }
                    mySQL.NgatKetnoiMySQL();
                }
                catch { MessageBox.Show("LOI"); }
            }
            else
            {
                MessageBox.Show("CHUA NHAP");
            }
        }
    }
}
