using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    /// <summary>
    /// 部门信息领域模型。
    /// </summary>
    public class DepartmentDomainModel
    {
        /// <summary>
        /// 部门ID。
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 上级部门ID。
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 部门名称。
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 成员数。
        /// </summary>
        public int MemberTotal { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 下级部门。
        /// </summary>
        public Dictionary<string, DepartmentDomainModel> ChildDepartment { get; set; }
    }
}
