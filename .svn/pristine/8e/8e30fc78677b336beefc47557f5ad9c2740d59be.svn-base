/*******************************************************************************
 * 
 * Author: HT
 * Description: HT-XA快速开发平台
 * Website：http://ht-xa.com
*********************************************************************************/
using EMS.Domain.Entity.SystemSecurity;
using EMS.Application.SystemSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Domain.Entity.SystemManage;
using EMS.Application.SystemManage;
using EMS.Code;
using EMS.Application;
using EMS.Domain.Entity.LogManage;
using EMS.Application.LogManage;

namespace EMS.Web.Controllers
{
    public class LoginController : Controller
    {
        private string LoginProvider = Configs.GetValue("LoginProvider");
        [HttpGet]
        public virtual ActionResult Index()
        {
            //var test = string.Format("{0:E2}", 1);
            return View();
        }
        // [HttpGet]
        //public ActionResult GetAuthCode()
        //{
        //    return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        //}
        [HttpGet]
        public ActionResult OutLogin()
        {
            new SysLogApp().WriteDbLog(new SysLogEntity
            {
                F_ModuleName = "系统登录",
                F_Type = DbLogType.Exit.ToString(),
                F_Account = OperatorProvider.Provider.GetCurrent().UserCode,
                F_NickName = OperatorProvider.Provider.GetCurrent().UserName,
                F_Result = true,
                F_Description = "安全退出系统",
            });
            Session.Abandon();
            Session.RemoveAll();
            OperatorProvider.Provider.RemoveCurrent();
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult CheckLogin(string username, string password)
        {
            SysLogEntity logEntity = new SysLogEntity();
            logEntity.F_ModuleName = "系统登录";
            logEntity.F_Type = DbLogType.Login.ToString();
            try
            {
                UserEntity userEntity = new UserApp().CheckLogindb(username, password);
                if (userEntity != null)
                {
                    #region 原始
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = userEntity.F_Id;
                    operatorModel.UserCode = userEntity.F_Account;
                    operatorModel.UserName = userEntity.F_RealName;
                    operatorModel.CompanyId = userEntity.F_OrganizeId;
                    operatorModel.DepartmentId = userEntity.F_DepartmentId;
                    operatorModel.RoleId = userEntity.F_RoleId;
                    operatorModel.LoginIPAddress = "";//Net.Ip;
                    operatorModel.LoginIPAddressName = "";// Net.GetLocation(operatorModel.LoginIPAddress);
                    //operatorModel.LoginTime = DateTime.Now;
                    operatorModel.LoginToken = Guid.NewGuid().ToString();//DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                    #endregion
                    if (userEntity.F_Account == "admin")
                    {
                        operatorModel.IsSystem = true;
                    }
                    else
                    {
                        operatorModel.IsSystem = false;
                    }
                    Session[LoginProvider] = operatorModel;
                    OperatorProvider.Provider.AddCurrent(operatorModel);
                    logEntity.F_Account = userEntity.F_Account;
                    logEntity.F_NickName = userEntity.F_RealName;
                    logEntity.F_Result = true;
                    logEntity.F_Description = "登录成功";
                    new SysLogApp().WriteDbLog(logEntity);
                }
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                logEntity.F_Account = username;
                logEntity.F_NickName = username;
                logEntity.F_Result = false;
                logEntity.F_Description = "登录失败，" + ex.Message;
                new SysLogApp().WriteDbLog(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }
    }
}
