using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Renci.SshNet;
namespace IP_PI
{
    public partial class Form1 : Form
    {
        private SshClient _client;
        int myport = 0;
        string ip = "", gate = "", dns = "";
        public Form1()
        {
            InitializeComponent();
        }
        private void appendLog(String strContent)
        {
            this.txtLog.AppendText(strContent + "\r\n");
        }
        private void initControl()
        {
            //init controls
            this.grpHeader.Enabled = true;
            this.grpBody.Enabled = false;

            this.btnConnect.Text = "Connect";
        }
        private Boolean validateParam()
        {
            bool blResult = true;

            //in hrere we will have some variable let validate ads bellow
            if (this.txtHostAddress.Text.Trim().Equals("")
                || this.txtPortNumber.Text.Trim().Equals("")
                || this.txtUserName.Text.Trim().Equals("")
                || this.txtPassword.Text.Trim().Equals(""))
            {
                //show warning box
                MessageBox.Show("Please fill all information let connect to Pi!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blResult = false;
            }
            else
            {
                try
                {
                    //validate port
                    myport = Convert.ToInt32(this.txtPortNumber.Text);
                    if (myport < 1 && myport > 65535)
                    {
                        MessageBox.Show("Port nummber have to be nummeric between 0 and 65535, please try again!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        blResult = false;
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Port nummber have to be integer, please try again!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    blResult = false;
                }
            }
            return blResult;
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.txtLog.Clear();
            if (this.btnConnect.Text == "Disconnect")
            {

                //this function let append text in log text box
                appendLog("Disconnect to Pi " + this.txtHostAddress.Text);
                if (_client != null)
                {
                    _client.Disconnect();

                }
                initControl();
                appendLog("Disonnect to Pi " + this.txtHostAddress.Text + " finished");
            }
            else if (validateParam())
            {
                try
                {
                    //after validate parameter
                    appendLog("Connect to Pi " + this.txtHostAddress.Text);

                    //connect to sftp server
                    _client = new SshClient(this.txtHostAddress.Text, this.txtUserName.Text, this.txtPassword.Text);
                    _client.Connect();
                    ForwardedPortDynamic port = new ForwardedPortDynamic("127.0.0.1", (uint)myport);
                    _client.AddForwardedPort(port);
                    if (_client.IsConnected)
                    {
                        port.Start();
                        //this.grpHeader.Enabled = false;
                        this.btnConnect.Text = "Disconnect";
                        this.grpBody.Enabled = true;
                        appendLog("Connected to Pi " + this.txtHostAddress.Text + " Good!");
                    }
                    else
                    {
                        appendLog("Connect to Pi " + this.txtHostAddress.Text + " Erro!");
                    }
                }
                catch { appendLog("Connect to Pi " + this.txtHostAddress.Text + " Erro!"); }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            appendLog("Loading form and init...");
            initControl();
            //creating tool tip text for controls
            createToolTipText(this.txtHostAddress, "Host address Pi");
            createToolTipText(this.txtPortNumber, "Port nummber between 0 and 65535");
            createToolTipText(this.txtUserName, "User name let login Pi");
            createToolTipText(this.txtPassword, "Password let login Pi");
            appendLog("Finished loading form and init...");
        }
        private void createToolTipText(Control c, String s)
        {
            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(c, s);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtLog.Clear();
                appendLog("Pi is Rebooting.......!");
                try
                {
                    _client.RunCommand("sudo reboot");
                }
                catch { }
                if (_client != null)
                {
                    _client.Disconnect();

                }
                appendLog("Disconnect to Pi " + this.txtHostAddress.Text);
                initControl();
            }
            catch { }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ip=_client.RunCommand("cat /etc/dhcpcd.conf | grep -e '^static ip_address=' | cut -d= -f2").Execute();
                gate= _client.RunCommand("cat /etc/dhcpcd.conf | grep -e '^static routers=' | cut -d= -f2").Execute();
                dns=_client.RunCommand("cat /etc/dhcpcd.conf | grep -e '^static domain_name_servers=' |cut -d= -f2").Execute();
                //  _client.RunCommand("echo " + ('"').ToString() + "#Generated by resolvconf\r\nnameserver " + textBox2.Text.Trim()+"\r\nnameserver "+ textBox3.Text.Trim() + ('"').ToString() + " > /etc/resolv.conf");
                //  _client.RunCommand("echo " + ('"').ToString() + file_dhcpcd(textBox4.Text.Trim(), textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim()) + ('"').ToString() + " > /etc/dhcpcd.conf");
                _client.RunCommand("sed -i -e " + ('"').ToString() + "s@static ip_address=" + ip.Trim() + "@static ip_address=" + textBox4.Text.Trim() + "@g" + ('"').ToString() + " /etc/dhcpcd.conf");
                _client.RunCommand("sed -i -e " + ('"').ToString() + "s@static routers=" + gate.Trim() + "@static routers=" + textBox1.Text.Trim() + "@g" + ('"').ToString() + " /etc/dhcpcd.conf");
                _client.RunCommand("sed -i -e " + ('"').ToString() + "s@static domain_name_servers=" + dns.Trim() + "@static domain_name_servers=" + textBox2.Text.Trim() +" "+ textBox3.Text.Trim() + "@g" + ('"').ToString() + " /etc/dhcpcd.conf");
                 appendLog("Update to Pi " + this.txtHostAddress.Text + " Good!");
            }
            catch { appendLog("Update to Pi " + this.txtHostAddress.Text + " Erro!"); }

        }
        private string file_dhcpcd(string ip, string gateway,string dns1, string dns2)
        {
            string dhcpcd = "# A sample configuration for dhcpcd." + "\r\n" +
                    "# See dhcpcd.conf(5) for details." + "\r\n" + "\r\n" +

                    "# Allow users of this group to interact with dhcpcd via the control socket." + "\r\n" +
                    "#controlgroup wheel" + "\r\n" + "\r\n" +

                    "# Inform the DHCP server of our hostname for DDNS." + "\r\n" +
                    "hostname" + "\r\n" + "\r\n" +

                    "# Use the hardware address of the interface for the Client ID." + "\r\n" +
                    "clientid" + "\r\n" +
                    "# or" + "\r\n" +
                    "# Use the same DUID + IAID as set in DHCPv6 for DHCPv4 ClientID as per RFC4361." + "\r\n" +
                    "#duid" + "\r\n" + "\r\n" +

                    "# Persist interface configuration when dhcpcd exits." + "\r\n" +
                    "persistent" + "\r\n" + "\r\n" +

                    "# Rapid commit support." + "\r\n" +
                    "# Safe to enable by default because it requires the equivalent option set" + "\r\n" +
                    "# on the server to actually work." + "\r\n" +
                    "option rapid_commit" + "\r\n" +"\r\n" +

                    "# A list of options to request from the DHCP server." + "\r\n" +
                    "option domain_name_servers, domain_name, domain_search, host_name" + "\r\n" +
                    "option classless_static_routes" + "\r\n" +
                    "# Most distributions have NTP support." + "\r\n" +
                    "option ntp_servers" + "\r\n" +
                    "# Respect the network MTU." + "\r\n" +
                    "# Some interface drivers reset when changing the MTU so disabled by default." + "\r\n" +
                    "#option interface_mtu" + "\r\n" + "\r\n" +

                    "# A ServerID is required by RFC2131." + "\r\n" +
                    "require dhcp_server_identifier" + "\r\n" + "\r\n" +

                    "# Generate Stable Private IPv6 Addresses instead of hardware based ones" + "\r\n" +
                    "slaac private" + "\r\n" + "\r\n" +

                    "# A hook script is provided to lookup the hostname if not set by the DHCP" + "\r\n" +
                    "# server, but it should not be run by default." + "\r\n" +
                    "nohook lookup-hostname" + "\r\n" +
                    "interface eth0" + "\r\n" + "\r\n" +

                    "static ip_address=" +ip+ "\r\n" +
                    "static routers="+gateway+ "\r\n" +
                    "static domain_name_servers="+dns1+ "\r\n" +
                    "static domain_search=" + dns2 ;

            return dhcpcd;
        } 
    }
}
