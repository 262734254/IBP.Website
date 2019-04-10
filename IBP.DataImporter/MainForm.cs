using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IBP.Models;
using Framework.Common;
using IBP.Services;

namespace IBP.DataImporter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void btnSyncCustomer_Click(object sender, EventArgs e)
        {
            btnSyncCustomer.Enabled = false;
            SyncCustomerBasicInfo();
            btnSyncCustomer.Enabled = true;
        }


        #region 同步客户基本信息

        /// <summary>
        /// 同步客户基本信息。
        /// </summary>
        private void SyncCustomerBasicInfo()
        {
            // 1. 获取旧库客户信息ID列表
            string sql = "select * from C_Info";
            DataTable oldCustomerIdTable = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);

            if (oldCustomerIdTable != null && oldCustomerIdTable.Rows.Count > 0)
            {
                Msg(string.Format("开始同步客户基本信息，总数 {0} 行", oldCustomerIdTable.Rows.Count), false);

                for (int i = 0; i < oldCustomerIdTable.Rows.Count; i++)
                {
                    if (CheckExistCustomerCodeFromNewDB(oldCustomerIdTable.Rows[i]["id"].ToString()))
                    {
                        // 客户信息存在于新库，执行更新操作
                    }
                    else
                    {
                        // 客户信息不存在于新库，执行插入操作
                        if (InsertNewCustomerBasicInfo(oldCustomerIdTable.Rows[i]) == false)
                        {
                            Msg(string.Format("插入旧库ID为【{0}】客户基本信息失败", oldCustomerIdTable.Rows[i]["id"]), false);
                        }
                    }
                }

                Msg("同步客户基本信息完成", false);

                UpdateNewCustomerInfoSaleForm();

                UpdateNewCustomerInfoLevel();

                UpdateNewCustomerInfoCarriers();

                UpdateNewCustomerInfoPhoneBrand();

                UpdateNewCustomerInfoPreferredBrand();

                UpdateNewCustomerInfoPhonePrice();

                UpdateNewCustomerInfoPhoneConsumer();


                string initsql = "update dbo.customer_basic_info set home_phone = '' where home_phone is null;";

                int cc = NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql);
                Msg(string.Format("更新客户固定电话为空的记录【{0}】条，更新为空字符串", cc), false);

                initsql = "update dbo.customer_basic_info set using_phone_type = '' where using_phone_type is null;";
                cc = NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql);
                Msg(string.Format("更新客户在用手机型号为空的记录【{0}】条，更新为空字符串", cc), false);

                initsql = "update dbo.customer_basic_info set mobile_phone = '' where mobile_phone is null;";
                cc = NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql);
                Msg(string.Format("更新客户在用手机号码为空的记录【{0}】条，更新为空字符串", cc), false);

                initsql = "update dbo.customer_basic_info set other_phone = '' where other_phone is null;";
                cc = NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql);
                Msg(string.Format("更新客户在用其他号码为空的记录【{0}】条，更新为空字符串", cc), false);

                initsql = "update dbo.customer_basic_info set using_smartphone = '3' where using_smartphone is null;";
                cc = NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql);
                Msg(string.Format("更新客户在用是否使用智能机为空的记录【{0}】条，更新为未知", cc), false);

                initsql = "update dbo.customer_basic_info set using_smartphone = '3' where using_smartphone = '4';";
                cc = NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql);
                Msg(string.Format("更新客户在用是否使用智能机为空的记录【{0}】条，更新为未知", cc), false);

                //UpdateNewCustomerMobileComeFrom();
                //UpdateNewCustomerPhoneComeFrom();
            }

        }


        /// <summary>
        /// 插入客户基本信息至新库。
        /// </summary>
        /// <param name="oldData"></param>
        /// <returns></returns>
        private bool InsertNewCustomerBasicInfo(DataRow oldData)
        {
            if (oldData == null)
                return false;

            CustomerBasicInfoModel basicInfo = new CustomerBasicInfoModel();
            basicInfo.CustomerId = Guid.NewGuid().ToString();
            basicInfo.CustomerCode = oldData["id"].ToString();
            basicInfo.CustomerName = (oldData["CName"] == DBNull.Value) ? null :  oldData["CName"].ToString().Replace("[新增]","");
            if (basicInfo.CustomerName == "未知")
            {
                basicInfo.CustomerName = null;
            }

            basicInfo.Sex = Convert.ToInt32(oldData["CSex"]);
            basicInfo.SalesFrom = oldData["CLY"].ToString();

            basicInfo.MobilePhone = (oldData["SJ"] == DBNull.Value || oldData["SJ"].ToString().Length == 0) ? null : oldData["SJ"].ToString();
            basicInfo.HomePhone = (oldData["Tel"] == DBNull.Value || oldData["Tel"].ToString().Length == 0) ? null : oldData["Tel"].ToString();
            basicInfo.OtherPhone = (oldData["OtherTel"] == DBNull.Value || oldData["OtherTel"].ToString().Length == 0) ? null : oldData["OtherTel"].ToString();

            basicInfo.Level = oldData["CLevel"].ToString();
            basicInfo.Carriers = (oldData["C_SJYYS"] == DBNull.Value) ? null : oldData["C_SJYYS"].ToString();
            basicInfo.UsingPhoneBrand = (oldData["C_SJPP"] == DBNull.Value) ? null : oldData["C_SJPP"].ToString();
            basicInfo.UsingPhoneType = (oldData["C_SJXH"] == DBNull.Value) ? null : oldData["C_SJXH"].ToString();
            basicInfo.CommunicationConsumer = (oldData["C_TXSF"] == DBNull.Value) ? null : oldData["C_TXSF"].ToString();
            basicInfo.PreferredPhoneBrand = (oldData["C_YXPP"] == DBNull.Value) ? null : oldData["C_YXPP"].ToString();
            if (oldData["C_SJZF"] != DBNull.Value)
            {
                basicInfo.UsingSmartphone = Convert.ToInt32(oldData["C_SJZF"]);
            }
            basicInfo.MobilePhonePrice = (oldData["C_SJJW"] == DBNull.Value) ? null : oldData["C_SJJW"].ToString();

            #region 身份证信息

            IdCardInfo cardInfo = GetIdCardByCustomerCodeFromOldDatabase(basicInfo.CustomerCode);
            if (cardInfo != null)
            {
                basicInfo.IdcardType = cardInfo.CardType;
                basicInfo.IdcardNumber = cardInfo.CardNum;
            }

            #endregion

            basicInfo.Status = 0;
            if (oldData["AddDate"] == DBNull.Value)
            {
                basicInfo.CreatedOn = Convert.ToDateTime("1900-01-01 00:00:00");
                CustomerMemoInfoModel memoInfo = new CustomerMemoInfoModel();
                memoInfo.Memo = "历史数据导入时无创建日期，使用初始值1900-01-01 00:00:00";
                memoInfo.CustomerId = basicInfo.CustomerId;
                memoInfo.MemoId = Guid.NewGuid().ToString();
                memoInfo.CreatedOn = DateTime.Now;
                memoInfo.CreatedBy = "Admin";

                 NewDbHelper.NewDB.Create(memoInfo);
            }
            else
            {
                basicInfo.CreatedOn = Convert.ToDateTime(oldData["AddDate"]);
            }
            basicInfo.StatusCode = 0;

            return NewDbHelper.NewDB.Create(basicInfo) > 0;
        }

        /// <summary>
        /// 同步客户备注信息。
        /// </summary>
        private void SyncCustomerMemoInfo()
        {
            string sql = @"select u.UserName, c.* from C_BZXX c left join lrs_Users u on c.UserID = u.UserID where CID in (select ID from C_Info)";
            string sql2 = @"select count(1) from customer_memo_info where modified_by = $modified_by$";

            DataTable oldMemoTable = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);
            ParameterCollection pc = new ParameterCollection();
            int succ = 0, skip = 0;
            if (oldMemoTable != null && oldMemoTable.Rows.Count > 0)
            {
                CustomerMemoInfoModel memo = null;

                for (int i = 0; i < oldMemoTable.Rows.Count; i++)
                {
                    pc.Clear();
                    pc.Add("modified_by", oldMemoTable.Rows[i]["id"].ToString());

                    //if (DbHelper.NewDB.IData.ExecuteScalar(sql2, pc).ToString() == "0")
                    //{
                        memo = new CustomerMemoInfoModel();
                        memo.MemoId = Guid.NewGuid().ToString();

                        memo.CustomerId = GetNewDBCustomerIdByCustomerCode(oldMemoTable.Rows[i]["cid"].ToString());

                        if (memo.CustomerId == null)
                        {
                            skip++;
                            continue;
                        }

                        memo.Memo = oldMemoTable.Rows[i]["notes"].ToString();
                        memo.Status = 0;

                        memo.CreatedOn = Convert.ToDateTime(oldMemoTable.Rows[i]["AddDate"]);
                        memo.CreatedBy = GetNewDBUserIdByWorkId(oldMemoTable.Rows[i]["username"].ToString());
                        memo.ModifiedBy = oldMemoTable.Rows[i]["id"].ToString();
                        memo.StatusCode = 0;

                        if (NewDbHelper.NewDB.Create(memo) != 1)
                        {
                            Msg(string.Format("插入旧库ID为【{0}】客户备注信息失败", oldMemoTable.Rows[i]["id"]), false);
                        }
                        else
                        {
                            succ++;
                        }
                    //}
                    //else
                    //{
                    //    skip++;
                    //}
                }

                Msg(string.Format("同步客户备注信息完成，共 {0} 行记录, 插入 {1} 行， 忽略 {2} 行", oldMemoTable.Rows.Count, succ, skip), false);
            }
        }

        /// <summary>
        /// 同步客户联系记录。
        /// </summary>
        private void SyncCustomerContactRecords()
        {
            string sql = @"select u.UserName,c.* from C_LXJL c left join lrs_Users u on c.UserID = u.UserID where CID in (select ID from C_Info)";
            string sql2 = @"select count(1) from customer_memo_info where modified_by = $modified_by$";

            CustomDataDomainModel purposeList = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("联系记录(联系目的)",true);
            CustomDataDomainModel resultList = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("联系记录(联系结果)", true);

            DataTable oldContactTable = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);
            ParameterCollection pc = new ParameterCollection();
            int succ = 0, skip = 0;
            if (oldContactTable != null && oldContactTable.Rows.Count > 0)
            {
                CustomerContactInfoModel record = null;
                int purpose = 0, result = 0;

                for (int i = 0; i < oldContactTable.Rows.Count; i++)
                {
                    pc.Clear();
                    pc.Add("modified_by", oldContactTable.Rows[i]["id"].ToString());

                    //if (DbHelper.NewDB.IData.ExecuteScalar(sql2, pc).ToString() == "0")
                    //{
                        record = new CustomerContactInfoModel();
                        record.ContactId = Guid.NewGuid().ToString();

                        record.CustomerId = GetNewDBCustomerIdByCustomerCode(oldContactTable.Rows[i]["cid"].ToString());

                        if (record.CustomerId == null)
                        {
                            skip++;
                            continue;
                        }

                        record.CustomerPhone = (oldContactTable.Rows[i]["L_Numble"] == DBNull.Value) ? null : oldContactTable.Rows[i]["L_Numble"].ToString();
                        record.Directions = Convert.ToInt32(oldContactTable.Rows[i]["JLType"]);

                        purpose = Convert.ToInt32(oldContactTable.Rows[i]["LXMD"]);
                        if (purpose <= 0)
                        {
                            purpose = 1;
                        }
                        else
                        {
                            purpose = purpose + 1;
                        }

                        result = Convert.ToInt32(oldContactTable.Rows[i]["LXJG"]);
                        if (result <= 0)
                        {
                            result = 1;
                        }
                        else
                        {
                            result = result + 1;
                        }

                        record.Purpose = GetContactPurposeOrResult(purpose, purposeList);
                        record.Results = GetContactPurposeOrResult(result, resultList);
                        
                        record.Description = oldContactTable.Rows[i]["notes"].ToString();
                        record.Status = 0;

                        record.CreatedOn = Convert.ToDateTime(oldContactTable.Rows[i]["AddDate"]);
                        record.CreatedBy = GetNewDBUserIdByWorkId(oldContactTable.Rows[i]["username"].ToString());
                        record.ModifiedBy = oldContactTable.Rows[i]["id"].ToString();
                        record.StatusCode = 0;

                        if (NewDbHelper.NewDB.Create(record) != 1)
                        {
                            Msg(string.Format("插入旧库ID为【{0}】客户联系记录信息失败", oldContactTable.Rows[i]["id"]), false);
                        }
                        else
                        {
                            succ++;
                        }
                    //}
                    //else
                    //{
                    //    skip++;
                    //}
                }

                Msg(string.Format("同步客户联系记录完成，共 {0} 行记录, 插入 {1} 行， 忽略 {2} 行", oldContactTable.Rows.Count, succ, skip), false);
            }
        }

        /// <summary>
        /// 同步客户信用卡信息记录。
        /// </summary>
        private void SyncCustomerCreditCardInfo()
        {
            string truncateSQL = "truncate table customer_creditcard_info";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(truncateSQL);

            string sql = @"select * from C_CardInfo where CardNum <> '' and CID in (select ID from C_Info)";
            string getbankSql = @"select * from dbo.bankcard_type_info where card_bin_code = $card_bin_code$";
            string updateCustomerSQL = @"update customer_basic_info set birthday = $birthday$  where customer_id = $customer_id$";
            DataTable oldCardList = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);
            ParameterCollection pc = new ParameterCollection();
            ParameterCollection pc2 = new ParameterCollection();
            BankcardTypeInfoModel bankModel = null;
            DataTable bankTable = null;

            CustomDataDomainModel IdTypeList = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("证件类型", false);
                
            if (oldCardList != null && oldCardList.Rows.Count > 0)
            {
                CustomerCreditcardInfoModel cardInfo = null;
                int succ = 0, skip = 0;
                for (int i = 0; i < oldCardList.Rows.Count; i++)
                {
                    cardInfo = new CustomerCreditcardInfoModel();
                    cardInfo.CreditcardId = Guid.NewGuid().ToString();

                    cardInfo.CustomerId = GetNewDBCustomerIdByCustomerCode(oldCardList.Rows[i]["cid"].ToString());
                    if (cardInfo.CustomerId == null)
                    {
                        skip++;
                        continue;
                    }
                    cardInfo.CreditcardNumber = (oldCardList.Rows[i]["CardNum"] == DBNull.Value) ? null : oldCardList.Rows[i]["CardNum"].ToString().Replace(" ","");
                    cardInfo.Period = (oldCardList.Rows[i]["YXQ"] == DBNull.Value) ? null : oldCardList.Rows[i]["YXQ"].ToString().Replace(" ", "");
                    cardInfo.SecurityCode = (oldCardList.Rows[i]["AQM"] == DBNull.Value) ? null : oldCardList.Rows[i]["AQM"].ToString().Replace(" ", "");
                    cardInfo.Bank = (oldCardList.Rows[i]["KKH"] == DBNull.Value) ? null : oldCardList.Rows[i]["KKH"].ToString().Replace(" ", "");
                    cardInfo.OpeningAddress = (oldCardList.Rows[i]["KKD"] == DBNull.Value) ? null : oldCardList.Rows[i]["KKD"].ToString().Replace(" ", "");
                    cardInfo.CardUsername = (oldCardList.Rows[i]["XM"] == DBNull.Value) ? null : oldCardList.Rows[i]["XM"].ToString().Replace(" ", "");
                    cardInfo.IdcardType = (oldCardList.Rows[i]["ZJLX"] == DBNull.Value) ? null : oldCardList.Rows[i]["ZJLX"].ToString().Replace(" ", "");
                    cardInfo.IdcardNumber = (oldCardList.Rows[i]["ZJNum"] == DBNull.Value) ? null : oldCardList.Rows[i]["ZJNum"].ToString().Replace(" ", "");

                    foreach (CustomDataValueDomainModel typeitem in IdTypeList.ValueList.Values)
                    {
                        if (typeitem.DataValueCode == cardInfo.IdcardType.Trim())
                        {
                            cardInfo.IdcardType = typeitem.ValueId;
                            break;
                        }
                    }

                    CreditCardInfo cardBill = GetCreditCardFromOldDB(cardInfo.CreditcardNumber, oldCardList.Rows[i]["cid"].ToString());
                    if (cardBill != null)
                    {
                        cardInfo.BillChinaId = cardBill.ChinaId;
                        cardInfo.BillAddress = cardBill.BillAddress;
                    }

                    if (string.IsNullOrEmpty(cardInfo.CreditcardNumber) == false && cardInfo.CreditcardNumber.Length > 6)
                    {
                        pc.Clear();
                        pc.Add("card_bin_code", cardInfo.CreditcardNumber.Substring(0, 6));
                        bankTable = NewDbHelper.NewDB.IData.ExecuteDataTable(getbankSql, pc);
                        if (bankTable != null)
                        {
                            bankModel = new BankcardTypeInfoModel();
                            NewDbHelper.NewDB.ConvertFrom(bankModel, bankTable);

                            cardInfo.Bank = bankModel.BankEnumValue;
                            cardInfo.CardType = bankModel.CardType;
                            cardInfo.CardLevel = bankModel.CardLevel;
                            cardInfo.CardBrand = bankModel.CardBrand;
                            cardInfo.CanbeStage = (bankModel.BankcardEnumValue == "FEB39D81-26EC-4A20-97F2-F148FDC87AFD") ? 0 : 1;
                        }
                    }

                    cardInfo.MainCard = 0;
                    cardInfo.StatusCode = 0;
                    cardInfo.Status = 0;
                    cardInfo.CreatedOn = DateTime.Now;
                    cardInfo.CreatedBy = "C792D747-6B74-4A58-BB5B-D98EF420F99F";

                    if (NewDbHelper.NewDB.Create(cardInfo) != 1)
                    {
                        Msg(string.Format("插入旧库ID为【{0}】客户信用卡信息失败", oldCardList.Rows[i]["id"]), false);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(cardInfo.IdcardNumber) == false)
                        {
                            if (cardInfo.IdcardNumber.Length == 15 || cardInfo.IdcardNumber.Length == 18)
                            {
                                if (cardInfo.IdcardNumber != "000000000000000000" && cardInfo.IdcardNumber.Contains("**") == false)
                                {
                                    if (Common.CommonUtil.GetBirthDayFromIdCardNumber(cardInfo.IdcardNumber) != null)
                                    {
                                        pc2.Clear();
                                        pc2.Add("birthday", Convert.ToDateTime(Common.CommonUtil.GetBirthDayFromIdCardNumber(cardInfo.IdcardNumber)));
                                        pc2.Add("customer_id", cardInfo.CustomerId);

                                        NewDbHelper.NewDB.IData.ExecuteNonQuery(updateCustomerSQL, pc2);
                                    }
                                }
                            }
                        }
                        succ++;
                    }
                }

                Msg(string.Format("同步客户信用卡完成，共 {0} 行记录, 插入 {1} 行， 忽略 {2} 行", oldCardList.Rows.Count, succ, skip), false);
            }
        }

        /// <summary>
        /// 同步托收账户信息。
        /// </summary>
        private void SyncCustomerEntrustCardInfo()
        {
            string sql = @"select * from Order_BaseInfo where TSZH <> '' and TSZH <> '0' and TSZH not in (select CardNum from C_CardInfo)";
            DataTable oldCardList = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);

            if (oldCardList != null && oldCardList.Rows.Count > 0)
            {
                CustomerCreditcardInfoModel cardInfo = null;
                int succ = 0, skip = 0;
                for (int i = 0; i < oldCardList.Rows.Count; i++)
                {
                    cardInfo = new CustomerCreditcardInfoModel();
                    cardInfo.CreditcardId = Guid.NewGuid().ToString();

                    cardInfo.CustomerId = GetNewDBCustomerIdByCustomerCode(oldCardList.Rows[i]["cid"].ToString());
                    if (cardInfo.CustomerId == null)
                    {
                        skip++;
                        continue;
                    }
                    cardInfo.CreditcardNumber = (oldCardList.Rows[i]["TSZH"] == DBNull.Value) ? null : oldCardList.Rows[i]["TSZH"].ToString();

                    //cardInfo.Period = (oldCardList.Rows[i]["YXQ"] == DBNull.Value) ? null : oldCardList.Rows[i]["YXQ"].ToString();
                    //cardInfo.SecurityCode = (oldCardList.Rows[i]["AQM"] == DBNull.Value) ? null : oldCardList.Rows[i]["AQM"].ToString();
                    cardInfo.Bank = (oldCardList.Rows[i]["TSYH"] == DBNull.Value) ? null : oldCardList.Rows[i]["TSYH"].ToString();
                    //cardInfo.OpeningAddress = (oldCardList.Rows[i]["KKD"] == DBNull.Value) ? null : oldCardList.Rows[i]["KKD"].ToString();
                    cardInfo.CardUsername = (oldCardList.Rows[i]["TSHM"] == DBNull.Value) ? null : oldCardList.Rows[i]["TSHM"].ToString();
                    cardInfo.IdcardType = (oldCardList.Rows[i]["ZJLX"] == DBNull.Value) ? null : oldCardList.Rows[i]["ZJLX"].ToString();
                    cardInfo.IdcardNumber = (oldCardList.Rows[i]["ZJNum"] == DBNull.Value) ? null : oldCardList.Rows[i]["ZJNum"].ToString();

                    CreditCardInfo cardBill = GetCreditCardFromOldDB(cardInfo.CreditcardNumber, oldCardList.Rows[i]["cid"].ToString());
                    if (cardBill != null)
                    {
                        cardInfo.BillChinaId = cardBill.ChinaId;
                        cardInfo.BillAddress = cardBill.BillAddress;
                    }

                    cardInfo.MainCard = 1;
                    cardInfo.StatusCode = 0;
                    cardInfo.Status = 0;
                    cardInfo.CreatedOn = DateTime.Now;
                    cardInfo.CreatedBy = "admin";

                    if (NewDbHelper.NewDB.Create(cardInfo) != 1)
                    {
                        Msg(string.Format("插入旧库ID为【{0}】客户托收账户信息失败", oldCardList.Rows[i]["id"]), false);
                    }
                    else
                    {
                        succ++;
                    }
                }

                Msg(string.Format("同步客户托收账户完成，共 {0} 行记录, 插入 {1} 行， 忽略 {2} 行", oldCardList.Rows.Count, succ, skip), false);

            }
            else
            {
                Msg(string.Format("没有获取到托收账户信息数据"), false);
            }
        }

        /// <summary>
        /// 同步客户配送地址信息。
        /// </summary>
        private void SyncCustomerDeliveryInfo()
        {
            string initsql = "update Order_BaseInfo set PSSF = '广西壮族自治区',PSCS='南宁',PSQX = '良庆' where CID = 'C11092800255' ";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(initsql);

            string sql = @"select * from Order_BaseInfo where PSDZ is not null and PSDZ <> '' and PSDZ <> '广东省 选择市 选择区/县 ' and CID in (select ID from C_Info)";
            DataTable oldData = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);

            if (oldData != null && oldData.Rows.Count > 0)
            {
                CustomerDeliveryInfoModel deliveryInfo = null;
                int succ = 0, skip = 0;
                for (int i = 0; i < oldData.Rows.Count; i++)
                {
                    deliveryInfo = new CustomerDeliveryInfoModel();
                    deliveryInfo.DeliveryId = Guid.NewGuid().ToString();

                    deliveryInfo.CustomerId = GetNewDBCustomerIdByCustomerCode(oldData.Rows[i]["cid"].ToString());
                    if (deliveryInfo.CustomerId == null)
                    {
                        skip++;
                        continue;
                    }
                    deliveryInfo.DeliveryType = (oldData.Rows[i]["PSSX"] == DBNull.Value) ? 2 : Convert.ToInt32(oldData.Rows[i]["PSSX"]);
                    deliveryInfo.NeedBills = (oldData.Rows[i]["ISFP"] == DBNull.Value) ? 2 : Convert.ToInt32(oldData.Rows[i]["ISFP"]);

                    deliveryInfo.DeliveryAddress = (oldData.Rows[i]["PSDZ"] == DBNull.Value) ? null : oldData.Rows[i]["PSDZ"].ToString();
                    deliveryInfo.PostCode = (oldData.Rows[i]["PSYB"] == DBNull.Value) ? null : oldData.Rows[i]["PSYB"].ToString();
                    deliveryInfo.Consignee = (oldData.Rows[i]["PSSHR"] == DBNull.Value) ? null : oldData.Rows[i]["PSSHR"].ToString();
                    deliveryInfo.ConsigneePhone = (oldData.Rows[i]["PSDH"] == DBNull.Value) ? null : oldData.Rows[i]["PSDH"].ToString();
                    deliveryInfo.BillTitle = (oldData.Rows[i]["PBIAOTOU"] == DBNull.Value) ? null : oldData.Rows[i]["PBIAOTOU"].ToString();

                    string province = (oldData.Rows[i]["PSSF"] == DBNull.Value) ? null : oldData.Rows[i]["PSSF"].ToString();
                    string city = (oldData.Rows[i]["PSCS"] == DBNull.Value) ? null : oldData.Rows[i]["PSCS"].ToString();
                    string county = (oldData.Rows[i]["PSQX"] == DBNull.Value) ? null : oldData.Rows[i]["PSQX"].ToString();
                    province = province.Replace("省", "");
                    city = city.Replace("市", "");
                    county = county.Replace("区", "");
                    county = county.Replace("从化市", "从化");
                    county = county.Replace("电白县", "电白");
                    county = county.Replace("东莞", "东莞");
                    county = county.Replace("清新县", "清新");
                    county = county.Replace("新丰县", "新丰");
                    county = county.Replace("增城市", "增城");
                    county = county.Replace("中山市", "中山");

                    int chinaId = GetNewDbChinaId(province, city, county);
                    if (chinaId != -1)
                    {
                        deliveryInfo.DeliveryRegionId = chinaId;
                    }
                    else
                    {
                        province = deliveryInfo.DeliveryAddress.Substring(0, 2);
                        city = deliveryInfo.DeliveryAddress.Substring(4, 2);
                        county = deliveryInfo.DeliveryAddress.Substring(8, 2);

                        chinaId = GetNewDbChinaId(province, city, county);
                        if (chinaId != -1)
                        {
                            //deliveryInfo.PostCode = deliveryInfo.DeliveryAddress.Substring(10, deliveryInfo.DeliveryAddress.Length - 10);
                            deliveryInfo.DeliveryAddress = deliveryInfo.DeliveryAddress.Substring(11, deliveryInfo.DeliveryAddress.Length - 11).Trim();//广东省 深圳市 宝安区 沙井帝唐路盈利达工业园5楼3车间
                            deliveryInfo.DeliveryRegionId = chinaId;
                        }
                        else
                        {
                            deliveryInfo.DeliveryRegionId = 0;
                        }
                    }

                    deliveryInfo.StatusCode = 0;
                    deliveryInfo.Status = 0;
                    deliveryInfo.CreatedOn = (oldData.Rows[i]["ODate"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(oldData.Rows[i]["ODate"]);
                    deliveryInfo.CreatedBy = "admin";

                    if (NewDbHelper.NewDB.Create(deliveryInfo) != 1)
                    {
                        Msg(string.Format("插入旧库ID为【{0}】客户配送地址信息失败", oldData.Rows[i]["id"]), false);
                    }
                    else
                    {
                        succ++;
                    }
                }

                Msg(string.Format("同步客户配送地址完成，共 {0} 行记录, 插入 {1} 行， 忽略 {2} 行", oldData.Rows.Count, succ, skip), false);

            }
        }

        private void SyncMobileNumber()
        {
            string sql = @"select * from BD_NumInfo";
            DataTable oldData = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);

            if (oldData != null && oldData.Rows.Count > 0)
            {
                ProductInfoModel productInfo = null;
                ProductAttributesValueModel valueInfo = null;
                CustomDataDomainModel yys = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("运营商", true);
                ProductCategoryDomainModel catDomain = ProductInfoService.Instance.GetProductCategoryDomainModelByName("电信合约号码");
                if (catDomain == null || catDomain.SalestatusList == null)
                {
                    Msg(string.Format("系统中“电信合约号码产品类型信息异常"), false);
                    return;
                }

                NewDbHelper.NewDB.BeginTransaction();

                for (int i = 0; i < oldData.Rows.Count; i++)
                {
                    productInfo = new ProductInfoModel();
                    productInfo.ProductId = Guid.NewGuid().ToString();
                    productInfo.ProductCode = oldData.Rows[i]["MobileNum"].ToString();
                    productInfo.ProductName = oldData.Rows[i]["MobileNum"].ToString();
                    //productInfo.PostPrice = 0;
                    //productInfo.SalesPrice = 0;
                    //productInfo.SalesCityId = GetNewDbChinaId(null, oldData.Rows[i]["M_address"].ToString(), null);

                    productInfo.CategoryId = catDomain.BasicInfo.ProductCategoryId;
                    int salestatus = (oldData.Rows[i]["State"] == DBNull.Value) ? -1 : Convert.ToInt32(oldData.Rows[i]["State"]);
                    switch (salestatus)
                    {
                        case 0:
                            productInfo.SalesStatus = catDomain.GetSalestatusIdByName("开放");
                            //productInfo.Description = "开放";
                            break;

                        case 1:
                            productInfo.SalesStatus = catDomain.GetSalestatusIdByName("已分配");
                            //productInfo.Description = "已分配";
                            break;

                        case 2:
                            productInfo.SalesStatus = catDomain.GetSalestatusIdByName("已锁定");
                            //productInfo.Description = "已锁定";
                            break;

                        case 3:
                            productInfo.SalesStatus = catDomain.GetSalestatusIdByName("已销售");
                            //productInfo.Description = "已销售";
                            break;

                        default:
                            NewDbHelper.NewDB.RollbackTransaction();
                            throw new Exception("插入手机号码状态异常");
                            break;
                    }
                    productInfo.CreatedOn = (oldData.Rows[i]["ImDate"] == DBNull.Value) ? Convert.ToDateTime("1900-1-1") : Convert.ToDateTime(oldData.Rows[i]["ImDate"]);
                    productInfo.CreatedBy = "admin";
                    productInfo.StatusCode = 0;
                    productInfo.Status = 0;

                    if (NewDbHelper.NewDB.Create(productInfo) != 1)
                    {
                        NewDbHelper.NewDB.RollbackTransaction();
                        throw new Exception("插入手机号码失败");
                    }
                    else
                    {
                        valueInfo = new ProductAttributesValueModel();
                        valueInfo.ValueId = Guid.NewGuid().ToString();
                        valueInfo.CreatedBy = "admin";
                        valueInfo.CreatedOn = productInfo.CreatedOn;
                        valueInfo.StatusCode = 0;
                        valueInfo.ProductCategoryId = productInfo.CategoryId;
                        valueInfo.ProductId = productInfo.ProductId;

                        valueInfo.AttributeId = "a4d5d8da-a2f8-4c3e-96a4-2ba8a9fe1567";
                        valueInfo.AttributeValue = (oldData.Rows[i]["YYS"] == DBNull.Value) ? null : oldData.Rows[i]["YYS"].ToString();

                        if (NewDbHelper.NewDB.Create(valueInfo) != 1)
                        {
                            NewDbHelper.NewDB.RollbackTransaction();
                            throw new Exception("插入手机号码运营商信息异常");
                        }

                        valueInfo.ValueId = Guid.NewGuid().ToString();
                        valueInfo.AttributeId = "26a3ac6f-b369-472f-8c1b-940d901210d3";
                        valueInfo.AttributeValue = (oldData.Rows[i]["HMJB"] == DBNull.Value) ? null : oldData.Rows[i]["HMJB"].ToString();
                        if (NewDbHelper.NewDB.Create(valueInfo) != 1)
                        {
                            NewDbHelper.NewDB.RollbackTransaction();
                            throw new Exception("插入手机号码号码级别信息异常");
                        }

                        valueInfo.ValueId = Guid.NewGuid().ToString();
                        valueInfo.AttributeId = "f89f168e-c902-4d06-ad2d-3ab2824c6809";
                        valueInfo.AttributeValue = (oldData.Rows[i]["GLYW"] == DBNull.Value) ? null : oldData.Rows[i]["GLYW"].ToString();
                        if (NewDbHelper.NewDB.Create(valueInfo) != 1)
                        {
                            NewDbHelper.NewDB.RollbackTransaction();
                            throw new Exception("插入手机号码所属营销计划信息异常");
                        }
                    }

                }
                
                NewDbHelper.NewDB.CommitTransaction();
               Msg(string.Format("成功插入手机号码产品共 {0} 行", oldData.Rows.Count), false);
            }
        }

        #region 更新枚举信息

        /// <summary>
        /// 更新新库客户来源。
        /// </summary>
        private void UpdateNewCustomerInfoSaleForm()
        {
            string sql = @"
update [customer_basic_info]
set
	sales_from = b.value_id
from 
	custom_data_value b
where 
	sales_from = b.data_value_code and b.data_id= '8D98350B-93AF-432F-88CB-A7381216E61F';
";

            int updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql);

            Msg(string.Format("更新客户基本信息表中 “客户来源” 共 {0} 行记录 ", updateCount), false);
        }

        /// <summary>
        /// 更新新库客户等级。
        /// </summary>
        private void UpdateNewCustomerInfoLevel()
        {
            string sql = @"
update [customer_basic_info]
set
	level = b.value_id
from 
	custom_data_value b
where 
	level = b.data_value_code and b.data_id= 'E8DCC43F-2823-450B-8ED4-E388F8FD5CA5';
";

            int updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql);

            Msg(string.Format("更新客户基本信息表中 “客户等级” 共 {0} 行记录 ", updateCount), false);
        }

        /// <summary>
        /// 更新新库运营商。
        /// </summary>
        private void UpdateNewCustomerInfoCarriers()
        {
            string sql = @"
update [customer_basic_info]
set
	carriers = b.value_id
from 
	custom_data_value b
where 
	carriers = b.data_value_code and b.data_id= '8AEFCEE9-3699-4F9F-A945-BD4376484402';
";

            int updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql);

            Msg(string.Format("更新客户基本信息表中 “运营商” 共 {0} 行记录 ", updateCount), false);
        }

        /// <summary>
        /// 更新新库手机品牌。
        /// </summary>
        private void UpdateNewCustomerInfoPhoneBrand()
        {
            string sql = @"
update [customer_basic_info]
set
	using_phone_brand = b.value_id
from 
	custom_data_value b
where 
	using_phone_brand = b.data_value_code and b.data_id= 'D61C6028-CDEC-43E9-B2D9-721A9E0570A0';
";

            int updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql);

            Msg(string.Format("更新客户基本信息表中 “手机品牌” 共 {0} 行记录 ", updateCount), false);
        }

        /// <summary>
        /// 更新新库优选品牌。
        /// </summary>
        private void UpdateNewCustomerInfoPreferredBrand()
        {
            string sql = @"
update [customer_basic_info]
set
	preferred_phone_brand = b.value_id
from 
	custom_data_value b
where 
	preferred_phone_brand = b.data_value_code and b.data_id= 'D61C6028-CDEC-43E9-B2D9-721A9E0570A0';
";

            int updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql);

            Msg(string.Format("更新客户基本信息表中 “优选品牌” 共 {0} 行记录 ", updateCount), false);

            string sql2 = "update dbo.customer_basic_info set preferred_phone_brand = '8B5596D1-2848-426F-9616-D42FC5AFA17A' where preferred_phone_brand is null;";
            updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql2);

            Msg(string.Format("更新客户基本信息表中 “优选品牌”为未知的 共 {0} 行记录 ", updateCount), false);

            string sql3 = "update dbo.customer_basic_info set using_phone_brand = '8B5596D1-2848-426F-9616-D42FC5AFA17A' where using_phone_brand is null;";
            updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql3);

            Msg(string.Format("更新客户基本信息表中 “在用品牌”为未知的 共 {0} 行记录 ", updateCount), false);


        }

        /// <summary>
        /// 更新新库手机价位。
        /// </summary>
        private void UpdateNewCustomerInfoPhonePrice()
        {
            string sql = @"
update [customer_basic_info]
set
	mobile_phone_price = b.value_id
from 
	custom_data_value b
where 
	mobile_phone_price = b.data_value_code and b.data_id= 'EDF77C7A-49FE-4749-9BCA-3CA2016AB251';
";

            int updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql);

            Msg(string.Format("更新客户基本信息表中 “手机价位” 共 {0} 行记录 ", updateCount), false);

            string sql2 = "update dbo.customer_basic_info set mobile_phone_price = '3DB0973E-66BE-4196-928B-7C5F363B762F' where mobile_phone_price is null;";

            updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql2);

            Msg(string.Format("更新客户基本信息表中 “手机价位” 为未知的共 {0} 行记录 ", updateCount), false);
        }

        /// <summary>
        /// 更新新库通讯消费。
        /// </summary>
        private void UpdateNewCustomerInfoPhoneConsumer()
        {
            string sql = @"
update [customer_basic_info]
set
	communication_consumer = b.value_id
from 
	custom_data_value b
where 
	communication_consumer = b.data_value_code and b.data_id= '1B714EA9-EE6B-4D10-AEA1-AB04DCCD6287';
";

            int updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql);

            Msg(string.Format("更新客户基本信息表中 “通讯消费” 共 {0} 行记录 ", updateCount), false);

            string sql2 = @"update dbo.customer_basic_info set communication_consumer = 'A22C0D97-37B5-4430-9842-3A3C80DAC391' where communication_consumer is null";

            updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql2);

            Msg(string.Format("更新未知客户通讯消费完成，共 {0} 行记录", updateCount), false);
        }

        /// <summary>
        /// 更新客户手机号码来源。
        /// </summary>
        private void UpdateNewCustomerMobileComeFrom()
        {
            string sql = @"
update [customer_basic_info]
set
	come_from = b.city,china_id = b.china_id
from 
	phone_location_info b
where 
	substring(mobile_phone,0,8) = b.phone_code;
";

            int updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql);

            Msg(string.Format("更新客户基本信息表中 “手机号码来源” 共 {0} 行记录 ", updateCount), false);
        }

        /// <summary>
        /// 更新客户电话号码来源。
        /// </summary>
        private void UpdateNewCustomerPhoneComeFrom()
        {
//            string sql = @"
//update [customer_basic_info]
//set
//	come_from = b.city
//from 
//	phone_location_info b
//where 
//	substring(home_phone,0,LEN(b.region_code) + 1) = b.region_code
//	AND mobile_phone is null;
//";

//            int updateCount = DbHelper.NewDB.IData.ExecuteNonQuery(sql);

            string sql = "select customer_id,home_phone from customer_basic_info where mobile_phone is null";
            DataTable dt = NewDbHelper.NewDB.IData.ExecuteDataTable(sql);
            int succ = 0, skip = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                string sql2 = "update customer_basic_info set come_from = $come_from$ where customer_id = $customer_id$";
                ParameterCollection pc = new ParameterCollection();
                string city = null; string chinaId = null;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    city = GetCityByPhoneNumber(dt.Rows[i]["home_phone"].ToString(), out chinaId);
                    if (city == null)
                    {
                        skip++;
                        continue;
                    }

                    pc.Clear();
                    pc.Add("come_from", city);
                    pc.Add("customer_id", dt.Rows[i]["customer_id"].ToString());

                    if (NewDbHelper.NewDB.IData.ExecuteNonQuery(sql2, pc) == 1)
                    {
                        succ++;
                    }
                    else
                    {
                        skip++;
                    }
                }
                Msg(string.Format("更新客户基本信息表中 “电话号码来源” 共 {0} 行记录, 成功 {1} 行， 失败 {2} 行。 ", dt.Rows.Count, succ, skip), false);
            }

            Msg("更新客户基本信息表中 “电话号码来源” 未执行，未获取到数据 ", false);
        }

        #endregion


        #endregion



        #region 辅助方法 
     

        private void Msg(string message, bool alert)
        {
            if (alert)
                MessageBox.Show(message);

            txtLogBox.Text = string.Format("{2}\r\n【{0}】：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), message, txtLogBox.Text);
        }


        public string GetNewDBUserIdByWorkId(string workId)
        {
            string sql = "select user_id from user_info where work_id = $work_id$";

            ParameterCollection pc = new ParameterCollection();
            pc.Add("work_id", "WORKID_" + workId);

            object result = NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc);

            return (result == null) ? null : result.ToString();

        }

        public int GetNewDbChinaId(string province, string city, string county)
        {
            // "select * from china_info where province_name = '' and city_name = '' and county_name = ''";
            string sql = "select id from china_info where province_name = $province_name$ and city_name = $city_name$ and county_name = $county_name$";
            string sql2 = "select id from china_info where province_name = $province_name$ and city_name = $city_name$";
            string sql3 = "select id from china_info where province_name = $province_name$";
            string sql4 = "select id from china_info where province_name is not null and city_name = $city_name$ and county_id is null";

            ParameterCollection pc = new ParameterCollection();
            pc.Add("province_name", province);
            pc.Add("city_name", city);
            pc.Add("county_name", county);
            object result = null;

            if (province == null && city != null && county == null)
            {
                pc.Clear();
                pc.Add("city_name", city);

                result = NewDbHelper.NewDB.IData.ExecuteScalar(sql4, pc);
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1;
                }
            }


            result = NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc);
            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            result = NewDbHelper.NewDB.IData.ExecuteScalar(sql2, pc);
            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            result = NewDbHelper.NewDB.IData.ExecuteScalar(sql3, pc);
            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            return -1;
        }

        private bool CheckExistCustomerCodeFromNewDB(string customerCode)
        {
            string sql = @"select COUNT(1) from customer_basic_info where customer_code = $customer_code$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_code", customerCode);

            return Convert.ToInt32(NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc)) > 0;
        }
        /// <summary>
        /// 基本信息
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        private bool CheckExistCustomerIdFromNewDB(string customerid)
        {
            string sql = @"select COUNT(1) from customer_basic_info where customer_id = $customer_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerid);

            return Convert.ToInt32(NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc)) > 0;
        }
        #region 客户基本信息
        /// <summary>
        /// 联系电话
        /// </summary>
        /// <param name="phoneId"></param>
        /// <returns></returns>
        private bool CheckExistCustomerPhoneFromNewDB(string phoneId)
        {
            string sql = @"select COUNT(1) from customer_phone_info where phone_id = $phone_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("phone_id", phoneId);

            return Convert.ToInt32(NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc)) > 0;
        }
        /// <summary>
        /// 联系记录
        /// </summary>
        /// <param name="contactid"></param>
        /// <returns></returns>
        private bool CheckExistCustomerContactFromNewDB(string contactid)
        {
            string sql = @"select COUNT(1) from customer_contact_info where contact_id = $contact_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("contact_id", contactid);

            return Convert.ToInt32(NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc)) > 0;
        }
        /// <summary>
        /// 配送地址
        /// </summary>
        /// <param name="delivery_id"></param>
        /// <returns></returns>
        private bool CheckExistCustomerdeliveryFromNewDB(string delivery_id)
        {
            string sql = @"select COUNT(1) from customer_delivery_info where delivery_id = $delivery_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("delivery_id", delivery_id);

            return Convert.ToInt32(NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc)) > 0;
        }

        /// <summary>
        /// 备注信息
        /// </summary>
        /// <param name="delivery_id"></param>
        /// <returns></returns>
        private bool CheckExistCustomermemoFromNewDB(string memo_id)
        {
            string sql = @"select COUNT(1) from customer_memo_info where memo_id = $memo_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("memo_id", memo_id);

            return Convert.ToInt32(NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc)) > 0;
        }

        /// <summary>
        ///信用卡信息
        /// </summary>
        /// <param name="delivery_id"></param>
        /// <returns></returns>
        private bool CheckExistCustomercreditcardFromNewDB(string creditcard_id)
        {
            string sql = @"select COUNT(1) from customer_creditcard_info where creditcard_id = $creditcard_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("creditcard_id", creditcard_id);

            return Convert.ToInt32(NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc)) > 0;

        }
        /// <summary>
        ///审核信息
        /// </summary>
        /// <param name="delivery_id"></param>
        /// <returns></returns>
        private bool CheckExistCustomerapprovalFromNewDB(string approval_id)
        {
            string sql = @"select COUNT(1) from customer_info_approval where approval_id = $approval_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("approval_id", approval_id);

            return Convert.ToInt32(NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc)) > 0;
        }
        
        #endregion

        /// <summary>
        /// 根据旧库客户ID获取新库客户ID。
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        private string GetNewDBCustomerIdByCustomerCode(string customerCode)
        {
            string sql = @"select customer_id from customer_basic_info where customer_code = $customer_code$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_code", customerCode);

            object result = NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc);

            return (result == null) ? null : result.ToString();
        }

        public CreditCardInfo GetCreditCardFromOldDB(string cardNumber, string customerCode)
        {
            string sql2 = @"update Order_BaseInfo set ZDSF = '广西壮族自治区', PSSF = '广西壮族自治区',PSCS='南宁',PSQX = '良庆' where CID = 'C11092800255' ";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql2);

            //string sql = @"select TSZH,REPLACE(ZDSF,'省','') as province, replace(ZDCS,'市','') as city, replace(ZDQX,'区','') as county,ZDDZ,* from Order_BaseInfo where ZDDZ <> ''  and ZDDZ <> '广东省 选择市 选择区/县 ' and CID in (select ID from C_Info) and TSZH = $cardNumber$ and cid = $customerCode$";
            string sql = @"select * from Order_BaseInfo where ZDDZ <> ''  and ZDDZ <> '广东省 选择市 选择区/县 ' and CID in (select ID from C_Info) and TSZH = $cardNumber$ and cid = $customerCode$";


            ParameterCollection pc = new ParameterCollection();
            pc.Add("cardNumber", cardNumber);
            pc.Add("customerCode", customerCode);
            CreditCardInfo card = null;

            DataTable oldData = NewDbHelper.OldDB.IData.ExecuteDataTable(sql, pc);
            if (oldData != null && oldData.Rows.Count > 0)
            {
                card = new CreditCardInfo();
                card.CardNumber = cardNumber;
                card.BillAddress = oldData.Rows[0]["ZDDZ"].ToString();

                string province = (oldData.Rows[0]["ZDSF"] == DBNull.Value) ? null : oldData.Rows[0]["ZDSF"].ToString();
                string city = (oldData.Rows[0]["ZDCS"] == DBNull.Value) ? null : oldData.Rows[0]["ZDCS"].ToString();
                string county = (oldData.Rows[0]["ZDQX"] == DBNull.Value) ? null : oldData.Rows[0]["ZDQX"].ToString();

                province = province.Replace("省", "");
                city = city.Replace("市", "");
                county = county.Replace("区", "");
                county = county.Replace("从化市", "从化");
                county = county.Replace("电白县", "电白");
                county = county.Replace("东莞", "东莞");
                county = county.Replace("清新县", "清新");
                county = county.Replace("新丰县", "新丰");
                county = county.Replace("增城市", "增城");
                county = county.Replace("中山市", "中山");

                int chinaId = GetNewDbChinaId(province, city, county);
                if (chinaId != -1)
                {
                    card.ChinaId = chinaId;
                }
                else
                {
                    province = card.BillAddress.Substring(0, 2);
                    city = card.BillAddress.Substring(4, 2);
                    county = card.BillAddress.Substring(8, 2);

                    chinaId = GetNewDbChinaId(province, city, county);
                    if (chinaId != -1)
                    {
                        card.BillAddress = card.BillAddress.Substring(11, card.BillAddress.Length - 11).Trim();//广东省 深圳市 宝安区 沙井帝唐路盈利达工业园5楼3车间
                        card.ChinaId = chinaId;
                    }
                    else
                    {
                        card.ChinaId =  0;
                    }
                }
            }

            return card;
        }

        public IdCardInfo GetIdCardByCustomerCodeFromOldDatabase(string customerCode)
        {
            IdCardInfo cardInfo = null;

            string sql = @"select ZJLX, ZJNum from C_CardInfo where ZJNum <> '0' AND ZJNum <> '' AND CID = $customerCode$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customerCode", customerCode);

            DataTable dt = NewDbHelper.OldDB.IData.ExecuteDataTable(sql,pc);

            if (dt != null && dt.Rows.Count > 0)
            {
                cardInfo = new IdCardInfo();
                cardInfo.CardType = dt.Rows[0]["ZJLX"].ToString();
                cardInfo.CardNum = dt.Rows[0]["ZJNum"].ToString();

                if (cardInfo.CardNum.Length != 15 && cardInfo.CardNum.Length != 18 && cardInfo.CardType == "C00001")
                {
                    cardInfo.CardType = "C00006";
                }
            }
            else
            {                
                //sql = @"select ZJLX, ZJNum, * from Order_BaseInfo where ZJNum <> '0' and ZJNum <> ''  and CID = $customerCode$";
                //dt = DbHelper.OldDB.IData.ExecuteDataTable(sql, pc);

                //if (dt != null)
                //{
                //    cardInfo = new IdCardInfo();
                //    cardInfo.CardType = dt.Rows[0]["ZJLX"].ToString();
                //    cardInfo.CardNum = dt.Rows[0]["ZJNum"].ToString();
                //}
            }

            return cardInfo;
        }

        public static string GetContactPurposeOrResult(int sortorder, CustomDataDomainModel list)
        {
            if (list == null || list.ValueList == null || list.ValueList.Count == 0)
                throw new Exception();

            foreach (CustomDataValueDomainModel item in list.ValueList.Values)
            {
                if (item.SortOrder == sortorder)
                {
                    return item.ValueId;
                }
            }

            return null;
        }

        public string GetCityByPhoneNumber(string phoneNumber, out string chinaId)
        {
            chinaId = null;
            string sql = @"select top 1 city,china_id from phone_location_info where region_code = substring($phoneNumber$, 0, datalength(region_code) + 1)";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("phoneNumber", phoneNumber);

            DataTable result = NewDbHelper.NewDB.IData.ExecuteDataTable(sql, pc);
            if (result != null && result.Rows.Count > 0)
            {
                chinaId = result.Rows[0][1].ToString();
                return result.Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
            // return (result == null) ? null : result.ToString();
        }

        #endregion

        #region 按钮事件

        private void btnClearCustomerTable_Click(object sender, EventArgs e)
        {
            btnClearCustomerTable.Enabled = false;
            string sql = @"
truncate table customer_basic_info;
truncate table customer_memo_info;
truncate table customer_contact_info;
truncate table customer_creditcard_info;
truncate table customer_delivery_info;
";

//            truncate table product_info;
//truncate table product_attributes_value;

            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql);

            btnClearCustomerTable.Enabled = true;
            Msg(@"
truncate table customer_basic_info;
truncate table customer_memo_info;
truncate table customer_contact_info;
truncate table customer_creditcard_info;
truncate table customer_delivery_info;
truncate table product_info;
truncate table product_attributes_value;
操作成功", false);
        }


       private void btnSyncAll_Click(object sender, EventArgs e)
       {
            btnSyncCustomer.Enabled = false;
            btnSyncCustomer.Enabled = false;
            btnSyncContact.Enabled = false;
            btnSyncCreditCard.Enabled = false;
            btnSyncEntruct.Enabled = false;
            btnSyncDelivery.Enabled = false;
            btnSyncMobileNumber.Enabled = false;

            SyncCustomerBasicInfo();           
           UpdateNewCustomerMobileComeFrom();
            UpdateNewCustomerPhoneComeFrom();
            SyncCustomerMemoInfo();
            SyncCustomerContactRecords();
            SyncCustomerCreditCardInfo();
            SyncCustomerEntrustCardInfo();
            SyncCustomerDeliveryInfo();
            UpdateCarriers();
            SyncWorkroderType();
            SyncWorkorder();
            SyncWorkorderStatus();
             

            btnSyncCustomer.Enabled = true;
            btnSyncCustomer.Enabled = true;
            btnSyncContact.Enabled = true;
            btnSyncCreditCard.Enabled = true;
            btnSyncEntruct.Enabled = true;
            btnSyncDelivery.Enabled = true;
            btnSyncMobileNumber.Enabled = true;
        }

        private void btnSyncCustomerMemo_Click(object sender, EventArgs e)
        {
            btnSyncCustomerMemo.Enabled = false;
            SyncCustomerMemoInfo();
            btnSyncCustomerMemo.Enabled = true;
        }

        private void btnSyncContact_Click(object sender, EventArgs e)
        {
            btnSyncContact.Enabled = false;
            SyncCustomerContactRecords();
            btnSyncContact.Enabled = true;
        }

        private void btnSyncCreditCard_Click(object sender, EventArgs e)
        {
            btnSyncCreditCard.Enabled = false;
            SyncCustomerCreditCardInfo();
            btnSyncCreditCard.Enabled = true;
        }

        private void btnSyncEntruct_Click(object sender, EventArgs e)
        {
            btnSyncEntruct.Enabled = false;
            SyncCustomerEntrustCardInfo();
            btnSyncEntruct.Enabled = true;
        }

        private void btnSyncDelivery_Click(object sender, EventArgs e)
        {
            btnSyncDelivery.Enabled = false;
            SyncCustomerDeliveryInfo();
            btnSyncDelivery.Enabled = true;
        }

        private void btnUpdateComeFrom_Click(object sender, EventArgs e)
        {
            btnUpdateComeFrom.Enabled = false;
            UpdateNewCustomerMobileComeFrom();
            UpdateNewCustomerPhoneComeFrom();
            btnUpdateComeFrom.Enabled = true;
        }

        private void btnSyncMobileNumber_Click(object sender, EventArgs e)
        {
            btnSyncMobileNumber.Enabled = false;
            //SyncMobileNumber();
            btnSyncMobileNumber.Enabled = true;
        }

        #endregion

        private void UpdateCarriers()
        {
            btnUpdateCarriers.Enabled = false;
            string sql = @"
update [customer_basic_info]
set
	carriers = 
	case substring(b.brand,0,5)
		when  '中国电信' then '87D5E079-0C68-4F91-B6D6-F639B8B2119A'
		when  '中国联通' then '42282285-9409-42F4-94E8-C556DC62B2A5'
		when  '中国移动' then '0CC77016-8F5D-4297-B193-1A02B54A750D'
	end
from 
	phone_location_info b
where 
	substring(mobile_phone,0,8) = b.phone_code;
";

            int updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql);

            Msg(string.Format("更新客户基本信息表中 “运营商” 共 {0} 行记录 ", updateCount), false);

            string sql2 = @"
update customer_basic_info set carriers = '42282285-9409-42F4-94E8-C556DC62B2A5'
where mobile_phone is not null and carriers is null and substring(mobile_phone,0,4) = '186';
";
            updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql2);

            Msg(string.Format("更新客户基本信息表中 “运营商” 共 {0} 行记录为中国联通号码 ", updateCount), false);

            string sql3 = @"update dbo.customer_basic_info set carriers = '00C9C4A6-81A3-4CEE-813C-B0965380C089' where carriers is null;";

            updateCount = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql3);

            Msg(string.Format("更新客户基本信息表没有手机号码记录共 {0} 行，设置“运营商”为“未知” ", updateCount), false);

            btnUpdateCarriers.Enabled = true;
        }

        private void btnUpdateCarriers_Click(object sender, EventArgs e)
        {

            UpdateCarriers();
        }

        private void btnSyncWorkorderType_Click(object sender, EventArgs e)
        {
            SyncWorkroderType();
        }

        private void SyncWorkroderType()
        {
            btnSyncWorkorderType.Enabled = false;

            Dictionary<string, WorkorderTypeInfoModel> Dict = WorkorderTypeInfoService.Instance.GetWorkOrderDictionary(true);

            foreach (WorkorderTypeInfoModel typeItem in Dict.Values)
            {
                WorkOrderTypeDomainModel model = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(typeItem.WorkorderTypeId, true);
                if (model != null)
                {
                    int statusSortOrder = 0, resultSortOrder = 0;
                    foreach (WorkorderStatusInfoModel status in model.StatusList.Values)
                    {
                        switch (status.StatusName)
                        {
                            case "未处理":
                                status.SortOrder = 0;
                                status.StatusTag = 0;
                                status.CustomStatus = "未处理";
                                break;

                            case "处理中":
                                status.SortOrder = 1;
                                status.StatusTag = 1;
                                status.CustomStatus = "处理中";
                                break;

                            case "转交单":
                                status.SortOrder = 2;
                                status.StatusTag = 1;
                                status.StatusName = "待审批";
                                status.CustomStatus = "待审批";
                                break;

                            case "待质检":
                                status.SortOrder = 3;
                                status.StatusTag = 1;
                                status.StatusName = "待质检";
                                status.CustomStatus = "待质检";
                                break;

                            case "已关闭":
                                status.SortOrder = 4;
                                status.StatusTag = 2;
                                status.CustomStatus = "已关闭";
                                break;

                            default:
                                break;
                        }
                        //status.SortOrder = statusSortOrder;
                        NewDbHelper.NewDB.Update(status);
                        //statusSortOrder++;
                    }

                    WorkorderStatusInfoModel newStatus = new WorkorderStatusInfoModel();
                    newStatus.StatusName = "待质检";
                    newStatus.CreatedBy = "C792D747-6B74-4A58-BB5B-D98EF420F99F";
                    newStatus.CreatedOn = DateTime.Now;
                    newStatus.Description = "待质检";
                    newStatus.CustomStatus = "待质检";
                    newStatus.SortOrder = 3;
                    newStatus.Status = 0;
                    newStatus.StatusCode = 0;
                    newStatus.StatusTag = 1;
                    newStatus.WorkorderStatusId = Guid.NewGuid().ToString();
                    newStatus.WorkorderTypeId = model.TypeInfo.WorkorderTypeId;
                    NewDbHelper.NewDB.Create(newStatus);


                    foreach (WorkorderResultInfoModel result in model.ResultList.Values)
                    {
                        result.SortOrder = resultSortOrder;
                        NewDbHelper.NewDB.Update(result);
                        resultSortOrder++;
                    }
                }
            }

            btnSyncWorkorderType.Enabled = true;
            Msg(string.Format("成功更新各工单类型状态和结果排序索引"), false);

        }



        private void btnSyncWorkorder_Click(object sender, EventArgs e)
        {
            SyncWorkorder();
        }

        private void SyncWorkorder()
        {
            btnSyncWorkorder.Enabled = false;
            string newDbCustomerId = null;
            string message = null;

            string truncateSql = "truncate table workorder_info;";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(truncateSql);

            truncateSql = "truncate table workorder_process_info;";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(truncateSql);

            truncateSql = "truncate table workorder_checks_info;";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(truncateSql);

            string oldDataSql = "select * from work_order_info";

            DataTable odt = NewDbHelper.OldDB.IData.ExecuteDataTable(oldDataSql);
            if (odt != null && odt.Rows.Count > 0)
            {
                Dictionary<string, WorkorderTypeInfoModel> typeList = WorkorderTypeInfoService.Instance.GetWorkOrderDictionary(false);
                Dictionary<string, UserInfoModel> userList = GetNewDbUserInfoByOldUserId();
                Dictionary<string, UserInfoModel> workIdList = GetNewDbUserInfoByOldWorkId();
                CustomDataDomainModel orderLevel = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("工单级别", false);
                WorkOrderTypeDomainModel tm = null;
                WorkorderInfoModel om = null;

                long workorderCode = 12070100000;

                //try
                //{
                NewDbHelper.NewDB.BeginTransaction();

                #region ddd

                for (int i = 0; i < odt.Rows.Count; i++)
                {
                    //try
                    //{ 

                    tm = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(odt.Rows[i]["workorder_type"].ToString(), false);

                    if (tm == null)
                    {
                        Msg(string.Format("旧数据中工单ID为【{0}】的记录无对应工单类型", odt.Rows[i]["work_order_id"]), false);
                        continue;
                    }

                    om = new WorkorderInfoModel();

                    om.WorkorderType = odt.Rows[i]["workorder_type"].ToString().ToUpper();

                    om.WorkOrderId = odt.Rows[i]["work_order_id"].ToString();
                    //om.WorkorderCode = odt.Rows[i]["workorder_code"].ToString();
                    om.WorkorderCode = "WKOR" + (workorderCode + i).ToString();
                    om.RelCustomerId = GetCustomerIdByCode(odt.Rows[i]["rel_customer_id"].ToString());
                    if (om.RelCustomerId == null)
                    {
                        Msg(string.Format("旧数据中工单ID为【{0}】的记录无对应客户信息", odt.Rows[i]["work_order_id"]), false);
                        continue;

                        //if (SyncCustomerInfoToNewDatabase(odt.Rows[i]["rel_customer_id"].ToString(), out newDbCustomerId, out message))
                        //{
                        //    om.RelCustomerId = newDbCustomerId;
                        //}
                        //else
                        //{
                        //    continue; 
                        //}
                    }


                    if (odt.Rows[i]["rel_usergroup_id"] != DBNull.Value && string.IsNullOrEmpty(odt.Rows[i]["rel_usergroup_id"].ToString()) == false)
                    {
                        om.RelUsergroupId = odt.Rows[i]["rel_usergroup_id"].ToString();
                    }


                    om.QualityCheckStatus = (odt.Rows[i]["QualityChecked"].ToString() == "1") ? 1 : 0;
                    if (odt.Rows[i]["now_result_id"] != DBNull.Value && tm.ResultList.ContainsKey(odt.Rows[i]["now_result_id"].ToString().ToUpper()))
                    {
                        om.NowResultId = tm.ResultList[odt.Rows[i]["now_result_id"].ToString().ToUpper()].WorkorderResultId;
                    }
                    else
                    {
                        //Msg(string.Format("旧数据中工单ID为【{0}】的记录无对应工单处理结果", odt.Rows[i]["work_order_id"]), false);

                        foreach (WorkorderResultInfoModel rr in tm.ResultList.Values)
                        {
                            if (rr.ResultName == "其他")
                            {
                                om.NowResultId = rr.WorkorderResultId;
                            }
                        }
                    }



                    foreach (WorkorderStatusInfoModel vv in tm.StatusList.Values)
                    {
                        if (odt.Rows[i]["now_status"].ToString().ToUpper() == vv.WorkorderStatusId)
                        {
                            if (vv.StatusName == "未处理")
                            {
                                om.ProcessStatus = 0;
                            }
                            else if (vv.StatusName == "已关闭")
                            {
                                om.ProcessStatus = 2;
                            }
                            else
                            {
                                om.ProcessStatus = 1;
                            }

                            om.NowStatusId = vv.WorkorderStatusId;
                            break;
                        }
                        else
                        {
                            //Msg(string.Format("旧数据中工单ID为【{0}】的记录无对应工单工单状态", odt.Rows[i]["work_order_id"]), false);

                            if (vv.StatusName == "未处理")
                            {
                                om.NowStatusId = vv.WorkorderStatusId;
                                om.ProcessStatus = 0;
                            }
                        }
                    }

                    if (odt.Rows[i]["modified_on"] != DBNull.Value)
                    {
                        om.ModifiedOn = Convert.ToDateTime(odt.Rows[i]["modified_on"]);
                    }
                    om.ModifiedBy = (odt.Rows[i]["modified_by"] == DBNull.Value || workIdList.ContainsKey(odt.Rows[i]["modified_by"].ToString()) == false) ? null : workIdList[odt.Rows[i]["modified_by"].ToString()].UserId;

                    foreach (CustomDataValueDomainModel leveitem in orderLevel.ValueList.Values)
                    {
                        if (leveitem.DataValueCode == odt.Rows[i]["level"].ToString())
                        {
                            om.Level = leveitem.ValueId;
                        }
                    }

                    if (om.Level == null)
                    {
                        om.Level = orderLevel.ValueList.First().Value.ValueId;
                    }

                    if (odt.Rows[i]["modified_first_by"] != DBNull.Value)
                    {
                        om.FirstProcessTime = Convert.ToDateTime(odt.Rows[i]["modified_first_by"]);
                    }

                    om.Description = (odt.Rows[i]["c_ntext"] == DBNull.Value) ? null : odt.Rows[i]["c_ntext"].ToString();
                    om.CreatedOn = Convert.ToDateTime(odt.Rows[i]["created_on"]);


                    //om.NowProcessUserid = "C792D747-6B74-4A58-BB5B-D98EF420F99F";
                    //om.FirstProcessUserid = "C792D747-6B74-4A58-BB5B-D98EF420F99F";
                    om.CreatedBy = (odt.Rows[i]["created_by"] == DBNull.Value || userList.ContainsKey(odt.Rows[i]["created_by"].ToString()) == false) ? "C792D747-6B74-4A58-BB5B-D98EF420F99F" : userList[odt.Rows[i]["created_by"].ToString()].UserId;
                    om.ClosedUser = (odt.Rows[i]["closed_user"] == DBNull.Value || workIdList.ContainsKey(odt.Rows[i]["closed_user"].ToString()) == false) ? null : workIdList[odt.Rows[i]["closed_user"].ToString()].UserId;
                    if (odt.Rows[i]["closed_time"] != DBNull.Value)
                    {
                        om.ClosedTime = Convert.ToDateTime(odt.Rows[i]["closed_time"]);
                        om.NowProcessUserid = om.ClosedUser;
                        om.NowStatusId = tm.EndStatusInfo.WorkorderStatusId;

                    }

                    if (odt.Rows[i]["Reservation_Long_Time"] != DBNull.Value)
                    {
                        om.AdvanceTime = Convert.ToDateTime(odt.Rows[i]["Reservation_Long_Time"]);
                    }



                    if (NewDbHelper.NewDB.Create(om) == 1)
                    {
                        string oldProcSql = "select * from [workorder_process_info] where workorder_id = $workorder_id$";

                        ParameterCollection ppc = new ParameterCollection();
                        ppc.Add("workorder_id", om.WorkOrderId);
                        DataTable opt = NewDbHelper.OldDB.IData.ExecuteDataTable(oldProcSql, ppc);
                        if (opt != null && opt.Rows.Count > 0)
                        {
                            WorkorderProcessInfoModel op = null;
                            for (int j = 0; j < opt.Rows.Count; j++)
                            {
                                op = new WorkorderProcessInfoModel();
                                op.AfterStatus = opt.Rows[j]["before_result"].ToString().ToUpper();
                                op.AfterResult = opt.Rows[j]["after_result"].ToString().ToUpper();
                                op.WorkorderId = om.WorkOrderId;
                                op.WorkorderTypeId = om.WorkorderType;
                                op.ProcessId = opt.Rows[j]["process_id"].ToString();
                                op.Description = opt.Rows[j]["description"].ToString();
                                op.CreatedOn = (opt.Rows[j]["created_on"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(opt.Rows[j]["created_on"]);
                                op.CreatedBy = (workIdList.ContainsKey(opt.Rows[j]["created_by"].ToString()) == true) ? workIdList[opt.Rows[j]["created_by"].ToString()].UserId : "C792D747-6B74-4A58-BB5B-D98EF420F99F";

                                if (NewDbHelper.NewDB.Create(op) != 1)
                                {
                                    NewDbHelper.NewDB.RollbackTransaction();
                                    throw new Exception("插入处理记录异常");
                                }
                            }
                        }

                        if (odt.Rows[i]["QualityChecked"] != DBNull.Value && odt.Rows[i]["QualityChecked"].ToString() != "0")
                        {
                            WorkorderChecksInfoModel ck = new WorkorderChecksInfoModel();
                            ck.WorkorderCheckId = Guid.NewGuid().ToString();
                            ck.WorkorderId = om.WorkOrderId;
                            ck.CheckDescription = odt.Rows[i]["QualityDescription"].ToString();
                            ck.CheckStatus = Convert.ToInt32(odt.Rows[i]["QualityChecked"]);
                            ck.CreatedOn = (odt.Rows[i]["QualityTime"] != DBNull.Value) ? Convert.ToDateTime(odt.Rows[i]["QualityTime"]) : Convert.ToDateTime("1900-01-01 00:00:00");
                            ck.CreatedBy = (workIdList.ContainsKey(odt.Rows[i]["QualityUserId"].ToString()) == true) ? workIdList[odt.Rows[i]["QualityUserId"].ToString()].UserId : "C792D747-6B74-4A58-BB5B-D98EF420F99F";
                            ck.StatusCode = 0;

                            if (NewDbHelper.NewDB.Create(ck) != 1)
                            {
                                NewDbHelper.NewDB.RollbackTransaction();
                                throw new Exception("插入质检记录异常");
                            }
                        }
                    }


                }

                #endregion

                NewDbHelper.NewDB.CommitTransaction();
                //}
                //catch (Exception ex)
                //{
                //    NewDbHelper.NewDB.RollbackTransaction();
                //    throw ex;
                //}
            }



            btnSyncWorkorder.Enabled = true;
        }

        protected string GetCustomerIdByCode(string code)
        {
            string sql = "select customer_id from [customer_basic_info] where customer_code = $code$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("code", code);

            object result = NewDbHelper.NewDB.IData.ExecuteScalar(sql, pc);

            return (result == null) ? null : result.ToString();
        }

        public bool SyncCustomerInfoToNewDatabase(string customerId, out string newDbCustomerId, out string message)
        {
            bool result = false;
            message = "操作失败";
            newDbCustomerId = "";

            string checkExistFromNewDBSQL = "select customer_id from customer_basic_info where customer_code = $CustomerCode$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("CustomerCode", customerId);

            object cc = NewDbHelper.NewDB.IData.ExecuteScalar(checkExistFromNewDBSQL, pc);
            if (cc != null)
            {
                newDbCustomerId = cc.ToString();
                SyncCustomerContactInfoToNewDatabase(customerId, newDbCustomerId, null);
                SyncCustomerCreditCardInfoToNewDatabase(customerId);
                SyncCustomerDeliveryInfo(customerId);

                message = "同步中止，数据库中已存在该客户信息";
                return true;
            }

            string getOldCustomerInfoSQL = "select * from C_Info where ID = $CustomerId$";
            pc.Clear();
            pc.Add("CustomerId", customerId);

            DataTable oldCustomerTable = NewDbHelper.OldDB.IData.ExecuteDataTable(getOldCustomerInfoSQL, pc);
            

            if (oldCustomerTable != null && oldCustomerTable.Rows.Count == 1)
            {
                try
                {
                    DataRow oldData = oldCustomerTable.Rows[0];

                    NewDbHelper.NewDB.BeginTransaction();

                    CustomerBasicInfoModel basicInfo = new CustomerBasicInfoModel();
                    basicInfo.CustomerId = Guid.NewGuid().ToString();
                    basicInfo.CustomerCode = oldData["id"].ToString();
                    basicInfo.CustomerName = (oldData["CName"] == DBNull.Value) ? null : oldData["CName"].ToString().Replace("[新增]", "");
                    if (basicInfo.CustomerName == "未知")
                    {
                        basicInfo.CustomerName = null;
                    }

                    basicInfo.Sex = Convert.ToInt32(oldData["CSex"]);
                    basicInfo.SalesFrom = oldData["CLY"].ToString();

                    basicInfo.MobilePhone = (oldData["SJ"] == DBNull.Value || oldData["SJ"].ToString().Length == 0) ? null : oldData["SJ"].ToString();
                    basicInfo.HomePhone = (oldData["Tel"] == DBNull.Value || oldData["Tel"].ToString().Length == 0) ? null : oldData["Tel"].ToString();
                    basicInfo.OtherPhone = (oldData["OtherTel"] == DBNull.Value || oldData["OtherTel"].ToString().Length == 0) ? null : oldData["OtherTel"].ToString();

                    basicInfo.Level = oldData["CLevel"].ToString();
                    basicInfo.Carriers = (oldData["C_SJYYS"] == DBNull.Value) ? null : oldData["C_SJYYS"].ToString();
                    basicInfo.UsingPhoneBrand = (oldData["C_SJPP"] == DBNull.Value) ? null : oldData["C_SJPP"].ToString();
                    basicInfo.UsingPhoneType = (oldData["C_SJXH"] == DBNull.Value) ? null : oldData["C_SJXH"].ToString();
                    basicInfo.CommunicationConsumer = (oldData["C_TXSF"] == DBNull.Value) ? null : oldData["C_TXSF"].ToString();
                    basicInfo.PreferredPhoneBrand = (oldData["C_YXPP"] == DBNull.Value) ? null : oldData["C_YXPP"].ToString();
                    if (oldData["C_SJZF"] != DBNull.Value)
                    {
                        basicInfo.UsingSmartphone = Convert.ToInt32(oldData["C_SJZF"]);
                    }
                    basicInfo.MobilePhonePrice = (oldData["C_SJJW"] == DBNull.Value) ? null : oldData["C_SJJW"].ToString();

                    #region 身份证信息

                    IdCardInfo cardInfo = GetIdCardByCustomerCodeFromOldDatabase(basicInfo.CustomerCode);
                    if (cardInfo != null)
                    {
                        basicInfo.IdcardType = cardInfo.CardType;
                        basicInfo.IdcardNumber = cardInfo.CardNum;
                    }

                    #endregion

                    basicInfo.Status = 0;
                    if (oldData["AddDate"] == DBNull.Value)
                    {
                        basicInfo.CreatedOn = Convert.ToDateTime("1900-01-01 00:00:00");
                        CustomerMemoInfoModel memoInfo = new CustomerMemoInfoModel();
                        memoInfo.Memo = "历史数据导入时无创建日期，使用初始值1900-01-01 00:00:00";
                        memoInfo.CustomerId = basicInfo.CustomerId;
                        memoInfo.MemoId = Guid.NewGuid().ToString();
                        memoInfo.CreatedOn = DateTime.Now;
                        memoInfo.CreatedBy = "Admin";

                        NewDbHelper.NewDB.Create(memoInfo);
                    }
                    else
                    {
                        basicInfo.CreatedOn = Convert.ToDateTime(oldData["AddDate"]);
                    }
                    basicInfo.StatusCode = 0;

                    if (NewDbHelper.NewDB.Create(basicInfo) != 1)
                    {
                        NewDbHelper.NewDB.RollbackTransaction();
                        message = "插入新系统客户数据失败";
                        return false;
                    }

                    newDbCustomerId = basicInfo.CustomerId;

                    UpdateSyncNewCustomerInfo(basicInfo.CustomerId);

                    SyncCustomerContactInfoToNewDatabase(oldData["id"].ToString(), basicInfo.CustomerId, null);

                    SyncCustomerCreditCardInfoToNewDatabase(oldData["id"].ToString());

                    SyncCustomerDeliveryInfo(oldData["id"].ToString());

                    result = true;

                }
                catch (Exception ex)
                {
                    NewDbHelper.NewDB.RollbackTransaction();
                    message = "同步客户信息至新系统异常，" + ex.Message;
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// 同步客户配送地址信息。
        /// </summary>
        private void SyncCustomerDeliveryInfo(string oldCustomerId)
        {
            string sql = @"select * from Order_BaseInfo where PSDZ is not null and PSDZ <> '' and PSDZ <> '广东省 选择市 选择区/县 ' and CID = $OldCustomerId$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("OldCustomerId", oldCustomerId);

            DataTable oldData = NewDbHelper.OldDB.IData.ExecuteDataTable(sql, pc);

            if (oldData != null && oldData.Rows.Count > 0)
            {
                CustomerDeliveryInfoModel deliveryInfo = null;
                int succ = 0, skip = 0;
                for (int i = 0; i < oldData.Rows.Count; i++)
                {
                    deliveryInfo = new CustomerDeliveryInfoModel();
                    deliveryInfo.DeliveryId = Guid.NewGuid().ToString();

                    deliveryInfo.CustomerId = GetNewDBCustomerIdByCustomerCode(oldData.Rows[i]["cid"].ToString());
                    if (deliveryInfo.CustomerId == null)
                    {
                        skip++;
                        continue;
                    }
                    deliveryInfo.DeliveryType = (oldData.Rows[i]["PSSX"] == DBNull.Value) ? 2 : Convert.ToInt32(oldData.Rows[i]["PSSX"]);
                    deliveryInfo.NeedBills = (oldData.Rows[i]["ISFP"] == DBNull.Value) ? 2 : Convert.ToInt32(oldData.Rows[i]["ISFP"]);

                    deliveryInfo.DeliveryAddress = (oldData.Rows[i]["PSDZ"] == DBNull.Value) ? null : oldData.Rows[i]["PSDZ"].ToString();
                    deliveryInfo.PostCode = (oldData.Rows[i]["PSYB"] == DBNull.Value) ? null : oldData.Rows[i]["PSYB"].ToString();
                    deliveryInfo.Consignee = (oldData.Rows[i]["PSSHR"] == DBNull.Value) ? null : oldData.Rows[i]["PSSHR"].ToString();
                    deliveryInfo.ConsigneePhone = (oldData.Rows[i]["PSDH"] == DBNull.Value) ? null : oldData.Rows[i]["PSDH"].ToString();
                    deliveryInfo.BillTitle = (oldData.Rows[i]["PBIAOTOU"] == DBNull.Value) ? null : oldData.Rows[i]["PBIAOTOU"].ToString();

                    string province = (oldData.Rows[i]["PSSF"] == DBNull.Value) ? null : oldData.Rows[i]["PSSF"].ToString();
                    string city = (oldData.Rows[i]["PSCS"] == DBNull.Value) ? null : oldData.Rows[i]["PSCS"].ToString();
                    string county = (oldData.Rows[i]["PSQX"] == DBNull.Value) ? null : oldData.Rows[i]["PSQX"].ToString();
                    province = province.Replace("省", "");
                    city = city.Replace("市", "");
                    county = county.Replace("区", "");
                    county = county.Replace("从化市", "从化");
                    county = county.Replace("电白县", "电白");
                    county = county.Replace("东莞", "东莞");
                    county = county.Replace("清新县", "清新");
                    county = county.Replace("新丰县", "新丰");
                    county = county.Replace("增城市", "增城");
                    county = county.Replace("中山市", "中山");

                    int chinaId = GetNewDbChinaId(province, city, county);
                    if (chinaId != -1)
                    {
                        deliveryInfo.DeliveryRegionId = chinaId;
                    }
                    else
                    {
                        province = deliveryInfo.DeliveryAddress.Substring(0, 2);
                        city = deliveryInfo.DeliveryAddress.Substring(4, 2);
                        county = deliveryInfo.DeliveryAddress.Substring(8, 2);

                        chinaId = GetNewDbChinaId(province, city, county);
                        if (chinaId != -1)
                        {
                            //deliveryInfo.PostCode = deliveryInfo.DeliveryAddress.Substring(10, deliveryInfo.DeliveryAddress.Length - 10);
                            deliveryInfo.DeliveryAddress = deliveryInfo.DeliveryAddress.Substring(11, deliveryInfo.DeliveryAddress.Length - 11).Trim();//广东省 深圳市 宝安区 沙井帝唐路盈利达工业园5楼3车间
                            deliveryInfo.DeliveryRegionId = chinaId;
                        }
                        else
                        {
                            deliveryInfo.DeliveryRegionId = 0;
                        }
                    }

                    deliveryInfo.StatusCode = 0;
                    deliveryInfo.Status = 0;
                    deliveryInfo.CreatedOn = (oldData.Rows[i]["ODate"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(oldData.Rows[i]["ODate"]);
                    deliveryInfo.CreatedBy = "admin";

                    if (NewDbHelper.NewDB.Create(deliveryInfo) != 1)
                    {
                        //Msg(string.Format("插入旧库ID为【{0}】客户配送地址信息失败", oldData.Rows[i]["id"]), false);

                    }
                }

                //Msg(string.Format("同步客户配送地址完成，共 {0} 行记录, 插入 {1} 行， 忽略 {2} 行", oldData.Rows.Count, succ, skip), false);

            }


        }

        public bool SyncCustomerCreditCardInfoToNewDatabase(string oldCustomerId)
        {
            string sql = @"select * from C_CardInfo where CardNum <> '' and CID = $OldCustomerId$";
            string getbankSql = @"select * from dbo.bankcard_type_info where card_bin_code = $card_bin_code$";
            string updateCustomerSQL = @"update customer_basic_info set birthday = $birthday$  where customer_id = $customer_id$";
            string checkExistSQL = "select count(1) from customer_creditcard_info where customer_id = $customer_id$ and creditcard_number = $creditNumber$;";

            ParameterCollection pc = new ParameterCollection();
            ParameterCollection pc2 = new ParameterCollection();
            ParameterCollection pc3 = new ParameterCollection();
            BankcardTypeInfoModel bankModel = null;
            DataTable bankTable = null;

            pc.Add("OldCustomerId", oldCustomerId);
            DataTable oldCardList = NewDbHelper.OldDB.IData.ExecuteDataTable(sql, pc);

            CustomDataDomainModel IdTypeList = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("证件类型", false);

            if (oldCardList != null && oldCardList.Rows.Count > 0)
            {
                CustomerCreditcardInfoModel cardInfo = null;
                int succ = 0, skip = 0;
                for (int i = 0; i < oldCardList.Rows.Count; i++)
                {
                    cardInfo = new CustomerCreditcardInfoModel();
                    cardInfo.CreditcardId = Guid.NewGuid().ToString();

                    cardInfo.CustomerId = GetNewDBCustomerIdByCustomerCode(oldCardList.Rows[i]["cid"].ToString());
                    if (cardInfo.CustomerId == null)
                    {
                        skip++;
                        continue;
                    }
                    cardInfo.CreditcardNumber = (oldCardList.Rows[i]["CardNum"] == DBNull.Value) ? null : oldCardList.Rows[i]["CardNum"].ToString().Replace(" ", "");
                    cardInfo.Period = (oldCardList.Rows[i]["YXQ"] == DBNull.Value) ? null : oldCardList.Rows[i]["YXQ"].ToString().Replace(" ", "");
                    cardInfo.SecurityCode = (oldCardList.Rows[i]["AQM"] == DBNull.Value) ? null : oldCardList.Rows[i]["AQM"].ToString().Replace(" ", "");
                    cardInfo.Bank = (oldCardList.Rows[i]["KKH"] == DBNull.Value) ? null : oldCardList.Rows[i]["KKH"].ToString().Replace(" ", "");
                    cardInfo.OpeningAddress = (oldCardList.Rows[i]["KKD"] == DBNull.Value) ? null : oldCardList.Rows[i]["KKD"].ToString().Replace(" ", "");
                    cardInfo.CardUsername = (oldCardList.Rows[i]["XM"] == DBNull.Value) ? null : oldCardList.Rows[i]["XM"].ToString().Replace(" ", "");
                    cardInfo.IdcardType = (oldCardList.Rows[i]["ZJLX"] == DBNull.Value) ? null : oldCardList.Rows[i]["ZJLX"].ToString().Replace(" ", "");
                    cardInfo.IdcardNumber = (oldCardList.Rows[i]["ZJNum"] == DBNull.Value) ? null : oldCardList.Rows[i]["ZJNum"].ToString().Replace(" ", "");

                    foreach (CustomDataValueDomainModel typeitem in IdTypeList.ValueList.Values)
                    {
                        if (typeitem.DataValueCode == cardInfo.IdcardType.Trim())
                        {
                            cardInfo.IdcardType = typeitem.ValueId;
                            break;
                        }
                    }

                    CreditCardInfo cardBill = GetCreditCardFromOldDB(cardInfo.CreditcardNumber, oldCardList.Rows[i]["cid"].ToString());
                    if (cardBill != null)
                    {
                        cardInfo.BillChinaId = cardBill.ChinaId;
                        cardInfo.BillAddress = cardBill.BillAddress;
                    }

                    if (string.IsNullOrEmpty(cardInfo.CreditcardNumber) == false && cardInfo.CreditcardNumber.Length > 6)
                    {
                        pc.Clear();
                        pc.Add("card_bin_code", cardInfo.CreditcardNumber.Substring(0, 6));
                        bankTable = NewDbHelper.NewDB.IData.ExecuteDataTable(getbankSql, pc);
                        if (bankTable != null)
                        {
                            bankModel = new BankcardTypeInfoModel();
                            NewDbHelper.NewDB.ConvertFrom(bankModel, bankTable);

                            cardInfo.Bank = bankModel.BankEnumValue;
                            cardInfo.CardType = bankModel.CardType;
                            cardInfo.CardLevel = bankModel.CardLevel;
                            cardInfo.CardBrand = bankModel.CardBrand;
                            cardInfo.CanbeStage = (bankModel.BankcardEnumValue == "FEB39D81-26EC-4A20-97F2-F148FDC87AFD") ? 0 : 1;
                        }
                    }

                    cardInfo.MainCard = 0;
                    cardInfo.StatusCode = 0;
                    cardInfo.Status = 0;
                    cardInfo.CreatedOn = DateTime.Now;
                    cardInfo.CreatedBy = "C792D747-6B74-4A58-BB5B-D98EF420F99F";

                    pc3.Clear();
                    pc3.Add("customer_id", cardInfo.CustomerId);
                    pc3.Add("creditNumber", cardInfo.CreditcardNumber);

                    if (NewDbHelper.NewDB.IData.ExecuteScalar(checkExistSQL, pc3).ToString() != "0")
                    {
                        continue;
                    }


                    if (NewDbHelper.NewDB.Create(cardInfo) != 1)
                    {
                        //  Msg(string.Format("插入旧库ID为【{0}】客户信用卡信息失败", oldCardList.Rows[i]["id"]), false);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(cardInfo.IdcardNumber) == false)
                        {
                            if (cardInfo.IdcardNumber.Length == 15 || cardInfo.IdcardNumber.Length == 18)
                            {
                                if (cardInfo.IdcardNumber != "000000000000000000" && cardInfo.IdcardNumber.Contains("**") == false)
                                {
                                    if (IBP.Common.CommonUtil.GetBirthDayFromIdCardNumber(cardInfo.IdcardNumber) != null)
                                    {
                                        pc2.Clear();
                                        pc2.Add("birthday", Convert.ToDateTime(IBP.Common.CommonUtil.GetBirthDayFromIdCardNumber(cardInfo.IdcardNumber)));
                                        pc2.Add("customer_id", cardInfo.CustomerId);

                                        NewDbHelper.NewDB.IData.ExecuteNonQuery(updateCustomerSQL, pc2);
                                    }
                                }
                            }
                        }
                        succ++;
                    }
                }

                //Msg(string.Format("同步客户信用卡完成，共 {0} 行记录, 插入 {1} 行， 忽略 {2} 行", oldCardList.Rows.Count, succ, skip), false);
            }

            return true;
        }

        public static bool SyncCustomerContactInfoToNewDatabase(string oldCustomerId, string newCustomerId, string relWorkorderId)
        {
            string getOldContactSQL = @"select u.UserName,c.* from C_LXJL c left join lrs_Users u on c.UserID = u.UserID where CID = $OldCustomerId$";
            string getNewUserSQL = "select * from user_info where modified_by = $OldUserId$";
            string checkExistSQL = "select count(1) from customer_contact_info where modified_by = $OldContactId$";
            CustomDataDomainModel purposeList = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("联系记录(联系目的)", true);
            CustomDataDomainModel resultList = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("联系记录(联系结果)", true);


            ParameterCollection pc = new ParameterCollection();
            pc.Add("OldCustomerId", oldCustomerId);
            DataTable oldContactTable = NewDbHelper.OldDB.IData.ExecuteDataTable(getOldContactSQL, pc);

            if (oldContactTable != null && oldContactTable.Rows.Count > 0)
            {
                try
                {
                    NewDbHelper.NewDB.BeginTransaction();

                    for (int i = 0; i < oldContactTable.Rows.Count; i++)
                    {
                        pc.Clear();
                        pc.Add("OldUserId", oldContactTable.Rows[i]["UserID"].ToString());
                        DataTable newUserTable = NewDbHelper.NewDB.IData.ExecuteDataTable(getNewUserSQL, pc);

                        if (newUserTable != null && newUserTable.Rows.Count > 0)
                        {
                            CustomerContactInfoModel record = null;
                            int purpose = 0, result = 0;


                            pc.Clear();
                            pc.Add("OldContactId", oldContactTable.Rows[i]["id"].ToString());

                            if (NewDbHelper.NewDB.IData.ExecuteScalar(checkExistSQL, pc).ToString() != "0")
                            {
                                continue;
                            }


                            record = new CustomerContactInfoModel();
                            record.ContactId = Guid.NewGuid().ToString();

                            record.CustomerId = newCustomerId;
                            record.RelWorkorderId = relWorkorderId;

                            record.CustomerPhone = (oldContactTable.Rows[i]["L_Numble"] == DBNull.Value) ? null : oldContactTable.Rows[i]["L_Numble"].ToString();
                            record.Directions = Convert.ToInt32(oldContactTable.Rows[i]["JLType"]);

                            purpose = Convert.ToInt32(oldContactTable.Rows[i]["LXMD"]);
                            if (purpose <= 0)
                            {
                                purpose = 1;
                            }
                            else
                            {
                                purpose = purpose + 1;
                            }

                            result = Convert.ToInt32(oldContactTable.Rows[i]["LXJG"]);
                            if (result <= 0)
                            {
                                result = 1;
                            }
                            else
                            {
                                result = result + 1;
                            }

                            record.Purpose = GetContactPurposeOrResult(purpose, purposeList);
                            record.Results = GetContactPurposeOrResult(result, resultList);

                            record.Description = oldContactTable.Rows[i]["notes"].ToString();
                            record.Status = 0;

                            record.CreatedOn = Convert.ToDateTime(oldContactTable.Rows[i]["AddDate"]);
                            record.CreatedBy = newUserTable.Rows[0]["user_id"].ToString();
                            record.ModifiedBy = oldContactTable.Rows[i]["id"].ToString();
                            record.StatusCode = 0;

                            if (NewDbHelper.NewDB.Create(record) != 1)
                            {
                                NewDbHelper.NewDB.RollbackTransaction();
                                return false;
                            }
                        }
                    }

                    NewDbHelper.NewDB.CommitTransaction();
                    return true;
                }
                catch (Exception ex)
                {
                    NewDbHelper.NewDB.RollbackTransaction();
                    return false;
                }

            }



            return false;

        }

 

        /// <summary>
        /// 更新新库客户来源。
        /// </summary>
        private bool UpdateSyncNewCustomerInfo(string customerId)
        {
            string sql = @"
update [customer_basic_info]
set
	sales_from = b.value_id
from 
	custom_data_value b
where 
    customer_id = $CustomerId$
	and sales_from = b.data_value_code and b.data_id= '8D98350B-93AF-432F-88CB-A7381216E61F';
";
            string sql2 = @"
update [customer_basic_info]
set
	level = b.value_id
from 
	custom_data_value b
where 
    customer_id = $CustomerId$
	and level = b.data_value_code and b.data_id= 'E8DCC43F-2823-450B-8ED4-E388F8FD5CA5';";

            string sql3 = @"
update [customer_basic_info]
set
	carriers = b.value_id
from 
	custom_data_value b
where 
    customer_id = $CustomerId$
	and carriers = b.data_value_code and b.data_id= '8AEFCEE9-3699-4F9F-A945-BD4376484402';";

            string sql4 = @"update [customer_basic_info]
set
	using_phone_brand = b.value_id
from 
	custom_data_value b
where 
    customer_id = $CustomerId$
	and using_phone_brand = b.data_value_code and b.data_id= 'D61C6028-CDEC-43E9-B2D9-721A9E0570A0';";

            ParameterCollection pc = new ParameterCollection();
            pc.Add("CustomerId", customerId);
            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql, pc);
            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql2, pc);
            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql3, pc);
            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql4, pc);


            string sql5 = @"
update [customer_basic_info]
set
	preferred_phone_brand = b.value_id
from 
	custom_data_value b
where 
    customer_id = $CustomerId$
	and preferred_phone_brand = b.data_value_code and b.data_id= 'D61C6028-CDEC-43E9-B2D9-721A9E0570A0';
";

            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql5, pc);


            string sql6 = "update dbo.customer_basic_info set preferred_phone_brand = '8B5596D1-2848-426F-9616-D42FC5AFA17A' where preferred_phone_brand is null and customer_id = $CustomerId$;";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql6, pc);

            string sql7 = "update dbo.customer_basic_info set using_phone_brand = '8B5596D1-2848-426F-9616-D42FC5AFA17A' where using_phone_brand is null and customer_id = $CustomerId$;";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql7, pc);


            string sql8 = @"
update [customer_basic_info]
set
	mobile_phone_price = b.value_id
from 
	custom_data_value b
where 
    customer_id = $CustomerId$
	and mobile_phone_price = b.data_value_code and b.data_id= 'EDF77C7A-49FE-4749-9BCA-3CA2016AB251';
";

            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql8, pc);

            string sql9 = "update dbo.customer_basic_info set mobile_phone_price = '3DB0973E-66BE-4196-928B-7C5F363B762F' where mobile_phone_price is null and customer_id = $CustomerId$;";

            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql9, pc);

            string sql10 = @"
