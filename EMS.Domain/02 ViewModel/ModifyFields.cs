using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.ViewModel
{
    public class ModifyFields : Attribute
    {
        public ModifyFields() {}
          public ModifyFields(string name) {
             this.FieldsName = name;
          }
        /// <summary>
        /// 修改的字段中文名        
       /// </summary>
         public string FieldsName { get;   set; }
    }
}
