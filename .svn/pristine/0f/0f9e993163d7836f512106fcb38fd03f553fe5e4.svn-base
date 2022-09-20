using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain.Entity.LogManage;
using EMS.Repository.LogManage;
using EMS.Domain.IRepository;
using EMS.Domain.IRepository.SysLogManage;
using EMS.Code;
using EMS.Domain.ViewModel;
using System.Reflection;

namespace EMS.Application.LogManage
{
    public  class OperationLogApp 
    {
        private IOperationLogRepository service = new OperationLogRepository();


        public List<OperationLogEntity> getList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<OperationLogEntity>();
            var queryParam = queryJson.ToJObject();

            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.AfterValue.Contains(keyword));
                expression = expression.Or(t => t.BeforeValue.Contains(keyword)); 
                expression = expression.Or(t => t.LastModifyUserId.Contains(keyword));
                expression = expression.Or(t => t.IPAddress.Contains(keyword));
            }
            if (!queryParam["timeType"].IsEmpty())
            {
                string timeType = queryParam["timeType"].ToString();
                DateTime startTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate();
                DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate().AddDays(1);
                switch (timeType)
                {
                    case "1":
                        break;
                    case "2":
                        startTime = DateTime.Now.AddDays(-7);
                        break;
                    case "3":
                        startTime = DateTime.Now.AddMonths(-1);
                        break;
                    case "4":
                        startTime = DateTime.Now.AddMonths(-3);
                        break;
                    default:
                        break;
                }
                expression = expression.And(t => t.F_ModifyTime >= startTime && t.F_ModifyTime <= endTime);
            }
            return service.FindList(expression, pagination);
        }

        public void clearLog(string keepTime)
        {
            DateTime operateTime = DateTime.Now;
            if (keepTime == "7")            //保留近一周
            {
                operateTime = DateTime.Now.AddDays(-7);
            }
            else if (keepTime == "1")       //保留近一个月
            {
                operateTime = DateTime.Now.AddMonths(-1);
            }
            else if (keepTime == "3")       //保留近三个月
            {
                operateTime = DateTime.Now.AddMonths(-3);
            }
            var expression = ExtLinq.True<OperationLogEntity>();
            expression = expression.And(t => t.F_CreatorTime <= operateTime);
            service.Delete(expression);
        }
        public void insert(OperationLogEntity operationLog) {
            service.Insert(operationLog);
        }
        public void IsUpdate<T>(T old, T current, string id)
          {
            OperationLogEntity history = new OperationLogEntity();
            ModifyFields atrr = null;
              Type type = typeof(T);
              PropertyInfo[] propertys = type.GetProperties();
              foreach (PropertyInfo property in propertys)
              {
                  if (property.PropertyType.IsValueType || property.PropertyType.Name == "String")
                {
                    if (property.PropertyType.FullName.Contains("Guid"))
                        continue;
                    //if (property.Name != "CreateUserID" && property.Name != "CreateTime" && property.Name != "ModifyUserID" &&                                    property.Name != "LastModifyTime")//排除这些字段不做判断
                    //{
                    if (property.GetCustomAttributes(typeof(ModifyFields), false).Count() > 0)
                    {
                        object o1 = property.GetValue(old, null); //以前的值
                        object o2 = property.GetValue(current, null); //修改后的值
                        string str1 = o1 == null ? string.Empty : o1.ToString();
                        string str2 = o2 == null ? string.Empty : o2.ToString();
                        //判断两者是否相同，不同则插入历史表中
                        if (str1 != str2)
                        {
                            history.BeforeValue = str1; //修改前的值
                            history.AfterValue = str2; //修改后的值
                            history.F_Id = Guid.NewGuid().ToString(); //修改数据的ID
                            history.F_ModifyTime = DateTime.Now;
                            history.LastModifyUserId = OperatorProvider.Provider.GetCurrent().UserName;
                            history.IPAddress = Net.Ip; //获取当前用户的IP地址
                            atrr = property.GetCustomAttributes(typeof(ModifyFields), false)[0] as ModifyFields;
                            history.ModifyField = property.Name;  //修改的字段名称
                            history.ModifyFieldName = atrr.FieldsName; //修改的字段中文名称

                            // 插入表中;
                            insert(history);
                        }
                    }
                    //}
                }
            }
         }
    }
}
