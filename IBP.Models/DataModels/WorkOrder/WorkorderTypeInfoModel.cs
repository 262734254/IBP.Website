/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-5-12
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.Serialization;

using Framework.Common;
using Framework.DataAccess;
using Framework.Utilities;

using IBP.Common;

namespace IBP.Models
{
    /// <summary>
    /// WorkorderTypeInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "workorder_type_info")]
    public class WorkorderTypeInfoModel : BaseModel
    {
        private string _workorderTypeId = null;
        private string _parentId = null;
        private string _typeName = null;
        private string _description = null;
        private int? _workorderTypeStatus = null;
        private int? _sortOrder = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "workorder_type_id", PrimaryKey = true)]
        public string WorkorderTypeId
        {
            get { return _workorderTypeId; }
            set { _workorderTypeId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "parent_id")]
        public string ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        /// <summary>
        /// 工单状态名称 
        /// </summary>
        [TableMapping(FieldName = "type_name")]
        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        /// <summary>
        /// 描述 
        /// </summary>
        [TableMapping(FieldName = "description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 状态 
        /// </summary>
        [TableMapping(FieldName = "workorder_type_status")]
        public int? WorkorderTypeStatus
        {
            get { return _workorderTypeStatus; }
            set { _workorderTypeStatus = value; }
        }

        /// <summary>
        /// 排序索引 
        /// </summary>
        [TableMapping(FieldName = "sort_order")]
        public int? SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "created_on")]
        public DateTime? CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "created_by")]
        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "modified_on")]
        public DateTime? ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "modified_by")]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "status_code")]
        public int? StatusCode
        {
            get { return _statusCode; }
            set { _statusCode = value; }
        }

    }
}
