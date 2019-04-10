/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-6
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


namespace IBP.Services
{
	/// <summary>
	/// CustomerExtAttributes业务逻辑类
	/// </summary>
	public partial class CustomerExtAttributesInfoService : BaseService
	{
        // 在此添加你的代码....


        public bool UpdateCustomerAttribute(CustomerExtAttributesModel attInfo, out string message,string oldName)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";
            CustomerExtAttributesModel model = GetCustomerAttributeById(attInfo.ExtAttributeId, true);


            Dictionary<string, CustomerAttributeGroupInfoModel> dict = CustomerAttributeGroupInfoService.Instance.GetCustomeGroupInfoList(true);

            CustomerAttributeGroupInfoModel oldInfo = dict[model.GroupId];

            try
            {
                BeginTransaction();
                if (attInfo.AttributeName != oldName)
                {
                    string TableName = "customer_info_" + CharacterUtil.ConvertToPinyin(oldInfo.GroupName);
                    string renameFieldSQL = DTableUtil.GetRenameFieldSQL(TableName, oldName, attInfo.AttributeName);
                    ExecuteNonQuery(renameFieldSQL);
                }
              if (Update(attInfo) == 1)
                {
                    GetCustomerAttributeList(attInfo.ExtAttributeId, true);
                    message = "成功更新客户属性";
                    result = true;

                    CommitTransaction();
                }
                else
                {
                    RollbackTransaction();
                    message = "更新产客户属性失败，请与管理员联系";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("更新客户属性异常", ex);
                throw ex;
            }

            return result;
        }


