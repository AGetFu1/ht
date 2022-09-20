using EMS.Domain.Entity.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Mapping.ExceptionManage
{
    class SysExptItemMap : EntityTypeConfiguration<SysExptItemEntity>
    {
        public SysExptItemMap()
        {
            this.ToTable("Sys_Expt_Item");
            this.HasKey(t => t.F_Id);
        }
    }
}
