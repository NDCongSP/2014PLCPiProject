namespace IP_PI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPortNumber = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtHostAddress = new System.Windows.Forms.TextBox();
            this.lbPortNumber = new System.Windows.Forms.Label();
            this.lbPassword = new System.Windows.Forms.Label();
            this.lbHostAddress = new System.Windows.Forms.Label();
            this.lbUserName = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.btnConnect);
            this.grpHeader.Controls.Add(this.txtUserName);
            this.grpHeader.Controls.Add(this.txtPortNumber);
            this.grpHeader.Controls.Add(this.txtPassword);
            this.grpHeader.Controls.Add(this.txtHostAddress);
            this.grpHeader.Controls.Add(this.lbPortNumber);
            this.grpHeader.Controls.Add(this.lbPassword);
            this.grpHeader.Controls.Add(this.lbHostAddress);
            this.grpHeader.Controls.Add(this.lbUserName);
            this.grpHeader.Location = new System.Drawing.Point(5, 0);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(591, 98);
            this.grpHeader.TabIndex = 1;
            this.grpHeader.TabStop = false;
            this.grpHeader.Text = "System Pi";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(479, 45);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(103, 34);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(325, 22);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(126, 20);
            this.txtUserName.TabIndex = 2;
            this.txtUserName.Text = "root";
            // 
            // txtPortNumber
            // 
            this.txtPortNumber.Location = new System.Drawing.Point(98, 55);
            this.txtPortNumber.Name = "txtPortNumber";
            this.txtPortNumber.Size = new System.Drawing.Size(126, 20);
            this.txtPortNumber.TabIndex = 1;
            this.txtPortNumber.Text = "22";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(325, 59);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '#';
            this.txtPassword.Size = new System.Drawing.Size(126, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "100100";
            // 
            // txtHostAddress
            // 
            this.txtHostAddress.Location = new System.Drawing.Point(98, 18);
            this.txtHostAddress.Name = "txtHostAddress";
            this.txtHostAddress.Size = new System.Drawing.Size(126, 20);
            this.txtHostAddress.TabIndex = 0;
            this.txtHostAddress.Text = "192.168.0.100";
            // 
            // lbPortNumber
            // 
            this.lbPortNumber.AutoSize = true;
            this.lbPortNumber.Location = new System.Drawing.Point(8, 62);
            this.lbPortNumber.Name = "lbPortNumber";
            this.lbPortNumber.Size = new System.Drawing.Size(69, 13);
            this.lbPortNumber.TabIndex = 3;
            this.lbPortNumber.Text = "Port Number:";
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(242, 62);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(56, 13);
            this.lbPassword.TabIndex = 2;
            this.lbPassword.Text = "Password:";
            // 
            // lbHostAddress
            // 
            this.lbHostAddress.AutoSize = true;
            this.lbHostAddress.Location = new System.Drawing.Point(8, 25);
            this.lbHostAddress.Name = "lbHostAddress";
            this.lbHostAddress.Size = new System.Drawing.Size(73, 13);
            this.lbHostAddress.TabIndex = 1;
            this.lbHostAddress.Text = "Host Address:";
            // 
            // lbUserName
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.Location = new System.Drawing.Point(242, 25);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(63, 13);
            this.lbUserName.TabIndex = 0;
            this.lbUserName.Text = "User Name:";
            // 
            // txtLog
            // 
            this.txtLog.AcceptsReturn = true;
            this.txtLog.AcceptsTab = true;
            this.txtLog.Location = new System.Drawing.Point(5, 198);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(591, 61);
            this.txtLog.TabIndex = 5;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.button2);
            this.grpBody.Controls.Add(this.label8);
            this.grpBody.Controls.Add(this.label7);
            this.grpBody.Controls.Add(this.label6);
            this.grpBody.Controls.Add(this.label5);
            this.grpBody.Controls.Add(this.button1);
            this.grpBody.Controls.Add(this.textBox1);
            this.grpBody.Controls.Add(this.textBox2);
            this.grpBody.Controls.Add(this.textBox3);
            this.grpBody.Controls.Add(this.textBox4);
            this.grpBody.Controls.Add(this.label1);
            this.grpBody.Controls.Add(this.label2);
            this.grpBody.Controls.Add(this.label3);
            this.grpBody.Controls.Add(this.label4);
            this.grpBody.Location = new System.Drawing.Point(5, 94);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(591, 98);
            this.grpBody.TabIndex = 6;
            this.grpBody.TabStop = false;
            this.grpBody.Text = "New Settings";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(479, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 33);
            this.button1.TabIndex = 4;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(325, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(126, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "192.168.0.1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(98, 69);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(126, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "8.8.8.8";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(325, 69);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(126, 20);
            this.textBox3.TabIndex = 3;
            this.textBox3.Text = "8.8.4.4";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(98, 26);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(126, 20);
            this.textBox4.TabIndex = 0;
            this.textBox4.Text = "192.168.0.200/24";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "DNS Server1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "DNS Server2:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Host Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(242, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "GateWay:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "(XXX.XXX.XXX.XXX/XX)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(331, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "( XXX.XXX.XXX.XXX )";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(105, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "( XXX.XXX.XXX.XXX )";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(331, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "( XXX.XXX.XXX.XXX )";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(479, 59);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 33);
            this.button2.TabIndex = 9;
            this.button2.Text = "Reboot";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 262);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.grpHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Change IP Pi";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPortNumber;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtHostAddress;
        private System.Windows.Forms.Label lbPortNumber;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Label lbHostAddress;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}

