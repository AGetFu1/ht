using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Repository.ShiftManage;
using EMS.Domain._03_Entity.ShiftManage;
using EMS.Data.Extensions;
using EMS.Repository;
using EMS.Domain._04_IRepository.ShiftManage;
using EMS.Application.LogManage;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using EMS.Code;
using EMS.Domain.Entity.ShiftManage;
using EMS.Domain.IRepository.ShiftManage;
using EMS.Data;

namespace EMS.Application.ShiftManage
{
    public class DepartmentITApp
    {
        private IDepartmentITRepository service = new DepartmentITRepository();

        public List<DepartmentITEntity> GetList(DepartmentITEntity itemsEntity)
        {           
            return service.getListModel(itemsEntity);
            //return data;
        }
        public List<DepartmentITEntity> GetList1(string department = "")
        {
            if (!string.IsNullOrEmpty(department))
            {
                return service.IQueryable(t => t.F_Department == "IT部门").OrderBy(t => t.F_SortCode).ToList();
            }
            return service.IQueryable(t => t.F_Department == "IT部门").OrderBy(t => t.F_SortCode).ToList();
        }
        
        public DepartmentITEntity GetForm(string keyValue)
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
        public void SubmitForm(DepartmentITEntity itemsEntity, string keyValue)
        {
            itemsEntity.F_Department = "IT部门";
            if (!string.IsNullOrEmpty(keyValue))
            {                
                DepartmentITEntity oldEntity = service.FindEntity(t => t.F_Id == keyValue);
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

        public List<DepartmentITEntity> GetMenu2LevelList(string parentid)
        {
            if (!string.IsNullOrEmpty(parentid))
            {
                return service.IQueryable(t => t.F_ParentId == parentid).OrderBy(t => t.F_SortCode).ToList();
            }
            return null;
        }

        public string RelationItemType(string department)
        {
            return (string)DbHelper.SelectForScalar("select ItemType from Dept_ItemTypeRelation where Dept_Id='" + department + "'");
        }
        //========================================================测试二部=================================================================================
        public List<DepartmentITEntity> GetListcs2(string department = "")
        {
            if (!string.IsNullOrEmpty(department))
            {
                return service.IQueryable(t => t.F_Department == "测试二部").OrderBy(t => t.F_SortCode).ToList();
            }
            return service.IQueryable(t => t.F_Department == "测试二部").OrderBy(t => t.F_SortCode).ToList();
        }
        //==========================================================制造一部========================================================================================
        public List<DepartmentITEntity> GetListzz1(string department = "")
        {
            if (!string.IsNullOrEmpty(department))
            {
                return service.IQueryable(t => t.F_Department == "制造一部").OrderBy(t => t.F_SortCode).ToList();
            }
            return service.IQueryable(t => t.F_Department == "制造一部").OrderBy(t => t.F_SortCode).ToList();
        }
    }
}
