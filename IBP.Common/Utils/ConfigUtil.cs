using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace IBP.Common
{
    public static class ConfigUtil
    {
        #region 数据库连接

        /// <summary>
        /// 获取全局配置数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetGlobalDBConnection()
        {
            return ConfigurationManager.ConnectionStrings["Global_DBConnection"].ConnectionString;
        }

        /// <summary>
        /// 获取业务平台数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetIBPDBConnection()
        {
            return ConfigurationManager.ConnectionStrings["IBP_DBConnection"].ConnectionString;
        }

        /// <summary>
        /// 获取IVR系统数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetIVRDBConnection()
        {
            return ConfigurationManager.ConnectionStrings["IVR_DBConnection"].ConnectionString;
        }

        #endregion


        /// <summary>
        /// 日志是否输出上下文信息
        /// </summary>
        /// <returns></returns>
        public static string OutLogContext()
        {
            return ConfigurationManager.AppSettings["LogContext"];
        }

        /// <summary>
        /// 日志是否输出上下文信息
        /// </summary>
        /// <returns></returns>
        public static string OutLogTrace()
        {
            return ConfigurationManager.AppSettings["LogTrace"];
        }

        /// <summary>
        /// 日志信息的内容，true：表示精简信息，false：表示所有信息
        /// </summary>
        /// <returns></returns>
        public static string GetSimpleTraceContent()
        {
            return ConfigurationManager.AppSettings["SimpleTraceContent"];
        }

        /// <summary>
        /// 用于日志过滤的正则表达式，用于记录指定客户端IP的访问所产生的日志，为空记录所有
        /// </summary>
        /// <returns></returns>
        public static string GetIPTraceFilterRegExp()
        {
            return ConfigurationManager.AppSettings["IPTraceFilterRegExp"];
        }

        /// <summary>
        /// 每页显示记录行数
        /// </summary>
        /// <returns></returns>
        public static int GetPageSize()
        {
            return int.Parse(ConfigurationManager.AppSettings["PageSize"]);
        }

        /// <summary>
        /// 用于日志过滤的正则表达式，用于记录指定用户名的访问所产生的日志，为空记录所有
        /// </summary>
        /// <returns></returns>
        public static string GetLoginNameTraceFilterRegExp()
        {
            return ConfigurationManager.AppSettings["LoginNameTraceFilterRegExp"];
        }


        public static string GetReleaseSetting()
        {
            return ConfigurationManager.AppSettings["ReleaseDetails"];
        }

        /// <summary>
        /// 获取用户尝试登录次数限制。
        /// </summary>
        /// <returns></returns>
        public static int GetLoginTryCountLimit()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["UserLoginTryCountLimit"]);
        }

        /// <summary>
        /// Excel模版保存路径
        /// </summary>
        /// <returns></returns>
        public static string GetExcelSavePath()
        {
            return ConfigurationManager.AppSettings["ExcelSavePath"];
        }

        /// <summary>
        /// Excel模版存在数据库中的访问路径
        /// </summary>
        /// <returns></returns>
        public static string GetExcelVisitPath()
        {
            return ConfigurationManager.AppSettings["ExcelVisitPath"];
        }

     

        /// <summary>
        /// 获得发送邮件名称
        /// </summary>
        /// <returns></returns>
        public static string GetSendEmailName()
        {
            return ConfigurationManager.AppSettings["EmailName"];
        }
        /// <summary>
        /// 获得发送邮件密码
        /// </summary>
        /// <returns></returns>
        public static string GetSendEmailPwd()
        {
            return ConfigurationManager.AppSettings["EmailPwd"];
        }
        /// <summary>
        /// 获得邮件服务器
        /// </summary>
        /// <returns></returns>
        public static string GetSendEmailServer()
        {
            return ConfigurationManager.AppSettings["MailServer"];
        }
        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        /// <returns></returns>
        public static int GetSendEmailPort()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"]);
        }

        /// <summary>
        /// 获得当前站点域名地址
        /// </summary>
        /// <returns></returns>
        public static string GetWebSiteAddress()
        {
            return ConfigurationManager.AppSettings["WebSite"];
        }

        /// <summary>
        /// 删除操作日志时传入的参数
        /// </summary>
        /// <returns></returns>
        public static int GetDeleteActionLogDayCount()
        {
            return int.Parse(ConfigurationManager.AppSettings["ActionLogCount"]);
        }


    }
}
