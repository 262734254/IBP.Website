using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    /// <summary>
    /// 自定义枚举信息领域模型。
    /// </summary>
    public class CustomDataDomainModel
    {
        /// <summary>
        /// 自定义枚举ID。
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        /// 自定义枚举名称。
        /// </summary>
        public string DataName { get; set; }

        /// <summary>
        /// 自定义枚举编码。
        /// </summary>
        public string DataCode { get; set; }

        /// <summary>
        /// 自定义枚举类型。
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 关联字段名称。
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 关联字符类型。
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// 最小长度。
        /// </summary>
        public int MinLength { get; set; }

        /// <summary>
        /// 最大长度。
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// 是否必填项。
        /// </summary>
        public bool Requested { get; set; }

        /// <summary>
        /// 排序索引。
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 状态。
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 枚举值列表。
        /// </summary>
        public Dictionary<string, CustomDataValueDomainModel> ValueList { get; set; }


        public string GetCustomDataValueByValueId(string valueId)
        {
            if (valueId == null)
                return null;

            CustomDataValueDomainModel model = GetCustomDataValueDomainByValueId(valueId);

            return (model != null) ? model.DataValue : null;
        }

        public string GetCustomDataValueByValueId(string valueId, string defaultValue)
        {
            if (valueId == null)
                return defaultValue;

            CustomDataValueDomainModel model = GetCustomDataValueDomainByValueId(valueId);

            return (model != null) ? model.DataValue : defaultValue;
        }


        public CustomDataValueDomainModel GetCustomDataValueDomainByValueId(string valueId)
        {
            if(string.IsNullOrEmpty(valueId))
                return null;

            return ValueList.ContainsKey(valueId) ? ValueList[valueId] : null;
        }

        public CustomDataValueDomainModel GetCustomDataValueDomainByDataValue(string dataValue)
        {
            CustomDataValueDomainModel model = null;

            foreach (CustomDataValueDomainModel item in ValueList.Values)
            {
                if (item.DataValue == dataValue)
                {
                    model = item;
                    break;
                }
            }

            return model;
        }

        public CustomDataValueDomainModel GetCustomDataValueDomainByDataCode(string dataValueCode)
        {
            CustomDataValueDomainModel model = null;

            foreach (CustomDataValueDomainModel item in ValueList.Values)
            {
                if (item.DataValueCode == dataValueCode)
                {
                    model = item;
                    break;
                }
            }

            return model;
        }
    }

    /// <summary>
    /// 自定义枚举值领域模型。
    /// </summary>
    public class CustomDataValueDomainModel
    {
        /// <summary>
        /// 主键ID。
        /// </summary>
        public string ValueId { get; set; }

        /// <summary>
        /// 所属自定义枚举ID。
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        /// 枚举值。
        /// </summary>
        public string DataValue { get; set; }

        /// <summary>
        /// 枚举值编码。
        /// </summary>
        public string DataValueCode { get; set; }

        /// <summary>
        /// 排序索引。
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 状态。
        /// </summary>
        public int Status { get; set; }
    }
}
