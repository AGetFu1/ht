using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Code;
using EMS.Application.ExceptionManage;
using EMS.Domain.Entity.ExceptionManage;
using EMS.Code.Web;
using EMS.Domain.Entity.SystemManage;

namespace EMS.Web.Areas.ExceptionManage.Controllers
{
    public class ExceptionPackageController : ControllerBase
    {

        private SysExptDetailApp itemsDetailApp = new SysExptDetailApp();
        private SysExptItemApp itemsApp = new SysExptItemApp();
        private Application.ExceptionManage.ESProvider eSProvider = new Application.ExceptionManage.ESProvider();
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult ContentDetails()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword, Pagination pagination)
        {
            //if (string.IsNullOrEmpty(itemId)) {
            //    SysExptItemEntity sysExptItemEntity= itemsApp.GetMenu2LevelList("0b77f101-8481-43d2-bfa2-9d43be0b6a17").ToArray().First();
            //}
            var data = new
            {
                rows = eSProvider.GetExptDetils(pagination, itemId, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
 
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string enCode)
        {
            var data = itemsDetailApp.GetItemList(enCode);
            List<object> list = new List<object>();
            foreach (SysExptEntity item in data)
            {
                list.Add(new { id = item.F_ItemCode, text = item.F_ItemName });
            }
            return Content(list.ToJson());
        }
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = itemsDetailApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SubmitForm(SysExptEntity itemsDetailEntity, string keyValue)
        {
            itemsDetailApp.SubmitForm(itemsDetailEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            itemsDetailApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

    }


}
