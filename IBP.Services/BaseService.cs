using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using IBP.Common;
using Framework.Common;
using System.Data;
using Framework.DataAccess;
using Framework.Utilities;

namespace IBP.Services
{
    /// <summary>
    /// 业务层基类。
    /// </summary>
    public class BaseService
    {
        public static DataSet ExecuteDataSet(string strSql)
        {
            Stopwatch sw = new Stopwatch();
            DataSet result = null;

            sw.Start();
            result = DbUtil.Current.IData.ExecuteDataSet(strSql);
            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

            if (ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);

            return result;
        }

        public static DataTable ExecuteDataTable(string strSql)
        {
            Stopwatch sw = new Stopwatch();
            DataTable result = null;

            sw.Start();
            result = DbUtil.Current.IData.ExecuteDataTable(strSql);
            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);
            
            if(ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);
            
            return result;
        }

        public static DataTable ExecuteDataTable(string strSql, ParameterCollection pc)
        {
            Stopwatch sw = new Stopwatch();
            DataTable result = null;

            sw.Start();

            result = DbUtil.Current.IData.ExecuteDataTable(strSql, pc);
            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

            if (ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);

            return result;
        }

        public static DataTable ExecuteDataTable(string strSql, ParameterCollection pc, OrderByCollection obc)
        {
            Stopwatch sw = new Stopwatch();
            DataTable result = null;

            sw.Start();
            strSql = string.Format("{0} ORDER BY {1} {2}", strSql, obc[0].FieldName, (obc[0].SortType == SortTypeEnum.Asc) ? "ASC" : "DESC");
            result = DbUtil.Current.IData.ExecuteDataTable(strSql, pc);
            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

            if (ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);

            return result;
        }

        public static DataTable ExecuteDataTable(string strSql, ParameterCollection pc, CommandTypeEnum cmdType)
        {
            Stopwatch sw = new Stopwatch();
            DataTable result = null;

            sw.Start();

            result = DbUtil.Current.IData.ExecuteDataTable(strSql, cmdType, pc);
            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

            if (ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);
            

            return result;
        }

        public static DataTable ExecuteDataTable(string strSql, int pageIndex, int pageSize)
        {
            Stopwatch sw = new Stopwatch();
            DataTable result = null;

            sw.Start();
            result = DbUtil.Current.IData.ExecuteDataTable(strSql, pageIndex, pageSize);
            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

            if (ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);
            

            return result;
        }

        public static DataTable ExecuteDataTable(string strSql, int pageIndex, int pageSize, OrderByCollection obc)
        {
            strSql = strSql + SqlBuilder.GetOrderBySql(obc);
            return ExecuteDataTable(strSql, pageIndex, pageSize);
        }

        public static DataTable ExecuteDataTable(string strSql, ParameterCollection pc, int pageIndex, int pageSize, OrderByCollection obc)
        {
            strSql = strSql + SqlBuilder.GetOrderBySql(obc);
            return ExecuteDataTable(strSql, pc, pageIndex, pageSize);
        }


        public static DataTable ExecuteDataTable(string strSql, ParameterCollection pc, int pageIndex, int pageSize)
        {
            Stopwatch sw = new Stopwatch();
            DataTable result = null;

            sw.Start();
            result = DbUtil.Current.IData.ExecuteDataTable(strSql, pc, pageIndex, pageSize);
            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

            if (ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);
            

            return result;
        }


        /// <summary>
        /// 执行SQl命令
        /// </summary>
        /// <param name="strSql"></param>
        public static int ExecuteNonQuery(string strSql)
        {
            Stopwatch sw = new Stopwatch();
            int result = 0;

            sw.Start();
            result = DbUtil.Current.IData.ExecuteNonQuery(strSql);

            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

            if (ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);
            

            return result;
        }

