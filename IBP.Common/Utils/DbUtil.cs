using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;

using Framework.Common;
using Framework.DataAccess;

namespace IBP.Common
{
    /// <summary>
    /// 业务平台系统数据库信息
    /// </summary>
    public class IBPDBManager : DataManager
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
                return ConfigUtil.GetIBPDBConnection();
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
    /// 业务平台系统数据库信息
    /// </summary>
    public class IVRDBManager : DataManager
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
                return ConfigUtil.GetIVRDBConnection();
            }
        }

        /// <summary>
        /// 上下文唯一ID
        /// </summary>
        protected override string ContextID
        {
            get
            {
                return Consts.IVR_DATA_MANAGER_CONTEXT_ID;
            }
        }

        #endregion
    }



    /// <summary>
    /// 全局配置数据库信息
    /// </summary>
    public class GlobalDBManager : DataManager
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
        public  override string ConnectionString
        {
            get
            {
                return ConfigUtil.GetGlobalDBConnection();
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
    public static class DbUtil
    {
        static IBPDBManager _IBPDBManager = null;
        static GlobalDBManager _GlobalDBManager = null;
        static IVRDBManager _IVRDBManager = null;

        #region 构造函数
        static DbUtil()
        {
            _IBPDBManager = new IBPDBManager();
            _GlobalDBManager = new GlobalDBManager();
            _IVRDBManager = new IVRDBManager(); 
        }
        #endregion

        #region 属性

        public static DataManager Current
        {
            get
            {
                return _IBPDBManager;
            }
        }

        /// <summary>
        /// 业务平台数据库管理类
        /// </summary> 
        public static IBPDBManager IBPDBManager
        {
            get
            {
                return _IBPDBManager;
            }
        }

        /// <summary>
        /// IVR系统数据库管理类
        /// </summary>
        public static IVRDBManager IVRDBManager
        {
            get
            {
                return _IVRDBManager;
            }
        }

        /// <summary>
        /// 全局配置数据库管理类
        /// </summary> 
        public static GlobalDBManager GlobalDBManager
        {
            get
            {
                return _GlobalDBManager;
            }
        }
  
        #endregion
    }
}
