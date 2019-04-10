using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Common
{
    // 所有的常量定义
    public class Consts
    {
        public const string IBP_DATA_MANAGER_CONTEXT_ID = "0";
        public const string IVR_DATA_MANAGER_CONTEXT_ID = "IVR_DATA_MANAGER_CONTEXT_ID";
        public const string SESSION_KEY = "1";
        public const string LOGIN_TRYCOUNT = "Login_TryCount";
        public const string LANGUAGE_CODE = "2";
        public const string AUTHORIZATION_CODE = "3";
        public const string PERMISSIONS = "4";
        public const string GLOBAL_DATA_MANAGER_CONTEXT_ID = "5";
        public const string ROLE_CACHE_KEY = "Role_Cache_Key";
        public const string CURRENCY_CACHE_KEY = "Currency_Cache_Key";
        public const string PERMISSION_ALL_CACHE = "Permission_All_Cache";
        public const string SYSTEM_SETTING_CACHE = "System_Setting_Cache";
        public const string SOFT_KEY_BOARD = "Soft_Key_Board";
        public const string USER_SESSION_ID_KEY = "User_Session_ID_Key ";
        public const string SUCCESSFULL = "SUCCESSED";
        public const string DOMAIN_NAME = "DomainName";
        public const string USER_COOKIE = "User_Cookies";

        #region 权限相关常数，与数据库设置保持一致

        /// <summary>
        /// 超级管理员角色的名称，不能修改。
        /// </summary>
        public const string SUPER_ADMIN_INFO = "超级管理员";



        /// <summary>
        /// 普通用户角色的名称，不能修改。
        /// </summary>
        public const string MEMBER_INFO = "普通用户";


        public const string LANGUAGE_CN = "zh-cn";
        public const string LANGUAGE_EN = "en";
        public const string LANGUAGE_TW = "zh-tw";


        #endregion

        internal const AttributeTargets VALID_TARGETS = AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Struct;


        public static string GetConstsString(string inputString)
        {
            return inputString;
        }
    }
}
