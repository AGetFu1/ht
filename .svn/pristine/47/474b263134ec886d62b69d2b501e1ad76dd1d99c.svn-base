using EMS.Application.LogManage;
using EMS.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Domain;
namespace EMS.Web.Areas.LogManager.Controllers
{
    public class OperationLogController : ControllerBase
    {
        private OperationLogApp logApp = new OperationLogApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult getList(Pagination pagination, string queryJson)
        {
            var data = logApp.getList(pagination, queryJson);

            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult submitClearLog(string keepTime)
        {
            logApp.clearLog(keepTime);
            return Success("清空成功");
        }
    }
}