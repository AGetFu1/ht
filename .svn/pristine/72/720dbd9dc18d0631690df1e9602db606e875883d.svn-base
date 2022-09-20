using EMS.Application.ExceptionManage;
using EMS.Application.LogManage;
using EMS.Application.ShiftManage;
using EMS.Code;
using EMS.Domain.Entity.ExceptionManage;
using EMS.Domain.ViewModel;
using System.Collections.Generic;
using System.Web.Http;

namespace EMS.Web.Api
{
    public class ReturnResult<T>
    {
        //返回结果
        public T data { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string ReturnMessage { get; set; }

    }

    public class AppClientController : ApiController
    {
        private SysExptItemApp itemsApp = new SysExptItemApp();
        private SysExptDetailApp itemsDetailApp = new SysExptDetailApp();
        private CalendarApp calendarApp = new CalendarApp();
        private OperationLogApp operLog = new OperationLogApp();
        private SysLogApp logApp = new SysLogApp();
        [HttpGet]
        public ReturnResult<List<SysExptItemEntity>> GetTypeListJson()
        {
            var returnRes = new ReturnResult<List<SysExptItemEntity>>();
            List<SysExptItemEntity> data = itemsApp.GetList();

            if (data.Count > 0)
            {
                returnRes.data = data;
                returnRes.ReturnMessage = "sucess";
            }
            else
            {
                returnRes.ReturnMessage = "未查询到数据";
            }
            return returnRes;

        }

        public ReturnResult<List<SysExptItemEntity>> GetMenu2LevelList(string parentid)
        {
            var returnRes = new ReturnResult<List<SysExptItemEntity>>();
            List<SysExptItemEntity> data = itemsApp.GetMenu2LevelList(parentid);

            if (data.Count > 0)
            {
                returnRes.data = data;
                returnRes.ReturnMessage = "sucess";
            }
            else
            {
                returnRes.ReturnMessage = "未查询到数据";
            }
            return returnRes;

        }
        public ReturnResult<List<TreeViewModel>> GetTypeList()
        {
            var returnRes = new ReturnResult<List<TreeViewModel>>();
            List<SysExptItemEntity> data = itemsApp.GetList();

            if (data.Count > 0)
            {

                var treeList = new List<TreeViewModel>();
                foreach (SysExptItemEntity item in data)
                {
                    TreeViewModel tree = new TreeViewModel();

                    tree.id = item.F_Id;
                    tree.text = item.F_FullName;
                    tree.value = item.F_EnCode;
                    tree.parentId = item.F_ParentId;
                    tree.icon = item.F_Icon;
                    // tree.hasChildren = hasChildren;  
                    treeList.Add(tree);
                }
                foreach (TreeViewModel model in treeList)
                {
                    List<TreeViewModel> tempList = findChildren(model, treeList);
                    if (tempList.Count > 0)
                    {
                        model.children = tempList;
                    }
                }

                returnRes.data = treeList;
                returnRes.ReturnMessage = "sucess";
            }
            else
            {
                returnRes.ReturnMessage = "未查询到数据";
            }
            return returnRes;

        }
        public List<TreeViewModel> findChildren(TreeViewModel root, List<TreeViewModel> list)
        {
            var treeList = new List<TreeViewModel>();
            list.ForEach(item =>
            {
                if (item.parentId == root.id)
                {
                    treeList.Add(item);
                }
            });

            return treeList;
        }

        public ReturnResult<SysExptEntity> GetItemDetailJson(string keyword)
        {
            var returnRes = new ReturnResult<SysExptEntity>();
            var data = itemsDetailApp.GetForm(keyword);
            if (data!=null)
            {
                returnRes.data = data;
                returnRes.ReturnMessage = "sucess";
            }
            else
            {
                returnRes.ReturnMessage = "未查询到数据";
            }
            return returnRes;
        }

        public ReturnResult<List<CalendarModel>> GetCalendarJson()
        {
            var returnRes = new ReturnResult<List<CalendarModel>>();
            var data = calendarApp.getListJson();
            if (data.Count > 0)
            {
                returnRes.data = data;
                returnRes.ReturnMessage = "sucess";
            }
            else
            {
                returnRes.ReturnMessage = "未查询到数据";
            }
            return returnRes;
        }

        public ReturnResult<List<CalendarModel>> GetCalendar(string date)
        {
            var returnRes = new ReturnResult<List<CalendarModel>>();
            var data = calendarApp.getJson(date);
            if (data.Count > 0)
            {
                returnRes.data = data;
                returnRes.ReturnMessage = "sucess";
            }
            else
            {
                returnRes.ReturnMessage = "未查询到数据";
            }
            return returnRes;
        }

        //public ReturnResult<List<SysExptEntity>> GetExceptionList(string Cid, string keyword)
        //{
        //    var returnRes = new ReturnResult<List<SysExptEntity>>();
        //    var data = itemsDetailApp.GetList(Cid, keyword);
        //    if (data.Count > 0)
        //    {
        //        returnRes.data = data;
        //        returnRes.ReturnMessage = "sucess";
        //    }
        //    else
        //    {
        //        returnRes.ReturnMessage = "未查询到数据";
        //    }
        //    return returnRes;
        //}
        //public ReturnResult<List<CalendarModel>> GetOperationLogList()
        //{
        //    var returnRes = new ReturnResult<List<CalendarModel>>();
        //    var data = operLog.getListJson();
        //    if (data.Count > 0)
        //    {
        //        returnRes.data = data;
        //        returnRes.ReturnMessage = "sucess";
        //    }
        //    else
        //    {
        //        returnRes.ReturnMessage = "未查询到数据";
        //    }
        //    return returnRes;
        //}
        //public ReturnResult<List<CalendarModel>> GetSystemLogList()
        //{
        //    var returnRes = new ReturnResult<List<CalendarModel>>();
        //    var data = logApp.getListJson();
        //    if (data.Count > 0)
        //    {
        //        returnRes.data = data;
        //        returnRes.ReturnMessage = "sucess";
        //    }
        //    else
        //    {
        //        returnRes.ReturnMessage = "未查询到数据";
        //    }
        //    return returnRes;
        //}
    }
}
