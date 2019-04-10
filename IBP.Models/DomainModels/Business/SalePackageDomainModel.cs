using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    public class SalePackageDomainModel
    {
        public SalesPackageInfoModel BasicInfo { get; set; }

        public Dictionary<string, ProductSalesGroupInfoModel> ProductCategoryList { get; set; }
    }
}
