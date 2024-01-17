using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Snap7;

namespace CSClient
{
    public partial class MainForm : Form
    {
        private S7Client Client;
        private byte[] Buffer = new byte[65536];

        private void ShowResult(int Result)
        {
            // This function returns a textual explaination of the error code
            TextError.Text = Client.ErrorText(Result);
        }

        public MainForm()
        {
            InitializeComponent();
            Client = new S7Client();
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            int Result;
            int Rack = System.Convert.ToInt32(TxtRack.Text);
            int Slot = System.Convert.ToInt32(TxtSlot.Text);
            Result = Client.ConnectTo(TxtIP.Text, Rack, Slot);
            ShowResult(Result);
            if (Result == 0)
            {
                TxtIP.Enabled = false;
                TxtRack.Enabled = false;
                TxtSlot.Enabled = false;
                ConnectBtn.Enabled = false;
                DisconnectBtn.Enabled = true;
                TxtDB.Enabled = true;
                TxtSize.Enabled = true;
                ReadBtn.Enabled = true;
                TxtDump.Enabled = true;
            }
        }

        private void DisconnectBtn_Click(object sender, EventArgs e)
        {
            Client.Disconnect();
            TxtIP.Enabled = true;
            TxtRack.Enabled = true;
            TxtSlot.Enabled = true;
            ConnectBtn.Enabled = true;
            DisconnectBtn.Enabled = false;
            TxtDB.Enabled = false;
            TxtSize.Enabled = false;
            ReadBtn.Enabled = false;
            TxtDump.Enabled = false;
        }

        private void ReadBtn_Click(object sender, EventArgs e)
        {
            // Declaration separated from the code for readability
            int DBNumber;
            int Size;
            int Result;
            int y;

            DBNumber = System.Convert.ToInt32(TxtDB.Text);
            Size = System.Convert.ToInt32(TxtSize.Text);
            Result = Client.DBRead(DBNumber, 0, Size, Buffer);
            ShowResult(Result);
            if (Result == 0)
            {
                y = 0;
                for (int c = 0; c < Size; c++)
                {
                    String S = Convert.ToString(Buffer[c], 16);
                    if (S.Length == 1) S = "0" + S;
                    TxtDump.Text = TxtDump.Text + "0x"+ S + " ";
                    y++;
                    if (y == 8)
                    {
                        y = 0;
                        TxtDump.Text = TxtDump.Text + (char)13 + (char)10;
                    }
                }
            }
        }
    }
}
