using EMS.Code;
using EMS.Domain.Entity.ExceptionManage;
using EMS.Domain.IRepository.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Repository.ExceptionManage;
using EMS.Data.Extensions;
using EMS.Application.LogManage;

namespace EMS.Application.ExceptionManage
{
    public class SysFlowApp
    {
        private ISysFlowRepository service = new SysFlowRepository();

        public List<SysFlowEntity> GetList(string title="")
        {
            var expression = ExtLinq.True<SysFlowEntity>();
            var data = service.IQueryable(expression).OrderBy(t => t.F_Id).ToList();
            return data;
        }
        public SysFlowEntity GetForm(string keyValue)
        {
            return service.FindEntity(t=>t.F_Title==keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_Title.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(string Datajson, string keyValue)
        {
            
            if (!string.IsNullOrEmpty(keyValue))
            {
                SysFlowEntity oldEntity = service.FindEntity(t => t.F_Title==keyValue);
                if (oldEntity!=null)
                {
                    service.Update(oldEntity);
                }
                else {
                    SysFlowEntity itemsEntity = new SysFlowEntity();
                    itemsEntity.F_JsonData = Datajson;
                    itemsEntity.F_Title = keyValue;
                    itemsEntity.Create();
                    service.Insert(itemsEntity);
                }
            }
            else
            {
                throw new Exception("请输入标题");
            }
        }

        public string SearchTitle(string title)
        {
             
            if (!string.IsNullOrEmpty(title)) {
                SysFlowEntity entity= service.FindEntity(t => t.F_Title.Contains(title));
                if (entity != null) {
                    return entity.F_Title;
                }
                return "Not Find";
            }
            return "请输入标题";
        }
    }
}