update [customer_basic_info]
set
	communication_consumer = b.value_id
from 
	custom_data_value b
where 
    customer_id = $CustomerId$
	and communication_consumer = b.data_value_code and b.data_id= '1B714EA9-EE6B-4D10-AEA1-AB04DCCD6287';
";

            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql10, pc);

            string sql11 = @"update dbo.customer_basic_info set communication_consumer = 'A22C0D97-37B5-4430-9842-3A3C80DAC391' where communication_consumer is null and customer_id = $CustomerId$";

            NewDbHelper.NewDB.IData.ExecuteNonQuery(sql11, pc);


            string initsql = "update dbo.customer_basic_info set home_phone = '' where customer_id = $CustomerId$ and home_phone is null;";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql, pc);

            initsql = "update dbo.customer_basic_info set using_phone_type = '' where customer_id = $CustomerId$ and  using_phone_type is null;";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql, pc);

            initsql = "update dbo.customer_basic_info set mobile_phone = '' where customer_id = $CustomerId$ and  mobile_phone is null;";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql, pc);

            initsql = "update dbo.customer_basic_info set other_phone = '' where customer_id = $CustomerId$ and  other_phone is null;";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql, pc);

            initsql = "update dbo.customer_basic_info set using_smartphone = '3' where customer_id = $CustomerId$ and  using_smartphone is null;";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql, pc);

            initsql = "update dbo.customer_basic_info set using_smartphone = '3' where customer_id = $CustomerId$ and  using_smartphone = '4';";
            NewDbHelper.NewDB.IData.ExecuteNonQuery(initsql, pc);

            return true;
        }


        protected Dictionary<string, UserInfoModel> GetNewDbUserInfoByOldUserId()
        {
            Dictionary<string, UserInfoModel> dict = null;

            string sql = "select * from user_info";
            DataTable dt = NewDbHelper.NewDB.IData.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                dict = new Dictionary<string, UserInfoModel>();
                UserInfoModel user = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    user = new UserInfoModel();
                    NewDbHelper.NewDB.ConvertFrom(user, dt, i);
                    dict[user.ModifiedBy] = user;
                }
            }
            return dict;
        }

        protected Dictionary<string, UserInfoModel> GetNewDbUserInfoByOldWorkId()
        {
            Dictionary<string, UserInfoModel> dict = null;

            string sql = "select * from user_info";
            DataTable dt = NewDbHelper.NewDB.IData.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                dict = new Dictionary<string, UserInfoModel>();
                UserInfoModel user = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    user = new UserInfoModel();
                    NewDbHelper.NewDB.ConvertFrom(user, dt, i);
                    dict[user.WorkId.Replace("WORKID_","")] = user;
                }
            }
            return dict;
        }

        private void btnSyncWorkorderStatus_Click(object sender, EventArgs e)
        {
            SyncWorkorderStatus();
        }

        private void SyncWorkorderStatus()
        {
            btnSyncWorkorderStatus.Enabled = false;

            string workProcessTableSQL = "select * from workorder_process_info";
            string updateProcessStatusSQL = "update workorder_process_info set after_status = $StatusId$ where process_id = $ProcessId$";
            DataTable workOrderProcessTable = NewDbHelper.NewDB.IData.ExecuteDataTable(workProcessTableSQL);
            ParameterCollection wppc = new ParameterCollection();
            WorkOrderTypeDomainModel typeModel = null;
            int updateProcessCounter = 0; int skipProcessCounter = 0;

            if (workOrderProcessTable != null)
            {
                for (int i = 0; i < workOrderProcessTable.Rows.Count; i++)
                {
                    typeModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(workOrderProcessTable.Rows[i]["workorder_type_id"].ToString(), false);
                    if (typeModel == null)
                    {
                        skipProcessCounter++;
                        continue;
                    }
                    if (typeModel.ResultList.ContainsKey(workOrderProcessTable.Rows[i]["after_result"].ToString()))
                    {
                        if (typeModel.ResultList[workOrderProcessTable.Rows[i]["after_result"].ToString()].ResultName == "成功" ||
                            typeModel.ResultList[workOrderProcessTable.Rows[i]["after_result"].ToString()].ResultName == "结束")
                        {
                            wppc.Clear();
                            wppc.Add("ProcessId", workOrderProcessTable.Rows[i]["process_id"].ToString());
                            wppc.Add("StatusId", typeModel.EndStatusInfo.WorkorderStatusId);

                            updateProcessCounter += NewDbHelper.NewDB.IData.ExecuteNonQuery(updateProcessStatusSQL, wppc);
                        }
                    }
                }
            }

            txtLogBox.Text += string.Format("【{0}】：成功更新{1}行工单信息处理记录状态信息\r\n", DateTime.Now, updateProcessCounter);
            txtLogBox.Text += string.Format("【{0}】：忽略{1}行未匹配工单类型的工单信息处理记录状态信息\r\n", DateTime.Now, skipProcessCounter);
           




            string getOrderInfoSQL = "select * from workorder_info";
            string getFirstProcessSQL = "select top 1 * from workorder_process_info where workorder_id = $WorkOrderId$ order by created_on asc";
            string getLastProcessSQL = @"
select top 1 s.status_name, p.*
from 
	workorder_process_info p
inner join
	workorder_status_info s on p.after_status = s.workorder_status_id
where 
	1=1
	and p.workorder_id =  $WorkOrderId$  
order by 
	p.created_on desc";


            string updateFirstProcessSQL = "update workorder_info set first_process_time = $FirstProcessTime$, first_process_userid = $FirstProcessUserId$, modified_on = $FirstProcessTime$, modified_by = $FirstProcessUserId$ where work_order_id = $WorkOrderId$";
            string updateLastProcessSQL = "";

            ParameterCollection pc = new ParameterCollection();
            ParameterCollection firstPc = new ParameterCollection();
            ParameterCollection lastPc = new ParameterCollection();

            DataTable workOrderTable = NewDbHelper.NewDB.IData.ExecuteDataTable(getOrderInfoSQL);
            DataTable firstProcessTable = null;
            DataTable lastProcessTable = null;

            int firstCounter = 0, closeCounter = 0, processCounter = 0, waitAuthCounter = 0;

            if (workOrderTable != null && workOrderTable.Rows.Count > 0)
            {
                for (int i = 0; i < workOrderTable.Rows.Count; i++)
                {
                    pc.Clear();
                    pc.Add("WorkOrderId", workOrderTable.Rows[i]["work_order_id"].ToString());
                    firstProcessTable = NewDbHelper.NewDB.IData.ExecuteDataTable(getFirstProcessSQL, pc);
                    lastProcessTable = NewDbHelper.NewDB.IData.ExecuteDataTable(getLastProcessSQL, pc);

                    #region  更新首次处理时间
                    if (firstProcessTable != null && firstProcessTable.Rows.Count > 0)
                    {
                        // 更新首次处理时间
                        firstPc.Clear();
                        firstPc.Add("WorkOrderId", workOrderTable.Rows[i]["work_order_id"].ToString());
                        firstPc.Add("FirstProcessTime", firstProcessTable.Rows[0]["created_on"].ToString());
                        firstPc.Add("FirstProcessUserId", firstProcessTable.Rows[0]["created_by"].ToString());

                        firstCounter += NewDbHelper.NewDB.IData.ExecuteNonQuery(updateFirstProcessSQL, firstPc);
                    }
                    #endregion

                    #region 更新最后处理记录

                    if (lastProcessTable != null && lastProcessTable.Rows.Count > 0)
                    {
                        // 更新最后处理记录

                        if (lastProcessTable.Rows[0]["status_name"].ToString() == "已关闭")
                        {
                            updateLastProcessSQL = @"
                                update workorder_info set 
                                    now_status_id = $NowStatusId$,
                                    now_result_id = $NowResultId$,
                                    now_process_userid = $NowProcessUserId$, 
                                    closed_time= $NowProcessTime$, 
                                    closed_user= $NowProcessUserId$,
                                    modified_on = $NowProcessTime$, 
                                    modified_by = $NowProcessUserId$,
                                    status_for_user = 2, 
                                    process_status = 2
                                where 
                                    work_order_id = $WorkOrderId$";

                            lastPc.Clear();
                            lastPc.Add("WorkOrderId", workOrderTable.Rows[i]["work_order_id"].ToString());
                            lastPc.Add("NowProcessTime", lastProcessTable.Rows[0]["created_on"].ToString());
                            lastPc.Add("NowProcessUserId", lastProcessTable.Rows[0]["created_by"].ToString());
                            lastPc.Add("NowStatusId", lastProcessTable.Rows[0]["after_status"].ToString());
                            lastPc.Add("NowResultId", lastProcessTable.Rows[0]["after_result"].ToString());

                            closeCounter += NewDbHelper.NewDB.IData.ExecuteNonQuery(updateLastProcessSQL, lastPc);
                        }
                        else if (lastProcessTable.Rows[0]["status_name"].ToString() == "待审批")
                        {
                            updateLastProcessSQL = @"
                                update workorder_info set 
                                    now_status_id = $NowStatusId$,
                                    now_result_id = $NowResultId$,
                                    now_process_userid = $NowProcessUserId$,
                                    modified_on = $NowProcessTime$, 
                                    modified_by = $NowProcessUserId$,
                                    status_for_user = 1, 
                                    process_status = 1
                                where 
                                    work_order_id = $WorkOrderId$";

                            lastPc.Clear();
                            lastPc.Add("WorkOrderId", workOrderTable.Rows[i]["work_order_id"].ToString());
                            lastPc.Add("NowProcessTime", lastProcessTable.Rows[0]["created_on"].ToString());
                            lastPc.Add("NowProcessUserId", lastProcessTable.Rows[0]["created_by"].ToString());
                            lastPc.Add("NowStatusId", lastProcessTable.Rows[0]["after_status"].ToString());
                            lastPc.Add("NowResultId", lastProcessTable.Rows[0]["after_result"].ToString());

                            waitAuthCounter += NewDbHelper.NewDB.IData.ExecuteNonQuery(updateLastProcessSQL, lastPc);
                        }
                        else if (lastProcessTable.Rows[0]["status_name"].ToString() == "处理中")
                        {
                            updateLastProcessSQL = @"
                                update workorder_info set 
                                    now_status_id = $NowStatusId$,
                                    now_result_id = $NowResultId$,
                                    now_process_userid = $NowProcessUserId$,
                                    modified_on = $NowProcessTime$, 
                                    modified_by = $NowProcessUserId$,
                                    status_for_user = 1, 
                                    process_status = 1
                                where 
                                    work_order_id = $WorkOrderId$";

                            lastPc.Clear();
                            lastPc.Add("WorkOrderId", workOrderTable.Rows[i]["work_order_id"].ToString());
                            lastPc.Add("NowProcessTime", lastProcessTable.Rows[0]["created_on"].ToString());
                            lastPc.Add("NowProcessUserId", lastProcessTable.Rows[0]["created_by"].ToString());
                            lastPc.Add("NowStatusId", lastProcessTable.Rows[0]["after_status"].ToString());
                            lastPc.Add("NowResultId", lastProcessTable.Rows[0]["after_result"].ToString());

                            processCounter += NewDbHelper.NewDB.IData.ExecuteNonQuery(updateLastProcessSQL, lastPc);
                        }
                    }

                    #endregion


                }

                txtLogBox.Text += string.Format("【{0}】：成功更新{1}行工单信息首次处理时间及处理人\r\n", DateTime.Now, firstCounter);
                txtLogBox.Text += string.Format("【{0}】：成功更新{1}行工单信息状态为处理中\r\n", DateTime.Now, processCounter);
                txtLogBox.Text += string.Format("【{0}】：成功更新{1}行工单信息状态为已关闭\r\n", DateTime.Now, closeCounter);
                txtLogBox.Text += string.Format("【{0}】：成功更新{1}行工单信息状态为待审批\r\n", DateTime.Now, waitAuthCounter);
            }

            string getWorkOrderProcStatusSQL = @"
select 
	w.work_order_id, s.status_name, w.process_status, w.status_for_user, w.modified_on, w.first_process_time, w.first_process_userid, w.closed_time, w.closed_user 
from 
	workorder_info w
inner join
	workorder_status_info s on w.now_status_id = s.workorder_status_id
where 
	1=1
";

            string updateStatusForProcAndUserSQL = "update workorder_info set status_for_user = $StatusForUser$, process_status = $ProcStatus$ where work_order_id = $WorkOrderId$";


            workOrderTable = NewDbHelper.NewDB.IData.ExecuteDataTable(getWorkOrderProcStatusSQL);
            string statusName = "";
            //int statusForUser = 0; 
            //int procStatus = 0;

            int waitCounter = 0;
            if (workOrderTable != null && workOrderTable.Rows.Count > 0)
            {
                for (int i = 0; i < workOrderTable.Rows.Count; i++)
                {
                    statusName = workOrderTable.Rows[i]["status_name"].ToString();
                    //statusForUser = Convert.ToInt32(workOrderTable.Rows[i]["status_for_user"]);
                    //procStatus = Convert.ToInt32(workOrderTable.Rows[i]["process_status"]);

                    pc.Clear();
                    pc.Add("WorkOrderId", workOrderTable.Rows[i]["work_order_id"].ToString());

                    switch (statusName)
                    {
                        case "未处理":
                            pc.Add("StatusForUser", 0);
                            pc.Add("ProcStatus", 0);

                            waitCounter += NewDbHelper.NewDB.IData.ExecuteNonQuery(updateStatusForProcAndUserSQL, pc);
                            break;

                        case "处理中":
                            pc.Add("StatusForUser", 1);
                            pc.Add("ProcStatus", 1);

                            processCounter += NewDbHelper.NewDB.IData.ExecuteNonQuery(updateStatusForProcAndUserSQL, pc);
                            break;

                        case "已关闭":
                            pc.Add("StatusForUser", 2);
                            pc.Add("ProcStatus", 2);

                            closeCounter += NewDbHelper.NewDB.IData.ExecuteNonQuery(updateStatusForProcAndUserSQL, pc);
                            break;

                        default:
                            break;
                    }
                }

                txtLogBox.Text += string.Format("【{0}】：成功更新{1}行工单信息状态（ProcessStatus）为未处理\r\n", DateTime.Now, waitCounter);
                txtLogBox.Text += string.Format("【{0}】：成功更新{1}行工单信息状态（ProcessStatus）为处理中\r\n", DateTime.Now, processCounter);
                txtLogBox.Text += string.Format("【{0}】：成功更新{1}行工单信息状态（ProcessStatus）为已关闭\r\n", DateTime.Now, closeCounter);

            }



            #region old

            //            string statusSQL2 = "select workorder_status_id from workorder_status_info where workorder_type_id = $type_id$ and status_name = '已关闭'";

            //            string updateWorkorderStatusFromProcessRecordSQL = "select p.* from workorder_process_info p left join workorder_status_info s on p.after_status = s.workorder_status_id where (s.status_name = '已关闭' or p.description like '关闭%')";
            //            string updateWorkorderStatusSQL = "update workorder_info set process_status = 2, now_status_id = $status$, closed_user = $closed_user$, closed_time = $closed_time$ where work_order_id = $workorderid$";
            //            DataTable npd = DbHelper.NewDB.IData.ExecuteDataTable(updateWorkorderStatusFromProcessRecordSQL);
            //            if (npd != null && npd.Rows.Count > 0)
            //            {
            //                int counter = 0;
            //                ParameterCollection pc = new ParameterCollection();
            //                ParameterCollection pc5 = new ParameterCollection();

            //                for (int i = 0; i < npd.Rows.Count; i++)
            //                {
            //                    pc5.Clear();
            //                    pc5.Add("type_id", npd.Rows[i]["workorder_type_id"].ToString());

            //                    object status = DbHelper.NewDB.IData.ExecuteScalar(statusSQL2, pc5);
            //                    if (status == null)
            //                    {
            //                        throw new Exception("数据中存在未知的工单类型状态ID");
            //                    }

            //                    pc.Clear();
            //                    pc.Add("status", status.ToString());
            //                    pc.Add("workorderid", npd.Rows[i]["workorder_id"].ToString());
            //                    pc.Add("closed_time", npd.Rows[i]["created_on"].ToString());
            //                    pc.Add("closed_user", npd.Rows[i]["created_by"].ToString());

            //                    if (DbHelper.NewDB.IData.ExecuteNonQuery(updateWorkorderStatusSQL, pc) == 1)
            //                    {
            //                        counter++;
            //                    }
            //                }

            //                Msg(string.Format("从工单处理记录中更新【{0}】条工单处理状态", counter), false);
            //            }

            //            string sql = @"
            //select w.work_order_id,w.workorder_type from workorder_info w left join workorder_status_info s 
            //on w.now_status_id = s.workorder_status_id 
            //where s.status_name = '未处理' and w.work_order_id in (select workorder_id from workorder_process_info)";

            //            string sql4 = "update workorder_status_info set status_name = '处理中' where workorder_status_id = '2D7E4025-C9BF-4BB4-855B-C6B57E03B597'";
            //            DbHelper.NewDB.IData.ExecuteNonQuery(sql4);

            //            DataTable wod = DbHelper.NewDB.IData.ExecuteDataTable(sql);
            //            DataTable wpd = null;

            //            if (wod != null && wod.Rows.Count > 0)
            //            {
            //                try
            //                {
            //                    DbHelper.NewDB.BeginTransaction();

            //                    WorkorderProcessInfoModel wp = null;

            //                    string wpSql = "select top 1 * from workorder_process_info where workorder_id = $workorder_id$ order by created_on asc";
            //                    string statusSQL = "select * from workorder_status_info where workorder_type_id = $type_id$ and status_name = '处理中'";
            //                    string woUpdateSQL = "update workorder_info set now_status_id = $now_status_id$, first_process_time = $first_process_time$, first_process_userid = $first_process_userid$ where work_order_id = $workorder_id$";
            //                    ParameterCollection pc2 = new ParameterCollection();
            //                    ParameterCollection pc3 = new ParameterCollection();
            //                    int counter = 0;

            //                    for (int i = 0; i < wod.Rows.Count; i++)
            //                    {
            //                        pc2.Clear();
            //                        pc2.Add("workorder_id", wod.Rows[i]["work_order_id"].ToString());
            //                        pc2.Add("type_id", wod.Rows[i]["workorder_type"].ToString());

            //                        wpd = DbHelper.NewDB.IData.ExecuteDataTable(wpSql, pc2);

            //                        if (wpd != null && wpd.Rows.Count > 0)
            //                        {
            //                            wp = new WorkorderProcessInfoModel();
            //                            DbHelper.NewDB.ConvertFrom(wp, wpd, 0);
            //                            if (wp != null)
            //                            {
            //                                object oo = DbHelper.NewDB.IData.ExecuteScalar(statusSQL, pc2);

            //                                if (oo != null)
            //                                {
            //                                    pc3.Clear();
            //                                    pc3.Add("now_status_id", oo.ToString());
            //                                    pc3.Add("first_process_time", wp.CreatedOn);
            //                                    pc3.Add("first_process_userid", wp.CreatedOn);
            //                                    pc3.Add("workorder_id", wod.Rows[i]["work_order_id"].ToString());

            //                                    if (DbHelper.NewDB.IData.ExecuteNonQuery(woUpdateSQL, pc3) != 1)
            //                                    {
            //                                        DbHelper.NewDB.RollbackTransaction();
            //                                        throw new Exception("操作失败");
            //                                    }
            //                                    else
            //                                    {
            //                                        counter++;
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    throw new Exception("获取状态ID失败");
            //                                }
            //                            }
            //                        }
            //                    }

            //                    DbHelper.NewDB.CommitTransaction();
            //                    Msg(string.Format("从工单处理记录中更新【{0}】条工单处理状态", counter), false);
            //                }
            //                catch (Exception ex)
            //                {
            //                    DbHelper.NewDB.RollbackTransaction();
            //                    throw ex;
            //                }
            //            }

            #endregion


            btnSyncWorkorderStatus.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OldDataMgr oldmgr = new OldDataMgr();
            oldmgr.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            SyncCustomerContactNumber();

            button3.Enabled = true;
        }

        private void SyncCustomerContactNumber()
        {
            string sql = "truncate table [customer_phone_info];";
            int cc = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql);
            Msg(string.Format("成功清除客户联系方式信息表"), false);

            string sql2 = @"
