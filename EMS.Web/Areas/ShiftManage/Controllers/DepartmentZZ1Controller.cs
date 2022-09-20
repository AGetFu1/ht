﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using EMS.Application.ShiftManage;
using EMS.Code;
using EMS.Domain.Entity.ShiftManage;

namespace EMS.Web.Areas.ShiftManage.Controllers
{
    public class DepartmentZZ1Controller : ControllerBase
    {
        TeamManageApp teamManageApp = new TeamManageApp();
    
  
        public ActionResult GetGridJson()
        {
            var data = teamManageApp.GetList();
            return Content(data.ToJson());
        }

 

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(TeamEntity teamEntity, string keyValue)
        {
            teamManageApp.SubmitForm(teamEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = teamManageApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        public ActionResult Home1()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        //
        // POST: /ShiftManager/Calendar/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ShiftManager/Calendar/Delete/5
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult DeleteForm(string keyValue)
        {
            teamManageApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        //
        // POST: /ShiftManager/Calendar/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
