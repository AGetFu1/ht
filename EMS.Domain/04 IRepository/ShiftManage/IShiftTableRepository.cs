using EMS.Data;
using EMS.Domain.Entity.ShiftManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.IRepository.ShiftManage
{
    public interface IShiftTableRepository : IRepositoryBase<ShiftTableEntity>
    {
        void SubmitForm(ShiftTableEntity shiftTableEntity, string keyValue);
    }
}
