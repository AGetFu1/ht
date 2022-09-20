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
    public class FlowController : ControllerBase
    {
        //
        // GET: /ExceptionManage/Flow/
        SysFlowApp sysFlowApp = new SysFlowApp();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Save()
        {
            return View();
        }
        public ActionResult GetGridJson()
        {
            var data = sysFlowApp.GetList();
            return Content(data.ToJson());
        }

        //
        // GET: /ShiftManager/Calendar/Create

        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = sysFlowApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(string Datajson, string Title)
        {
            sysFlowApp.SubmitForm(Datajson,Title);
            return Success("操作成功。");
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult SearchTitle(string Title)
        {
            var title= sysFlowApp.SearchTitle(Title);
            return Content(title);
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult DeleteForm(string keyValue)
        {
            sysFlowApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
