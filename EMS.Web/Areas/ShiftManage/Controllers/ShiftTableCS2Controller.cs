using EMS.Application.ShiftManage;
using EMS.Code;
using EMS.Domain.Entity.ShiftManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.Web.Areas.ShiftManage.Controllers
{
    public class ShiftTableCS2Controller : ControllerBase
    {
        ShiftTableApp shiftTableApp = new ShiftTableApp();
        //
        // GET: /ShiftManager/Calendar/Details/5

        
        public ActionResult GetGridJson(string val)
        {
            var data = shiftTableApp.getTeamJsoncs2(val);
            return Content(data.ToJson());
        }
        public ActionResult GetJsoncs2(Pagination pagination, string queryJson)
        {
            var data = shiftTableApp.getListJsoncs2( pagination,  queryJson);
            return Content(data.ToJson());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = shiftTableApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(ShiftTableEntity shiftTableEntity, string keyValue)
        {
            shiftTableApp.SubmitFormcs2(shiftTableEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult DeleteForm(string keyValue)
        {
            shiftTableApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
       
    }
}
