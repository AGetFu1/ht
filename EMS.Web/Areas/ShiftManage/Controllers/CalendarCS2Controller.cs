using EMS.Application.ShiftManage;
using EMS.Code;
using EMS.Code.Excel;
using EMS.Domain.Entity.ShiftManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Text;
//using EMS.Domain._04_IRepository.ShiftManage;
//using EMS.Domain.ViewModel;
//using EMS.Application;

namespace EMS.Web.Areas.ShiftManage.Controllers
{
    public class CalendarCS2Controller : ControllerBase
    {
        CalendarApp calendarApp = new CalendarApp();

        [HttpGet]
       
        public ActionResult GetJson()
        {
            var data = calendarApp.getcs2ListJson();
            return Content(data.ToJson());
        }
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {            
            var data = new
            {
                rows = calendarApp.getListJsoncs2(pagination,  queryJson).OrderByDescending(t => t.F_Start).ToList(),//数据
                total = pagination.total,//总页数
                page = pagination.page,//当前页数
                records = pagination.records//数据总条数
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(CalendarEntity calendarEntity, string keyValue)
        {            
            string[] Array = calendarEntity.F_Start.Split(',',' ');//将获取到的日期字符串按逗号断开并存入数组,并去除空格
            //var de = calendarEntity.F_Start.ToDate();
            //DateTime beginTime = Convert.ToDateTime(d); //转为时间格式
            
            for (int i = 0; i <Array.Length ; i++)
            {
                if (!Array[i].IsEmpty())//去掉空数组
                {
                    calendarEntity.F_Start = Array[i];
                    calendarApp.SubmitFormcs2(calendarEntity, keyValue);
                }
                
            }
            return Success("操作成功。");
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = calendarApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        public ActionResult Edit(int id)
        {
            return View();
        }
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
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult DeleteForm(string keyValue)
        {
            calendarApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
