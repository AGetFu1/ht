using EMS.Application.SystemManage;
using EMS.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.Web.Areas.SystemManage.Controllers
{
    public class ChangePasswordController : ControllerBase
    {
        private UserLogOnApp userLogOnApp = new UserLogOnApp();
        private UserApp userApp = new UserApp();

        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            userLogOnApp.RevisePassword(userPassword, keyValue);
            return Success("重置密码成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult RevisePassword()
        {
            return View();
        }
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult ChangePassword(string OldPass, string NewPass, string NewPass1)
        {
            if (NewPass == NewPass1)
            {
                string name = OperatorProvider.Provider.GetCurrent().UserName.Trim();
                string code = OperatorProvider.Provider.GetCurrent().UserCode.Trim();
                string msg = userApp.ChangePasswordbus(OldPass, NewPass, NewPass1, name, code);
                if (msg == "Success")
                {
                    return Success("修改成功");
                }
                else
                {
                    if (msg == "Fail")
                    {
                        return Error("修改失败");
                    }
                    else if (msg == "E1001")
                    {
                        return Error("旧密码不匹配，请重新输入！");
                    }
                    else
                    {
                        return Error("未知原因，请重新修改密码。");
                    }
                }
            }
            else
            {
                return Error("新密码与确认密码不一致，请重新输入！");
            }
        }
    }
}
