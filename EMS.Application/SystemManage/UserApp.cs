/*******************************************************************************
 * 
 * Author: HT
 * Description: HT-XA快速开发平台
 * Website：http://ht-xa.com
*********************************************************************************/
using EMS.Application.LogManage;
using EMS.Code;
using EMS.Data.Extensions;
using EMS.Domain.Entity.SystemManage;
using EMS.Domain.IRepository.SystemManage;
using EMS.Repository.SystemManage;
using System;
using System.Collections.Generic;

namespace EMS.Application.SystemManage
{
    public class UserApp
    {
        private IUserRepository service = new UserRepository();
        // UserLogOnApp userLogOnApp = new UserLogOnApp();
        OperationLogApp logApp = new OperationLogApp();
        public List<UserEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<UserEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Account.Contains(keyword));
                expression = expression.Or(t => t.F_RealName.Contains(keyword));
                expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            expression = expression.And(t => t.F_Account != "admin");
            return service.FindList(expression, pagination);
        }
        public UserEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            
            if (!string.IsNullOrEmpty(keyValue))
            {
                UserEntity userOldEntity = service.FindEntity(t => t.F_Id == keyValue);
                logApp.IsUpdate(userOldEntity,userEntity, keyValue);
                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity, userLogOnEntity, keyValue);
        }
        public void UpdateForm(UserEntity userEntity)
        {
            service.Update(userEntity);
        }
        public UserEntity CheckLogindb(string username, string password)
        {
            #region
            UserEntity userEntity = service.FindEntity(t=>t.F_Account==username);
            if (userEntity != null)
            {
                if (userEntity.F_EnabledMark == true)
                {
                    #region  原始
                    //UserLogOnEntity userLogOnEntity = userLogOnApp.GetForm(userEntity.F_Id);
                    //string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                    //if (dbPassword == userLogOnEntity.F_UserPassword)
                    //{
                    //    DateTime lastVisitTime = DateTime.Now;
                    //    int LogOnCount = (userLogOnEntity.F_LogOnCount).ToInt() + 1;
                    //    if (userLogOnEntity.F_LastVisitTime != null)
                    //    {
                    //        userLogOnEntity.F_PreviousVisitTime = userLogOnEntity.F_LastVisitTime.ToDate();
                    //    }
                    //    userLogOnEntity.F_LastVisitTime = lastVisitTime;
                    //    userLogOnEntity.F_LogOnCount = LogOnCount;
                    //    userLogOnApp.UpdateForm(userLogOnEntity);
                    #endregion
                    if (userEntity.F_UserPassword == password)
                    {
                        return userEntity;
                    }
                    else
                    {
                        throw new Exception("密码不正确，请重新输入");
                    }
                }
                else
                {
                    throw new Exception("账户被系统锁定,请联系管理员");
                }
            }
            else
            {
                throw new Exception("账户不存在，请重新输入");
            }
            #endregion
        }

        public string  ChangePasswordbus(string OldPass, string NewPass, string NewPass1,string usrName, string usrCode)
        {
            string flag = string.Empty;
            object db = DbHelper.SelectForScalar("SELECT TOP 1 F_UserPassword FROM Sys_User WHERE F_Account='" + usrCode + "' AND F_RealName ='" + usrName + "'");
            string SelectPassWord = (string)db;
            if (SelectPassWord == OldPass)
            {
                bool SOrE = DbHelper.Update("UPDATE  Sys_User SET F_UserPassword='" + NewPass1 + "'    WHERE F_Account='" + usrCode + "' AND F_RealName ='" + usrName + "'");
                if (SOrE == true)
                {
                    flag = "Success";
                }
                else 
                {
                    flag = "Fail";
                }
            }
            else
            {
                flag = "E1001";
            }
            return flag;
        }
    }
}
