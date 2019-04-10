using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    public class WorkOrderTypeTreeDomainModel
    {
        public WorkorderTypeInfoModel BasicInfo { get; set; }

        public Dictionary<string, WorkOrderTypeTreeDomainModel> ChildTypeList { get; set; }
    }
}
