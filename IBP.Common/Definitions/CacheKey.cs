using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Framework.Common;
using System.IO;
using System.Reflection;

namespace IBP.Common
{
    /// <summary>
    /// 本类主要保存 Memcache 中 Key 的定义
    /// </summary>
    public static class CacheKey
    {
        private static Dictionary<string, string> _cacheKeys = null;

        static CacheKey()
        {
            _cacheKeys = new Dictionary<string, string>();
            string reportPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\\"));

            int count = reportPath.LastIndexOf("\\TestResults\\");
            //次判断仅单元测试使用
            if (count > 0)
            {
                reportPath = reportPath.Substring(0, count) + "\\Evt.HRP.Web\\Resource\\CacheKey.xml";
            }
            else
            {
                reportPath += "\\Resource\\CacheKey.xml";
            }
            //reportPath += "\\Resource\\CacheKey.xml";
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(reportPath);
            XmlNodeList nodes = xdoc.DocumentElement.SelectNodes("CacheKey");

            foreach (XmlNode n1 in nodes)
            {
                string key = n1.Attributes["name"].Value.ToLower();
                string value = n1.Attributes["value"].Value;

                if (_cacheKeys.ContainsKey(key))
                {
                    throw new FaultException(string.Format("关键字{0}已经存在，请检查资源冲突。", key));
                }
                _cacheKeys[key] = value;
            }
        }

        private static string GetXmlResource()
        {

            string reportPath = Assembly.GetExecutingAssembly().CodeBase;
            int count = reportPath.LastIndexOf("///") + 3;
            reportPath = reportPath.Substring(count, reportPath.Length - count);
            count = reportPath.LastIndexOf("/TestResults/");
            reportPath = reportPath.Substring(0, count);
            return reportPath;
        }



        /// <summary>
        /// 组合变量和参数信息，返回完整的字符串； keyString 中的占位符必须与参数个数相同
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns>返回组合后的字符串</returns>
        public static string GetKeyDefine(this string keyString, params object[] parameters)
        {
            return string.Format(keyString, parameters).ToLower();
        }

        /// <summary>
        /// 从缓存中获取Cache key的实际值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetKeyValue(string key)
        {
            key = key.ToLower();
            return _cacheKeys[key];
        }

        #region 用户相关缓存KEY

        /// <summary>
        /// 用户登录名KEY。
        /// </summary>
        public static string USER_LOGIN_NAME_KEY
        {
            get { return GetKeyValue("ibp_user_login_name_key"); }
        }

        /// <summary>
        /// 用户登录名KEY。
        /// </summary>
        public static string USER_WORKID_KEY
        {
            get { return GetKeyValue("ibp_user_login_workid_key"); }
        }

        /// <summary>
        /// 用户领域模型缓存。
        /// </summary>
        public static string USERDOMAIN_INFO
        {
            get { return GetKeyValue("ibp_userdomain_info"); }
        }

        /// <summary>
        /// 用户组信息字典。
        /// </summary>
        public static string USERGROUP_DICT
        {
            get { return GetKeyValue("ibp_usergroup_dict"); }
        }

        #endregion

        #region 部门结构树

        /// <summary>
        /// 部门结构树。
        /// </summary>
        public static string DEPARTMENT_TREE
        {
            get { return GetKeyValue("ibp_department_tree"); }
        }

        #endregion

        #region 角色信息

        /// <summary>
        /// 角色信息字典。
        /// </summary>
        public static string ROLE_DICT
        {
            get { return GetKeyValue("ibp_role_dict"); }
        }

        /// <summary>
        /// 操作菜单结构树。
        /// </summary>
        public static string ACTION_TREE
        {
            get { return GetKeyValue("ibp_action_tree"); }
        }

        #endregion

        #region 系统相关

        /// <summary>
        /// 自定义枚举字典。
        /// </summary>
        public static string CUSTOM_DATA_DICT
        {
            get { return GetKeyValue("ibp_customdata_dict"); }
        }

        /// <summary>
        /// 自定义枚举值字典。
        /// </summary>
        public static string CUSTOM_DATA_INFO_BYID
        {
            get { return GetKeyValue("ibp_customdata_info_byid"); }
        }

        /// <summary>
        /// 归属地信息模型。
        /// </summary>
        public static string LOCATION_MODEL
        {
            get { return GetKeyValue("ibp_location_model"); }
        }

        /// <summary>
        /// 中国地域信息模型。
        /// </summary>
        public static string CHINA_INFO_MODEL
        {
            get { return GetKeyValue("ibp_chinainfo_model"); }
        }

        /// <summary>
        /// 用户组领域模型。
        /// </summary>
        public static string USERGROUP_DOMAINMODEL
        {
            get { return GetKeyValue("ibp_usergroup_domainmodel"); }
        }

        #endregion

        #region 工单相关

        /// <summary>
        /// 工单类型字典。
        /// </summary>
        public static string WORKORDER_TYPE_DICT
        {
            get { return GetKeyValue("ibp_workorder_type_dict"); }
        }

        /// <summary>
        /// 工单类型结构树。
        /// </summary>
        public static string WORKORDER_TYPE_TREE
        {
            get { return GetKeyValue("ibp_workorder_type_tree"); }
        }

        /// <summary>
        /// 工单类型信息领域模型。
        /// </summary>
        public static string WORKORDER_TYPE_DOMAINMODEL
        {
            get { return GetKeyValue("ibp_workorder_type_domainmodel"); }
        }

        /// <summary>
        /// 工单信息领域模型。
        /// </summary>
        public static string WORKORDER_DOMAINMODEL
        {
            get { return GetKeyValue("ibp_workorder_domainmodel"); }
        }

