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
    public class SysFlowRepository : RepositoryBase<SysFlowEntity>, ISysFlowRepository
    {
        public  List<string> getTitle(string title)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT F_Title FROM [dbo].[Sys_Flow] WHERE F_Title =@title ");
            DbParameter[] parameter =
            {
                 new SqlParameter("@title",title)
            };
            return dbcontext.Database.SqlQuery<string>(strSql.ToString(), parameter).ToList();
        }

    }
}
