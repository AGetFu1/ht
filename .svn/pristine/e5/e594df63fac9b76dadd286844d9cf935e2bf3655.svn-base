/*******************************************************************************
 * 
 * Author: HT
 * Description: HT-XA快速开发平台
 * Website：http://ht-xa.com
*********************************************************************************/
using EMS.Code;
using EMS.Domain.Entity.SystemManage;
using EMS.Domain.IRepository.SystemManage;
using EMS.Repository.SystemManage;

namespace EMS.Application.SystemManage
{
    public class UserLogOnApp
    {
        private IUserLogOnRepository service = new UserLogOnRepository();
        private IUserRepository userService = new UserRepository();
        public UserLogOnEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void UpdateForm(UserLogOnEntity userLogOnEntity)
        {
            service.Update(userLogOnEntity);
        }
        public void RevisePassword(string userPassword,string keyValue)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_Id = keyValue;
            userEntity.F_UserPassword = userPassword;
            userService.Update(userEntity);
        }
    }
}
