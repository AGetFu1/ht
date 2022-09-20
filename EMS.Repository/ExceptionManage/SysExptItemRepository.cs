using System.Collections.Generic;
using System.Linq;
using EMS.Domain.Entity.ExceptionManage;
using System.Threading.Tasks;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using EMS.Data;
using EMS.Domain.IRepository;
using EMS.Domain.IRepository.ExceptionManage;

namespace EMS.Repository.ExceptionManage
{
    public class SysExptItemRepository : RepositoryBase<SysExptItemEntity>, ISysExptItemRepository
    {
         

        public void SubmitForm(SysExptItemEntity itemsEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(itemsEntity);
                }
                else
                {
                    //userLogOnEntity.F_Id = userEntity.F_Id;
                    //userLogOnEntity.F_UserId = userEntity.F_Id;
                    //userLogOnEntity.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                    //userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.F_UserPassword, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                    db.Insert(itemsEntity);
                    //db.Insert(userLogOnEntity);
                }
                db.Commit();
            }
        }
    }
}
