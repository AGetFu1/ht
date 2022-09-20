using EMS.Domain.IRepository.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Repository.ExceptionManage;
using EMS.Domain.Entity.ExceptionManage;
using EMS.Data.Extensions;
using EMS.Application.LogManage;
using EMS.Repository.ShiftManage;
using EMS.Domain.IRepository.ShiftManage;

namespace EMS.Application.ExceptionManage
{
    public class FileItemApp
    {
        private IFileItemRepository service = new FileItemRepository();

        public List<FileItemEntity> GetList(string department="")
        {
            if (!string.IsNullOrEmpty(department)) {
               return service.IQueryable(t => t.F_Id == department || t.F_ParentId == department).OrderBy(t => t.F_SortCode).ToList();
            }
            return service.IQueryable().OrderBy(t => t.F_SortCode).ToList();
        }
        public FileItemEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(FileItemEntity itemsEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                FileItemEntity oldEntity = service.FindEntity(t => t.F_Id == keyValue);
                new OperationLogApp().IsUpdate(oldEntity, itemsEntity, keyValue);
                itemsEntity.Modify(keyValue);
                //service.Update(itemsEntity);
            }
            else
            {
                itemsEntity.Create();
                 
            }
            service.SubmitForm(itemsEntity, keyValue);
        }

        public List<FileItemEntity> GetMenu2LevelList(string parentid)
        {
            if (!string.IsNullOrEmpty(parentid))
            {
                return service.IQueryable(t => t.F_ParentId == parentid).OrderBy(t => t.F_SortCode).ToList();
            }
            return null;
        }

        public string RelationItemType(string department)
        {
            return (string)DbHelper.SelectForScalar("select ItemType from Dept_ItemTypeRelation where Dept_Id='" + department+"'");   
        }
    }
}
