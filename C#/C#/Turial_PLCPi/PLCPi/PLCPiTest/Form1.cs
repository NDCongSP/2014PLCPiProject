using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLCPiProject;

namespace PLCPiTest
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PLCPi myPLCPi1 = new PLCPi();// nếu em ko using namespace PLCPiPrpject thì chịu khó gõ dài

            myPLCPi1.Output("V0.0", 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PLCPi myPLCPi2 = new PLCPi();// nếu em ko using namespace PLCPiPrpject thì chịu khó gõ dài
            myPLCPi2.Output("V0.0", 0);
        }
    }
}
