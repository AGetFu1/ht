using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain._03_Entity.ShiftManage;
using EMS.Domain._04_IRepository.ShiftManage;
using EMS.Data;

namespace EMS.Repository.ShiftManage
{
    public class DepartmentITRepository : RepositoryBase<DepartmentITEntity>, IDepartmentITRepository
    {
        public void SubmitForm(DepartmentITEntity departmentITEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(departmentITEntity);
                }
                else
                {
                    db.Insert(departmentITEntity);
                }
                db.Commit();
            }
        }
        public List<DepartmentITEntity> getListModel(DepartmentITEntity itemsEntity)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  F_FullName FROM Shift_TableIT  WHERE   F_Department='IT部门' ORDER BY F_SortCode");
            return dbcontext.Database.SqlQuery<DepartmentITEntity>(strSql.ToString()).ToList<DepartmentITEntity>();
        }
    }
}
