using EMS.Domain.Entity.ShiftManage;
using EMS.Domain.IRepository.ShiftManage;
using EMS.Repository.ShiftManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.ShiftManage
{
    public class TeamManageApp
    {
        private ITeamManageRepository service = new TeamManageRepository();

        public List<TeamEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        public TeamEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(TeamEntity areaEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                areaEntity.Modify(keyValue);
                service.Update(areaEntity);
            }
            else
            {
                areaEntity.Create();
                service.Insert(areaEntity);
            }
        }
    }
}
