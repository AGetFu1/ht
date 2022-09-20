using EMS.Domain.Entity.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Mapping.ExceptionManage
{
    class SysFlowMap : EntityTypeConfiguration<SysFlowEntity>
    {
        public SysFlowMap()
        {
            this.ToTable("Sys_Flow");
            this.HasKey(t => t.F_Id);
        }
    }
}
