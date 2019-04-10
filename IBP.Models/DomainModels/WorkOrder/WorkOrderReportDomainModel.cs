using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models.DomainModels
{
    /// <summary>
    /// 工单报表领域模型。
    /// </summary>
    public class WorkOrderReportDomainModel
    {
        /// <summary>
        /// 工单总数。
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 按类型统计报表。
        /// </summary>
        public Dictionary<string, WorkOrderTypeReport> TypeReport { get; set; }

        /// <summary>
        /// 按级别统计报表。
        /// </summary>
        public Dictionary<string, WorkOrderLevelReport> LevelReport { get; set; }

        /// <summary>
        /// 按处理状态统计报表。
        /// </summary>
        public Dictionary<string,WorkOrderProcessStatusReport> ProcessStatusReport { get; set; }
    }

    /// <summary>
    /// 工单按类型统计报表项实体类。
    /// </summary>
    public class WorkOrderTypeReport
    {
        /// <summary>
        /// 类型ID。
        /// </summary>
        public string TypeId { get; set; }
        /// <summary>
        /// 类型名称。
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 本类型工单总数。
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 本类型工单按状态统计报表。
        /// </summary>
        public Dictionary<string,WorkOrderTypeStatusReport> StatusReport { get; set; }

        /// <summary>
        /// 本类型工单按处理结果统计报表。
        /// </summary>
        public Dictionary<string,WorkOrderTypeResultReport> ResultReport { get; set; }
    }

    /// <summary>
    /// 工单按类型状态统计报表实体类。
    /// </summary>
    public class WorkOrderTypeStatusReport
    {
        public string StatusId { get; set; }

        public string StatusName { get; set; }

        public int Total { get; set; }
    }

    /// <summary>
    /// 工单按类型处理结果统计报表实体类。
    /// </summary>
    public class WorkOrderTypeResultReport
    {
        public string ResultId { get; set; }

        public string ResultName { get; set; }

        public int Total { get; set; }
    }

    /// <summary>
    /// 工单按级别统计报表项实体类。
    /// </summary>
    public class WorkOrderLevelReport
    {
        /// <summary>
        /// 级别ID。
        /// </summary>
        public string LevelId { get; set; }

        /// <summary>
        /// 级别名称。
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// 本级别总数。
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 工单按处理状态统计报表项实体类。
    /// </summary>
    public class WorkOrderProcessStatusReport
    {
        /// <summary>
        /// 处理状态ID。
        /// </summary>
        public string ProcessStatusId { get; set; }

        /// <summary>
        /// 处理状态名称。
        /// </summary>
        public string ProcessStatusName { get; set; }

        /// <summary>
        /// 本处理状态工单总数。
        /// </summary>
        public int Total { get; set; }
    }
}
