using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    public class ProductInfoDomainModel
    {
        public ProductInfoModel BasicInfo { get; set; }

        public Dictionary<string, string> AttributeList { get; set; }
    }
}
