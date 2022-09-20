﻿using EMS.Application.ShiftManage;
using EMS.Code;
using EMS.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace EMS.Web.Areas.ShiftManage.Controllers
{
    public class ShiftShowController : ControllerBase
    {
        CalendarApp calendarApp = new CalendarApp();
        [HttpGet]
        public ActionResult GetWeekWorking()
        {

            DateTime dt = DateTime.Now;
            DateTime weekDate;
            DateTime preWeek;
            int today = (int)dt.DayOfWeek;
            List<CalendarModel> resultData;
            if (dt.DayOfWeek.ToString() != "Saturday")//也可以使用today!=0
            {
                weekDate = dt.AddDays(6-today).Date;
                preWeek = dt.AddDays(-today - 1).Date;
            }
            else
            {
                weekDate = dt.AddDays(0).Date;//若今天是周日，获取到的上周日的日期是本周周日的日期，所以要减去7天
                preWeek = dt.AddDays(-today-1).Date;
            }

            List<CalendarModel> holidays = calendarApp.getHolidayJson().OrderBy(t => t.F_Start).ToList();
            if (holidays != null && holidays.Count > 0)
            {
                resultData = holidays;
            }
            else {
                var data = calendarApp.getWeekJson(weekDate).OrderBy(t => t.F_Start).ToList();
                if (data == null)
                {
                    data = calendarApp.getWeekJson(preWeek).OrderBy(t => t.F_Start).ToList();
                }
                resultData = data;
            }
                       
            return Content(resultData.ToJson());
        }
        [HttpGet]
        public ActionResult GetShiftOrder() {
           var data=  calendarApp.GetShiftOrder();
            return Content(data.ToJson());
        }
    }
}
