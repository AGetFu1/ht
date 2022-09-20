using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain.Entity.LogManage;
namespace EMS.Mapping.LoggerManage
{
    class OperationLogMap : EntityTypeConfiguration<OperationLogEntity>
    {
        public OperationLogMap()
        {
            this.ToTable("operation_log");
            this.HasKey(t => t.F_Id);
        }
    }
}
