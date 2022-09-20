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
    public class CalendarController : ControllerBase
    {
        CalendarApp calendarApp = new CalendarApp();

        [HttpGet]
        public virtual ActionResult UploadFile()
        {
            return View();
        }

        // GET: /ShiftManager/Calendar/Details/5
        public ActionResult GetJson()
        {
            var data = calendarApp.getITListJson();
            return Content(data.ToJson());
        }
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            //var data = calendarApp.getList1Json( pagination, queryJson);
            var data = new
            {
                rows = calendarApp.getListJson(pagination,  queryJson).OrderByDescending(t => t.F_Start).ToList(),//数据
                total = pagination.total,//总页数
                page = pagination.page,//当前页数
                records = pagination.records//数据总条数
            };
            return Content(data.ToJson());
        }

        //
        // GET: /ShiftManager/Calendar/Create

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(CalendarEntity calendarEntity, string keyValue)
        {
            calendarApp.SubmitForm(calendarEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = calendarApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        //
        // GET: /ShiftManager/Calendar/Edit/5

        public ActionResult Edit(int id)
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
            calendarApp.DeleteForm(keyValue);
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
        public ActionResult UploadFileJson()
        {
            var msg = "";
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase file1 = files["file-0"];
            if (file1 != null)
            {
                string fileType = Path.GetExtension(file1.FileName);
                string fileName = Path.GetFileNameWithoutExtension(file1.FileName);
                const string directory = "~/upload/file";
                var filePath = Server.MapPath(directory + fileName + fileType);
                if (fileType.ToLower() != ".xlsx")
                {
                    //"文件格式不正确!!!"
                    msg += "文件格式不正确";

                }
                else
                {
                    if (!Directory.Exists(Server.MapPath(directory)))
                        Directory.CreateDirectory(Server.MapPath(directory));
                    file1.SaveAs(filePath);
                    DataTable dt = new NPOIExcel(filePath).ExcelToDataTable(filePath, true);

                    var insertNum = calendarApp.importProgram(dt);
                    //删除文件
                    FileInfo file = new FileInfo(filePath);//指定文件路径
                    if (file.Exists)//判断文件是否存在
                    {
                        file.Attributes = FileAttributes.Normal;//将文件属性设置为普通,比方说只读文件设置为普通
                        file.Delete();//删除文件
                    }

                }

            }
            return Success("插入成功");
        }
        [HttpGet]
        public ActionResult Shift()
        {
            return View();
        }
        [HttpPost]
        public List<string> getinWeekModel(CalendarEntity calendarEntity)
        {
            return calendarApp.inWeekitModel(calendarEntity);
        }
        [HttpPost]
       
        public ActionResult Scheduling(CalendarEntity calendarEntity, string keyValue)
        {

            string tmbegin = calendarEntity.F_Start;//开始时间
            string tmend = calendarEntity.F_End;    //结束时间
            DateTime beginTime = Convert.ToDateTime(tmbegin); //转为时间格式
            DateTime endTime = Convert.ToDateTime(tmend);
            TimeSpan ts = endTime.Date - beginTime.Date;
            
            List<string> workArray = new List<string>();//存放输入的工作日期
            if (calendarEntity.F_Title == "周内值班")
            {
                for (int i = 0; i < ts.Days; i++)//将输入日期中所有工作日存放在数组中
                {
                    DateTime StartTime = beginTime.Date.AddDays(i);
                    if (StartTime.DayOfWeek != DayOfWeek.Saturday && StartTime.DayOfWeek != DayOfWeek.Sunday)
                    {
                        workArray.Add(StartTime.ToString("yyyy-MM-dd"));
                    }

                }
                //List<string> arrayds = shiftTableApp.getTeamJson(calendarEntity);
                List<string> arrayds = calendarApp.inWeekitModel(calendarEntity);//获取周内排班人员存入数组
                int n = arrayds.IndexOf(calendarEntity.F_Text);//获取首日值班人员索引号
                for (int i = 0; i < workArray.Count; i++)//遍历workArray数组
                {
                    for (int j = n; j < (workArray.Count + arrayds.Count); j++)//遍历值班人员数组，与值班日期相匹配
                    {
                        if (j == arrayds.Count)
                        {
                            n = 0; j = 0;
                        }
                        calendarEntity.F_Start = workArray[i];
                        calendarEntity.F_Text = arrayds[j];
                        calendarEntity.F_Department = "IT部门";
                        calendarApp.SubmitForm(calendarEntity, keyValue); n++;
                        break;
                    }
                }
                
            }
            else if (calendarEntity.F_Title=="周末值班")           
            {
                for (int i = 0; i < ts.Days; i++)//将输入日期中所有工作日存放在数组中
                {
                    DateTime StartTime = beginTime.Date.AddDays(i);
                    if (StartTime.DayOfWeek == DayOfWeek.Saturday || StartTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        workArray.Add(StartTime.ToString("yyyy-MM-dd"));
                    }

                }
                List<string> arrayds = calendarApp.outWeekitModel(calendarEntity);//获取周末排班人员存入数组
                int n = arrayds.IndexOf(calendarEntity.F_Text);//获取首日值班人员索引号
                for (int i = 0; i < workArray.Count; i++)//遍历workArray数组
                {
                    for (int j = n; j < (workArray.Count + arrayds.Count); j++)
                    {
                        if (j == arrayds.Count)
                        {
                            n = 0; j = 0;
                        }
                        calendarEntity.F_Start = workArray[i];
                        calendarEntity.F_Text = arrayds[j];
                        calendarEntity.F_Department = "IT部门";
                        calendarApp.SubmitForm(calendarEntity, keyValue); n++;
                        break;
                    }
                }
                
            }
            return Success("操作成功。");
        }

    }
}
