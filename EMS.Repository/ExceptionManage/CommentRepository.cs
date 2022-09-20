using EMS.Data;
using EMS.Domain.Entity.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain.IRepository.ExceptionManage;
namespace EMS.Repository.ExceptionManage
{
    public class CommentRepository : RepositoryBase<CommentEntity>, ICommentRepository
    {
    }
}