        public bool CreateCustomerAttribute(CustomerExtAttributesModel attInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";
            CustomerAttributeGroupInfoModel GroupInfoModel = null;
            GroupInfoModel = CustomerAttributeGroupInfoService.Instance.GetCustomerGroupFromDatabase(attInfo.GroupId);
            if (attInfo.FieldType.ToLower() == "string")
            {
                if (attInfo.FieldMinLength <= 1 || attInfo.FieldMaxLength <= 1 || attInfo.FieldMaxLength <= attInfo.FieldMinLength)
                {
                    message = "属性字段长度有误，请检查输入";
                    return false;
                }
            }
            if (CheckExistAttributename(attInfo.AttributeName))
            {
                message = string.Format("操作失败，已经存在名为【{0}】属性名称", attInfo.AttributeName);
                return false;
            }

            Dictionary<string, CustomerAttributeGroupInfoModel> dict = CustomerAttributeGroupInfoService.Instance.GetCustomeGroupInfoList(false);
            attInfo.SortOrder = (dict == null) ? 1 : dict.Count + 1;
            string TableName = "customer_info_" + CharacterUtil.ConvertToPinyin(GroupInfoModel.GroupName);

         

            #region 添加属性

            attInfo.ExtAttributeId = Guid.NewGuid().ToString();
            attInfo.SortOrder = (dict == null) ? 1 : dict.Count + 1;
            attInfo.NodeId = "0";
            attInfo.ParnetId = "0";

            FieldInfo fieldInfo = new FieldInfo();
            fieldInfo.FieldName = attInfo.AttributeName;

            if (attInfo.FieldType == "string" || attInfo.FieldType == "custom")
            {
                fieldInfo.FieldType = "varchar";
                fieldInfo.MinLength = Convert.ToInt32(attInfo.FieldMinLength);
                fieldInfo.MaxLength = Convert.ToInt32(attInfo.FieldMaxLength);
            }
            else
            {
                fieldInfo.FieldType = attInfo.FieldType;
                fieldInfo.MinLength = Convert.ToInt32(attInfo.FieldMinLength);
                fieldInfo.MaxLength = Convert.ToInt32(attInfo.FieldMaxLength);
            }

            if (fieldInfo.MinLength < 0 && fieldInfo.FieldType == "varchar")
            {
                fieldInfo.MinLength = 50;
            }

            if (fieldInfo.MaxLength < 0 && fieldInfo.FieldType == "varchar")
            {
                fieldInfo.MaxLength = 50;
            }

            if (attInfo.FieldType == "text")
            {
                fieldInfo.FieldType = "varchar";
                fieldInfo.MaxLength = -1;
            }

            fieldInfo.DefaultValue = attInfo.DefaultValue;
            fieldInfo.Description = attInfo.Description;
            string addFieldSQL = DTableUtil.GetAddFieldSQL(TableName, fieldInfo);
            #endregion

            try
            {
                BeginTransaction();
                if (Create(attInfo) == 1)
                {
                    ExecuteNonQuery(addFieldSQL);
                    message = "成功创建客户属性";
                    result = true;
                    GetCustomerAttributeList(attInfo.ExtAttributeId, true);
                    CommitTransaction();
                }
                else
                {
                    RollbackTransaction();
                    result = false;
                    message = "创建客户属性失败，请与管理员联系";
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建客户属性异常", ex);
                throw ex;
            }
        
            return result;
        }

        #region 查询客户扩展属性信息表
        /// <summary>
        /// </summary>
        /// <param name="queryCollection"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderField"></param>
        /// <param name="orderDirection"></param>
        /// <param name="total"></param>
        /// <returns></returns>

        public List<string> GetCustomerAttributesList(Dictionary<string, QueryItemDomainModel> queryCollection, int pageIndex, int pageSize, string orderField, string orderDirection, out int total)
        {
            total = 0;
            List<string> result = null;
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("FROM customer_ext_attributes WHERE 1=1 ");
            ParameterCollection pc = new ParameterCollection();
            int count = 0;

            #region 构造查询条件

            foreach (QueryItemDomainModel item in queryCollection.Values)
            {
                switch (item.Operation)
                {
                    case "equal":
                        sqlBuilder.AppendFormat(@" AND [{0}] = $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "notequal":
                        sqlBuilder.AppendFormat(@" AND [{0}] <> $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "contain":
                        sqlBuilder.AppendFormat(@" AND [{0}] LIKE $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), "%" + item.SearchValue + "%");
                        break;

                    case "greater":
                        sqlBuilder.AppendFormat(@" AND [{0}] > $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "greaterequal":
                        sqlBuilder.AppendFormat(@" AND [{0}] >= $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "less":
                        sqlBuilder.AppendFormat(@" AND [{0}] < $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "lessequal":
                        sqlBuilder.AppendFormat(@" AND [{0}] <= $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "between":
                        sqlBuilder.AppendFormat(@" AND [{0}] BETWEEN $begin{1}$ AND $end{1}$", item.FieldType, count);
                        pc.Add("begin" + count.ToString(), item.BeginTime);
                        pc.Add("end" + count.ToString(), item.EndTime);
                        break;

                    case "today":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(DAY,[{0}],GETDATE()) = 0", item.FieldType);
                        break;

                    case "week":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(WEEK,[{0}],GETDATE()) = 0", item.FieldType);
                        break;

                    case "month":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(MONTH,[{0}],GETDATE()) = 0", item.FieldType);
                        break;

                    case "quarter":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(QUARTER,[{0}],GETDATE()) = 0", item.FieldType);
                        break;

                    case "year":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(YEAR,[{0}],GETDATE()) = 0", item.FieldType);
                        break;

                    default:
                        break;
                }

                count++;
            }

            #endregion

            total = Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) " + sqlBuilder.ToString(), pc));

            DataTable dt = ExecuteDataTable("SELECT ext_attribute_id " + sqlBuilder.ToString(), pc, pageIndex, pageSize, OrderByCollection.Create(orderField, orderDirection));
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result.Add(dt.Rows[i][0].ToString());
                }
            }

            return result;
        }

        #endregion
        public Dictionary<string, CustomerExtAttributesModel> GetCustomerAttributeList(string extattributeid, bool clear)
        {
            if (string.IsNullOrEmpty(extattributeid))
                return null;

            Dictionary<string, CustomerExtAttributesModel> dict = null;
            string cacheKey = CacheKey.CUSTOMER_ATTRIBUTE_DICT.GetKeyDefine(extattributeid);
            dict = CacheUtil.Get<Dictionary<string, CustomerExtAttributesModel>>(cacheKey);

            if (dict == null || clear)
            {
                dict = GetProductCustomerAttributeListFromDatabase(extattributeid);
                if (dict != null)
                {
                    CacheUtil.Set(cacheKey, dict);
                }
                else
                {
                    CacheUtil.Remove(cacheKey);
                }
            }

            return dict;
        }


        /// <summary>
        /// 检查登名称是否存在。
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        private bool CheckExistAttributename(string name)
        {
            bool result = false;

            string sql = @"
SELECT 
    COUNT(1) 
FROM 
    customer_ext_attributes 
WHERE 
    attribute_name = $name$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("name", name);
            result = Convert.ToInt32(ExecuteScalar(sql, pc)) > 0;

            return result;
        }

        /// <summary>
        /// 根据ID删除客户属性信息。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool DeleteCustomerAttributeById(string extattributeid, out string message)
        {
            message = "操作失败，请与管理员联系";
            bool result = false;
            CustomerExtAttributesModel model = GetCustomerAttributeById(extattributeid, true);


            Dictionary<string, CustomerAttributeGroupInfoModel> dict = CustomerAttributeGroupInfoService.Instance.GetCustomeGroupInfoList(true);

            CustomerAttributeGroupInfoModel oldInfo = dict[model.GroupId];
            try
            {
                BeginTransaction();
                string TableName = "customer_info_" + CharacterUtil.ConvertToPinyin(oldInfo.GroupName);
                string deleteFieldSQL = DTableUtil.GetDeleteFieldSQL(TableName, model.AttributeName);

               ExecuteNonQuery(deleteFieldSQL);
                if (Delete(extattributeid) > 0)
                {
                    CommitTransaction();
                    result = true;
                    message = "成功删除客户属性信息";
                    GetCustomerAttributeList(extattributeid, true);
                }
                else
                {
                    RollbackTransaction();
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除删除客户属性信息异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 根据ID获取领域模型。
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public CustomerExtAttributesModel GetCustomerAttributeById(string Id, bool clear)
        {
            if (string.IsNullOrEmpty(Id))
                return null;

            string cacheKey = CacheKey.GetKeyDefine(CacheKey.CUSTOMER_ATTRIBUTE_DICT, Id);
            CustomerExtAttributesModel result = CacheUtil.Get<CustomerExtAttributesModel>(cacheKey);

            if (result == null || clear)
            {
                CustomerExtAttributesModel basicInfo = GettCustomerAttributeFromDatabase(Id);
                if (basicInfo != null)
                {
                    result = new CustomerExtAttributesModel();
                    result = basicInfo;
                    CacheUtil.Set(cacheKey, result);
                }
            }

            return result;

       
        }


        protected CustomerExtAttributesModel GettCustomerAttributeFromDatabase(string extattributeid)
        {
            CustomerExtAttributesModel model = null;

            string sql = @"
SELECT 
    * 
FROM 
	customer_ext_attributes
WHERE
	ext_attribute_id = $extattributeid$ 
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("extattributeid", extattributeid);

            DataTable dt = ExecuteDataTable(sql, pc);

            if (dt != null && dt.Rows.Count > 0)
            {

                model = new CustomerExtAttributesModel();
                ModelConvertFrom(model, dt, 0);
            }

            return model;
        }
        private Dictionary<string, CustomerExtAttributesModel> GetProductCustomerAttributeListFromDatabase(string extattributeid)
        {
            Dictionary<string, CustomerExtAttributesModel> dict = null;
            ParameterCollection pc = new ParameterCollection();
            pc.Add("ext_attribute_id", extattributeid);

            List<CustomerExtAttributesModel> list = RetrieveMultiple(pc, OrderByCollection.Create("sort_order", "asc"));
            if (list != null)
            {
                dict = new Dictionary<string, CustomerExtAttributesModel>();
                foreach (CustomerExtAttributesModel item in list)
                {
                    dict.Add(item.ExtAttributeId, item);
                }
            }

            return dict;
        }




        ///// <summary>
        ///// 获取客户扩展属性及分组列表。
        ///// </summary>
        ///// <returns></returns>
        ///// <remarks>
        ///// Dictionary<属性名称,分组名称>
        ///// </remarks>
        //public Dictionary<string, string> GetCustomerExtAttributeNameAndGroupList()
        //{
        //    return null;
        //}

        /// <summary>
        /// 获取列表字典。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public Dictionary<string, CustomerExtAttributesModel> GetCustomerAttributeList(bool clear)
        {
            string cacheKey = CacheKey.CUSTOMER_ATTRIBUTE_DICT;
            Dictionary<string, CustomerExtAttributesModel> dict = CacheUtil.Get<Dictionary<string, CustomerExtAttributesModel>>(cacheKey);
            if (dict == null || clear)
            {
                dict = GetCustometerAttriListFromDatabase();
                if (dict != null)
                {
                    CacheUtil.Set(cacheKey, dict);
                }
            }

            return dict;
        }

        public CustomerExtAttributesModel GetCustomerExtAttributeInfoModelByAttributeName(string attributeName, bool clear)
        {
            Dictionary<string, CustomerExtAttributesModel> dict = GetCustomerAttributeList(clear);
            if (dict != null)
            {
                foreach (CustomerExtAttributesModel item in dict.Values)
                {
                    if (item.AttributeName == attributeName)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

     

        /// <summary>
        /// 从数据库获取列表。
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, CustomerExtAttributesModel> GetCustometerAttriListFromDatabase()
        {
            Dictionary<string, CustomerExtAttributesModel> dict = null;
            List<CustomerExtAttributesModel> list = RetrieveMultiple(new ParameterCollection(), OrderByCollection.Create("created_on", "desc"));
            if (list != null)
            {
                dict = new Dictionary<string, CustomerExtAttributesModel>();
                foreach (CustomerExtAttributesModel item in list)
                {
                    dict[item.ExtAttributeId] = item;
                }
            }

            return dict;
        }




	}
}

