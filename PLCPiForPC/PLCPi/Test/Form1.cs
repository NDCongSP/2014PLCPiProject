using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLCPiProject;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PLCPi MyPLC = new PLCPi();
        private void Form1_Load(object sender, EventArgs e)
        {
            Debug.WriteLine(MyPLC.SMS.Port_USB3G);
        }
    }
}
