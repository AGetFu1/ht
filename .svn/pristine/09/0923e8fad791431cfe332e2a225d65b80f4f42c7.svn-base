using EMS.Application.ExceptionManage;
using EMS.Code;
using EMS.Domain.Entity.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.Web.Areas.ExceptionManage.Controllers
{

    public class CommentController : ControllerBase
    {
        private CommentApp service = new CommentApp();
        [HttpGet]
        public ActionResult GetList(String exceptionId) {
            var data= service.GetList(exceptionId);
            return Content(data.ToJson());
        }
        [HttpPost]
        public ActionResult Submit(CommentEntity commentEntity)
        {
            service.SubmitData(commentEntity);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string keyValue)
        {
            service.DeleteComment(keyValue);
            return Success("删除成功。");
        }
    }
}