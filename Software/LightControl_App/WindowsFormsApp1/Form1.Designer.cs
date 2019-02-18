namespace WindowsFormsApp1
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
            this.components = new System.ComponentModel.Container();
            this.PbConnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CbSecCom = new System.Windows.Forms.ComboBox();
            this.Com = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.LbStatus = new System.Windows.Forms.Label();
            this.PbSend = new System.Windows.Forms.Button();
            this.txtKp = new System.Windows.Forms.TextBox();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.zed = new ZedGraph.ZedGraphControl();
            this.PbMode = new System.Windows.Forms.Button();
            this.PbExit = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtKi = new System.Windows.Forms.TextBox();
            this.txtKd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PidCtl = new System.Windows.Forms.RadioButton();
            this.OnOffCtl = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSetPoint = new System.Windows.Forms.TextBox();
            this.txtDeadband = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lberror = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // PbConnect
            // 
            this.PbConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PbConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PbConnect.Location = new System.Drawing.Point(102, 46);
            this.PbConnect.Name = "PbConnect";
            this.PbConnect.Size = new System.Drawing.Size(66, 29);
            this.PbConnect.TabIndex = 0;
            this.PbConnect.Text = "Conenct";
            this.PbConnect.UseVisualStyleBackColor = true;
            this.PbConnect.Click += new System.EventHandler(this.PbConnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CbSecCom);
            this.groupBox1.Controls.Add(this.PbConnect);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 94);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Com";
            // 
            // CbSecCom
            // 
            this.CbSecCom.FormattingEnabled = true;
            this.CbSecCom.Location = new System.Drawing.Point(90, 19);
            this.CbSecCom.Name = "CbSecCom";
            this.CbSecCom.Size = new System.Drawing.Size(91, 21);
            this.CbSecCom.TabIndex = 2;
            // 
            // Com
            // 
            this.Com.PortName = "COM7";
            this.Com.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.OnCom);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LbStatus
            // 
            this.LbStatus.AutoSize = true;
            this.LbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbStatus.Location = new System.Drawing.Point(9, 124);
            this.LbStatus.Name = "LbStatus";
            this.LbStatus.Size = new System.Drawing.Size(85, 16);
            this.LbStatus.TabIndex = 4;
            this.LbStatus.Text = "Disconnect";
            // 
            // PbSend
            // 
            this.PbSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PbSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PbSend.Location = new System.Drawing.Point(59, 335);
            this.PbSend.Name = "PbSend";
            this.PbSend.Size = new System.Drawing.Size(57, 24);
            this.PbSend.TabIndex = 7;
            this.PbSend.Text = "Send";
            this.PbSend.UseVisualStyleBackColor = true;
            this.PbSend.Click += new System.EventHandler(this.PbSend_Click);
            // 
            // txtKp
            // 
            this.txtKp.Location = new System.Drawing.Point(72, 197);
            this.txtKp.Name = "txtKp";
            this.txtKp.Size = new System.Drawing.Size(57, 20);
            this.txtKp.TabIndex = 6;
            // 
            // txtReceive
            // 
            this.txtReceive.Location = new System.Drawing.Point(17, 20);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.Size = new System.Drawing.Size(126, 54);
            this.txtReceive.TabIndex = 10;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtReceive);
            this.groupBox3.Location = new System.Drawing.Point(272, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(165, 91);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Receive";
            // 
            // zed
            // 
            this.zed.Location = new System.Drawing.Point(147, 127);
            this.zed.Name = "zed";
            this.zed.ScrollGrace = 0D;
            this.zed.ScrollMaxX = 0D;
            this.zed.ScrollMaxY = 0D;
            this.zed.ScrollMaxY2 = 0D;
            this.zed.ScrollMinX = 0D;
            this.zed.ScrollMinY = 0D;
            this.zed.ScrollMinY2 = 0D;
            this.zed.Size = new System.Drawing.Size(483, 284);
            this.zed.TabIndex = 12;
            // 
            // PbMode
            // 
            this.PbMode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PbMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PbMode.Location = new System.Drawing.Point(11, 18);
            this.PbMode.Name = "PbMode";
            this.PbMode.Size = new System.Drawing.Size(73, 29);
            this.PbMode.TabIndex = 13;
            this.PbMode.Text = "COMPACT";
            this.PbMode.UseVisualStyleBackColor = true;
            this.PbMode.Click += new System.EventHandler(this.PbMode_Click);
            // 
            // PbExit
            // 
            this.PbExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PbExit.Location = new System.Drawing.Point(12, 53);
            this.PbExit.Name = "PbExit";
            this.PbExit.Size = new System.Drawing.Size(72, 27);
            this.PbExit.TabIndex = 14;
            this.PbExit.Text = "Close";
            this.PbExit.UseVisualStyleBackColor = true;
            this.PbExit.Click += new System.EventHandler(this.PbExit_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.PbExit);
            this.groupBox4.Controls.Add(this.PbMode);
            this.groupBox4.Location = new System.Drawing.Point(480, 27);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(93, 91);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            // 
            // txtKi
            // 
            this.txtKi.Location = new System.Drawing.Point(72, 233);
            this.txtKi.Name = "txtKi";
            this.txtKi.Size = new System.Drawing.Size(57, 20);
            this.txtKi.TabIndex = 16;
            // 
            // txtKd
            // 
            this.txtKd.Location = new System.Drawing.Point(72, 268);
            this.txtKd.Name = "txtKd";
            this.txtKd.Size = new System.Drawing.Size(57, 20);
            this.txtKd.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Kp";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(42, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Ki";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(42, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Kd";
            // 
            // PidCtl
            // 
            this.PidCtl.AutoSize = true;
            this.PidCtl.Checked = true;
            this.PidCtl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PidCtl.Location = new System.Drawing.Point(12, 365);
            this.PidCtl.Name = "PidCtl";
            this.PidCtl.Size = new System.Drawing.Size(104, 20);
            this.PidCtl.TabIndex = 21;
            this.PidCtl.TabStop = true;
            this.PidCtl.Text = "PID Control";
            this.PidCtl.UseVisualStyleBackColor = true;
            this.PidCtl.CheckedChanged += new System.EventHandler(this.PidCtl_CheckedChanged);
            // 
            // OnOffCtl
            // 
            this.OnOffCtl.AutoSize = true;
            this.OnOffCtl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OnOffCtl.Location = new System.Drawing.Point(12, 391);
            this.OnOffCtl.Name = "OnOffCtl";
            this.OnOffCtl.Size = new System.Drawing.Size(135, 20);
            this.OnOffCtl.TabIndex = 22;
            this.OnOffCtl.Text = "ON/OFF Control";
            this.OnOffCtl.UseVisualStyleBackColor = true;
            this.OnOffCtl.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Set Temp";
            // 
            // txtSetPoint
            // 
            this.txtSetPoint.Location = new System.Drawing.Point(72, 163);
            this.txtSetPoint.Name = "txtSetPoint";
            this.txtSetPoint.Size = new System.Drawing.Size(57, 20);
            this.txtSetPoint.TabIndex = 24;
            // 
            // txtDeadband
            // 
            this.txtDeadband.Enabled = false;
            this.txtDeadband.Location = new System.Drawing.Point(72, 299);
            this.txtDeadband.Name = "txtDeadband";
            this.txtDeadband.Size = new System.Drawing.Size(57, 20);
            this.txtDeadband.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Deadband";
            // 
            // lberror
            // 
            this.lberror.AutoSize = true;
            this.lberror.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lberror.ForeColor = System.Drawing.Color.Red;
            this.lberror.Location = new System.Drawing.Point(42, 414);
            this.lberror.Name = "lberror";
            this.lberror.Size = new System.Drawing.Size(0, 16);
            this.lberror.TabIndex = 27;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 444);
            this.Controls.Add(this.lberror);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDeadband);
            this.Controls.Add(this.txtSetPoint);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.OnOffCtl);
            this.Controls.Add(this.PidCtl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKd);
            this.Controls.Add(this.txtKi);
            this.Controls.Add(this.PbSend);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtKp);
            this.Controls.Add(this.zed);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.LbStatus);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PbConnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.IO.Ports.SerialPort Com;
        private System.Windows.Forms.ComboBox CbSecCom;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LbStatus;
        private System.Windows.Forms.Button PbSend;
        private System.Windows.Forms.TextBox txtKp;
        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.GroupBox groupBox3;
        private ZedGraph.ZedGraphControl zed;
        private System.Windows.Forms.Button PbMode;
        private System.Windows.Forms.Button PbExit;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtKi;
        private System.Windows.Forms.TextBox txtKd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton PidCtl;
        private System.Windows.Forms.RadioButton OnOffCtl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSetPoint;
        private System.Windows.Forms.TextBox txtDeadband;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lberror;
    }
}

