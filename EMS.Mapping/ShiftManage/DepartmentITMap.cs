using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain._03_Entity.ShiftManage;
using EMS.Domain._03_Entity;
using EMS.Domain.Entity.ShiftManage;
using System.Data.Entity.ModelConfiguration;

namespace EMS.Mapping.ShiftManage
{
    public class DepartmentITMap : EntityTypeConfiguration<DepartmentITEntity>
    {
        public DepartmentITMap()
        {
            this.ToTable("Shift_TableIT");
            this.HasKey(t => t.F_Id);
        }
    }
   
}