INSERT INTO [customer_phone_info]
           ([phone_id],[customer_id],[phone_number],[phone_type],[from_city_name],[from_city_id],[rel_order_id],[description],[call_status],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select
			NEWID(),customer_id, mobile_phone, 'C09DB28A-F4CE-4CD8-A2E9-FFDAACCA7A82',null,null,null,null,'34B9BC9B-4037-4EA9-8549-C98C11BEE0F9',0,GETDATE(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0
from
	customer_basic_info		
where 
	mobile_phone <> '' and mobile_phone is not null;";

            cc = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql2);
            Msg(string.Format("成功插入客户联系方式（手机号码）记录【{0}】条", cc), false);

            string sql3 = @"
update [customer_phone_info]
set
	from_city_name = b.city,from_city_id = b.china_id
from 
	phone_location_info b
where 
	substring(phone_number,0,8) = b.phone_code;";

            cc = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql3);
            Msg(string.Format("成功更新客户联系方式（手机号码）记录号码来源信息【{0}】条", cc), false);

            string sql4 = @"INSERT INTO [customer_phone_info]
           ([phone_id],[customer_id],[phone_number],[phone_type],[from_city_name],[from_city_id],[rel_order_id],[description],[call_status],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select
			NEWID(),customer_id, home_phone, '5B350231-8FC5-4331-AFED-940C580F79EF',null,null,null,null,'34B9BC9B-4037-4EA9-8549-C98C11BEE0F9',0,GETDATE(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0
from
	customer_basic_info		
where 
	home_phone <> '' and home_phone is not null;";

            cc = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql4);
            Msg(string.Format("成功插入客户联系方式（固定号码）记录【{0}】条", cc), false);


            string sql5 = @"INSERT INTO [customer_phone_info]
           ([phone_id],[customer_id],[phone_number],[phone_type],[from_city_name],[from_city_id],[rel_order_id],[description],[call_status],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code])
