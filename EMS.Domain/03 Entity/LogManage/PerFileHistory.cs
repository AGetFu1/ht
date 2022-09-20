using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Entity.LogManage
{
    public class PerFileHistory
    {
        public string BeforeValue {get;set;}
        public string AfterValue { get; set; }
        public string PCardNo { get; set; } 
        public string IPAddress { get; set; }
        public string ModifyField { get; set; }
        public string ModifyFieldName { get; set; }
}
}
