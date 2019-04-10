using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    public class RoleDomainModel
    {
        /// <summary>
        /// 角色ID。
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 角色名称。
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 中文名称。
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态。
        /// </summary>
        public int Status { get; set; }
    }
}
