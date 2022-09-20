using EMS.Data;
using EMS.Domain.Entity.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.IRepository.ExceptionManage
{
    public interface ISysExptItemRepository : IRepositoryBase<SysExptItemEntity>
    {
        void SubmitForm(SysExptItemEntity itemsEntity, string keyValue);
    }
}
