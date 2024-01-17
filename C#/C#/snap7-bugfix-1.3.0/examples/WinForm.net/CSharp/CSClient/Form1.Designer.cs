namespace CSClient
{
    partial class MainForm
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
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.ReadBtn = new System.Windows.Forms.Button();
            this.Label7 = new System.Windows.Forms.Label();
            this.TxtSize = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.TxtDump = new System.Windows.Forms.TextBox();
            this.TxtDB = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.DisconnectBtn = new System.Windows.Forms.Button();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.TxtSlot = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.TxtRack = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.TxtIP = new System.Windows.Forms.TextBox();
            this.TextError = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(314, 40);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(81, 13);
            this.Label11.TabIndex = 41;
            this.Label11.Text = "Rack=0, Slot=0";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(314, 27);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(81, 13);
            this.Label10.TabIndex = 40;
            this.Label10.Text = "See HW Config";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(314, 14);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(81, 13);
            this.Label9.TabIndex = 39;
            this.Label9.Text = "Rack=0, Slot=2";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(232, 27);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(76, 13);
            this.Label8.TabIndex = 38;
            this.Label8.Text = "S7400/WinAC";
            // 
            // ReadBtn
            // 
            this.ReadBtn.Enabled = false;
            this.ReadBtn.Location = new System.Drawing.Point(14, 136);
            this.ReadBtn.Name = "ReadBtn";
            this.ReadBtn.Size = new System.Drawing.Size(97, 23);
            this.ReadBtn.TabIndex = 37;
            this.ReadBtn.Text = "DB Read";
            this.ReadBtn.UseVisualStyleBackColor = true;
            this.ReadBtn.Click += new System.EventHandler(this.ReadBtn_Click);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(196, 113);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(62, 13);
            this.Label7.TabIndex = 36;
            this.Label7.Text = "Size (Bytes)";
            // 
            // TxtSize
            // 
            this.TxtSize.Enabled = false;
            this.TxtSize.Location = new System.Drawing.Point(264, 110);
            this.TxtSize.Name = "TxtSize";
            this.TxtSize.Size = new System.Drawing.Size(45, 20);
            this.TxtSize.TabIndex = 29;
            this.TxtSize.Text = "32";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(119, 113);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(22, 13);
            this.Label6.TabIndex = 35;
            this.Label6.Text = "DB";
            // 
            // TxtDump
            // 
            this.TxtDump.BackColor = System.Drawing.Color.White;
            this.TxtDump.Enabled = false;
            this.TxtDump.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDump.ForeColor = System.Drawing.Color.Black;
            this.TxtDump.Location = new System.Drawing.Point(122, 136);
            this.TxtDump.Multiline = true;
            this.TxtDump.Name = "TxtDump";
            this.TxtDump.ReadOnly = true;
            this.TxtDump.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtDump.Size = new System.Drawing.Size(302, 170);
            this.TxtDump.TabIndex = 34;
            // 
            // TxtDB
            // 
            this.TxtDB.Enabled = false;
            this.TxtDB.Location = new System.Drawing.Point(146, 110);
            this.TxtDB.Name = "TxtDB";
            this.TxtDB.Size = new System.Drawing.Size(45, 20);
            this.TxtDB.TabIndex = 28;
            this.TxtDB.Text = "1";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(232, 40);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(73, 13);
            this.Label5.TabIndex = 33;
            this.Label5.Text = "S71200/1500";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(232, 14);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(38, 13);
            this.Label4.TabIndex = 32;
            this.Label4.Text = "S7300";
            // 
            // DisconnectBtn
            // 
            this.DisconnectBtn.Enabled = false;
            this.DisconnectBtn.Location = new System.Drawing.Point(122, 67);
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.Size = new System.Drawing.Size(100, 23);
            this.DisconnectBtn.TabIndex = 31;
            this.DisconnectBtn.Text = "Disconnect";
            this.DisconnectBtn.UseVisualStyleBackColor = true;
            this.DisconnectBtn.Click += new System.EventHandler(this.DisconnectBtn_Click);
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(12, 67);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(100, 23);
            this.ConnectBtn.TabIndex = 26;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(178, 7);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(25, 13);
            this.Label3.TabIndex = 30;
            this.Label3.Text = "Slot";
            // 
            // TxtSlot
            // 
            this.TxtSlot.Location = new System.Drawing.Point(178, 25);
            this.TxtSlot.Name = "TxtSlot";
            this.TxtSlot.Size = new System.Drawing.Size(44, 20);
            this.TxtSlot.TabIndex = 25;
            this.TxtSlot.Text = "2";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(122, 7);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(33, 13);
            this.Label2.TabIndex = 27;
            this.Label2.Text = "Rack";
            // 
            // TxtRack
            // 
            this.TxtRack.Location = new System.Drawing.Point(122, 25);
            this.TxtRack.Name = "TxtRack";
            this.TxtRack.Size = new System.Drawing.Size(44, 20);
            this.TxtRack.TabIndex = 23;
            this.TxtRack.Text = "0";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 7);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(58, 13);
            this.Label1.TabIndex = 24;
            this.Label1.Text = "IP Address";
            // 
            // TxtIP
            // 
            this.TxtIP.Location = new System.Drawing.Point(12, 25);
            this.TxtIP.Name = "TxtIP";
            this.TxtIP.Size = new System.Drawing.Size(100, 20);
            this.TxtIP.TabIndex = 22;
            this.TxtIP.Text = "192.168.0.72";
            // 
            // TextError
            // 
            this.TextError.BackColor = System.Drawing.Color.White;
            this.TextError.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextError.ForeColor = System.Drawing.Color.Black;
            this.TextError.Location = new System.Drawing.Point(0, 321);
            this.TextError.Name = "TextError";
            this.TextError.ReadOnly = true;
            this.TextError.Size = new System.Drawing.Size(438, 20);
            this.TextError.TabIndex = 21;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 341);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.ReadBtn);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.TxtSize);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.TxtDump);
            this.Controls.Add(this.TxtDB);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.DisconnectBtn);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.TxtSlot);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.TxtRack);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TxtIP);
            this.Controls.Add(this.TextError);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "C# Simple Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Button ReadBtn;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox TxtSize;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox TxtDump;
        internal System.Windows.Forms.TextBox TxtDB;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button DisconnectBtn;
        internal System.Windows.Forms.Button ConnectBtn;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox TxtSlot;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox TxtRack;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TxtIP;
        internal System.Windows.Forms.TextBox TextError;
    }
}

