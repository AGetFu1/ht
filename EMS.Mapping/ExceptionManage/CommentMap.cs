using EMS.Domain.Entity.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Mapping.ExceptionManage
{
    public class CommentMap : EntityTypeConfiguration<CommentEntity>
    {
        public CommentMap()
        {
            this.ToTable("Comment");
            this.HasKey(t => t.F_Id);
        }
    }
}
