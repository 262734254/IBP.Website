using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    /// <summary>
    /// 工单类型信息领域模型。
    /// </summary>
    public class WorkOrderTypeDomainModel
    {
        /// <summary>
        /// 类型基本信息。
        /// </summary>
        public WorkorderTypeInfoModel TypeInfo { get; set; }

        /// <summary>
        /// 本类型开始状态。
        /// </summary>
        public WorkorderStatusInfoModel BeginStatusInfo { get; set; }

        /// <summary>
        /// 本类型结束状态。
        /// </summary>
        public WorkorderStatusInfoModel EndStatusInfo { get; set; }

        /// <summary>
        /// 待审批状态。
        /// </summary>
        public WorkorderStatusInfoModel ApprovalStatusInfo { get; set; }

        /// <summary>
        /// 待质检状态。
        /// </summary>
        public WorkorderStatusInfoModel QuilityCheckedStatusInfo { get; set; }

        /// <summary>
        /// 新建工单时默认的处理结果。
        /// </summary>
        public WorkorderResultInfoModel BeginResultInfo { get; set; }

        /// <summary>
        /// 本类型状态列表。
        /// </summary>
        public Dictionary<string, WorkorderStatusInfoModel> StatusList { get; set; }

        /// <summary>
        /// 本类型结果列表。
        /// </summary>
        public Dictionary<string, WorkorderResultInfoModel> ResultList { get; set; }
    }
}
