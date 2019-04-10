using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    /// <summary>
    /// 自动外呼任务运行结果领域模型。
    /// </summary>
    public class AutoDialerTaskResultDomainModel
    {
        /// <summary>
        /// 任务ID。
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// IVR系统任务ID。
        /// </summary>
        public int IVRProjectId { get; set; }

        /// <summary>
        /// 计划发起外呼号码总数。
        /// </summary>
        public int PlanningNumberTotal { get; set; }

        /// <summary>
        /// IVR系统剩余外呼号码总数。
        /// </summary>
        public int IVRSurplusNumberTotal { get; set; }

        /// <summary>
        /// 已发起外呼总数。
        /// </summary>
        public int OutDialerNumberTotal { get; set; }

        /// <summary>
        /// 外呼状态统计信息。
        /// </summary>
        public Dictionary<string, string> OutDialerStatusList { get; set; }

        /// <summary>
        /// 成功外呼返回代码统计信息。
        /// </summary>
        public Dictionary<string, string> ReturnCodeList { get; set; }
    }
}
