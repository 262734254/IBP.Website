using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Common;
using System.Diagnostics;
using Framework.Utilities;
using System.Data;
using AGENTOCXLib;
using AxAGENTOCXLib;
namespace IBP.CtiSmartClient
{
    public partial class MainForm : Form
    {
        public string NowInComeCallId = null;
        public string CustomerCreditCardNumber = null;//信用卡号
        public string CustomerIdCardNumber = null;//身份证号
        public string CustomerPeriodOfValidity = null;//有效期
        public string Customersecurity_code = null;//安全码
        private frmDialOut DialOutBox = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void axaOCX1_SayHello(object sender, EventArgs e)
        {
            MessageBox.Show("hoho");
        }

        private void btnCtiConfig_Click(object sender, EventArgs e)
        {
            axaOCX1.ShowConfig();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            axaOCX1.AgentID = txtCtiId.Text.Trim();
            axaOCX1.Password = txtCtiPwd.Text.Trim();
            axaOCX1.LogIn();

            LogUtil.Debug(string.Format("坐席登录CTI，账号：{0},密码：{1}", txtCtiId.Text.Trim(), txtCtiPwd.Text.Trim()));
        }

        private void axaOCX1_EVTLoginFailed(object sender, AxAGENTOCXLib._IaOCXEvents_EVTLoginFailedEvent e)
        {
            MessageBox.Show(e.reason);
            LogUtil.Debug(string.Format("坐席登录CTI失败，账号：{0},密码：{1}", txtCtiId.Text.Trim(), txtCtiPwd.Text.Trim()));
        }

        private void axaOCX1_EVTLoginSucEvent(object sender, EventArgs e)
        {
            MessageBox.Show("登录成功");
            btnLogin.Enabled = false;
            btnLogout.Enabled = true;
            btnGetBankIdNumber.Enabled = true;
            btnGetIdCardNumber.Enabled = true;
            btnShowCustomerInfo.Enabled = true;
            btnSendCustomerInfo.Enabled = true;
            No_CardInfo.Enabled = true;
            BtnCusBtomerPeriodOfValidity.Enabled = true;
            btnSecurity_code.Enabled = true;
            btnCtiConfig.Enabled = false;

            txtCtiId.Enabled = false;
            txtCtiPwd.Enabled = false;

            LogUtil.Debug(string.Format("坐席登录CTI成功，账号：{0},密码：{1}", txtCtiId.Text.Trim(), txtCtiPwd.Text.Trim()));

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            axaOCX1.LogOUT();
            btnLogin.Enabled = true;
            btnLogout.Enabled = false;

            btnGetBankIdNumber.Enabled = false;
            btnGetIdCardNumber.Enabled = false;
            btnShowCustomerInfo.Enabled = false;
            btnSendCustomerInfo.Enabled = false;
            No_CardInfo.Enabled = false;
            BtnCusBtomerPeriodOfValidity.Enabled = false;
            btnSecurity_code.Enabled = false;
            btnCtiConfig.Enabled = true;
            txtCtiId.Enabled = true;
            txtCtiPwd.Enabled = true;

            LogUtil.Debug(string.Format("坐席退出CTI，账号：{0},密码：{1}", txtCtiId.Text.Trim(), txtCtiPwd.Text.Trim()));

        }

