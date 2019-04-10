using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    /// <summary>
    /// 客户敏感信息领域模型。
    /// </summary>
    public class CustomerSecurityInfoDomainModel
    {
        /// <summary>
        /// 客户ID。
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 数据标识（yyyyMMddHHmmssfff）
        /// </summary>
        public string DataTag { get; set; }

        /// <summary>
        /// 持卡号码。
        /// </summary>
        public string CreditCardNumber { get; set; }

        /// <summary>
        /// 编码后持卡号码。
        /// </summary>
        public string CreditCardNumber_Base64 { get; set; }


        /// <summary>
        /// 来电号码。
        /// </summary>
        public string IncomeCallNumber { get; set; }

        /// <summary>
        /// 编码后来电号码。
        /// </summary>
        public string IncomeCallNumber_Base64 { get; set; }

        /// <summary>
        /// 应答号码。
        /// </summary>
        public string AnswerCallNumber { get; set; }

        /// <summary>
        /// 编码后应答号码。
        /// </summary>
        public string AnswerCallNumber_Base64 { get; set; }

        /// <summary>
        /// 安全码。
        /// </summary>
        public string SecurityCode { get; set; }

        /// <summary>
        /// 编码后安全码。
        /// </summary>
        public string SecurityCode_Base64 { get; set; }

        /// <summary>
        /// 有效期。
        /// </summary>
        public string PeriodCode { get; set; }

        /// <summary>
        /// 编码后有效期。
        /// </summary>
        public string PeriodCode_Base64 { get; set; }

        /// <summary>
        /// 证件号码。
        /// </summary>
        public string IdCardNumber { get; set; }

        /// <summary>
        /// 编码后证件号码。
        /// </summary>
        public string IdCardNumber_Base64 { get; set; }

        /// <summary>
        /// 操作员工号。
        /// </summary>
        public string OperatorCode { get; set; }

        /// <summary>
        /// 编码后操作员工号。
        /// </summary>
        public string OperatorCode_Base64 { get; set; }
    }
}
