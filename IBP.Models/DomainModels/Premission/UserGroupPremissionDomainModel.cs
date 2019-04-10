using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    public class UserGroupPremissionDomainModel
    {
        public UserGroupInfoModel UserGroupInfo { get; set; }

        public Dictionary<string, RelUsergroupPremissionModel> WorkOrderPremissionList { get; set; }
    }
}