select
			NEWID(),customer_id, other_phone, '21FAA07F-4F0A-47FD-A5AB-6C0F0A45E831',null,null,null,null,'34B9BC9B-4037-4EA9-8549-C98C11BEE0F9',0,GETDATE(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0
from
	customer_basic_info		
where 
	other_phone <> '' and other_phone is not null;";


            cc = NewDbHelper.NewDB.IData.ExecuteNonQuery(sql5);
            Msg(string.Format("成功插入客户联系方式（其他号码）记录【{0}】条", cc), false);


            string getNotmobileNumberSQL = "select * from customer_phone_info where phone_type <> 'C09DB28A-F4CE-4CD8-A2E9-FFDAACCA7A82'";
            string updateSQL = "update customer_phone_info set from_city_id= $city_id$, from_city_name=$city_name$ where phone_id = $phone_id$";

            DataTable dt = NewDbHelper.NewDB.IData.ExecuteDataTable(getNotmobileNumberSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                int skip = 0; int succ = 0; string city = null; string chinaId = null;

                ParameterCollection pc = new ParameterCollection();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    city = GetCityByPhoneNumber(dt.Rows[i]["phone_number"].ToString(),out chinaId);

                    if (city == null)
                    {
                        skip++;
                        continue;
                    }

                    pc.Clear();
                    pc.Add("city_id", chinaId);
                    pc.Add("city_name", city);
                    pc.Add("phone_id", dt.Rows[i]["phone_id"].ToString());
                    if (NewDbHelper.NewDB.IData.ExecuteNonQuery(updateSQL, pc) == 1)
                    {
                        succ++;
                    }
                    else
                    {
                        skip++;
                    }
                }

                Msg(string.Format("更新客户联系方式信息表中 “号码来源” 共 {0} 行记录, 成功 {1} 行， 失败 {2} 行。 ", dt.Rows.Count, succ, skip), false);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;

            UpdateCustomerContactComeFrom();

            button4.Enabled = true;
        }

        private void UpdateCustomerContactComeFrom()
        {
            string sql = "select * from customer_contact_info;";
            string updateSQL = "update customer_contact_info set from_city_id = $chinaId$, from_city_name = $cityName$ where contact_id = $contactId$";

            DataTable dt = NewDbHelper.NewDB.IData.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                PhoneLocationInfoModel loc = null;
                ParameterCollection pc = new ParameterCollection();
                int succ = 0; int skip = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    loc = PhoneLocationInfoService.Instance.GetLocationInfoFromDatabase(dt.Rows[i]["customer_phone"].ToString());
                    if (loc != null)
                    {
                        pc.Clear();
                        pc.Add("chinaId", loc.ChinaId);
                        pc.Add("cityName", loc.City);
                        pc.Add("contactId", dt.Rows[i]["contact_id"].ToString());

                        succ = succ + NewDbHelper.NewDB.IData.ExecuteNonQuery(updateSQL, pc);
                    }
                }

                Msg(string.Format("更新客户联系方式信息表中 “号码归属” 共 {0} 行记录, 成功 {1} 行， 失败 {2} 行。 ", dt.Rows.Count, succ, skip), false);

            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
       
            string Sql = "select * from customer_basic_info where sales_from='87364c41-7597-4d18-b17f-a82fca94bfad' ;";
            DataTable oldCustomerIdTable = NewDbHelper.OldDB.IData.ExecuteDataTable(Sql);
            if (oldCustomerIdTable != null && oldCustomerIdTable.Rows.Count > 0)
            {
                Msg(string.Format("开始同步客户基本信息，总数 {0} 行", oldCustomerIdTable.Rows.Count), false);

                for (int i = 0; i < oldCustomerIdTable.Rows.Count; i++)
                {

                    if (CheckExistCustomerIdFromNewDB(oldCustomerIdTable.Rows[i]["customer_id"].ToString()))
                    {

                        continue; 
                        // 客户信息存在于新库，执行更新操作
                    }
                    else
                    {
                         //客户信息不存在于新库，执行插入操作
                        if (InsertCustomerBasicInfo(oldCustomerIdTable.Rows[i]) == false)
                        {


                            Msg(string.Format("插入旧库customer_id为【{0}】客户基本信息失败", oldCustomerIdTable.Rows[i]["customer_id"]), false);
                        }
                    }
                }

                Msg("同步客户基本信息完成", false);


            }
        }

        /// <summary>
        /// 插入客户基本信息至新库。
        /// </summary>
        /// <param name="oldData"></param>
        /// <returns></returns>
        private bool InsertCustomerBasicInfo(DataRow oldData)
        {
            if (oldData == null)
                return false;

            CustomerBasicInfoModel basicInfo = new CustomerBasicInfoModel();
            basicInfo.CustomerId = oldData["customer_id"].ToString();
            basicInfo.CustomerCode = oldData["customer_code"].ToString();
            basicInfo.CustomerName = oldData["customer_name"].ToString();
            if (oldData["sex"].ToString() != null && oldData["sex"].ToString() != "")
            {
                basicInfo.Sex = Convert.ToInt32(oldData["sex"].ToString());
            }
            basicInfo.SalesFrom = oldData["sales_from"].ToString();
            basicInfo.Level = oldData["level"].ToString();
            basicInfo.NowOwnerId = oldData["now_owner_id"].ToString();
            basicInfo.MobilePhone = oldData["mobile_phone"].ToString();
            basicInfo.HomePhone = oldData["home_phone"].ToString();
            basicInfo.OtherPhone = oldData["other_phone"].ToString();
            basicInfo.ComeFrom = oldData["come_from"].ToString();
            if (oldData["china_id"].ToString() != null && oldData["china_id"].ToString() != "")
            {
                basicInfo.ChinaId = Convert.ToInt32(oldData["china_id"].ToString());
            }
            basicInfo.Carriers = oldData["carriers"].ToString();
            basicInfo.UsingPhoneBrand = oldData["using_phone_brand"].ToString();
            basicInfo.UsingPhoneType = oldData["using_phone_type"].ToString();
            basicInfo.CommunicationConsumer = oldData["communication_consumer"].ToString();
            basicInfo.PreferredPhoneBrand = oldData["preferred_phone_brand"].ToString();
            basicInfo.UsingSmartphone =Convert.ToInt32(oldData["using_smartphone"].ToString());
            basicInfo.MobilePhonePrice = oldData["mobile_phone_price"].ToString();
            if (oldData["birthday"].ToString() != null&&oldData["birthday"].ToString() !="")
            {
                basicInfo.Birthday = Convert.ToDateTime(oldData["birthday"].ToString());
            }
            basicInfo.IdcardType = oldData["idcard_type"].ToString();
            basicInfo.IdcardNumber = oldData["idcard_number"].ToString();
            if (oldData["status"].ToString() != null && oldData["status"].ToString() != "")
            {
                basicInfo.Status = Convert.ToInt32(oldData["status"].ToString());
            }
            if (oldData["created_on"].ToString() != null && oldData["created_on"].ToString() != "")
            {
                basicInfo.CreatedOn = Convert.ToDateTime(oldData["created_on"].ToString());
            }
            basicInfo.CreatedBy = oldData["created_by"].ToString();
            if (oldData["modified_on"].ToString() != null && oldData["modified_on"].ToString() != "")
            {
                basicInfo.ModifiedOn = Convert.ToDateTime(oldData["modified_on"].ToString());
            }
            basicInfo.ModifiedBy = oldData["modified_by"].ToString();
            if (oldData["status_code"].ToString() != null && oldData["status_code"].ToString() != "")
            {
                basicInfo.StatusCode = Convert.ToInt32(oldData["status_code"].ToString());
            }

            NewDbHelper.NewDB.BeginTransaction();
            int num= NewDbHelper.NewDB.Create(basicInfo);

            if (num > 0)
            {

                //插入联系号码
                if (InsertCustomerphoneInfo(oldData["customer_id"].ToString()) == false)
                {
                    NewDbHelper.NewDB.RollbackTransaction();
                    return false;
                }
                //插入联系记录
                if (InsertCustomercontactInfo(oldData["customer_id"].ToString()) == false)
                {
                    NewDbHelper.NewDB.RollbackTransaction();
                    return false;
                }
                 //插入配送地址
                if (InsertCustomerdeliveryInfo(oldData["customer_id"].ToString()) == false)
                {
                    NewDbHelper.NewDB.RollbackTransaction();
                    return false;
                }
                  //插入备注信息
                if (InsertCustomerMemoInfo(oldData["customer_id"].ToString()) == false)
                {
                    NewDbHelper.NewDB.RollbackTransaction();
                    return false;
                }
                ///插入信用卡信息
                if (InsertCustomercreditcardInfo(oldData["customer_id"].ToString()) == false)
                {
                    NewDbHelper.NewDB.RollbackTransaction();
                    return false;
                }
                 ///插入审核信息
                if (InsertCustomerapprovalInfo(oldData["customer_id"].ToString()) == false)
                {
                    NewDbHelper.NewDB.RollbackTransaction();
                    return false;
                }
                   NewDbHelper.NewDB.CommitTransaction();
                   return true;
            }
            else
            {
                NewDbHelper.NewDB.RollbackTransaction();
                return false;
            }
   

        }

        /// <summary>
        /// 插入客户联系号码至新库。
        /// </summary>
        /// <param name="oldData"></param>
        /// <returns></returns>
        private bool InsertCustomerphoneInfo(string customerid)
        {
            bool result = false;
            if (customerid == null)
                return result;

            string Sql = "select * from customer_phone_info where customer_id= $customer_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerid);
            DataTable oldCustomerIdTable = NewDbHelper.OldDB.IData.ExecuteDataTable(Sql, pc);
            if (oldCustomerIdTable != null && oldCustomerIdTable.Rows.Count > 0)
            {
                for (int i = 0; i < oldCustomerIdTable.Rows.Count; i++)
                {
                    if (CheckExistCustomerPhoneFromNewDB(oldCustomerIdTable.Rows[i]["phone_id"].ToString()))
                    {
                        result = true;
                       
                    }
                    else
                    {
                        CustomerPhoneInfoModel phoneInfo = new CustomerPhoneInfoModel();

                        phoneInfo.PhoneId = oldCustomerIdTable.Rows[i]["phone_id"].ToString();
                        phoneInfo.CustomerId = oldCustomerIdTable.Rows[i]["customer_id"].ToString();
                        phoneInfo.PhoneNumber = oldCustomerIdTable.Rows[i]["phone_number"].ToString();
                        phoneInfo.PhoneType = oldCustomerIdTable.Rows[i]["phone_type"].ToString();
                        phoneInfo.FromCityName = oldCustomerIdTable.Rows[i]["from_city_name"].ToString();
                        phoneInfo.FromCityId = oldCustomerIdTable.Rows[i]["from_city_id"].ToString();
                        phoneInfo.RelOrderId = oldCustomerIdTable.Rows[i]["rel_order_id"].ToString();
                        phoneInfo.Description = oldCustomerIdTable.Rows[i]["description"].ToString();
                        phoneInfo.CallStatus = oldCustomerIdTable.Rows[i]["call_status"].ToString();

                        if (oldCustomerIdTable.Rows[i]["status"].ToString() != null && oldCustomerIdTable.Rows[i]["status"].ToString() != "")
                        {
                            phoneInfo.Status = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status"].ToString());
                        }
                        if (oldCustomerIdTable.Rows[i]["created_on"].ToString() != null && oldCustomerIdTable.Rows[i]["created_on"].ToString() != "")
                        {
                            phoneInfo.CreatedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["created_on"].ToString());
                        }
                        phoneInfo.CreatedBy = oldCustomerIdTable.Rows[i]["created_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["modified_on"].ToString() != null && oldCustomerIdTable.Rows[i]["modified_on"].ToString() != "")
                        {
                            phoneInfo.ModifiedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["modified_on"].ToString());
                        }
                        phoneInfo.ModifiedBy = oldCustomerIdTable.Rows[i]["modified_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["status_code"].ToString() != null && oldCustomerIdTable.Rows[i]["status_code"].ToString() != "")
                        {
                            phoneInfo.StatusCode = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status_code"].ToString());
                        }
                        int num = NewDbHelper.NewDB.Create(phoneInfo);

                        if (num > 0)
                        {
                            result = true;
                            return result;
                        }
                        else
                        {
                            result = false;
                            return result;
                        }
                    }
                }
            }
       
              return result;
         


        }



        /// <summary>
        /// 插入客户联系记录至新库。
        /// </summary>
        /// <param name="oldData"></param>
        /// <returns></returns>
        private bool InsertCustomercontactInfo(string customerid)
        {
            bool result = false;
            if (customerid == null)
                return result;

            string Sql = "select * from customer_contact_info where customer_id= $customer_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerid);
            DataTable oldCustomerIdTable = NewDbHelper.OldDB.IData.ExecuteDataTable(Sql, pc);
            if (oldCustomerIdTable != null && oldCustomerIdTable.Rows.Count > 0)
            {
                for (int i = 0; i < oldCustomerIdTable.Rows.Count; i++)
                {
                    if (CheckExistCustomerContactFromNewDB(oldCustomerIdTable.Rows[i]["contact_id"].ToString()))
                    {
                        result = true;

                    }
                    else
                    {
                        CustomerContactInfoModel contactInfo = new CustomerContactInfoModel();

                        contactInfo.ContactId = oldCustomerIdTable.Rows[i]["contact_id"].ToString();
                        contactInfo.CustomerId = oldCustomerIdTable.Rows[i]["customer_id"].ToString();
                        contactInfo.RelWorkorderId = oldCustomerIdTable.Rows[i]["rel_workorder_id"].ToString();
                        if (oldCustomerIdTable.Rows[i]["from_city_id"].ToString() != null && oldCustomerIdTable.Rows[i]["from_city_id"].ToString() != "")
                        {

                            contactInfo.FromCityId = Convert.ToInt32(oldCustomerIdTable.Rows[i]["from_city_id"].ToString());
                        }
                        contactInfo.FromCityName = oldCustomerIdTable.Rows[i]["from_city_name"].ToString();
                        contactInfo.CalledNumber = oldCustomerIdTable.Rows[i]["called_number"].ToString();
                        if (oldCustomerIdTable.Rows[i]["directions"].ToString() != null && oldCustomerIdTable.Rows[i]["directions"].ToString() != "")
                        {
                            contactInfo.Directions = Convert.ToInt32(oldCustomerIdTable.Rows[i]["directions"].ToString());
                        }
                        contactInfo.Purpose = oldCustomerIdTable.Rows[i]["purpose"].ToString();
                        contactInfo.Results = oldCustomerIdTable.Rows[i]["results"].ToString();
                        contactInfo.Description = oldCustomerIdTable.Rows[i]["description"].ToString();
                        if (oldCustomerIdTable.Rows[i]["status"].ToString() != null && oldCustomerIdTable.Rows[i]["status"].ToString() != "")
                        {
                            contactInfo.Status = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status"].ToString());
                        }
                        if (oldCustomerIdTable.Rows[i]["created_on"].ToString() != null && oldCustomerIdTable.Rows[i]["created_on"].ToString() != "")
                        {
                            contactInfo.CreatedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["created_on"].ToString());
                        }
                        contactInfo.CreatedBy = oldCustomerIdTable.Rows[i]["created_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["modified_on"].ToString() != null && oldCustomerIdTable.Rows[i]["modified_on"].ToString() != "")
                        {
                            contactInfo.ModifiedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["modified_on"].ToString());
                        }
                        contactInfo.ModifiedBy = oldCustomerIdTable.Rows[i]["modified_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["status_code"].ToString() != null && oldCustomerIdTable.Rows[i]["status_code"].ToString() != "")
                        {
                            contactInfo.StatusCode = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status_code"].ToString());
                        }
                        int num = NewDbHelper.NewDB.Create(contactInfo);

                        if (num > 0)
                        {
                            result = true;
                            return result;
                        }
                        else
                        {
                            result = false;
                            return result;
                        }
                    }
                }
            }

            return result;



        }


        /// <summary>
        /// 插入客户配送地址至新库。
        /// </summary>
        /// <param name="oldData"></param>
        /// <returns></returns>
        private bool InsertCustomerdeliveryInfo(string customerid)
        {
            bool result = false;
            if (customerid == null)
                return result;

            string Sql = "select * from customer_delivery_info where customer_id= $customer_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerid);
            DataTable oldCustomerIdTable = NewDbHelper.OldDB.IData.ExecuteDataTable(Sql, pc);
            if (oldCustomerIdTable != null && oldCustomerIdTable.Rows.Count > 0)
            {
                for (int i = 0; i < oldCustomerIdTable.Rows.Count; i++)
                {
                    if (CheckExistCustomerdeliveryFromNewDB(oldCustomerIdTable.Rows[i]["delivery_id"].ToString()))
                    {
                        result = true;

                    }
                    else
                    {
                        CustomerDeliveryInfoModel deliveryInfo = new CustomerDeliveryInfoModel();

                        deliveryInfo.DeliveryId = oldCustomerIdTable.Rows[i]["delivery_id"].ToString();

                        deliveryInfo.CustomerId = oldCustomerIdTable.Rows[i]["customer_id"].ToString();
                        if (oldCustomerIdTable.Rows[i]["delivery_type"].ToString() != null && oldCustomerIdTable.Rows[i]["delivery_type"].ToString() != "")
                        {

                            deliveryInfo.DeliveryType = Convert.ToInt32(oldCustomerIdTable.Rows[i]["delivery_type"].ToString());
                        }
                        if (oldCustomerIdTable.Rows[i]["delivery_region_id"].ToString() != null && oldCustomerIdTable.Rows[i]["delivery_region_id"].ToString() != "")
                        {
                            deliveryInfo.DeliveryRegionId = Convert.ToInt32( oldCustomerIdTable.Rows[i]["delivery_region_id"].ToString());
                        }
                        deliveryInfo.DeliveryAddress = oldCustomerIdTable.Rows[i]["delivery_address"].ToString();
                        deliveryInfo.PostCode = oldCustomerIdTable.Rows[i]["post_code"].ToString();

                       deliveryInfo.Consignee = oldCustomerIdTable.Rows[i]["consignee"].ToString();

                        deliveryInfo.ConsigneePhone = oldCustomerIdTable.Rows[i]["consignee_phone"].ToString();
                        if (oldCustomerIdTable.Rows[i]["need_bills"].ToString() != null && oldCustomerIdTable.Rows[i]["need_bills"].ToString() != "")
                        {
                            deliveryInfo.NeedBills = Convert.ToInt32(oldCustomerIdTable.Rows[i]["need_bills"].ToString());
                        }
                        deliveryInfo.BillTitle = oldCustomerIdTable.Rows[i]["bill_title"].ToString();
                        if (oldCustomerIdTable.Rows[i]["sort_order"].ToString() != null && oldCustomerIdTable.Rows[i]["sort_order"].ToString() != "")
                        {
                            deliveryInfo.SortOrder =Convert.ToInt32( oldCustomerIdTable.Rows[i]["sort_order"].ToString());
                        }
                        
                        if (oldCustomerIdTable.Rows[i]["status"].ToString() != null && oldCustomerIdTable.Rows[i]["status"].ToString() != "")
                        {
                            deliveryInfo.Status = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status"].ToString());
                        }
                        if (oldCustomerIdTable.Rows[i]["created_on"].ToString() != null && oldCustomerIdTable.Rows[i]["created_on"].ToString() != "")
                        {
                            deliveryInfo.CreatedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["created_on"].ToString());
                        }
                        deliveryInfo.CreatedBy = oldCustomerIdTable.Rows[i]["created_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["modified_on"].ToString() != null && oldCustomerIdTable.Rows[i]["modified_on"].ToString() != "")
                        {
                            deliveryInfo.ModifiedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["modified_on"].ToString());
                        }
                        deliveryInfo.ModifiedBy = oldCustomerIdTable.Rows[i]["modified_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["status_code"].ToString() != null && oldCustomerIdTable.Rows[i]["status_code"].ToString() != "")
                        {
                            deliveryInfo.StatusCode = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status_code"].ToString());
                        }
                        int num = NewDbHelper.NewDB.Create(deliveryInfo);

                        if (num > 0)
                        {
                            result = true;
                            return result;
                        }
                        else
                        {
                            result = false;
                            return result;
                        }
                    }
                }
            }

            return true;



        }


        /// <summary>
        /// 插入客户备注信息至新库。
        /// </summary>
        /// <param name="oldData"></param>
        /// <returns></returns>
        private bool InsertCustomerMemoInfo(string customerid)
        {
            bool result = false;
            if (customerid == null)
                return result;

            string Sql = "select * from customer_memo_info where customer_id= $customer_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerid);
            DataTable oldCustomerIdTable = NewDbHelper.OldDB.IData.ExecuteDataTable(Sql, pc);
            if (oldCustomerIdTable != null && oldCustomerIdTable.Rows.Count > 0)
            {
                for (int i = 0; i < oldCustomerIdTable.Rows.Count; i++)
                {
                    if (CheckExistCustomermemoFromNewDB(oldCustomerIdTable.Rows[i]["memo_id"].ToString()))
                    {
                        result = true;

                    }
                    else
                    {
                        CustomerMemoInfoModel memoInfo = new CustomerMemoInfoModel();

 
                        memoInfo.MemoId = oldCustomerIdTable.Rows[i]["memo_id"].ToString();
                        memoInfo.CustomerId = oldCustomerIdTable.Rows[i]["customer_id"].ToString();
                        memoInfo.Memo = oldCustomerIdTable.Rows[i]["memo"].ToString();

                        if (oldCustomerIdTable.Rows[i]["status"].ToString() != null && oldCustomerIdTable.Rows[i]["status"].ToString() != "")
                        {
                            memoInfo.Status = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status"].ToString());
                        }
                        if (oldCustomerIdTable.Rows[i]["created_on"].ToString() != null && oldCustomerIdTable.Rows[i]["created_on"].ToString() != "")
                        {
                            memoInfo.CreatedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["created_on"].ToString());
                        }
                        memoInfo.CreatedBy = oldCustomerIdTable.Rows[i]["created_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["modified_on"].ToString() != null && oldCustomerIdTable.Rows[i]["modified_on"].ToString() != "")
                        {
                            memoInfo.ModifiedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["modified_on"].ToString());
                        }
                        memoInfo.ModifiedBy = oldCustomerIdTable.Rows[i]["modified_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["status_code"].ToString() != null && oldCustomerIdTable.Rows[i]["status_code"].ToString() != "")
                        {
                            memoInfo.StatusCode = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status_code"].ToString());
                        }
                        int num = NewDbHelper.NewDB.Create(memoInfo);

                        if (num > 0)
                        {
                            result = true;
                            return result;
                        }
                        else
                        {
                            result = false;
                            return result;
                        }
                    }
                }
            }

            return true;



        }


        /// <summary>
        /// 插入客户信用卡信息至新库。
        /// </summary>
        /// <param name="oldData"></param>
        /// <returns></returns>
        private bool InsertCustomercreditcardInfo(string customerid)
        {
            bool result = false;
            if (customerid == null)
                return result;

            string Sql = "select * from customer_creditcard_info where customer_id= $customer_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerid);
            DataTable oldCustomerIdTable = NewDbHelper.OldDB.IData.ExecuteDataTable(Sql, pc);
            if (oldCustomerIdTable != null && oldCustomerIdTable.Rows.Count > 0)
            {
                for (int i = 0; i < oldCustomerIdTable.Rows.Count; i++)
                {
                    if (CheckExistCustomercreditcardFromNewDB(oldCustomerIdTable.Rows[i]["creditcard_id"].ToString()))
                    {
                        result = true;

                    }
                    else
                    {
                        CustomerCreditcardInfoModel creditcardInfo = new CustomerCreditcardInfoModel();


                        creditcardInfo.CreditcardId = oldCustomerIdTable.Rows[i]["creditcard_id"].ToString();
                        creditcardInfo.IvrDataId = oldCustomerIdTable.Rows[i]["ivr_data_id"].ToString();
                        creditcardInfo.CustomerId = oldCustomerIdTable.Rows[i]["customer_id"].ToString();
                        creditcardInfo.CreditcardNumber = oldCustomerIdTable.Rows[i]["creditcard_number"].ToString();
                        creditcardInfo.OpeningAddress = oldCustomerIdTable.Rows[i]["opening_address"].ToString();
                        creditcardInfo.CardLevel = oldCustomerIdTable.Rows[i]["card_level"].ToString();
                        creditcardInfo.Period = oldCustomerIdTable.Rows[i]["period"].ToString();
                        creditcardInfo.SecurityCode = oldCustomerIdTable.Rows[i]["security_code"].ToString();
                        creditcardInfo.CardType = oldCustomerIdTable.Rows[i]["card_type"].ToString();
                        creditcardInfo.CardUsername = oldCustomerIdTable.Rows[i]["card_username"].ToString();
                        creditcardInfo.IdcardType = oldCustomerIdTable.Rows[i]["idcard_type"].ToString();
                        creditcardInfo.IdcardNumber = oldCustomerIdTable.Rows[i]["idcard_number"].ToString();
                        creditcardInfo.CardBrand = oldCustomerIdTable.Rows[i]["card_brand"].ToString();
                        if (oldCustomerIdTable.Rows[i]["canbe_stage"].ToString() != null && oldCustomerIdTable.Rows[i]["canbe_stage"].ToString() != "")
                        {
                            creditcardInfo.CanbeStage = Convert.ToInt32(oldCustomerIdTable.Rows[i]["canbe_stage"].ToString());
                        }
                        if (oldCustomerIdTable.Rows[i]["main_card"].ToString() != null && oldCustomerIdTable.Rows[i]["main_card"].ToString() != "")
                        {
                            creditcardInfo.MainCard = Convert.ToInt32(oldCustomerIdTable.Rows[i]["main_card"].ToString());
                        }
                        creditcardInfo.BillAddress = oldCustomerIdTable.Rows[i]["bill_address"].ToString();
                        creditcardInfo.InfoType = oldCustomerIdTable.Rows[i]["info_type"].ToString();
                        if (oldCustomerIdTable.Rows[i]["bill_china_id"].ToString() != null && oldCustomerIdTable.Rows[i]["bill_china_id"].ToString() != "")
                        {
                            creditcardInfo.BillChinaId = Convert.ToInt32(oldCustomerIdTable.Rows[i]["bill_china_id"].ToString());
                        }
                        creditcardInfo.BillZipcode = oldCustomerIdTable.Rows[i]["bill_zipcode"].ToString();
                        creditcardInfo.InfoType = oldCustomerIdTable.Rows[i]["info_type"].ToString();
                        if (oldCustomerIdTable.Rows[i]["status"].ToString() != null && oldCustomerIdTable.Rows[i]["status"].ToString() != "")
                        {
                            creditcardInfo.Status = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status"].ToString());
                        }
                        if (oldCustomerIdTable.Rows[i]["created_on"].ToString() != null && oldCustomerIdTable.Rows[i]["created_on"].ToString() != "")
                        {
                            creditcardInfo.CreatedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["created_on"].ToString());
                        }
                        creditcardInfo.CreatedBy = oldCustomerIdTable.Rows[i]["created_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["modified_on"].ToString() != null && oldCustomerIdTable.Rows[i]["modified_on"].ToString() != "")
                        {
                            creditcardInfo.ModifiedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["modified_on"].ToString());
                        }
                        creditcardInfo.ModifiedBy = oldCustomerIdTable.Rows[i]["modified_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["status_code"].ToString() != null && oldCustomerIdTable.Rows[i]["status_code"].ToString() != "")
                        {
                            creditcardInfo.StatusCode = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status_code"].ToString());
                        }
                        int num = NewDbHelper.NewDB.Create(creditcardInfo);

                        if (num > 0)
                        {
                            result = true;
                            return result;
                        }
                        else
                        {
                            result = false;
                            return result;
                        }
                    }
                }
            }

            return true;



        }


        /// <summary>
        /// 插入客户审核信息至新库。
        /// </summary>
        /// <param name="oldData"></param>
        /// <returns></returns>
        private bool InsertCustomerapprovalInfo(string customerid)
        {
            bool result = false;
            if (customerid == null)
                return result;

            string Sql = "select * from customer_info_approval where customer_id= $customer_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerid);
            DataTable oldCustomerIdTable = NewDbHelper.OldDB.IData.ExecuteDataTable(Sql, pc);
            if (oldCustomerIdTable != null && oldCustomerIdTable.Rows.Count > 0)
            {
                for (int i = 0; i < oldCustomerIdTable.Rows.Count; i++)
                {
                    if (CheckExistCustomerapprovalFromNewDB(oldCustomerIdTable.Rows[i]["approval_id"].ToString()))
                    {
                        result = true;

                    }
                    else
                    {
                        CustomerInfoApprovalModel approvalInfo = new CustomerInfoApprovalModel();


                        approvalInfo.ApprovalId = oldCustomerIdTable.Rows[i]["approval_id"].ToString();
                        approvalInfo.ApprovalTaskId = oldCustomerIdTable.Rows[i]["approval_task_id"].ToString();
                        approvalInfo.CustomerId = oldCustomerIdTable.Rows[i]["customer_id"].ToString();
                        approvalInfo.UpdateField = oldCustomerIdTable.Rows[i]["update_field"].ToString();
                        approvalInfo.UpdateFieldName = oldCustomerIdTable.Rows[i]["update_field_name"].ToString();
                        approvalInfo.OldDataId = oldCustomerIdTable.Rows[i]["old_data_id"].ToString();
                        approvalInfo.OldData = oldCustomerIdTable.Rows[i]["old_data"].ToString();
                        approvalInfo.NewDataId = oldCustomerIdTable.Rows[i]["new_data_id"].ToString();
                        approvalInfo.NewData = oldCustomerIdTable.Rows[i]["new_data"].ToString();
                        approvalInfo.Description = oldCustomerIdTable.Rows[i]["description"].ToString();

                     
                        if (oldCustomerIdTable.Rows[i]["status"].ToString() != null && oldCustomerIdTable.Rows[i]["status"].ToString() != "")
                        {
                            approvalInfo.Status = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status"].ToString());
                        }
                        if (oldCustomerIdTable.Rows[i]["created_on"].ToString() != null && oldCustomerIdTable.Rows[i]["created_on"].ToString() != "")
                        {
                            approvalInfo.CreatedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["created_on"].ToString());
                        }
                        approvalInfo.CreatedBy = oldCustomerIdTable.Rows[i]["created_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["modified_on"].ToString() != null && oldCustomerIdTable.Rows[i]["modified_on"].ToString() != "")
                        {
                            approvalInfo.ModifiedOn = Convert.ToDateTime(oldCustomerIdTable.Rows[i]["modified_on"].ToString());
                        }
                        approvalInfo.ModifiedBy = oldCustomerIdTable.Rows[i]["modified_by"].ToString();
                        if (oldCustomerIdTable.Rows[i]["status_code"].ToString() != null && oldCustomerIdTable.Rows[i]["status_code"].ToString() != "")
                        {
                            approvalInfo.StatusCode = Convert.ToInt32(oldCustomerIdTable.Rows[i]["status_code"].ToString());
                        }
                        int num = NewDbHelper.NewDB.Create(approvalInfo);

                        if (num > 0)
                        {
                            result = true;
                            return result;
                        }
                        else
                        {
                            result = false;
                            return result;
                        }
                    }
                }
            }

            return true;



        }
        private void btnPhone_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmProductMgr productForm = new frmProductMgr();
            productForm.ShowDialog();
        }
    }

    public class CreditCardInfo
    {
        public CreditCardInfo()
        {
        }

        public string CardNumber { get; set; }
        public int ChinaId { get; set; }
        public string BillAddress { get; set; }
    }

    /// <summary>
    /// 身份证信息实体。
    /// </summary>
    public class IdCardInfo
    {
        public IdCardInfo()
        {
        }

        public string CardType { get; set; }
        public string CardNum { get; set; }
    }
}
