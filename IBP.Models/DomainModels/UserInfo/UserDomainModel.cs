using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    /// <summary>
    /// 用户信息领域模型。
    /// </summary>
    public class UserDomainModel
    {
        public string UserId
        {
            get
            {
                if (BasicInfo != null)
                {
                    return BasicInfo.UserId;
                }

                return null;
            }
        }

        public string WorkId
        {
            get
            {
                if (BasicInfo != null)
                {
                    return BasicInfo.WorkId.Replace("WORKID_", "");
                }

                return null;
            }
        }

        /// <summary>
        /// 用户名称和工号。
        /// </summary>
        public string NameAndWorkId
        {
            get
            {
                if (BasicInfo == null)
                    return null;

                return string.Format("{0}/({1})", BasicInfo.CnName, WorkId);
            }
        }

        /// <summary>
        /// 用户基本信息。
        /// </summary>
        public UserInfoModel BasicInfo { get; set; }

        public List<string> InGroupList = null;

        
    }
}
