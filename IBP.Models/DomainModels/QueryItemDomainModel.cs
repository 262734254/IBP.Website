using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    public class QueryItemDomainModel
    {
        public string FieldType { get; set; }

        public string Operation { get; set; }

        public string SearchValue { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
