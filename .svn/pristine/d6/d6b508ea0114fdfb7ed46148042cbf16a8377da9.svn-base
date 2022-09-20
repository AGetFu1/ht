using EMS.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Application.LogManage;
using EMS.Domain.Entity.LogManage;

namespace EMS.Web.Areas.LogManager.Controllers
{
    public class SysLogController : ControllerBase
    {

        private  SysLogApp logApp = new SysLogApp();
         
        //
        // GET: /LogManager/SysLog/

       

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult getList(Pagination pagination, string queryJson) {
            var data = logApp.getList(pagination, queryJson);               
            //LogFactory.GetLogger(this.GetType().ToString()).Info("查询到的数据：" + data.ToJson());
            return Content(data.ToJson()); 
        }
        [OperationLog("删除日志", OperateLog.ImportantLevel.危险操作)]
        public ActionResult submitClearLog(string keepTime) {
            logApp.clearLog(keepTime);
            return Success("清空成功");
        }
        public ActionResult ClearLog()
        {
            return View();
        }
    }
        
    
}
