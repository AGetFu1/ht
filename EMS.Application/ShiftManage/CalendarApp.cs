using EMS.Code;
using EMS.Domain.Entity.ShiftManage;
using System.Collections.Generic;
using EMS.Repository.ShiftManage;
using EMS.Domain.IRepository.ShiftManage;
using EMS.Application.LogManage;
using EMS.Domain.ViewModel;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.ShiftManage
{
    public class CalendarApp
    {
        ICalendarRepository service = new CalendarRepository();
        public List<CalendarModel> getListJson()
        {
            var expression = ExtLinq.True<CalendarEntity>();           
            var data = service.getList();
            return data;
        }
        public List<CalendarModel> getJson(string date)
        {
            if (!date.IsEmpty()) {
                return service.getModel(date);
                
            }
            return null;
            
        }
        public List<CalendarModel> getITListJson()
        {
            var expression = ExtLinq.True<CalendarEntity>();
            var data = service.getITList();
            return data;
        }
        public List<CalendarModel> getZZ1ListJson()
        {
            var expression = ExtLinq.True<CalendarEntity>();
            var data = service.getZZ1List();
            return data;
        }
        public List<string> inWeekitModel(CalendarEntity calendarEntity)
        {          
                return service.inWeekitModel(calendarEntity);
        }
        public List<string> outWeekitModel(CalendarEntity calendarEntity)
        {
            return service.outWeekitModel(calendarEntity);
        }
        public  List<CalendarEntity> getListJson(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<CalendarEntity>();
            var queryParam = queryJson.ToJObject();
            // var data= service.IQueryable(expression).OrderBy(t => t.F_Id).ToList();
            expression = expression.And(t => t.F_Department.Contains("IT部门"));
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.F_Text.Contains(keyword));
                expression = expression.Or(t => t.F_Title.Contains(keyword));
                expression = expression.Or(t => t.F_Start.Contains(keyword));
                expression = expression.Or(t => t.F_Team.Contains(keyword));
            }
            //foreach (var item in data)
            //{
            //    item.F_Start=item.F_Start.ToDateString().ToDate();
            //    item.F_End=item.F_End.ToDateString().ToDate();
            //}

            //pagination.sord = "F_Start desc";
            //pagination.sidx = "F_Text,F_Title,F_Start,F_Team,F_Department";
            //list.Skip((index - 1) * size).Take(size).ToList()
            return service.FindList(expression, pagination).OrderBy(t => t.F_Start).ThenBy(t => t.F_Text).ToList();//按日期排序
        }
        public List<CalendarEntity> getList1Json(Pagination pagination, string queryJson)
        {
            var data = service.IQueryable(t => t.F_Department == "IT部门").OrderByDescending(t => t.F_Start).ToList();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                data = service.IQueryable(t => t.F_Department == "IT部门" &&( t.F_Text.Contains(keyword)|| t.F_Title.Contains(keyword)|| t.F_Start.Contains(keyword)|| t.F_Team.Contains(keyword))).OrderByDescending(t => t.F_Start).ToList();
                
            }
            
            return data;
        }
        public List<CalendarModel> getHolidayJson()
        {
            return service.getHolidayModel();
        }

        public List<ShiftOrderModel> GetShiftOrder()
        {
            return service.GetShiftOrder();
        }

        public List<CalendarModel> getWeekJson(DateTime weekDate)
        {
            if (!weekDate.IsEmpty())
            {
                return service.getWeekModel(weekDate);

            }
            return null;
        }

        public CalendarEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void SubmitForm(CalendarEntity calendarEntity, string keyValue)
        {           
            calendarEntity.F_Department ="IT部门";
            if (!string.IsNullOrEmpty(keyValue))
            {
                CalendarEntity oldEntity = service.FindEntity(t => t.F_Id == keyValue);
                calendarEntity.F_Department = oldEntity.F_Department;
                calendarEntity.F_Team = oldEntity.F_Team;
                new OperationLogApp().IsUpdate(oldEntity, calendarEntity, keyValue);
                calendarEntity.Modify(keyValue);
            }
            else
            {
                calendarEntity.Create();
            }
            service.SubmitForm(calendarEntity, keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);             
        }

        public  int importProgram(DataTable dt)
        {
            List<CalendarEntity> listModel = new List<CalendarEntity>();
            int num=0;
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CalendarEntity entity = new CalendarEntity();
                    if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                    {

                        var rowErr = string.Empty;
                        entity.F_Text = dt.Rows[i][0].ToString().Trim();
                        entity.F_Id = Common.GuId();
                        if (dt.Rows[i][1] != null)
                        {
                            string strDate = dt.Rows[i][1].ToString();

                            entity.F_Start = strDate.ToDate().ToDateString();
                        }
                        entity.F_CreatorTime =  DateTime.Now;
                        listModel.Add(entity);
                    }
                }
                //service.FindList();
                //service.Delete(t => t.F_Id == keyValue);
               num=  service.Insert(listModel);
            }
            return num;
        }
        //==================================================测试二部=======================================================================================
        public List<CalendarEntity> getListJsoncs2(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<CalendarEntity>();
            var queryParam = queryJson.ToJObject();           
            expression = expression.And(t => t.F_Department.Contains("测试二部"));
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.F_Text.Contains(keyword));
                expression = expression.Or(t => t.F_Title.Contains(keyword));
                expression = expression.Or(t => t.F_Start.Contains(keyword));
                expression = expression.Or(t => t.F_Team.Contains(keyword));
            }          
            return service.FindList(expression, pagination).OrderBy(t => t.F_Start).ThenBy(t => t.F_Text).ToList();//按日期排序
        }
        public void SubmitFormcs2(CalendarEntity calendarEntity, string keyValue)
        {
            calendarEntity.F_Department = "测试二部";
            if (!string.IsNullOrEmpty(keyValue))
            {
                CalendarEntity oldEntity = service.FindEntity(t => t.F_Id == keyValue);
                calendarEntity.F_Department = oldEntity.F_Department;
                calendarEntity.F_Team = oldEntity.F_Team;
                new OperationLogApp().IsUpdate(oldEntity, calendarEntity, keyValue);
                calendarEntity.Modify(keyValue);
            }
            else
            {
                calendarEntity.Create();
            }
            service.SubmitForm(calendarEntity, keyValue);
        }
        public List<CalendarModel> getcs2ListJson()
        {
            var expression = ExtLinq.True<CalendarEntity>();
            var data = service.getcs2List();
            return data;
        }
        //==================================================制造一部=======================================================================================
        public void Submitzz1Form(CalendarEntity calendarEntity, string keyValue)
        {
            calendarEntity.F_Department = "制造一部";
            if (!string.IsNullOrEmpty(keyValue))
            {
                CalendarEntity oldEntity = service.FindEntity(t => t.F_Id == keyValue);
                new OperationLogApp().IsUpdate(oldEntity, calendarEntity, keyValue);
                calendarEntity.Modify(keyValue);
            }
            else
            {
                calendarEntity.Create();
            }
            service.SubmitForm(calendarEntity, keyValue);
        }
        public List<CalendarEntity> getListz1Json(Pagination pagination, string queryJson)
        {                      
            var data = service.IQueryable(t => t.F_Department == "制造一部").OrderByDescending(t => t.F_Start).ToList();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                data = service.IQueryable(t => t.F_Department == "制造一部" && (t.F_Text.Contains(keyword) || t.F_Title.Contains(keyword) || t.F_Start.Contains(keyword) || t.F_Team.Contains(keyword))).OrderByDescending(t => t.F_Start).ToList();
            }
            return data;
        }
        public List<CalendarEntity> getListzz1Json(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<CalendarEntity>();
            var queryParam = queryJson.ToJObject();
            // var data= service.IQueryable(expression).OrderBy(t => t.F_Id).ToList();
            expression = expression.And(t => t.F_Department.Contains("制造一部"));
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.F_Text.Contains(keyword));
                expression = expression.Or(t => t.F_Title.Contains(keyword));
                expression = expression.Or(t => t.F_Start.Contains(keyword));
                expression = expression.Or(t => t.F_Team.Contains(keyword));
            }            
            //pagination.sord = "desc";
            //pagination.sidx = "F_Start,F_Text,F_Title,F_Team,F_Department";
            return service.FindList(expression, pagination).OrderByDescending(t => t.F_Start).ThenByDescending(t => t.F_Text).ToList();//按日期排序
        }     
        public List<string> inWeekzz1Model(CalendarEntity calendarEntity)
        {
            return service.inWeekzz1Model(calendarEntity);
        }
        public List<string> outWeekzz1Model(CalendarEntity calendarEntity)
        {
            return service.outWeekzz1Model(calendarEntity);
        }
    }
}
