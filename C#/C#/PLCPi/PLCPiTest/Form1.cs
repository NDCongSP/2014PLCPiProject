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
        PLCPi myPLCPi = new PLCPi();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            myPLCPi.Ngo_Ra("R0.0", 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myPLCPi.Ngo_Ra("R0.0", 0);
        }
    }
}
