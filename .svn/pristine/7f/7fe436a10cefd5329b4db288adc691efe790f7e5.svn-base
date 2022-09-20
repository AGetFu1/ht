using EMS.Code;
using EMS.Data.Extensions;
using EMS.Domain.Entity.LogManage;
using EMS.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static EMS.Domain.Entity.LogManage.OperateLog;

namespace EMS.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class OperationLogAttribute : ActionFilterAttribute
    {
        
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="operate">具体操作</param>
        public OperationLogAttribute(string operate, ImportantLevel level = ImportantLevel.一般操作, string description = null)
        {
            this.Operate = operate;
            Level = level;
            Description = description;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (OperatorProvider.Provider.GetCurrent() == null) return;

            OperateLog operateLog = new OperateLog();
            operateLog.Id = Guid.NewGuid();
            operateLog.OperateTime = DateTime.Now;
            operateLog.Operate = Operate;
            operateLog.Description = Description;
            operateLog.LevelEnum = Level;
            operateLog.OperatorId = OperatorProvider.Provider.GetCurrent().UserName;
            //DbHelper.Insert("");
            base.OnActionExecuted(filterContext);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string userName = OperatorProvider.Provider.GetCurrent().UserName;
            base.OnActionExecuting(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            
            filterContext.HttpContext.Response.Write("返回Result之前" + Message + "<br />");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            filterContext.HttpContext.Response.Write("返回Result之后" + Message + "<br />");
        }
        public string Message { get; set; }
        /// <summary>
        /// 操作员Id
        /// </summary>
        public Guid OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// 具体操作
        /// </summary>
        public string Operate { get; set; }
        /// <summary>
        /// 危险等级
        /// </summary>
        public ImportantLevel Level { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}