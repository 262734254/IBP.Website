using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    /// <summary>
    /// 操作信息领域模型。
    /// </summary>
    public class ActionDomainModel
    {
        /// <summary>
        /// 操作ID。
        /// </summary>
        public string ActionId { get; set; }

        /// <summary>
        /// 节点ID。
        /// </summary>
        public string NodeId { get; set; }

        /// <summary>
        /// 父节点。
        /// </summary>
        public string ParentNode { get; set; }

        /// <summary>
        /// 操作名称。
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 操作类型，1菜单，2操作。
        /// </summary>
        public int ActionType { get; set; }

        /// <summary>
        /// 所属组。
        /// </summary>
        public string ActionGroup { get; set; }

        /// <summary>
        /// 显示名称。
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 控制器名称。
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 排序索引。
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 子菜单列表。
        /// </summary>
        public Dictionary<string, ActionDomainModel> ChildActionList { get; set; }

        /// <summary>
        /// 组成员列表。
        /// </summary>
        public Dictionary<string, ActionDomainModel> ActionGroupList { get; set; }
    }
}
