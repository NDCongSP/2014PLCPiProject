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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
           //đọc ngõ vào I0 rồi hiển thị lên TextBox1
           TextBox1.Text = myPLC.NgoVao.DocNgoVao("I0").ToString();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            //xuất giá trị 255 ra ngõ ra Q5
            myPLC.NgoRa.XuatNgoRa("Q5", 255);
            //đọc trạng thái ngõ ra Q5 hiển thị lên TextBox3
            TextBox3.Text = myPLC.NgoRa.DocNgoRa("Q5").ToString();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            //xuất giá trị 0 ra ngõ ra Q5
            myPLC.NgoRa.XuatNgoRa("Q5", 0);
            //đọc trạng thái ngõ ra Q5 hiển thị lên TextBox3
            TextBox3.Text = myPLC.NgoRa.DocNgoRa("Q5").ToString();
        }
    }
}