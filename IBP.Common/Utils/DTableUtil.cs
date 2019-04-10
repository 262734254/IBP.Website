using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Common
{
    public static class DTableUtil
    {
        /// <summary>
        /// 获取数据表重命名脚本。
        /// </summary>
        /// <param name="oldTableName"></param>
        /// <param name="newTableName"></param>
        /// <returns></returns>
        public static string GetRenameTableSQL(string oldTableName, string newTableName)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"execute sp_rename '{0}','{1}';", oldTableName, newTableName);
            return sql.ToString();
        }

        /// <summary>
        /// 获取删除字段脚本。
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetDeleteFieldSQL(string tableName, string fieldName)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"ALTER TABLE [{0}] DROP COLUMN [{1}];", tableName, fieldName);
            return sql.ToString();
        }

        /// <summary>
        /// 获取重命名字段脚本。
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="oldFieldName"></param>
        /// <param name="newFieldName"></param>
        /// <returns></returns>
        public static string GetRenameFieldSQL(string tableName, string oldFieldName, string newFieldName)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"use ibp_db;EXECUTE sp_rename N'{0}.{1}', N'{2}', 'COLUMN';", tableName, oldFieldName, newFieldName);
            return sql.ToString();
        }

        /// <summary>
        /// 获取添加字段脚本。
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public static string GetAddFieldSQL(string tableName, FieldInfo fieldInfo)
        {
            StringBuilder sql = new StringBuilder();
            
            switch (fieldInfo.FieldType)
            {
                case "varchar":
                    if (fieldInfo.MaxLength == 0)
                    {
                        fieldInfo.MaxLength = 50;
                    }
                    sql.AppendFormat(@"ALTER TABLE [{0}] ADD [{1}] varchar({2}) NULL",
                        tableName,
                        fieldInfo.FieldName.ToLower(),
                        (fieldInfo.MaxLength == -1) ? "max" : fieldInfo.MaxLength.ToString());
                    break;

                case "int":
                    sql.AppendFormat(@"ALTER TABLE [{0}] ADD [{1}] int NULL",
                        tableName,
                        fieldInfo.FieldName.ToLower());
                    break;

                case "decimal":
                    sql.AppendFormat(@"ALTER TABLE [{0}] ADD [{1}] decimal({2}, {3}) NULL",
                        tableName,
                        fieldInfo.FieldName.ToLower(),
                        fieldInfo.MinLength,
                        fieldInfo.MaxLength);
                    break;

                case "datetime":
                    sql.AppendFormat(@"ALTER TABLE [{0}] ADD [{1}] datetime {2} NULL",
                        tableName,
                        fieldInfo.FieldName.ToLower(),
                        (string.IsNullOrEmpty(fieldInfo.DefaultValue)) ? "" : " DEFAULT GETDATE() ");
                    break;

                default:
                    break;
            }


            return sql.ToString();
        }

        /// <summary>
        /// 生成删除表脚本。
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetDropTableSQL(string tableName)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('{0}') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [{0}];", tableName);

            return sql.ToString();
        }

        /// <summary>
        /// 生成创建表脚本。
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldList"></param>
        /// <returns></returns>
        public static string GetCreateTableSQL(string tableName, List<FieldInfo> fieldList)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('{0}') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1) DROP TABLE [{0}];", tableName);
            sql.AppendLine("");
            sql.AppendFormat(@"CREATE TABLE [{0}] ( ", tableName);

            for (int i = 0; i < fieldList.Count; i++)
            {
                switch (fieldList[i].FieldType)
                {
                    case "varchar":
                        sql.AppendFormat(@"[{0}] varchar({1}) DEFAULT '{2}' {3} NULL",
                            fieldList[i].FieldName.ToLower(),
                            fieldList[i].MaxLength,
                            fieldList[i].DefaultValue,
                            ((fieldList[i].IsNull) ? "" : "NOT"));
                        break;

                    case "int":
                        sql.AppendFormat(@"[{0}] int DEFAULT {1} {2} NULL",
                            fieldList[i].FieldName.ToLower(),
                            fieldList[i].DefaultValue,
                            ((fieldList[i].IsNull) ? "" : "NOT"));
                        break;

                    case "decimal":
                        sql.AppendFormat(@"[{0}] decimal({1}, {2}) DEFAULT {3} {4} NULL",
                            fieldList[i].FieldName.ToLower(),
                            fieldList[i].MinLength,
                            fieldList[i].MaxLength,
                            fieldList[i].DefaultValue,
                            ((fieldList[i].IsNull) ? "" : "NOT"));
                        break;

                    case "datetime":
                        sql.AppendFormat(@"[{0}] datetime {1} {2} NULL",
                            fieldList[i].FieldName.ToLower(),
                            (string.IsNullOrEmpty(fieldList[i].DefaultValue)) ? "" : " DEFAULT GETDATE() ",
                            ((fieldList[i].IsNull) ? "" : "NOT"));
                        break;

                    default:
                        break;
                }

                

                if (i != fieldList.Count - 1)
                {
                    sql.Append(",");
                }

                sql.AppendLine("");
            }

            sql.Append(");");
            sql.AppendLine("");
            for (int i = 0; i < fieldList.Count; i++)
            {
                if (fieldList[i].IsPrimaryKey)
                {
                    sql.AppendFormat(@"ALTER TABLE [{0}] ADD CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ([{1}]);", tableName, fieldList[i].FieldName.ToLower());
                    sql.AppendLine("");
                }
            }

            return sql.ToString();
        }
    }

    public class FieldInfo
    {
        public FieldInfo()
        {
        }

        public bool IsPrimaryKey { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public bool IsNull { get; set; }
        public string DefaultValue { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public string Description { get; set; }
    }
}