        /// <summary>
        /// 返回执行结果。
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string strSql)
        {
            Stopwatch sw = new Stopwatch();
            object result = 0;

            sw.Start();
            result = DbUtil.Current.IData.ExecuteScalar(strSql);

            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

            if (ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);
            

            return result;
        }

        /// <summary>
        /// 返回执行结果。
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string strSql, ParameterCollection pc)
        {
            Stopwatch sw = new Stopwatch();
            object result = 0;

            sw.Start();
            result = DbUtil.Current.IData.ExecuteScalar(strSql, pc);

            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

            if (ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);
            

            return result;
        }

        /// <summary>
        /// 返回执行结果。
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string strSql, CommandTypeEnum sqlType, ParameterCollection pc)
        {
            Stopwatch sw = new Stopwatch();
            object result = 0;

            sw.Start();
            result = DbUtil.Current.IData.ExecuteScalar(strSql, sqlType, pc);

            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

            if (ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);
            

            return result;
        }

        /// <summary>
        /// 执行带参数的SQL命令
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="pc"></param>
        public static int ExecuteNonQuery(string strSql, ParameterCollection pc)
        {
            Stopwatch sw = new Stopwatch();
            int result = 0;

            sw.Start();
            result = DbUtil.Current.IData.ExecuteNonQuery(strSql, pc);
            sw.Stop();
            LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

            if (ConfigUtil.GetSimpleTraceContent() == "false")
                LogUtil.Debug(strSql);
            

            return result;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="sqlType"></param>
        /// <param name="pc"></param>
        public static bool ExecuteNonQuery(string strSql, CommandTypeEnum sqlType, ParameterCollection pc)
        {
            Stopwatch sw = new Stopwatch();
            try
            {
                sw.Start();
                DbUtil.Current.IData.ExecuteNonQuery(strSql, sqlType, pc);
                sw.Stop();
                LogUtil.Debug("数据库操作执行完毕，共耗时(毫秒)：" + sw.ElapsedMilliseconds);

                if (ConfigUtil.GetSimpleTraceContent() == "false")
                    LogUtil.Debug(strSql);

                return true;
            }
            catch (System.Exception e)
            {
                LogUtil.Error(e.Message);
                return false;
            }
        }

        /// <summary>
        /// 启动事务。
        /// </summary>
        protected void BeginTransaction()
        {
            DbUtil.Current.BeginTransaction();
        }

        /// <summary>
        /// 提交事务。
        /// </summary>
        protected void CommitTransaction()
        {
            DbUtil.Current.CommitTransaction();
        }

        /// <summary>
        /// 回滚事务。
        /// </summary>
        protected void RollbackTransaction()
        {
            DbUtil.Current.RollbackTransaction();
        }

        protected string GetGuid()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        protected static void ModelConvertFrom(Model model, DataTable dataTable, int index)
        {
            DbUtil.Current.ConvertFrom(model, dataTable, index);
        }

        protected static void ModelConvertFrom(Model model, DataRow row)
        {
            DbUtil.Current.ConvertFrom(model, row);
        }

        protected static List<string> ModelConvertFrom(DataTable dataTable)
        {
            List<string> list = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                list = new List<string>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    list.Add(dataTable.Rows[i][0].ToString());
                }
            }

            return list;
        }

        protected static List<T> ModelConvertFrom<T>(DataTable dataTable)
        {
            List<T> list = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                list = new List<T>();
                T model = default(T);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    model = System.Activator.CreateInstance<T>();
                    DbUtil.Current.ConvertFrom(model, dataTable, i);
                    list.Add(model);
                }
            }

            return list;
        }

        protected static Dictionary<string, T> ModelConvertFrom<T>(DataTable dataTable, string keyFieldName)
        {
            Dictionary<string, T> list = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                list = new Dictionary<string, T>();
                T model = default(T);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    model = System.Activator.CreateInstance<T>();
                    DbUtil.Current.ConvertFrom(model, dataTable, i);
                    list[dataTable.Rows[i][keyFieldName].ToString()] = model;
                }
            }

            return list;
        }
    }
}
