using EMS.Domain.Entity.ExceptionManage;
using EMS.Domain.IRepository.ExceptionManage;
using EMS.Repository.ExceptionManage;
using System;
using System.Collections.Generic;
using System.Linq;
using EMS.Code;
using EMS.Application.LogManage;
using EMS.Domain.ViewModel;

namespace EMS.Application.ExceptionManage
{
    public class SysExptDetailApp
    {
        private ISysExptRepository service = new SysExptRepository();

        public List<SysExptEntity> GetList(Pagination pagination, string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<SysExptEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
                expression = expression.And(t => t.F_ItemName.Contains(keyword));
                expression = expression.Or(t => t.F_ItemCode.Contains(keyword));
                expression = expression.Or(t => t.F_Description.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        public List<SysExptEntity> GetItemList(string enCode)
        {
            return service.GetItemList(enCode);
        }
        public SysExptEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
            ESProvider.Delete(keyValue);
            LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Info("删除表Sys_Expt，ID 值为：" + keyValue);
        }
        public void SubmitForm(SysExptEntity itemsDetailEntity, string keyValue)
        {

            
            if (!string.IsNullOrEmpty(keyValue))
            {
                SysExptEntity oldexptEntity = service.FindEntity(t => t.F_Id == keyValue);
                new OperationLogApp().IsUpdate(oldexptEntity, itemsDetailEntity, keyValue);
                ESProvider.Update(keyValue, itemsDetailEntity.F_ItemCode, itemsDetailEntity.F_Description, itemsDetailEntity.F_ItemName);
                itemsDetailEntity.Modify(keyValue);  
            }
            else
            {
                itemsDetailEntity.Create();
                SysExptEntityModel model = new SysExptEntityModel
                {
                    f_id = itemsDetailEntity.F_Id,
                    f_creatortime = itemsDetailEntity.F_CreatorTime,
                    f_creatoruserid = itemsDetailEntity.F_CreatorUserId,
                    f_deletemark = itemsDetailEntity.F_DeleteMark,
                    f_deletetime = itemsDetailEntity.F_DeleteTime,
                    f_deleteuserid = itemsDetailEntity.F_DeleteUserId,
                    f_description = itemsDetailEntity.F_Description,
                    f_enabledmark = itemsDetailEntity.F_EnabledMark,
                    f_isdefault = itemsDetailEntity.F_IsDefault,
                    f_itemcode = itemsDetailEntity.F_ItemCode,
                    f_itemid = itemsDetailEntity.F_ItemId,
                    f_lastmodifytime = itemsDetailEntity.F_LastModifyTime,
                    f_lastmodifyuserid = itemsDetailEntity.F_LastModifyUserId,
                    f_layers = itemsDetailEntity.F_Layers,
                    f_parentid = itemsDetailEntity.F_ParentId,
                    f_simplespelling = itemsDetailEntity.F_SimpleSpelling,
                    f_sortcode = itemsDetailEntity.F_SortCode,
                    f_itemname = itemsDetailEntity.F_ItemName
                };
                ESProvider.Index(model);
                LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Info("插入表Sys_Expt，ID 值为："+ keyValue);
            }
            service.SubmitForm(itemsDetailEntity,keyValue);
        }
    }
}