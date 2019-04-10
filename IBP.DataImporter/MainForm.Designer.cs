namespace IBP.DataImporter
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
            this.txtLogBox = new System.Windows.Forms.TextBox();
            this.btnSyncCustomer = new System.Windows.Forms.Button();
            this.btnClearCustomerTable = new System.Windows.Forms.Button();
            this.btnSyncCustomerMemo = new System.Windows.Forms.Button();
            this.btnSyncAll = new System.Windows.Forms.Button();
            this.btnSyncContact = new System.Windows.Forms.Button();
            this.btnSyncCreditCard = new System.Windows.Forms.Button();
            this.btnSyncEntruct = new System.Windows.Forms.Button();
            this.btnSyncDelivery = new System.Windows.Forms.Button();
            this.btnUpdateComeFrom = new System.Windows.Forms.Button();
            this.btnSyncMobileNumber = new System.Windows.Forms.Button();
            this.btnUpdateCarriers = new System.Windows.Forms.Button();
            this.btnSyncWorkorderType = new System.Windows.Forms.Button();
            this.btnSyncWorkorder = new System.Windows.Forms.Button();
            this.btnSyncWorkorderStatus = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLogBox
            // 
            this.txtLogBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogBox.Location = new System.Drawing.Point(12, 157);
            this.txtLogBox.Multiline = true;
            this.txtLogBox.Name = "txtLogBox";
            this.txtLogBox.Size = new System.Drawing.Size(977, 329);
            this.txtLogBox.TabIndex = 0;
            // 
            // btnSyncCustomer
            // 
            this.btnSyncCustomer.Location = new System.Drawing.Point(245, 12);
            this.btnSyncCustomer.Name = "btnSyncCustomer";
            this.btnSyncCustomer.Size = new System.Drawing.Size(117, 23);
            this.btnSyncCustomer.TabIndex = 1;
            this.btnSyncCustomer.Text = "同步客户基本信息";
            this.btnSyncCustomer.UseVisualStyleBackColor = true;
            this.btnSyncCustomer.Click += new System.EventHandler(this.btnSyncCustomer_Click);
            // 
            // btnClearCustomerTable
            // 
            this.btnClearCustomerTable.Location = new System.Drawing.Point(12, 12);
            this.btnClearCustomerTable.Name = "btnClearCustomerTable";
            this.btnClearCustomerTable.Size = new System.Drawing.Size(103, 23);
            this.btnClearCustomerTable.TabIndex = 2;
            this.btnClearCustomerTable.Text = "清空客户信息表";
            this.btnClearCustomerTable.UseVisualStyleBackColor = true;
            this.btnClearCustomerTable.Click += new System.EventHandler(this.btnClearCustomerTable_Click);
            // 
            // btnSyncCustomerMemo
            // 
            this.btnSyncCustomerMemo.Location = new System.Drawing.Point(469, 12);
            this.btnSyncCustomerMemo.Name = "btnSyncCustomerMemo";
            this.btnSyncCustomerMemo.Size = new System.Drawing.Size(92, 23);
            this.btnSyncCustomerMemo.TabIndex = 3;
            this.btnSyncCustomerMemo.Text = "同步客户备注";
            this.btnSyncCustomerMemo.UseVisualStyleBackColor = true;
            this.btnSyncCustomerMemo.Click += new System.EventHandler(this.btnSyncCustomerMemo_Click);
            // 
            // btnSyncAll
            // 
            this.btnSyncAll.Location = new System.Drawing.Point(121, 12);
            this.btnSyncAll.Name = "btnSyncAll";
            this.btnSyncAll.Size = new System.Drawing.Size(75, 23);
            this.btnSyncAll.TabIndex = 4;
            this.btnSyncAll.Text = "同步所有";
            this.btnSyncAll.UseVisualStyleBackColor = true;
            this.btnSyncAll.Click += new System.EventHandler(this.btnSyncAll_Click);
            // 
            // btnSyncContact
            // 
            this.btnSyncContact.Location = new System.Drawing.Point(567, 12);
            this.btnSyncContact.Name = "btnSyncContact";
            this.btnSyncContact.Size = new System.Drawing.Size(92, 23);
            this.btnSyncContact.TabIndex = 5;
            this.btnSyncContact.Text = "同步联系记录";
            this.btnSyncContact.UseVisualStyleBackColor = true;
            this.btnSyncContact.Click += new System.EventHandler(this.btnSyncContact_Click);
            // 
            // btnSyncCreditCard
            // 
            this.btnSyncCreditCard.Location = new System.Drawing.Point(665, 12);
            this.btnSyncCreditCard.Name = "btnSyncCreditCard";
            this.btnSyncCreditCard.Size = new System.Drawing.Size(75, 23);
            this.btnSyncCreditCard.TabIndex = 6;
            this.btnSyncCreditCard.Text = "同步信用卡";
            this.btnSyncCreditCard.UseVisualStyleBackColor = true;
            this.btnSyncCreditCard.Click += new System.EventHandler(this.btnSyncCreditCard_Click);
            // 
            // btnSyncEntruct
            // 
            this.btnSyncEntruct.Location = new System.Drawing.Point(746, 12);
            this.btnSyncEntruct.Name = "btnSyncEntruct";
            this.btnSyncEntruct.Size = new System.Drawing.Size(92, 23);
            this.btnSyncEntruct.TabIndex = 7;
            this.btnSyncEntruct.Text = "同步托收账户";
            this.btnSyncEntruct.UseVisualStyleBackColor = true;
            this.btnSyncEntruct.Click += new System.EventHandler(this.btnSyncEntruct_Click);
            // 
            // btnSyncDelivery
            // 
            this.btnSyncDelivery.Location = new System.Drawing.Point(245, 41);
            this.btnSyncDelivery.Name = "btnSyncDelivery";
            this.btnSyncDelivery.Size = new System.Drawing.Size(96, 23);
            this.btnSyncDelivery.TabIndex = 8;
            this.btnSyncDelivery.Text = "同步配送地址";
            this.btnSyncDelivery.UseVisualStyleBackColor = true;
            this.btnSyncDelivery.Click += new System.EventHandler(this.btnSyncDelivery_Click);
            // 
            // btnUpdateComeFrom
            // 
            this.btnUpdateComeFrom.Location = new System.Drawing.Point(368, 13);
            this.btnUpdateComeFrom.Name = "btnUpdateComeFrom";
            this.btnUpdateComeFrom.Size = new System.Drawing.Size(95, 23);
            this.btnUpdateComeFrom.TabIndex = 9;
            this.btnUpdateComeFrom.Text = "更新号码来源";
            this.btnUpdateComeFrom.UseVisualStyleBackColor = true;
            this.btnUpdateComeFrom.Click += new System.EventHandler(this.btnUpdateComeFrom_Click);
            // 
            // btnSyncMobileNumber
            // 
            this.btnSyncMobileNumber.Location = new System.Drawing.Point(348, 41);
            this.btnSyncMobileNumber.Name = "btnSyncMobileNumber";
            this.btnSyncMobileNumber.Size = new System.Drawing.Size(75, 23);
            this.btnSyncMobileNumber.TabIndex = 10;
            this.btnSyncMobileNumber.Text = "同步手机号";
            this.btnSyncMobileNumber.UseVisualStyleBackColor = true;
            this.btnSyncMobileNumber.Click += new System.EventHandler(this.btnSyncMobileNumber_Click);
            // 
            // btnUpdateCarriers
            // 
            this.btnUpdateCarriers.Location = new System.Drawing.Point(429, 41);
            this.btnUpdateCarriers.Name = "btnUpdateCarriers";
            this.btnUpdateCarriers.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateCarriers.TabIndex = 11;
            this.btnUpdateCarriers.Text = "更新运营商";
            this.btnUpdateCarriers.UseVisualStyleBackColor = true;
            this.btnUpdateCarriers.Click += new System.EventHandler(this.btnUpdateCarriers_Click);
            // 
            // btnSyncWorkorderType
            // 
            this.btnSyncWorkorderType.Location = new System.Drawing.Point(511, 41);
            this.btnSyncWorkorderType.Name = "btnSyncWorkorderType";
            this.btnSyncWorkorderType.Size = new System.Drawing.Size(93, 23);
            this.btnSyncWorkorderType.TabIndex = 12;
            this.btnSyncWorkorderType.Text = "同步工单类型";
            this.btnSyncWorkorderType.UseVisualStyleBackColor = true;
            this.btnSyncWorkorderType.Click += new System.EventHandler(this.btnSyncWorkorderType_Click);
            // 
            // btnSyncWorkorder
            // 
            this.btnSyncWorkorder.Location = new System.Drawing.Point(610, 41);
            this.btnSyncWorkorder.Name = "btnSyncWorkorder";
            this.btnSyncWorkorder.Size = new System.Drawing.Size(75, 23);
            this.btnSyncWorkorder.TabIndex = 13;
            this.btnSyncWorkorder.Text = "同步工单";
            this.btnSyncWorkorder.UseVisualStyleBackColor = true;
            this.btnSyncWorkorder.Click += new System.EventHandler(this.btnSyncWorkorder_Click);
            // 
            // btnSyncWorkorderStatus
            // 
            this.btnSyncWorkorderStatus.Location = new System.Drawing.Point(692, 41);
            this.btnSyncWorkorderStatus.Name = "btnSyncWorkorderStatus";
            this.btnSyncWorkorderStatus.Size = new System.Drawing.Size(90, 23);
            this.btnSyncWorkorderStatus.TabIndex = 14;
            this.btnSyncWorkorderStatus.Text = "同步工单状态";
            this.btnSyncWorkorderStatus.UseVisualStyleBackColor = true;
            this.btnSyncWorkorderStatus.Click += new System.EventHandler(this.btnSyncWorkorderStatus_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 498);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "处理旧数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "同步增量客户信息";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(844, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "同步客户联系方式";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(789, 41);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(126, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "更新联系记录归属";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(155, 80);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(93, 23);
            this.btnCustomer.TabIndex = 19;
            this.btnCustomer.Text = "同步客户信息";
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(121, 498);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(97, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "导入产品信息";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 533);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnCustomer);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSyncWorkorderStatus);
            this.Controls.Add(this.btnSyncWorkorder);
            this.Controls.Add(this.btnSyncWorkorderType);
            this.Controls.Add(this.btnUpdateCarriers);
            this.Controls.Add(this.btnSyncMobileNumber);
            this.Controls.Add(this.btnUpdateComeFrom);
            this.Controls.Add(this.btnSyncDelivery);
            this.Controls.Add(this.btnSyncEntruct);
            this.Controls.Add(this.btnSyncCreditCard);
            this.Controls.Add(this.btnSyncContact);
            this.Controls.Add(this.btnSyncAll);
            this.Controls.Add(this.btnSyncCustomerMemo);
            this.Controls.Add(this.btnClearCustomerTable);
            this.Controls.Add(this.btnSyncCustomer);
            this.Controls.Add(this.txtLogBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLogBox;
        private System.Windows.Forms.Button btnSyncCustomer;
        private System.Windows.Forms.Button btnClearCustomerTable;
        private System.Windows.Forms.Button btnSyncCustomerMemo;
        private System.Windows.Forms.Button btnSyncAll;
        private System.Windows.Forms.Button btnSyncContact;
        private System.Windows.Forms.Button btnSyncCreditCard;
        private System.Windows.Forms.Button btnSyncEntruct;
        private System.Windows.Forms.Button btnSyncDelivery;
        private System.Windows.Forms.Button btnUpdateComeFrom;
        private System.Windows.Forms.Button btnSyncMobileNumber;
        private System.Windows.Forms.Button btnUpdateCarriers;
        private System.Windows.Forms.Button btnSyncWorkorderType;
        private System.Windows.Forms.Button btnSyncWorkorder;
        private System.Windows.Forms.Button btnSyncWorkorderStatus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Button button5;
    }
}