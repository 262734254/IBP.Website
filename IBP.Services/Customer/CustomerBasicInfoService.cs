/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-14
*/


using System.Linq;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using Excel = Microsoft.Office.Interop.Excel;
using Framework.Common;
using Framework.DataAccess;
using Framework.Utilities;
using IBP.Common;
using IBP.Models;
using System.Text;
using System.Data.OleDb;


namespace IBP.Services
{
	/// <summary>
	/// CustomerBasicInfo业务逻辑类
	/// </summary>
	public partial class CustomerBasicInfoService : BaseService
	{
        static bool locke = false;
		// 在此添加你的代码...

        private int _ReturnStatus;
        private string _ReturnMessage;



        private const string CREATE_ERROR = "Can not create excel file, may be your computer has not installed excel!";
        private const string IMPORT_ERROR = "Your excel file is open, please save and close it.";
        private const string EXPORT_ERROR = "This is an error that the excel file may be open when it be exported. /n";



        public int ReturnStatus
        {
            get { return _ReturnStatus; }
        }



        public string ReturnMessage
        {
            get { return _ReturnMessage; }
        }


        public DataSet ImportCustomerBasicInfoFromExcel(string fileName)
        {
            Excel.Application xlApp = new Excel.ApplicationClass();

            if (xlApp == null)
            {
                _ReturnStatus = -1;
                _ReturnMessage = CREATE_ERROR;
                return null;
            }

            Excel.Workbook workbook;

            try
            {
                workbook = xlApp.Workbooks.Open(fileName, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, 1, 0);
            }
            catch
            {
                _ReturnStatus = -1;
                _ReturnMessage = IMPORT_ERROR;
                return null;
            }



            int n = workbook.Worksheets.Count;
            string[] SheetSet = new string[n];
            System.Collections.ArrayList al = new System.Collections.ArrayList();

            for (int i = 1; i <= n; i++)
            {
                SheetSet[i - 1] = ((Excel.Worksheet)workbook.Worksheets[i]).Name;
            }



            workbook.Close(null, null, null);
            xlApp.Quit();

            if (workbook != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                workbook = null;
            }

            if (xlApp != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlApp = null;
            }
            GC.Collect();



            DataSet ds = new DataSet();
            string connStr = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + fileName + ";Extended Properties=Excel 8.0";

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                OleDbDataAdapter da;

                for (int i = 1; i <= n; i++)
                {
                    string sql = "select * from [" + SheetSet[i - 1] + "$] ";
                    da = new OleDbDataAdapter(sql, conn);
                    da.Fill(ds, SheetSet[i - 1]);
                    da.Dispose();

                }

                conn.Close();
                conn.Dispose();
            }

            return ds;
        }


