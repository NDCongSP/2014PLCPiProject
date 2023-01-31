using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Email
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            Load += Form2_Load;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("giamsat.canhbao@gmail.com", "ujgknqghwcjqxfvq"),
                EnableSsl = true,
            };

            smtpClient.Send("giamsat.canhbao@gmail.com", "ndcong08cddv02@gmail.com,phucthinh.automations@gmail.com", "TEST", "Test email 111111.");
        }
    }
}
