using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.DataAccess;
using Framework.Common;
using IBP.Common;
using System.Configuration;

namespace IBP.DataImporter
{
    /// <summary>
    /// 业务平台系统数据库信息
    /// </summary>
    public class NewDBManager : DataManager
    {
        #region 属性
        /// <summary>
        /// 当前使用的数据库类型
        /// </summary>
        protected override DatabaseTypeEnum DatabaseType
        {
            get
            {
                return DatabaseTypeEnum.SqlServer;
            }
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public override string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["IBP_DBConnection"].ConnectionString;
            }
        }

        /// <summary>
        /// 上下文唯一ID
        /// </summary>
        protected override string ContextID
        {
            get
            {
                return Consts.IBP_DATA_MANAGER_CONTEXT_ID;
            }
        }

        #endregion
    }

    /// <summary>
    /// 全局配置数据库信息
    /// </summary>
    public class OldDBManager : DataManager
    {
        #region  属性
        /// <summary>
        /// 数据库类型
        /// </summary>
        protected override DatabaseTypeEnum DatabaseType
        {
            get
            {
                return DatabaseTypeEnum.SqlServer;
            }
        }

        /// <summary>
        /// 数据库连接串
        /// </summary>
        public override string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["TXMS_DBConnection"].ConnectionString;
            }
        }

        /// <summary>
        /// 上下文ID
        /// </summary>
        protected override string ContextID
        {
            get
            {
                return Consts.GLOBAL_DATA_MANAGER_CONTEXT_ID;
            }
        }
        #endregion
    }


    /// <summary>
    /// 封装数据管理类为单态模式
    /// </summary>
    public static class NewDbHelper
    {
        static NewDBManager _NewDBManager = null;
        static OldDBManager _OldDBManager = null;

        #region 构造函数
        static NewDbHelper()
        {
            _NewDBManager = new NewDBManager();
            _OldDBManager = new OldDBManager();
        }
        #endregion

        #region 属性

        public static DataManager NewDB
        {
            get
            {
                return _NewDBManager;
            }
        }

        public static DataManager OldDB
        {
            get
            {
                return _OldDBManager;
            }
        }

        ///// <summary>
        ///// 业务平台数据库管理类
        ///// </summary> 
        //public static NewDBManager IBPDBManager
        //{
        //    get
        //    {
        //        return _NewDBManager;
        //    }
        //}



        ///// <summary>
        ///// 全局配置数据库管理类
        ///// </summary> 
        //public static OldDBManager OldDBManager
        //{
        //    get
        //    {
        //        return _OldDBManager;
        //    }
        //}

        #endregion
    }
}