        public bool ImportCustomerBasicInfo(DataTable customerTable, out string importLogs, out string message)
        {
         
            message = "操作失败，请与管理员联系";
            importLogs = "";
            bool result = false;
   
            if (customerTable == null && customerTable.Rows.Count == 0)
            {
                message = "客户信息表为空，请检查Excel文件是否正确";
                importLogs = message;
                return false;
            }

  

#region 验证表头

            List<CustomerAttributeGroupInfoModel> attGrouList = new List<CustomerAttributeGroupInfoModel>();
            CustomerExtAttributesModel attributeInfo = null;
            for (int i = 0; i < customerTable.Rows.Count; i++)
            {
                for (int j = 0; j < customerTable.Columns.Count; j++)
                {
                   attributeInfo = CustomerExtAttributesInfoService.Instance.GetCustomerExtAttributeInfoModelByAttributeName(customerTable.Columns[j].ColumnName,true);
                    if (attributeInfo == null)
                    {
                        message = string.Format("【{0}】列不存在数据库中", customerTable.Columns[j].ColumnName);
                        
                        return false;
                    }
                    switch (attributeInfo.FieldType)
                    {
                        case "string":
                            break;

                        case "text":
                            break;

                        case "custom":
                            List<string> customList = attributeInfo.CustomValue.Split(',').ToList();
                            if (customList != null && customList.Contains(customerTable.Rows[i][j].ToString()) == false)
                            {
                                message = string.Format("因为Excel第【{0}】行第【{0}】列的值不存在于属性名为A的自定义值列表中", customerTable.Rows[i], customerTable.Columns[j]);
                                return false;
                            }
                            break;

                        case "decimal":
                            if (Framework.Utilities.CharacterUtil.isNumber(customerTable.Rows[i][j].ToString()) == false)
                            {
                                message =string.Format("因为Excel第【{0}】行第【{0}】列的值不为数值" ,customerTable.Rows[i], customerTable.Columns[j]);
                                return false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
#endregion
            CustomerAttributeGroupInfoModel attGroupInfo = CustomerAttributeGroupInfoService.Instance.GetGroupInfoById(attributeInfo.GroupId, false);
            if (attGroupInfo == null)
            {
                message = "操作失败，请与管理员联系";
                return false;
            }
            attGrouList.Add(attGroupInfo);
            
            List<string> insertSQLList = new List<string>();

            foreach(CustomerAttributeGroupInfoModel groupInfo in attGrouList)
            {
                StringBuilder insertSQLBuilder = new StringBuilder();
                insertSQLBuilder.AppendFormat("INSERT INTO {0} (customer_id, created_on, created_by, status_code,", groupInfo.Tabname);
                for (int k = 0; k < customerTable.Columns.Count; k++)
                {
                     attributeInfo = CustomerExtAttributesInfoService.Instance.GetCustomerExtAttributeInfoModelByAttributeName(customerTable.Columns[k].ColumnName,true);
                    if(attributeInfo.GroupId == groupInfo.GroupId)
                    {
                        insertSQLBuilder.AppendFormat("[{0}],", attributeInfo.AttributeName);
                    }
                }
                insertSQLBuilder.Length = insertSQLBuilder.Length - 1;
                insertSQLBuilder.Append(" ) VALUES ( $customer_id$, GETDATE(), $created_by$, 0, ");

                for (int k = 0; k < customerTable.Columns.Count; k++)
                {
                    attributeInfo = CustomerExtAttributesInfoService.Instance.GetCustomerExtAttributeInfoModelByAttributeName(customerTable.Columns[k].ColumnName, true);
                    if(attributeInfo.GroupId == groupInfo.GroupId)
                    {
                        insertSQLBuilder.AppendFormat("${0}$,", CharacterUtil.ConvertToPinyin(attributeInfo.AttributeName));
                    }
                }
                insertSQLBuilder.Length = insertSQLBuilder.Length - 1;
                insertSQLBuilder.Append(" )");

                insertSQLList.Add(insertSQLBuilder.ToString());
            }

            try
            {
                ParameterCollection pc = new ParameterCollection();

                CustomerBasicInfoModel basicInfo = null;
                BeginTransaction();

                    for (int i = 0; i < customerTable.Rows.Count; i++)
                    {
                     #region 插入客户基本信息表数据
                        basicInfo = new CustomerBasicInfoModel();
                        basicInfo.CustomerId = GetGuid();
                        basicInfo.MobilePhone = customerTable.Rows[i]["手机号码"].ToString();
                        basicInfo.HomePhone = customerTable.Rows[i]["电话号码"].ToString();
                        basicInfo.OtherPhone = customerTable.Rows[i]["其他号码"].ToString();
                        basicInfo.Sex = (customerTable.Rows[i]["性别"].ToString() == "男") ? 0 : 1;
                        if (!string.IsNullOrEmpty(customerTable.Rows[i]["手机价位"].ToString()))
                        {
                            basicInfo.MobilePhonePrice = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("手机价位", false).GetCustomDataValueDomainByDataValue(customerTable.Rows[i]["手机价位"].ToString()).ValueId;

                        }
                        else
                        {
                            basicInfo.MobilePhonePrice = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("手机价位", false).GetCustomDataValueDomainByDataValue("未知").ValueId;

                        }

                        basicInfo.Level = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("客户等级", false).GetCustomDataValueDomainByDataValue(customerTable.Rows[i]["客户等级"].ToString()).ValueId;
                        if (!string.IsNullOrEmpty(customerTable.Rows[i]["运营商"].ToString()))
                        {
                            basicInfo.Carriers = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("运营商", false).GetCustomDataValueDomainByDataValue(customerTable.Rows[i]["运营商"].ToString()).ValueId;

                        }
                        else
                        {
                            basicInfo.Carriers = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("运营商", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
                        } 
                        basicInfo.UsingPhoneType = customerTable.Rows[i]["手机型号"].ToString();
                        basicInfo.CommunicationConsumer = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("通讯消费", false).GetCustomDataValueDomainByDataValue(customerTable.Rows[i]["通讯消费"].ToString()).ValueId;
                        basicInfo.PreferredPhoneBrand = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("优选品牌", false).GetCustomDataValueDomainByDataValue(customerTable.Rows[i]["优选品牌"].ToString()).ValueId;
                        basicInfo.UsingPhoneBrand = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("优选品牌", false).GetCustomDataValueDomainByDataValue(customerTable.Rows[i]["在用品牌"].ToString()).ValueId;
                        basicInfo.UsingSmartphone = (customerTable.Rows[i]["是否智能机"].ToString() == "是") ? 0 : 1;

                        basicInfo.SalesFrom = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("客户来源", false).GetCustomDataValueDomainByDataValue(customerTable.Rows[i]["客户来源"].ToString()).ValueId;
                        basicInfo.CustomerCode = "C" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + i;
                        PhoneLocationInfoModel loc = PhoneLocationInfoService.Instance.GetLocationInfo(basicInfo.MobilePhone, false);
                        basicInfo.CustomerId = GetGuid();
                        if (loc != null)
                        {
                            basicInfo.ChinaId = loc.ChinaId;
                            basicInfo.ComeFrom = loc.City;
                        }
                        else
                        {
                            basicInfo.ComeFrom = "";
                        }
                        if (!string.IsNullOrEmpty(basicInfo.MobilePhone))
                        {
                            if (CheckExistPhoneNumber(basicInfo.MobilePhone))
                            {
                                continue;
                            }
                        }
                        if (!string.IsNullOrEmpty(basicInfo.HomePhone))
                        {
                            if (CheckExistPhoneNumber(basicInfo.HomePhone))
                            {
                                continue;
                            }
                        }
                        if (!string.IsNullOrEmpty(basicInfo.OtherPhone))
                        {
                            if (CheckExistPhoneNumber(basicInfo.OtherPhone))
                            {
                                continue;
                            }
                        }
                        
                        #endregion

                        pc.Clear();
                        pc.Add("customer_id", basicInfo.CustomerId);
                        pc.Add("created_by", SessionUtil.Current.UserId);

                        for (int k = 0; k < customerTable.Columns.Count; k++)
                        {
                            pc.Add(CharacterUtil.ConvertToPinyin(customerTable.Columns[k].ColumnName), customerTable.Rows[i][k].ToString());
                        }

                        if (CustomerBasicInfoService.Instance.Create(basicInfo) > 0)
                        {
                            foreach (string sql in insertSQLList)
                            {
                                if (DbUtil.Current.IData.ExecuteNonQuery(sql, pc) != 1)
                                {
                                    RollbackTransaction();
                                    message = "";
                                    return false;
                                }
                             
                            }
                             #region 判断插入customer_phone_info表
                            if (!string.IsNullOrEmpty(basicInfo.MobilePhone))
                            {
                                if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(basicInfo.MobilePhone, basicInfo.CustomerId, out message))
                                {
                                    RollbackTransaction();

                                    return false;
                                }
                            }

                            if (!string.IsNullOrEmpty(basicInfo.HomePhone))
                            {
                                if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(basicInfo.HomePhone, basicInfo.CustomerId, out message))
                                {
                                    RollbackTransaction();
                                    return false;
                                }
                            }
                            if (!string.IsNullOrEmpty(basicInfo.OtherPhone))
                            {
                                if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(basicInfo.OtherPhone, basicInfo.CustomerId, out message))
                                {
                                    RollbackTransaction();
                                    return false;
                                }
                            }
                        #endregion
                            CustomerInfoService.Instance.GetCustomerDomainModelById(basicInfo.CustomerId, true);
                        }

                        
                    }
                

                CommitTransaction();
                message = "成功添加客户信息";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("导入客户信息异常", ex);
                throw ex;
            }

            return result;
        }


      


     

        /// <summary>
        /// 检查电话号码否存在。
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        private bool CheckExistPhoneNumber(string phoneNumber)
        {
            bool result = false;

            string sql = @"
SELECT 
    COUNT(1) 
FROM 
    customer_phone_info 
WHERE 
    phone_number = $phone_number$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("phone_number", phoneNumber);
            result = Convert.ToInt32(ExecuteScalar(sql, pc)) > 0;

            return result;
        }

        /// <summary>
        /// 批量新建客户信息。
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool BatchCreateCustomeBasicInfo(CustomerBasicInfoModel info, out string message,string phoneNumebr)
        {
            if (locke)
            {
                message = "正在操作，请稍后再试";
                return false;
            }
            locke = true;
            int num = 1;
            bool result = false;
            message = "操作失败，请与管理员联系";
            List<string> PhoneNumebrList = phoneNumebr.Split('\n').ToList();
            if (PhoneNumebrList == null && PhoneNumebrList.Count == 0)
            {
                message = "请输入电话号码";
                locke = false;
                return false;
            }

            try
            {
                BeginTransaction();
                foreach (string phone in PhoneNumebrList)
                {
                    //Regex test = new Regex("^(13[0-9]|15[0|3|6|7|8|9]|18[8|9])\d{8}$", RegexOptions.Compiled);
                    //bool isChinese = test.IsMatch(phone);
                    if (RegexUtil.IsMobilePhone(phone))
                    {
                        info.MobilePhone = phone;
                    }
                    else
                    {
                        info.MobilePhone = "";
                    }

                    if (RegexUtil.IsPhone(phone))
                    {
                        info.HomePhone = phone;
                    }
                    else
                    {
                        info.HomePhone = "";
                    }
                    info.CustomerCode = "C" + DateTime.Now.ToString("yyyyMMddHHmmssfff")+num++;
                    PhoneLocationInfoModel loc = PhoneLocationInfoService.Instance.GetLocationInfo(phone, false);
                    if (loc != null)
                    {
                        info.ChinaId = loc.ChinaId;
                        info.ComeFrom = loc.City;
                    }
                    else
                    { 
                        info.ComeFrom="";
                    }
                    if (!string.IsNullOrEmpty(info.MobilePhone))
                    {
                        if (CheckExistPhoneNumber(info.MobilePhone))
                        {
                            continue;
                        }
                    }
                    if (!string.IsNullOrEmpty(info.HomePhone))
                    {
                        if (CheckExistPhoneNumber(info.HomePhone))
                        {
                            continue;
                        }
                    }
                    if (!string.IsNullOrEmpty(info.OtherPhone))
                    {
                        if (CheckExistPhoneNumber(info.OtherPhone))
                        {
                            continue;
                        }
                    }
                    info.CustomerId = GetGuid();
                    if (CustomerBasicInfoService.Instance.Create(info) > 0)
                    {
                        if (!string.IsNullOrEmpty(info.MobilePhone))
                        {
                            if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(info.MobilePhone, info.CustomerId, out message))
                            {
                                RollbackTransaction();
                                locke = false;
                                return false;
                            }
                        }

                        if (!string.IsNullOrEmpty(info.HomePhone))
                        {
                            if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(info.HomePhone, info.CustomerId, out message))
                            {
                                RollbackTransaction();
                                locke = false;
                                return false;
                            }
                        }
                        if (!string.IsNullOrEmpty(info.OtherPhone))
                        {
                            if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(info.OtherPhone, info.CustomerId, out message))
                            {
                                RollbackTransaction();
                                locke = false;
                                return false;
                            }
                        }
                        CustomerInfoService.Instance.GetCustomerDomainModelById(info.CustomerId, true);
                    }
                }
                CommitTransaction();
                message = "批量添加客户信息成功";
                locke = false;
                result = true;
            }
          
