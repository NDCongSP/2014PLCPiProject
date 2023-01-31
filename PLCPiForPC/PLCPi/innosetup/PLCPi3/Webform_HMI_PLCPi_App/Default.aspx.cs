using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using PLCPiProject;

namespace $safeprojectname$
{
    public partial class Default : System.Web.UI.Page
    {
        //Khoi tao doi tuong myPLC
        static PLCPi myPLC = new PLCPi(); 
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}