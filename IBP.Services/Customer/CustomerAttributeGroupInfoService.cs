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
using System.Data.OleDb;

namespace IBP.Services
{
	/// <summary>
	/// CustomeGroupInfo业务逻辑类
	/// </summary>
    public partial class CustomerAttributeGroupInfoService : BaseService
	{
		// 在此添加你的代码...


        /// <summary>
        /// 根据ID删除客户分组属性信息。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool DeleteGroupInfoById(string groupid, out string message)
        {
            message = "操作失败，请与管理员联系";
            bool result = false;
            Dictionary<string, CustomerAttributeGroupInfoModel> dict = GetCustomeGroupInfoList(true);

            CustomerAttributeGroupInfoModel oldInfo = dict[groupid];

            string TableName = "customer_info_" + CharacterUtil.ConvertToPinyin(oldInfo.GroupName);
            string deleteCustomerAttributesSQL = "DELETE FROM customer_ext_attributes WHERE group_id = $groupid$;";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("groupid", groupid);
            try
            {
                BeginTransaction();

                if (Delete(groupid) > 0)
                {
                    ExecuteNonQuery(deleteCustomerAttributesSQL, pc);
                    string dropTableSQL = DTableUtil.GetDropTableSQL(TableName);
                    ExecuteNonQuery(dropTableSQL);

                    CommitTransaction();
                    result = true;
                    message = "成功删除客户属性信息";
                    GetCustomeGroupInfoList(true);
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


        public bool UpdateGroupInfo(CustomerAttributeGroupInfoModel model, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";
            model.Tabname = "customer_info_" + CharacterUtil.ConvertToPinyin(model.GroupName);
            Dictionary<string, CustomerAttributeGroupInfoModel> dict = GetCustomeGroupInfoList(true);

            CustomerAttributeGroupInfoModel oldInfo = dict[model.GroupId];

            try
            {
                BeginTransaction();

                if (oldInfo.GroupName != model.GroupName)
                {
                    string NewsTableName = "customer_info_" + CharacterUtil.ConvertToPinyin(model.GroupName);
                    string TableName = "customer_info_" + CharacterUtil.ConvertToPinyin(oldInfo.GroupName);
                   string renSQL = DTableUtil.GetRenameTableSQL(TableName, NewsTableName);
                    ExecuteNonQuery(renSQL);
                }

                if (Update(model) == 1)
                {
                    GetGroupInfoById(model.GroupId, true);
                    message = "成功更新客户分组属性";
                    result = true;

                    CommitTransaction();
                }
                else
                {
                    RollbackTransaction();
                    message = "更新产客户分组属性失败，请与管理员联系";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("更新客户分组属性异常", ex);
                throw ex;
            }

            return result;
        }
        /// <summary>
        /// 检查登名称是否存在。
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        private bool CheckExistGroupname(string group_name)
        {
            bool result = false;

            string sql = @"
SELECT 
    COUNT(1) 
FROM 
    customer_attribute_group_info 
WHERE 
    group_name = $group_name$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("group_name", group_name);
            result = Convert.ToInt32(ExecuteScalar(sql, pc)) > 0;

            return result;
        }

        public bool CreateGroupInfo(CustomerAttributeGroupInfoModel model, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";
            model.GroupId = GetGuid();
            model.Tabname = "customer_info_" + CharacterUtil.ConvertToPinyin(model.GroupName);
            if (CheckExistGroupname(model.GroupName))
            {
                message = string.Format("操作失败，已经存在名为【{0}】属性名称", model.GroupName);
                return false;
            }

            string TableName = "customer_info_" + CharacterUtil.ConvertToPinyin(model.GroupName);

            #region 创建表字段

            List<FieldInfo> fieldList = new List<FieldInfo>();
            FieldInfo field = new FieldInfo();
            field.FieldName = "customer_id";
            field.FieldType = "varchar";
            field.MinLength = 50;
            field.MaxLength = 50;
            field.IsNull = false;
            field.IsPrimaryKey = true;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "created_on";
            field.FieldType = "datetime";
            field.IsNull = false;
            field.IsPrimaryKey = false;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "created_by";
            field.FieldType = "varchar";
            field.MinLength = 50;
            field.MaxLength = 50;
            field.IsNull = false;
            field.IsPrimaryKey = false;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "modified_on";
            field.FieldType = "datetime";
            field.IsNull = true;
            field.IsPrimaryKey = false;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "modified_by";
            field.FieldType = "varchar";
            field.MinLength = 50;
            field.MaxLength = 50;
            field.IsNull = true;
            field.IsPrimaryKey = false;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "status_code";
            field.FieldType = "int";
            field.IsNull = true;
            fieldList.Add(field);

            string createTableSql = DTableUtil.GetCreateTableSQL(TableName, fieldList);

            #endregion
      


            try
            {
                BeginTransaction();
                ExecuteNonQuery(createTableSql);
                if (Create(model) == 1)
                {

                    message = "成功创建分组属性";
                    result = true;
                    GetGroupInfoById(model.GroupId, true);
                    CommitTransaction();
                }
                else
                {
                    RollbackTransaction();
                    result = false;
                    message = "创建客户分组属性失败，请与管理员联系";
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建客户分组属性异常", ex);
                throw ex;
            }

            return result;
        }
        #region 查询客户分组属性信息表
        /// <summary>
        /// </summary>
        /// <param name="queryCollection"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderField"></param>
        /// <param name="orderDirection"></param>
        /// <param name="total"></param>
        /// <returns></returns>

        public List<string> GetGroupInfoList(Dictionary<string, QueryItemDomainModel> queryCollection, int pageIndex, int pageSize, string orderField, string orderDirection, out int total)
        {
            total = 0;
            List<string> result = null;
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("FROM customer_attribute_group_info WHERE 1=1 ");
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

            DataTable dt = ExecuteDataTable("SELECT group_id " + sqlBuilder.ToString(), pc, pageIndex, pageSize, OrderByCollection.Create(orderField, orderDirection));
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
        /// <summary>
        /// 根据ID获取领域模型。
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public CustomerAttributeGroupInfoModel GetGroupInfoById(string Id, bool clear)
        {
            if (string.IsNullOrEmpty(Id))
                return null;

            string cacheKey = CacheKey.GetKeyDefine(CacheKey.CUSTOMER_GroupInfo_DICT, Id);
            CustomerAttributeGroupInfoModel result = CacheUtil.Get<CustomerAttributeGroupInfoModel>(cacheKey);

            if (result == null || clear)
            {
                CustomerAttributeGroupInfoModel basicInfo = GetCustomerGroupFromDatabase(Id);
                if (basicInfo != null)
                {
                    result = new CustomerAttributeGroupInfoModel();
                    result = basicInfo;
                    CacheUtil.Set(cacheKey, result);
                }
            }

            return result;

       
        }


        public CustomerAttributeGroupInfoModel GetCustomerGroupFromDatabase(string group_id)
        {
            CustomerAttributeGroupInfoModel model = null;

            string sql = @"
SELECT 
    * 
FROM 
	customer_attribute_group_info
WHERE
	group_id = $group_id$ 
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("group_id", group_id);

            DataTable dt = ExecuteDataTable(sql, pc);

            if (dt != null && dt.Rows.Count > 0)
            {

                model = new CustomerAttributeGroupInfoModel();
                ModelConvertFrom(model, dt, 0);
            }

            return model;
        }
        /// <summary>
        /// 获取分组信息字典。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public Dictionary<string, CustomerAttributeGroupInfoModel> GetCustomeGroupInfoList(bool clear)
        {
            string cacheKey = CacheKey.CUSTOMER_GroupInfo_DICT;

            Dictionary<string, CustomerAttributeGroupInfoModel> dict = CacheUtil.Get<Dictionary<string, CustomerAttributeGroupInfoModel>>(cacheKey);

            if (dict == null || clear)
            {
                dict = GetGroupInfoListFromDatabse();
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
        /// 从数据库中获取分组信息。
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, CustomerAttributeGroupInfoModel> GetGroupInfoListFromDatabse()
        {
            Dictionary<string, CustomerAttributeGroupInfoModel> dict = null;

            string sql = @"
SELECT
	*
FROM 
	customer_attribute_group_info
";

            DataTable dt = ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                dict = new Dictionary<string, CustomerAttributeGroupInfoModel>();
                CustomerAttributeGroupInfoModel model = null;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    model = new CustomerAttributeGroupInfoModel();
                    model.GroupId = dt.Rows[i]["group_id"].ToString();
                    model.GroupName = dt.Rows[i]["group_name"].ToString();

                    model.Status = Convert.ToInt32(dt.Rows[i]["status"]);

                    dict.Add(model.GroupId, model);
                }
            }

            return dict;
        }


    
	}
	
}

