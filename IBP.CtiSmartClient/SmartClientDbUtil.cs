using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.DataAccess;
using Framework.Common;

namespace IBP.CtiSmartClient
{
    /// <summary>
    /// 业务平台系统数据库信息
    /// </summary>
    public class TFS_DBManager : DataManager
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
                return "Data Source=192.168.3.150;Initial Catalog=TF_CMS;Persist Security Info=True;User ID=sa;Password=sa";
                //return "Provider=SQLOLEDB.1;Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=TF_CMS;Data Source=192.168.3.150";
            }
        }

        /// <summary>
        /// 上下文唯一ID
        /// </summary>
        protected override string ContextID
        {
            get
            {
                return "IBP.SMART.CTI.CLIENT";
            }
        }

        #endregion
    }

    public static class SmartClientDbUtil
    {
        static TFS_DBManager tfs = null;

        static SmartClientDbUtil()
        {
            tfs = new TFS_DBManager();
        }

        public static DataManager Current
        {
            get
            {
                return tfs;
            }
        }
    }
}