        /// <summary>
        /// 来电后触发事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axaOCX1_CallArrive(object sender, AxAGENTOCXLib._IaOCXEvents_CallArriveEvent e)
        {
            txtInComeNumber.Text = e.dnis;
            txtCallNumber.Text = e.ani;
            NowInComeCallId = Guid.NewGuid().ToString();
            btnLogout.Enabled = true;

            

            txtBusinessCode.Text = axaOCX1.DoGetAssociatedData("BusinessSelectCode");
            txtCreditCardNumber.Text = "";
            txtIdCardNumber.Text = "";
            txtCustomerPeriodOfValidity.Text = "";
            txtsecurity_code.Text = "";

            LogUtil.Debug(string.Format("CTI来电:\r\n账号：{0},\r\n密码：{1},\r\n时间：{2}\r\n来电号码：{3}\r\n被叫号码：{4}\r\n新来电会话标识：{5}",
                txtCtiId.Text.Trim(),
                txtCtiPwd.Text.Trim(),
                DateTime.Now.ToString(),
                 txtInComeNumber.Text,
                 txtCallNumber.Text,
                 NowInComeCallId));

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnLogout.Enabled = false;
            btnGetBankIdNumber.Enabled = false;
            btnGetIdCardNumber.Enabled = false;
            btnShowCustomerInfo.Enabled = false;
            btnSendCustomerInfo.Enabled = false;
            No_CardInfo.Enabled = false;
            BtnCusBtomerPeriodOfValidity.Enabled = false;
            btnSecurity_code.Enabled = false;
            btnLogin.Enabled = true;
            txtCtiId.Enabled = true;
            txtCtiPwd.Enabled = true;
            //btnCtiConfig.Enabled = true;
            txtCtiId.Focus();
        }

