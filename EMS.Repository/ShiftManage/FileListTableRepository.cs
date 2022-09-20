using EMS.Data;
using EMS.Domain.Entity.ShiftManage;
using EMS.Domain.IRepository.ShiftManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Repository.ShiftManage
{
    public class FileListTableRepository : RepositoryBase<FileListTableEntity>, IFileListTableRepository
    {
        public void SubmitForm(FileListTableEntity fileListTableEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(fileListTableEntity);
                }
                else
                {
                    //userLogOnEntity.F_Id = userEntity.F_Id;
                    //userLogOnEntity.F_UserId = userEntity.F_Id;
                    //userLogOnEntity.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                    //userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.F_UserPassword, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                    db.Insert(fileListTableEntity);
                    //db.Insert(userLogOnEntity);
                }
                db.Commit();
            }
        }
    }
}
