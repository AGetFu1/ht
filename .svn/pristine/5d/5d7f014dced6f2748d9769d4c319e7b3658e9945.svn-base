using EMS.Application.ExceptionManage;
using EMS.Code;
using EMS.Domain.Entity.ExceptionManage;
using EMS.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace EMS.Web.Areas.ExceptionManage.Controllers
{
    public class ExceptionTypeController : ControllerBase
    {
        private SysExptItemApp itemsApp = new SysExptItemApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = itemsApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (SysExptItemEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_FullName;
                treeModel.parentId = item.F_ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            OperatorModel operatorModel = new OperatorModel();
            string departmentId = "";
            string ItemType = "";
            operatorModel = OperatorProvider.Provider.GetCurrent();
            if (operatorModel != null && !operatorModel.IsSystem) {
                departmentId=operatorModel.DepartmentId;
                //根据部门id去关联分类id
                ItemType= RelationItemType(departmentId);
            }
            
            var data = itemsApp.GetList(ItemType);
            var treeList = new List<TreeViewModel>();
            foreach (SysExptItemEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.F_FullName;
                tree.value = item.F_EnCode;
                tree.parentId = item.F_ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson()
        {
            var data = itemsApp.GetList();
            var treeList = new List<TreeGridModel>();
            foreach (SysExptItemEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = itemsApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysExptItemEntity itemsEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            itemsApp.SubmitForm(itemsEntity,keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            itemsApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        public string RelationItemType(string department) {
            return itemsApp.RelationItemType(department);  
        }

    }
}