        private void btnGetBankIdNumber_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NowInComeCallId))
            {
                axaOCX1.DoSetAssociatedData("WorkId", "" + txtCtiId.Text.Trim() + "");
                axaOCX1.DoSetAssociatedData("CallSensitiveService", "" + NowInComeCallId + "|1");
                axaOCX1.cmdAuto2("ToAuto", "CONF");
                btnGetBankIdNumber.Enabled = false;
                btnGetIdCardNumber.Enabled = false;
                BtnCusBtomerPeriodOfValidity.Enabled = false;
                btnSecurity_code.Enabled = false;
                btnGetBankIdNumber.Text = "正在获取客户信用卡号";
                btnGetIdCardNumber.Text = "接入语音获取证件号码";
                BtnCusBtomerPeriodOfValidity.Text = "接入语音获取有效期";
                btnSecurity_code.Text = "接入语音获取安全码";

                LogUtil.Debug(string.Format("坐席点击获取信用卡号码:\r\n账号：{0},\r\n密码：{1},\r\n时间：{2}\r\n来电号码：{3}\r\n被叫号码：{4}\r\n新来电会话标识：{5}",
                    txtCtiId.Text.Trim(),
                    txtCtiPwd.Text.Trim(),
                    DateTime.Now.ToString(),
                     txtInComeNumber.Text,
                     txtCallNumber.Text,
                     NowInComeCallId));

            }
            else
            {
                MessageBox.Show("生成编号出错！系统重新生成编号");
            }

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    axaOCX1.DoSetAssociatedData("CallSensitiveService", "" + NowInComeCallId + "|2");
        //    axaOCX1.cmdAuto2("ToAuto", "CONF");
        //}

        //private void btnShowCustomerInfo_Click(object sender, EventArgs e)
        //{
        //    GetCustomerInfo();
        //}

        private void GetCustomerInfo()
        {
            string sql = "SELECT * FROM [sensitive_info] WHERE user_id = $userId$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("userId", NowInComeCallId);

            DataTable dt = SmartClientDbUtil.Current.IData.ExecuteDataTable(sql, pc);
            if (dt != null && dt.Rows.Count > 0)
            {
                CustomerCreditCardNumber = dt.Rows[0]["creditcard_number"].ToString();
                CustomerIdCardNumber = dt.Rows[0]["idcard_number"].ToString();
                CustomerPeriodOfValidity = dt.Rows[0]["period"].ToString().Trim();
                Customersecurity_code = dt.Rows[0]["security_code"].ToString().Trim();
                txtCreditCardNumber.Text = (string.IsNullOrEmpty(CustomerCreditCardNumber)) ? "" : CustomerCreditCardNumber.Substring(0, 6) + "*******" + CustomerCreditCardNumber.Substring(CustomerCreditCardNumber.Length - 4, 4);
                txtIdCardNumber.Text = (string.IsNullOrEmpty(CustomerIdCardNumber)) ? "" : CustomerIdCardNumber.Substring(0, 6) + "********" + CustomerIdCardNumber.Substring(CustomerIdCardNumber.Length - 4, 4);
                txtCustomerPeriodOfValidity.Text = string.IsNullOrEmpty(CustomerPeriodOfValidity) ? "" : CustomerPeriodOfValidity;
                txtsecurity_code.Text = string.IsNullOrEmpty(Customersecurity_code) ? "" : "***";
            }

            LogUtil.Debug(string.Format("获取敏感信息结果，从系统中提取数据:\r\n账号：{0},\r\n密码：{1},\r\n时间：{2}\r\n来电号码：{3}\r\n被叫号码：{4}\r\n新来电会话标识：{5}\r\n信用卡号：{6}，\r\n身份证：{7}，\r\n有效期：{8}，\r\n安全码：{9}",
    txtCtiId.Text.Trim(),
    txtCtiPwd.Text.Trim(),
    DateTime.Now.ToString(),
     txtInComeNumber.Text,
     txtCallNumber.Text,
     NowInComeCallId,
     CustomerCreditCardNumber,
     CustomerIdCardNumber,
     CustomerPeriodOfValidity,
     Customersecurity_code));

            //SetDbDataDelegate postDelegate = new SetDbDataDelegate(MainForm.Log);
            //postDelegate.BeginInvoke(NowInComeCallId, txtInComeNumber.Text, txtCallNumber.Text, txtCtiId.Text, CustomerCreditCardNumber, CustomerIdCardNumber, null, null);


            //Log(NowInComeCallId, txtInComeNumber.Text,
            //    txtCallNumber.Text,txtCtiId.Text,
            //    CustomerCreditCardNumber, CustomerIdCardNumber);
        }

        //        private static void Log(string autoId, string incomeNumber, string calledNumber, string workId, string creditNumber, string idcardNumber)
        //        {
        //            string checkExistSQL = "select * from credit_message_info where auto_id = $auto_id$";
        //            ParameterCollection pc = new ParameterCollection();
        //            pc.Add("auto_id", autoId);
        //            pc.Add("work_id", workId);
        //            pc.Add("income_number", incomeNumber);
        //            pc.Add("called_number", calledNumber);
        //            pc.Add("idcard_number", idcardNumber);
        //            pc.Add("creditcard_number", creditNumber);

        //            DataTable exists = SmartClientDbUtil.Current.IData.ExecuteDataTable(checkExistSQL, pc);
        //            if (exists != null)
        //            {
        //                string updateSQL = @"UPDATE 
        //                                            [credit_message_info]
        //                                       SET 
        //                                          [work_id] = $work_id$
        //                                          ,[income_number] = $income_number$
        //                                          ,[called_number] = $called_number$
        //                                          ,[idcard_number] = $idcard_number$
        //                                          ,[creditcard_number] = $creditcard_number$
        //                                          ,[modified_on] = GETDATE()
        //                                     WHERE 
        //                                          auto_id = $auto_id$";

        //                SmartClientDbUtil.Current.IData.ExecuteNonQuery(updateSQL, pc);
        //            }
        //            else
        //            {
        //                string insertSQL = @"INSERT INTO [credit_message_info]
        //                           ([auto_id],[work_id],[income_number],[called_number],[idcard_number],[creditcard_number],[created_on],[modified_on])
        //                                VALUES
        //                           ($auto_id$,$work_id$,$income_number$ ,$called_number$,$idcard_number$,$creditcard_number$,GETDATE(),GETDATE())";

        //                SmartClientDbUtil.Current.IData.ExecuteNonQuery(insertSQL, pc);
        //            }
        //        }

        ///   <summary>   
        ///   启动其他的应用程序   
        ///   </summary>   
        ///   <param   name="file">应用程序名称</param>   
        ///   <param   name="workdirectory">应用程序工作目录</param>   
        ///   <param   name="args">命令行参数</param>   
        ///   <param   name="style">窗口风格</param>   
        public static bool StartProcess(string file, string args, ProcessWindowStyle style)
        {
            try
            {
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(file, args);
                startInfo.WindowStyle = style;
                //startInfo.WorkingDirectory = workdirectory;
                myprocess.StartInfo = startInfo;
                myprocess.StartInfo.UseShellExecute = false;
                myprocess.Start();
                return true;
            }
            catch (Exception e0)
            {
                MessageBox.Show("启动应用程序时出错！原因：" + e0.Message);
            }
            return false;
        }

        private string GetBase64(string srouce)
        {
            if (srouce == null)
            {
                srouce = "";
            }

            byte[] bytes = Encoding.Default.GetBytes(srouce);
            //编码
            return Convert.ToBase64String(bytes);
        }

        private void btnSendCustomerInfo_Click(object sender, EventArgs e)
        {
            //IVR按键：BusinessSelectCode身份证号：CustomerIdCardNumber信用卡号：CustomerCreditCardNumber安全码：   CustomerSecurityCode有效期：   CustomerPeriodCode

            //txtCreditCardNumber.Text = axaOCX1.DoGetAssociatedData("\"CustomerCreditCardNumber\"");
            //txtIdCardNumber.Text = axaOCX1.DoGetAssociatedData("\"CustomerIdCardNumber\"");
            //txtInComeNumber.Text = "13670122126";
            //http://icbcjf.intsun.com/logincustomer?cardsize=6222305893033359&docnumber=452421198402171052&telnumber=13670122126

            string url = (chkPublicNet.Checked == false)
                ? "http://172.16.23.2/logincustomer?cardsize={0}&docnumber={1}&telnumber={2}"
                : "http://121.15.166.101/logincustomer?cardsize={0}&docnumber={1}&telnumber={2}";


            if (string.IsNullOrEmpty(CustomerCreditCardNumber) || string.IsNullOrEmpty(CustomerIdCardNumber))
            {
                if (MessageBox.Show("系统未能完整获取客户信息，继续向业务系统发送数据吗？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    string NewCustomerIdCardNumber = string.Empty;
                    if (CustomerIdCardNumber.IndexOf('*') == -1)
                    {
                        NewCustomerIdCardNumber = CustomerIdCardNumber;
                    }
                    else
                    {
                        NewCustomerIdCardNumber = CustomerIdCardNumber.Replace('*', 'X');
                    }
                    StartProcess(
                        @"C:\\Program Files\\Internet Explorer\\IExplore.exe",
                        string.Format(url,
                                        GetBase64(CustomerCreditCardNumber),//客户的信用卡号
                                        GetBase64(NewCustomerIdCardNumber),//客户身份证
                                        GetBase64(txtCallNumber.Text.Trim())),
                        ProcessWindowStyle.Normal);

                    LogUtil.Debug(string.Format(@"发送信息至积分兑换网站：\r\n客户信用卡：{0},\r\n身份证号：{1},\r\n安全码：{2},\r\n有效期：{3},\r\n工号：{4},\r\n操作员：{5}\r\nURL地址：{6}",
                    CustomerCreditCardNumber,
                    CustomerIdCardNumber,
                    Customersecurity_code,
                    txtCustomerPeriodOfValidity.Text.Trim(),
                    txtCtiId.Text.Trim(),
                    TxtUseName.Text.Trim(),
                    string.Format(url, 
                            CustomerCreditCardNumber,        //客户的卡号
                            NewCustomerIdCardNumber,    //客户证件号
                            txtCustomerPeriodOfValidity.Text.Trim(), //有效期
                            TxtUseName.Text.Trim(),//操作员
                            Customersecurity_code)));
                }
            }
            else
            {
                string NewCustomerIdCardNumber = string.Empty;
                if (CustomerIdCardNumber.IndexOf('*') == -1)
                {
                    NewCustomerIdCardNumber = CustomerIdCardNumber;
                }
                else
                {
                    NewCustomerIdCardNumber = CustomerIdCardNumber.Replace('*', 'X');
                }
                StartProcess(
                        @"C:\\Program Files\\Internet Explorer\\IExplore.exe",
                        string.Format(url,
                                        GetBase64(CustomerCreditCardNumber),   //客户的信用卡号
                                        GetBase64(NewCustomerIdCardNumber), //身份证
                                        GetBase64(txtCallNumber.Text.Trim())),
                        ProcessWindowStyle.Normal);

                LogUtil.Debug(string.Format(@"发送信息至积分兑换网站：\r\n客户信用卡：{0},\r\n身份证号：{1},\r\n安全码：{2},\r\n有效期：{3},\r\n工号：{4},\r\n操作员：{5}\r\nURL地址：{6}",
                    CustomerCreditCardNumber,
                    CustomerIdCardNumber,
                    Customersecurity_code,
                    txtCustomerPeriodOfValidity.Text.Trim(),
                    txtCtiId.Text.Trim(),
                    TxtUseName.Text.Trim(),
                    string.Format(url,
                            CustomerCreditCardNumber,   //客户的信用卡号
                            NewCustomerIdCardNumber,    //客户身份证
                            txtCustomerPeriodOfValidity.Text.Trim(), //有效期
                            TxtUseName.Text.Trim(),//操作员
                            Customersecurity_code)));
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            axaOCX1.LogOUT();
        }

        private void btnGetIdCardNumber_Click(object sender, EventArgs e)
        {
            axaOCX1.DoSetAssociatedData("CallSensitiveService", "" + NowInComeCallId + "|2");
            axaOCX1.cmdAuto2("ToAuto", "CONF");
            btnGetBankIdNumber.Enabled = false;
            btnGetIdCardNumber.Enabled = false;
            BtnCusBtomerPeriodOfValidity.Enabled = false;
            btnSecurity_code.Enabled = false;
            btnGetBankIdNumber.Text = "接入语音获取信用卡号码";
            btnGetIdCardNumber.Text = "正在获取客户身份证号";
            BtnCusBtomerPeriodOfValidity.Text = "接入语音获取有效期";
            btnSecurity_code.Text = "接入语音获取安全码";


            LogUtil.Debug(string.Format("坐席点击获取身份证号:\r\n账号：{0},\r\n密码：{1},\r\n时间：{2}\r\n来电号码：{3}\r\n被叫号码：{4}\r\n新来电会话标识：{5}",
    txtCtiId.Text.Trim(),
    txtCtiPwd.Text.Trim(),
    DateTime.Now.ToString(),
     txtInComeNumber.Text,
     txtCallNumber.Text,
     NowInComeCallId));
        }

        private void txtCtiId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                txtCtiPwd.Focus();
            }
        }

        private void txtCtiPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnLogin.Focus();
            }
        }

        private void axaOCX1_EVTReturnStatusEvent(object sender, AxAGENTOCXLib._IaOCXEvents_EVTReturnStatusEvent e)
        {
            lblStatus.Text = e.rstatus;
            //txtStatus.Text += e.rstatus + "\r\n";


            if (e.rstatus == "ACD")
            {
                btnGetBankIdNumber.Enabled = true;
                btnGetIdCardNumber.Enabled = true;
                BtnCusBtomerPeriodOfValidity.Enabled = true;
                btnSecurity_code.Enabled = true;
                btnGetBankIdNumber.Text = "接入语音获取信用卡号码";
                btnGetIdCardNumber.Text = "接入语音获取证件号码";
                BtnCusBtomerPeriodOfValidity.Text = "接入语音获取有效期";
                btnSecurity_code.Text = "接入语音获取安全码";
                GetCustomerInfo();
            }
        }

        private void axaOCX1_EVTDialOutEvent(object sender, EventArgs e)
        {
            if (DialOutBox == null)
            {
                this.DialOutBox = new frmDialOut(this);
            }

            DialOutBox.Show();
        }


        private void axaOCX1_EVTButtonStatusEvent(object sender, AxAGENTOCXLib._IaOCXEvents_EVTButtonStatusEvent e)
        {
            switch (e.sTitle)
            {

                case "OnHook":
                    e.sTitle = "挂机";
                    break;
                case "OffHook":
                    e.sTitle = "摘机";
                    break;
                case "HOOKOFFCONSULT":
                    e.sTitle = "接受";
                    break;
                case "Hold":
                    e.sTitle = "保持";
                    break;
                case "HoldCancel":
                    e.sTitle = "取消";
                    break;
                case "Transfer":
                    e.sTitle = "转接";
                    break;
                case "CancelTransfer":
                    e.sTitle = "取消";
                    break;
                case "DialOut":
                    e.sTitle = "外拨";
                    lblStatus.Text = e.sTitle;
                    break;
                case "CancelDialOut":
                    e.sTitle = "取消";
                    break;
                case "Consultation":
                    e.sTitle = "磋商";
                    break;
                case "CancelConsultation":
                    e.sTitle = "取消";
                    break;
                case "StopConsultation":
                    e.sTitle = "磋商结束";
                    break;
                case "ConsultTransfer":
                    e.sTitle = "磋商转接";
                    break;
                case "Auto":
                    e.sTitle = "磋商转接";
                    break;
                case "OutPhone":
                    e.sTitle = "自动";
                    break;
                case "CancelOutPhone":
                    e.sTitle = "取消";
                    break;
                case "Play":
                    e.sTitle = "放音";
                    break;
                case "PlayCancel":
                    e.sTitle = "结束";
                    break;
                case "Fax":
                    e.sTitle = "传真";
                    break;
                case "FaxStop":
                    e.sTitle = "结束";
                    break;
                case "Pause":
                    e.sTitle = "暂停";
                    break;
                case "Continue":
                    e.sTitle = "恢复";
                    break;
                case "ContinueDialTask":
                    e.sTitle = "放弃回访";
                    break;
                case "Listen":
                    e.sTitle = "监听";
                    break;
                case "CancelListen":
                    e.sTitle = "结束";
                    break;
                case "Disconnect":
                    e.sTitle = "强插";
                    break;
                case "Conference":
                    e.sTitle = "会议";
                    break;
                case "CancelConference":
                    e.sTitle = "取消";
                    break;
                case "RopCall":
                    e.sTitle = "拦截";
                    break;
                case "LOGINSUCC":
                    e.sTitle = "登录成功";
                    break;
                case "LOGINFAIL":
                    e.sTitle = "登录失败";
                    break;
                case "TRANSFERFAIL":
                    e.sTitle = "转接失败";
                    break;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void No_CardInfo_Click(object sender, EventArgs e)
        {
            //IVR按键：BusinessSelectCode身份证号：CustomerIdCardNumber信用卡号：CustomerCreditCardNumber安全码：   CustomerSecurityCode有效期：   CustomerPeriodCode

            //txtCreditCardNumber.Text = axaOCX1.DoGetAssociatedData("\"CustomerCreditCardNumber\"");
            //txtIdCardNumber.Text = axaOCX1.DoGetAssociatedData("\"CustomerIdCardNumber\"");
            //txtInComeNumber.Text = "13670122126";
            string url = (chkPublicNet.Checked == false)
                ? "http://172.16.21.22:6001/Bank/AddCardTwo.aspx?IDcard={0}&carid={1}&Validity={2}&WriteMan={3}&CVN2={4}"
                : "http://121.15.166.100:6001/Bank/AddCardTwo.aspx?IDcard={0}&carid={1}&Validity={2}&WriteMan={3}&CVN2={4}";



            if (string.IsNullOrEmpty(CustomerCreditCardNumber) || string.IsNullOrEmpty(CustomerIdCardNumber) || string.IsNullOrEmpty(TxtUseName.Text.Trim()) || string.IsNullOrEmpty(txtCustomerPeriodOfValidity.Text.Trim()) || string.IsNullOrEmpty(txtsecurity_code.Text.Trim()))
            {
                MessageBox.Show("系统未能完整获取客户信息,请获取完整信息后重试");
            }
            else
            {
                string NewCustomerIdCardNumber = string.Empty;
                if (CustomerIdCardNumber.IndexOf('*') == -1)
                {
                    NewCustomerIdCardNumber = CustomerIdCardNumber;
                }
                else
                {
                    NewCustomerIdCardNumber = CustomerIdCardNumber.Replace('*', 'X');
                }
                StartProcess(
                    @"C:\\Program Files\\Internet Explorer\\IExplore.exe",
                    string.Format(url,
                                    GetBase64(NewCustomerIdCardNumber),    //客户证件号
                                    GetBase64(CustomerCreditCardNumber),        //客户的卡号
                                    GetBase64(txtCustomerPeriodOfValidity.Text.Trim()), //有效期
                                    GetBase64(TxtUseName.Text.Trim()),//操作员
                                    GetBase64(Customersecurity_code)),    //安全码 
                    ProcessWindowStyle.Normal);


                LogUtil.Debug(string.Format(@"发送信息至无卡支付系统：\r\n客户信用卡：{0},\r\n身份证号：{1},\r\n安全码：{2},\r\n有效期：{3},\r\n工号：{4},\r\n操作员：{5}\r\nURL地址：{6}",
                    NewCustomerIdCardNumber,
                    CustomerCreditCardNumber,
                    Customersecurity_code,
                    txtCustomerPeriodOfValidity.Text.Trim(),
                    txtCtiId.Text.Trim(),
                    TxtUseName.Text.Trim(),
                    string.Format(url,
                            NewCustomerIdCardNumber,    //客户证件号
                            CustomerCreditCardNumber,        //客户的卡号
                            txtCustomerPeriodOfValidity.Text.Trim(), //有效期
                            TxtUseName.Text.Trim(),//操作员
                            Customersecurity_code)));

            }
        }

        private void btnShowCustomerInfo_Click(object sender, EventArgs e)
        {

        }

        private void BCusBtomerPeriodOfValidity_Click(object sender, EventArgs e)
        {
            axaOCX1.DoSetAssociatedData("CallSensitiveService", "" + NowInComeCallId + "|3");
            axaOCX1.cmdAuto2("ToAuto", "CONF");
            btnGetBankIdNumber.Enabled = false;
            btnGetIdCardNumber.Enabled = false;
            BtnCusBtomerPeriodOfValidity.Enabled = false;
            btnSecurity_code.Enabled = false;
            BtnCusBtomerPeriodOfValidity.Text = "正在获取客户有效期";
            btnGetBankIdNumber.Text = "接入语音获取客户信用卡号";
            btnGetIdCardNumber.Text = "接入语音获取证件号码";
            btnSecurity_code.Text = "接入语音获取安全码";

            LogUtil.Debug(string.Format("坐席点击获取有效期:\r\n账号：{0},\r\n密码：{1},\r\n时间：{2}\r\n来电号码：{3}\r\n被叫号码：{4}\r\n新来电会话标识：{5}",
    txtCtiId.Text.Trim(),
    txtCtiPwd.Text.Trim(),
    DateTime.Now.ToString(),
     txtInComeNumber.Text,
     txtCallNumber.Text,
     NowInComeCallId));
        }

        private void btnSecurity_code_Click(object sender, EventArgs e)
        {
            axaOCX1.DoSetAssociatedData("CallSensitiveService", "" + NowInComeCallId + "|4");
            axaOCX1.cmdAuto2("ToAuto", "CONF");
            btnGetBankIdNumber.Enabled = false;
            btnGetIdCardNumber.Enabled = false;
            BtnCusBtomerPeriodOfValidity.Enabled = false;
            btnSecurity_code.Enabled = false;
            BtnCusBtomerPeriodOfValidity.Text = "接入语音获取有效期";
            btnGetIdCardNumber.Text = "接入语音获取证件号码";
            BtnCusBtomerPeriodOfValidity.Text = "接入语音获取有效期";
            btnSecurity_code.Text = "正在获取客户安全码";

            LogUtil.Debug(string.Format("坐席点击获取安全码:\r\n账号：{0},\r\n密码：{1},\r\n时间：{2}\r\n来电号码：{3}\r\n被叫号码：{4}\r\n新来电会话标识：{5}",
    txtCtiId.Text.Trim(),
    txtCtiPwd.Text.Trim(),
    DateTime.Now.ToString(),
     txtInComeNumber.Text,
     txtCallNumber.Text,
     NowInComeCallId));
        }
        private void axaOCX1_EVTWrapUpEvent(object sender, EventArgs e)
        {
            if (this.cheAutoWrapend.Checked)
            {
                this.axaOCX1.WrapEnd();
            }
        }

        private void axaOCX1_EVTListenEvent(object sender, _IaOCXEvents_EVTListenEvent e)
        {
            axaOCX1.ShowListenDlg();
        }
    }


    //internal delegate void SetDbDataDelegate(string autoId, string incomeNumber, string calledNumber, string workId, string creditNumber, string idcardNumber);

}


