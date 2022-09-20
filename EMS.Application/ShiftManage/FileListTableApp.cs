using EMS.Application.LogManage;
using EMS.Code;
using EMS.Domain.Entity.ShiftManage;
using EMS.Domain.IRepository.ShiftManage;
using EMS.Repository.ShiftManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace EMS.Application.ShiftManage
{
    public class FileListTableApp
    {
        IFileListTableRepository service = new FileListTableRepository();
        public List<FileListTableEntity> getListJson(Pagination pagination, string itemId, string queryJson)
        {
 
            var expression = ExtLinq.True<FileListTableEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            var queryParam = queryJson.ToJObject();
            // var data= service.IQueryable(expression).OrderBy(t => t.F_Id).ToList();
            if (!queryParam["keyword"].IsEmpty())
            {//按输入的keyword进行搜索
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.F_UseName.Trim().Contains(keyword.Trim()));
                expression = expression.Or(t => t.F_FileName.Trim().Contains(keyword.Trim()));
                expression = expression.Or(t => t.F_GongXv.Trim().Contains(keyword.Trim()));
                expression = expression.Or(t => t.F_Type.Trim().Contains(keyword.Trim()));
                
            }
            if (!queryParam["startDate"].IsEmpty()) {
                string StartDate = queryParam["startDate"].ToString();
                DateTime StartTime = Convert.ToDateTime(StartDate);
                expression = expression.And(t => t.F_Date > StartTime);
            }
            if (!queryParam["endDate"].IsEmpty()) {
                
                string EndDate = queryParam["endDate"].ToString();
                DateTime EndTime = Convert.ToDateTime(EndDate);
                expression = expression.And(t => t.F_Date < EndTime);
            }
            //foreach (var item in data)
            //{
            //    item.F_Start=item.F_Start.ToDateString().ToDate();
            //    item.F_End=item.F_End.ToDateString().ToDate();
            //}
            pagination.sord = "desc";
            pagination.sidx = "F_UseName,F_FileName,F_GongXv";
            return service.FindList(expression, pagination).OrderByDescending(t => t.F_Date).ToList();//按日期排序
            //return service.FindList(expression, pagination);
        }

        public FileListTableEntity GetForm(string keyValue)
        {
            

            return service.FindEntity(keyValue);
        }
        public Object download(string keyValue) {
            Object path ;
            FileListTableEntity entity = GetForm(keyValue);
            path = entity.F_Address;
            return path;
        }
        public void SubmitForm(FileListTableEntity fileListTableEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                FileListTableEntity oldEntity = service.FindEntity(t => t.F_Id == keyValue);
                new OperationLogApp().IsUpdate(oldEntity, fileListTableEntity, keyValue);
                fileListTableEntity.Modify(keyValue);
            }
            else
            {
                fileListTableEntity.Create();
                //service.Insert(FileListTableEntity);
            }
            service.SubmitForm(fileListTableEntity, keyValue);
        }
 
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }
        public int importProgram(DataTable dt)
        {
            List<FileListTableEntity> listModel = new List<FileListTableEntity>();
            int num = 0;
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    FileListTableEntity entity = new FileListTableEntity();
                    if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                    {

                        var rowErr = string.Empty;
                        entity.F_FileName = dt.Rows[i][0].ToString().Trim();
                        entity.F_Id = Common.GuId();
                        if (dt.Rows[i][1] != null)
                        {
                            string strDate = dt.Rows[i][1].ToString();

                            entity.F_Description= strDate.ToDate().ToDateString();
                        }
                        entity.F_CreatorTime = DateTime.Now;
                        listModel.Add(entity);
                    }
                }
                //service.FindList();
                //service.Delete(t => t.F_Id == keyValue);
                num = service.Insert(listModel);
            }
            return num;
        }
 
        public void SubmitForm(FileListTableEntity fileListTableEntity)
        {

                fileListTableEntity.Create();
                service.Insert(fileListTableEntity);
           
        }
    }
}
