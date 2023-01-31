using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using PLCPiProject;

namespace Webform_HMI_PLCPi_App1
{
    public partial class Default : System.Web.UI.Page
    {
        //Khoi tao doi tuong myPLC
        static PLCPi myPLC = new PLCPi();
        static byte tam = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Timer1.Enabled = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (tam == 0)
            {
                //myPLC.NgoRa.XuatNgoRa("Q0", 255);
                tam = 1;
            }
            else if(tam==1)
            {
                //myPLC.NgoRa.XuatNgoRa("Q0", 0);
                tam = 0;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            if (tam == 0) myPLC.NgoRa.XuatNgoRa("Q0", 255);
            else myPLC.NgoRa.XuatNgoRa("Q0", 0);
            TextBox1.Text = myPLC.NgoVao.DocNgoVao("I0").ToString();
            TextBox2.Text = myPLC.NgoRa.DocNgoRa("Q0").ToString();
            //TextBox1.Text = tam.ToString();
            Timer1.Enabled = true;
        }
    }
}