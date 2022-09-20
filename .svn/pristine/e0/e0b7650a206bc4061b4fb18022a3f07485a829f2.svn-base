using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain.Entity.LogManage;
using EMS.Domain.Entity.ShiftManage;

namespace EMS.Mapping.LoggerManage
{
    class SysLogMap : EntityTypeConfiguration<SysLogEntity>
    {
        public SysLogMap()
        {
            this.ToTable("Sys_Logger");
            this.HasKey(t => t.F_Id);
        }
    }
}
