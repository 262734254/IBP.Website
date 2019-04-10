namespace IBP.CtiSmartClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnCtiConfig = new System.Windows.Forms.Button();
            this.txtCtiId = new System.Windows.Forms.TextBox();
            this.txtCtiPwd = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBusinessCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInComeNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCallNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cheAutoWrapend = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtsecurity_code = new System.Windows.Forms.TextBox();
            this.btnSecurity_code = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCustomerPeriodOfValidity = new System.Windows.Forms.TextBox();
            this.BtnCusBtomerPeriodOfValidity = new System.Windows.Forms.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.TxtUseName = new System.Windows.Forms.TextBox();
            this.No_CardInfo = new System.Windows.Forms.Button();
            this.chkPublicNet = new System.Windows.Forms.CheckBox();
            this.btnSendCustomerInfo = new System.Windows.Forms.Button();
            this.btnShowCustomerInfo = new System.Windows.Forms.Button();
            this.btnGetIdCardNumber = new System.Windows.Forms.Button();
            this.txtIdCardNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGetBankIdNumber = new System.Windows.Forms.Button();
            this.txtCreditCardNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.axaOCX1 = new AxAGENTOCXLib.AxaOCX();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axaOCX1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCtiConfig
            // 
            this.btnCtiConfig.Enabled = false;
            this.btnCtiConfig.Location = new System.Drawing.Point(15, 397);
            this.btnCtiConfig.Name = "btnCtiConfig";
            this.btnCtiConfig.Size = new System.Drawing.Size(75, 48);
            this.btnCtiConfig.TabIndex = 1;
            this.btnCtiConfig.Text = "CTI配置";
            this.btnCtiConfig.UseVisualStyleBackColor = true;
            this.btnCtiConfig.Click += new System.EventHandler(this.btnCtiConfig_Click);
            // 
            // txtCtiId
            // 
            this.txtCtiId.Location = new System.Drawing.Point(485, 398);
            this.txtCtiId.Name = "txtCtiId";
            this.txtCtiId.Size = new System.Drawing.Size(100, 21);
            this.txtCtiId.TabIndex = 2;
            this.txtCtiId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCtiId_KeyDown);
            // 
            // txtCtiPwd
            // 
            this.txtCtiPwd.Location = new System.Drawing.Point(485, 425);
            this.txtCtiPwd.Name = "txtCtiPwd";
            this.txtCtiPwd.PasswordChar = '*';
            this.txtCtiPwd.Size = new System.Drawing.Size(100, 21);
            this.txtCtiPwd.TabIndex = 3;
            this.txtCtiPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCtiPwd_KeyDown);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(601, 397);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 49);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(683, 397);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(76, 49);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "注销";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtBusinessCode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtInComeNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCallNumber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(774, 57);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前坐席线路信息";
            // 
            // txtBusinessCode
            // 
            this.txtBusinessCode.BackColor = System.Drawing.Color.AliceBlue;
            this.txtBusinessCode.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBusinessCode.Location = new System.Drawing.Point(619, 18);
            this.txtBusinessCode.Name = "txtBusinessCode";
            this.txtBusinessCode.ReadOnly = true;
            this.txtBusinessCode.Size = new System.Drawing.Size(143, 30);
            this.txtBusinessCode.TabIndex = 5;
            this.txtBusinessCode.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(554, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "客户按键:";
            this.label5.Visible = false;
            // 
            // txtInComeNumber
            // 
            this.txtInComeNumber.BackColor = System.Drawing.Color.AliceBlue;
            this.txtInComeNumber.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInComeNumber.Location = new System.Drawing.Point(356, 18);
            this.txtInComeNumber.Name = "txtInComeNumber";
            this.txtInComeNumber.ReadOnly = true;
            this.txtInComeNumber.Size = new System.Drawing.Size(182, 30);
            this.txtInComeNumber.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "被叫号码:";
            // 
            // txtCallNumber
            // 
            this.txtCallNumber.BackColor = System.Drawing.Color.AliceBlue;
            this.txtCallNumber.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCallNumber.Location = new System.Drawing.Point(90, 18);
            this.txtCallNumber.Name = "txtCallNumber";
            this.txtCallNumber.ReadOnly = true;
            this.txtCallNumber.Size = new System.Drawing.Size(182, 30);
            this.txtCallNumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "主叫号码:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cheAutoWrapend);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtsecurity_code);
            this.groupBox2.Controls.Add(this.btnSecurity_code);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCustomerPeriodOfValidity);
            this.groupBox2.Controls.Add(this.BtnCusBtomerPeriodOfValidity);
            this.groupBox2.Controls.Add(this.Label6);
            this.groupBox2.Controls.Add(this.TxtUseName);
            this.groupBox2.Controls.Add(this.No_CardInfo);
            this.groupBox2.Controls.Add(this.chkPublicNet);
            this.groupBox2.Controls.Add(this.btnSendCustomerInfo);
            this.groupBox2.Controls.Add(this.btnShowCustomerInfo);
            this.groupBox2.Controls.Add(this.btnGetIdCardNumber);
            this.groupBox2.Controls.Add(this.txtIdCardNumber);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnGetBankIdNumber);
            this.groupBox2.Controls.Add(this.txtCreditCardNumber);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(0, 140);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(774, 229);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "当前坐席线路信息";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // cheAutoWrapend
            // 
            this.cheAutoWrapend.Location = new System.Drawing.Point(556, 181);
            this.cheAutoWrapend.Name = "cheAutoWrapend";
            this.cheAutoWrapend.Size = new System.Drawing.Size(96, 24);
            this.cheAutoWrapend.TabIndex = 18;
            this.cheAutoWrapend.Text = "自动WrapEnd";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "安全码:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtsecurity_code
            // 
            this.txtsecurity_code.BackColor = System.Drawing.Color.AliceBlue;
            this.txtsecurity_code.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtsecurity_code.Location = new System.Drawing.Point(90, 144);
            this.txtsecurity_code.Name = "txtsecurity_code";
            this.txtsecurity_code.ReadOnly = true;
            this.txtsecurity_code.Size = new System.Drawing.Size(260, 30);
            this.txtsecurity_code.TabIndex = 16;
            // 
            // btnSecurity_code
            // 
            this.btnSecurity_code.Location = new System.Drawing.Point(356, 144);
            this.btnSecurity_code.Name = "btnSecurity_code";
            this.btnSecurity_code.Size = new System.Drawing.Size(147, 30);
            this.btnSecurity_code.TabIndex = 15;
            this.btnSecurity_code.Text = "接入语音获取安全码";
            this.btnSecurity_code.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSecurity_code.UseVisualStyleBackColor = true;
            this.btnSecurity_code.Click += new System.EventHandler(this.btnSecurity_code_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "有效期:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCustomerPeriodOfValidity
            // 
            this.txtCustomerPeriodOfValidity.BackColor = System.Drawing.Color.AliceBlue;
            this.txtCustomerPeriodOfValidity.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCustomerPeriodOfValidity.Location = new System.Drawing.Point(90, 104);
            this.txtCustomerPeriodOfValidity.Name = "txtCustomerPeriodOfValidity";
            this.txtCustomerPeriodOfValidity.ReadOnly = true;
            this.txtCustomerPeriodOfValidity.Size = new System.Drawing.Size(260, 30);
            this.txtCustomerPeriodOfValidity.TabIndex = 13;
            // 
            // BtnCusBtomerPeriodOfValidity
            // 
            this.BtnCusBtomerPeriodOfValidity.Location = new System.Drawing.Point(356, 104);
            this.BtnCusBtomerPeriodOfValidity.Name = "BtnCusBtomerPeriodOfValidity";
            this.BtnCusBtomerPeriodOfValidity.Size = new System.Drawing.Size(147, 30);
            this.BtnCusBtomerPeriodOfValidity.TabIndex = 12;
            this.BtnCusBtomerPeriodOfValidity.Text = "接入语音获取有效期";
            this.BtnCusBtomerPeriodOfValidity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCusBtomerPeriodOfValidity.UseVisualStyleBackColor = true;
            this.BtnCusBtomerPeriodOfValidity.Click += new System.EventHandler(this.BCusBtomerPeriodOfValidity_Click);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(37, 195);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(47, 12);
            this.Label6.TabIndex = 11;
            this.Label6.Text = "操作员:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtUseName
            // 
            this.TxtUseName.BackColor = System.Drawing.Color.AliceBlue;
            this.TxtUseName.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtUseName.Location = new System.Drawing.Point(90, 186);
            this.TxtUseName.Name = "TxtUseName";
            this.TxtUseName.Size = new System.Drawing.Size(260, 30);
            this.TxtUseName.TabIndex = 1;
            // 
            // No_CardInfo
            // 
            this.No_CardInfo.Location = new System.Drawing.Point(656, 92);
            this.No_CardInfo.Name = "No_CardInfo";
            this.No_CardInfo.Size = new System.Drawing.Size(106, 47);
            this.No_CardInfo.TabIndex = 7;
            this.No_CardInfo.Text = "发送至无卡支付系统";
            this.No_CardInfo.UseVisualStyleBackColor = true;
            this.No_CardInfo.Click += new System.EventHandler(this.No_CardInfo_Click);
            // 
            // chkPublicNet
            // 
            this.chkPublicNet.AutoSize = true;
            this.chkPublicNet.Location = new System.Drawing.Point(666, 185);
            this.chkPublicNet.Name = "chkPublicNet";
            this.chkPublicNet.Size = new System.Drawing.Size(96, 16);
            this.chkPublicNet.TabIndex = 8;
            this.chkPublicNet.Text = "使用应急通道";
            this.chkPublicNet.UseVisualStyleBackColor = true;
            // 
            // btnSendCustomerInfo
            // 
            this.btnSendCustomerInfo.Location = new System.Drawing.Point(656, 20);
            this.btnSendCustomerInfo.Name = "btnSendCustomerInfo";
            this.btnSendCustomerInfo.Size = new System.Drawing.Size(106, 47);
            this.btnSendCustomerInfo.TabIndex = 7;
            this.btnSendCustomerInfo.Text = "发送至盈实业务系统";
            this.btnSendCustomerInfo.UseVisualStyleBackColor = true;
            this.btnSendCustomerInfo.Click += new System.EventHandler(this.btnSendCustomerInfo_Click);
            // 
            // btnShowCustomerInfo
            // 
            this.btnShowCustomerInfo.Location = new System.Drawing.Point(522, 65);
            this.btnShowCustomerInfo.Name = "btnShowCustomerInfo";
            this.btnShowCustomerInfo.Size = new System.Drawing.Size(79, 74);
            this.btnShowCustomerInfo.TabIndex = 6;
            this.btnShowCustomerInfo.Text = "获取客户信息输入项";
            this.btnShowCustomerInfo.UseVisualStyleBackColor = true;
            this.btnShowCustomerInfo.Visible = false;
            this.btnShowCustomerInfo.Click += new System.EventHandler(this.btnShowCustomerInfo_Click);
            // 
            // btnGetIdCardNumber
            // 
            this.btnGetIdCardNumber.Location = new System.Drawing.Point(356, 65);
            this.btnGetIdCardNumber.Name = "btnGetIdCardNumber";
            this.btnGetIdCardNumber.Size = new System.Drawing.Size(147, 30);
            this.btnGetIdCardNumber.TabIndex = 5;
            this.btnGetIdCardNumber.Text = "接入语音获取证件号码";
            this.btnGetIdCardNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetIdCardNumber.UseVisualStyleBackColor = true;
            this.btnGetIdCardNumber.Click += new System.EventHandler(this.btnGetIdCardNumber_Click);
            // 
            // txtIdCardNumber
            // 
            this.txtIdCardNumber.BackColor = System.Drawing.Color.AliceBlue;
            this.txtIdCardNumber.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIdCardNumber.Location = new System.Drawing.Point(90, 64);
            this.txtIdCardNumber.Name = "txtIdCardNumber";
            this.txtIdCardNumber.ReadOnly = true;
            this.txtIdCardNumber.Size = new System.Drawing.Size(260, 30);
            this.txtIdCardNumber.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "证件号码:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGetBankIdNumber
            // 
            this.btnGetBankIdNumber.Location = new System.Drawing.Point(356, 20);
            this.btnGetBankIdNumber.Name = "btnGetBankIdNumber";
            this.btnGetBankIdNumber.Size = new System.Drawing.Size(147, 30);
            this.btnGetBankIdNumber.TabIndex = 4;
            this.btnGetBankIdNumber.Text = "接入语音获取信用卡号码";
            this.btnGetBankIdNumber.UseVisualStyleBackColor = true;
            this.btnGetBankIdNumber.Click += new System.EventHandler(this.btnGetBankIdNumber_Click);
            // 
            // txtCreditCardNumber
            // 
            this.txtCreditCardNumber.BackColor = System.Drawing.Color.AliceBlue;
            this.txtCreditCardNumber.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCreditCardNumber.Location = new System.Drawing.Point(90, 20);
            this.txtCreditCardNumber.Name = "txtCreditCardNumber";
            this.txtCreditCardNumber.ReadOnly = true;
            this.txtCreditCardNumber.Size = new System.Drawing.Size(260, 30);
            this.txtCreditCardNumber.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "信用卡号码:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(108, 433);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 12);
            this.lblStatus.TabIndex = 9;
            // 
            // axaOCX1
            // 
            this.axaOCX1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axaOCX1.Enabled = true;
            this.axaOCX1.Location = new System.Drawing.Point(0, 0);
            this.axaOCX1.Name = "axaOCX1";
            this.axaOCX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axaOCX1.OcxState")));
            this.axaOCX1.Size = new System.Drawing.Size(774, 71);
            this.axaOCX1.TabIndex = 0;
            this.axaOCX1.SayHello += new System.EventHandler(this.axaOCX1_SayHello);
            this.axaOCX1.CallArrive += new AxAGENTOCXLib._IaOCXEvents_CallArriveEventHandler(this.axaOCX1_CallArrive);
            this.axaOCX1.EVTDialOutEvent += new System.EventHandler(this.axaOCX1_EVTDialOutEvent);
            this.axaOCX1.EVTReturnStatusEvent += new AxAGENTOCXLib._IaOCXEvents_EVTReturnStatusEventHandler(this.axaOCX1_EVTReturnStatusEvent);
            this.axaOCX1.EVTLoginSucEvent += new System.EventHandler(this.axaOCX1_EVTLoginSucEvent);
            this.axaOCX1.EVTWrapUpEvent += new System.EventHandler(this.axaOCX1_EVTWrapUpEvent);
            this.axaOCX1.EVTButtonStatusEvent += new AxAGENTOCXLib._IaOCXEvents_EVTButtonStatusEventHandler(this.axaOCX1_EVTButtonStatusEvent);
            this.axaOCX1.EVTListenEvent += new AxAGENTOCXLib._IaOCXEvents_EVTListenEventHandler(this.axaOCX1_EVTListenEvent);
            this.axaOCX1.EVTLoginFailed += new AxAGENTOCXLib._IaOCXEvents_EVTLoginFailedEventHandler(this.axaOCX1_EVTLoginFailed);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 458);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtCtiPwd);
            this.Controls.Add(this.txtCtiId);
            this.Controls.Add(this.btnCtiConfig);
            this.Controls.Add(this.axaOCX1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(782, 485);
            this.MinimumSize = new System.Drawing.Size(782, 485);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axaOCX1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCtiConfig;
        private System.Windows.Forms.TextBox txtCtiId;
        private System.Windows.Forms.TextBox txtCtiPwd;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtInComeNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCallNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtIdCardNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCreditCardNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSendCustomerInfo;
        private System.Windows.Forms.Button btnShowCustomerInfo;
        private System.Windows.Forms.Button btnGetIdCardNumber;
        private System.Windows.Forms.Button btnGetBankIdNumber;
        private System.Windows.Forms.TextBox txtBusinessCode;
        private System.Windows.Forms.Label label5;
        public AxAGENTOCXLib.AxaOCX axaOCX1;
        public System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkPublicNet;
        private System.Windows.Forms.Button No_CardInfo;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCustomerPeriodOfValidity;
        private System.Windows.Forms.Button BtnCusBtomerPeriodOfValidity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtsecurity_code;
        private System.Windows.Forms.Button btnSecurity_code;
        private System.Windows.Forms.TextBox TxtUseName;
        public System.Windows.Forms.CheckBox cheAutoWrapend;
    }
}