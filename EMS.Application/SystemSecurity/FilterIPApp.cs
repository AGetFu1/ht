/*******************************************************************************
 * 
 * Author: HT
 * Description: HT-XA快速开发平台
 * Website：http://ht-xa.com
*********************************************************************************/
using EMS.Code;
using EMS.Domain.Entity.SystemSecurity;
using EMS.Domain.IRepository.SystemSecurity;
using EMS.Repository.SystemSecurity;
using System.Collections.Generic;
using System.Linq;

namespace EMS.Application.SystemSecurity
{
    public class FilterIPApp
    {
        private IFilterIPRepository service = new FilterIPRepository();

        public List<FilterIPEntity> GetList(string keyword)
        {
            var expression = ExtLinq.True<FilterIPEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_StartIP.Contains(keyword));
            }
            return service.IQueryable(expression).OrderByDescending(t => t.F_DeleteTime).ToList();
        }
        public FilterIPEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(FilterIPEntity filterIPEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                filterIPEntity.Modify(keyValue);
                service.Update(filterIPEntity);
            }
            else
            {
                filterIPEntity.Create();
                service.Insert(filterIPEntity);
            }
        }
    }
}
