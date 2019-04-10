using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    public class WorkOrderDomainModel
    {
        /// <summary>
        /// 工单基本信息。
        /// </summary>
        public WorkorderInfoModel BasicInfo { get; set; }

        /// <summary>
        /// 工单处理记录。
        /// </summary>
        public Dictionary<string, WorkorderProcessInfoModel> ProcessList { get; set; }

        /// <summary>
        /// 工单质检记录。
        /// </summary>
        public Dictionary<string, WorkorderChecksInfoModel> CheckList { get; set; }
    }
}
