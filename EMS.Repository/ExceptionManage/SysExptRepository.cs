using EMS.Data;
using EMS.Domain.Entity.SystemManage;
using EMS.Domain.IRepository.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Linq;
using EMS.Domain.Entity.ExceptionManage;
using System.Threading.Tasks;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace EMS.Repository.ExceptionManage
{
    public class SysExptRepository : RepositoryBase<SysExptEntity>, ISysExptRepository
    {
        public List<SysExptEntity> GetItemList(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  d.*
                            FROM    Sys_Expt d
                                    INNER  JOIN Sys_Expt_Item i ON i.F_Id = d.F_ItemId
                            WHERE   1 = 1
                                    AND i.F_EnCode = @enCode
                                    AND d.F_EnabledMark = 1
                                    AND d.F_DeleteMark = 0
                            ORDER BY d.F_SortCode ASC");
            DbParameter[] parameter =
            {
                 new SqlParameter("@enCode",enCode)
            };
            return this.FindList(strSql.ToString(), parameter);
        }
        public void SubmitForm(SysExptEntity  Entity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(Entity);
                }
                else
                {
                    //userLogOnEntity.F_Id = userEntity.F_Id;
                    //userLogOnEntity.F_UserId = userEntity.F_Id;
                    //userLogOnEntity.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                    //userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.F_UserPassword, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                    db.Insert(Entity);
                    //db.Insert(userLogOnEntity);
                }
                db.Commit();
            }
        }
    }
}