            catch (Exception ex)
            {
                locke = false;
                RollbackTransaction();
                LogUtil.Error(message, ex);
                throw ex;
            }
            locke = false;
            return result;
        }
        /// <summary>
        /// 新建用户信息。
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool CreateCustomeBasicInfo(CustomerBasicInfoModel info, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            try
            {
                BeginTransaction();
                if (!string.IsNullOrEmpty(info.MobilePhone))
                {
                    if (CheckExistPhoneNumber(info.MobilePhone))
                    {
                        message = string.Format("操作失败，已经存在为【{0}】手机号码", info.MobilePhone);
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(info.HomePhone))
                {
                    if (CheckExistPhoneNumber(info.HomePhone))
                    {
                        message = string.Format("操作失败，已经存在为【{0}】固定电话", info.HomePhone);
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(info.OtherPhone))
                {
                    if (CheckExistPhoneNumber(info.OtherPhone))
                    {
                        message = string.Format("操作失败，已经存在为【{0}】电话号码", info.OtherPhone);
                        return false;
                    }
                }
                if (CustomerBasicInfoService.Instance.Create(info) > 0)
                {
                    if (!string.IsNullOrEmpty(info.MobilePhone))
                    {
                        if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(info.MobilePhone, info.CustomerId, out message))
                        {
                            RollbackTransaction();
                            return false;
                        }
                    }

                    if (!string.IsNullOrEmpty(info.HomePhone))
                    {
                        if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(info.HomePhone, info.CustomerId, out message))
                        {
                            RollbackTransaction();
                            return false;
                        }
                    }

                    if (!string.IsNullOrEmpty(info.OtherPhone))
                    {
                        if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(info.OtherPhone, info.CustomerId, out message))
                        {
                            RollbackTransaction();
                            return false;
                        }
                    }

                    CommitTransaction();
                    CustomerInfoService.Instance.GetCustomerDomainModelById(info.CustomerId, true);
                    message = "添加客户信息成功";
                    result = true;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error(message, ex);
                throw ex;
            }

            return result;
        }
	}
}

