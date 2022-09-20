using EMS.Domain.Entity.ExceptionManage;
using EMS.Domain.IRepository.ExceptionManage;
using EMS.Repository.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.ExceptionManage
{
    public class CommentApp
    {
        private ICommentRepository service = new CommentRepository();

        public void SubmitData(CommentEntity commentEntity)
        {
            Int32 len = commentEntity.F_Content.ToString().Length;
            if (len > 1000)
            {
                throw new Exception("输入的字数不能超过1000个，请重新输入");
            }
            else {
                commentEntity.Create();
                service.Insert(commentEntity);
            }
        }

        public List<CommentEntity> GetList(String exceptionId)
        {
            if (String.IsNullOrEmpty(exceptionId)) {
                return null;
            }
           return service.IQueryable(t=>t.F_ExpId==exceptionId).ToList();
        }

        public void DeleteComment(string keyValue)
        {
            service.Delete(t=>t.F_Id==keyValue);
        }
    }
}
