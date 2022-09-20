﻿using EMS.Application.ShiftManage;
using EMS.Code;
using EMS.Domain.Entity.ShiftManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.Web.Areas.ShiftManage.Controllers
{
    public class ShiftTableController : ControllerBase
    {
        ShiftTableApp shiftTableApp = new ShiftTableApp();
        //
        // GET: /ShiftManager/Calendar/Details/5

        
        public ActionResult GetGridJson(string val)
        {
            var data = shiftTableApp.getTeamJson(val);
            return Content(data.ToJson());
        }
        public ActionResult GetITJson(Pagination pagination, string queryJson)
        {
            var data = shiftTableApp.getListITJson( pagination,  queryJson);
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
            shiftTableApp.SubmitForm(shiftTableEntity, keyValue);
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
        //==============================================================================
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetJsoncs2(Pagination pagination, string queryJson)
        {
            var data = shiftTableApp.getListJsoncs2(pagination, queryJson);
            return Content(data.ToJson());
        }
        
        public ActionResult SubmitFormcs2(ShiftTableEntity shiftTableEntity, string keyValue)
        {
            shiftTableApp.SubmitFormcs2(shiftTableEntity, keyValue);
            return Success("操作成功。");
        }
        //==============================================================================
        public ActionResult Submitzz1Form(ShiftTableEntity shiftTableEntity, string keyValue)
        {
            shiftTableApp.Submitzz1Form(shiftTableEntity, keyValue);
            return Success("操作成功。");
        }
        public ActionResult GetListzz1Json(Pagination pagination, string queryJson)
        {
            var data = shiftTableApp.getListzz1Json(pagination, queryJson);
            return Content(data.ToJson());
        }
        public ActionResult GetGridzz1Json(string val)
        {
            var data = shiftTableApp.getTeamZZ1Json(val);

            return Content(data.ToJson());
        }
    }
}