        /// <summary>
        /// 工单状态信息字典。
        /// </summary>
        public static string WORKORDER_STATUSNAME_DICT
        {
            get { return GetKeyValue("ibp_workorder_statusname_dict"); }
        }

        /// <summary>
        /// 工单结果信息字典。
        /// </summary>
        public static string WORKORDER_RESULTNAME_DICT
        {
            get { return GetKeyValue("ibp_workorder_resultname_dict"); }
        }

        /// <summary>
        /// 工单报表领域模型。
        /// </summary>
        public static string WORKORDER_REPORT_DOMAINMODEL
        {
            get { return GetKeyValue("ibp_workorder_report_domainmodel"); }
        }

        #endregion

        #region 产品相关

        /// <summary>
        /// 产品类别信息字典。
        /// </summary>
        public static string PRODUCT_CATEGORY_DICT
        {
            get { return GetKeyValue("ibp_product_category_dict"); }
        }

        /// <summary>
        /// 产品类别分组信息字典。
        /// </summary>
        public static string PRODUCT_CATEGORY_GROUP_DICT
        {
            get { return GetKeyValue("ibp_product_categorygroup_dict"); }
        }

        /// <summary>
        /// 产品类别分组信息列表。
        /// </summary>
        public static string PRODUCT_CATEGORYGROUP_LIST
        {
            get { return GetKeyValue("ibp_product_categorygroup_dict"); }
        }

        /// <summary>
        /// 产品类别销售状态字典。
        /// </summary>
        public static string PRODUCT_CATEGORY_SALESTATUS_DICT
        {
            get { return GetKeyValue("ibp_product_category_salestatus_dict"); }
        }

        /// <summary>
        /// 产品类别属性列表字典。
        /// </summary>
        public static string PRODUCT_CATEGORY_ATTRIBUTE_DICT
        {
            get { return GetKeyValue("ibp_product_category_attribute_dict"); }
        }

        /// <summary>
        /// 产品信息领域模型。
        /// </summary>
        public static string PRODUCT_DOMAIN_MODEL
        {
            get { return GetKeyValue("ibp_product_domainmodel"); }
        }

        #endregion

        #region 业务相关

        /// <summary>
        /// 营销项目领域模型。
        /// </summary>
        public static string SALE_PACKAGE_DOMAINMODEL
        {
            get { return GetKeyValue("ibp_salepackage_domainmodel"); }
        }

        #endregion

        #region 客户信息相关

        /// <summary>
        /// 客户信息领域模型。
        /// </summary>
        public static string CUSTOMER_DOMAINMODEL
        {
            get { return GetKeyValue("ibp_customer_domainmodel"); }
        }

        /// <summary>
        /// 客户属性列表字典。
        /// </summary>
        public static string CUSTOMER_ATTRIBUTE_DICT
        {
            get { return GetKeyValue("ibp_customer_attribute_dict"); }
        }

        /// <summary>
        /// 客户分组属性列表字典。
        /// </summary>
        public static string CUSTOMER_GroupInfo_DICT
        {
            get { return GetKeyValue("ibp_customer_GroupInfo_dict"); }
        }
        #endregion

        #region 销售订单相关

        /// <summary>
        /// 扣款POS机数据模型。
        /// </summary>
        public static string POS_MACHINE_DATAMODEL
        {
            get { return GetKeyValue("ibp_posmachine_info"); }
        }

        /// <summary>
        /// 扣款POS机字典。
        /// </summary>
        public static string POS_MACHINE_DICT
        {
            get { return GetKeyValue("ibp_posmachine_dict"); }
        }
        
        /// <summary>
        /// 订单信息领域模型。
        /// </summary>
        public static string SALESORDER_DOMAINMODEL
        {
            get { return GetKeyValue("ibp_salesorder_domainmodel"); }
        }


        /// <summary>
        /// 订单数量。
        /// </summary>
        public static string SALESORDER_TOTAL
        {
            get { return GetKeyValue("ibp_salesorder_total"); }
        }
        #endregion

        /// <summary>
        /// 分组信息字典。
        /// </summary>
        public static string GROUPINFO_DICT
        {
            get { return GetKeyValue("ibp_GroupInfo_dict"); }
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        public static string USER_INFO
        {
            get { return GetKeyValue("trw_user_info"); }
        }

        /// <summary>
        /// 角色信息
        /// </summary>
        public static string ROLE_INFO
        {
            get { return GetKeyValue("trw_role_info"); }
        }

        /// <summary>
        /// 角色的权限列表。
        /// </summary>
        public static string ROLE_PERMISSIONS
        {
            get { return GetKeyValue("ibp_role_permissions"); }
        }

        /// <summary>
        /// 用户组的权限列表。
        /// </summary>
        public static string USERGROUP_PERMISSIONS
        {
            get { return GetKeyValue("ibp_usergroup_permissions"); }
        }

       
        /// <summary>
        /// 省份信息
        /// </summary>
        public static string CHINA_PROVINCE_INFO
        {
            get { return GetKeyValue("trw_china_province_info"); }
        }

        /// <summary>
        /// 模块信息
        /// </summary>
        public static string MODULE_INFO
        {
            get { return GetKeyValue("trw_module_info_info"); }
        }

        /// <summary>
        ///产品型号信息
        /// </summary>
        public static string PRODUCT_TYPE_INFO
        {
            get { return GetKeyValue("trw_product_type_info"); }
        }


        /// <summary>
        /// 订单类型信息字典。
        /// </summary>
        public static string SALESORDER_TYPE_INFO
        {
            get { return GetKeyValue("ibp_salesorder_type_info"); }
        }
        /// <summary>
        /// 订单状态类型信息字典。
        /// </summary>
        public static string SALESORDER_TYPE_STATUS_INFO
        {
            get { return GetKeyValue("ibp_salesorder_type_status_info"); }
        }
    }
}