using System;
using System.Collections.Generic;
using EMS.Code;
using EMS.Domain.Entity.LogManage;
using EMS.Domain.IRepository.SysLogManage;
using EMS.Repository.LogManage;
namespace EMS.Application.LogManage
{
    public class SysLogApp 
    {
        private ISysLogRepository service = new SysLogRepository();
        public List<SysLogEntity> getList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<SysLogEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty()) {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.F_Account.Contains(keyword));
            }
            if (!queryParam["timeType"].IsEmpty()) {
                string timeType = queryParam["timeType"].ToString();
                DateTime startTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate();
                DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate().AddDays(1);
                switch (timeType) {
                    case "1":
                        break;
                    case "2":
                        startTime=DateTime.Now.AddDays(-7);
                        break;
                    case "3":
                        startTime = DateTime.Now.AddDays(-1);
                        break;
                    case "4":
                        startTime = DateTime.Now.AddDays(-3);
                        break;
                    default:
                        break;
                }
                expression = expression.And(t => t.F_Date >= startTime && t.F_Date <= endTime);

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
            var expression = ExtLinq.True<SysLogEntity>();
            expression = expression.And(t => t.F_Date <= operateTime);
            service.Delete(expression);
        }

        public void WriteDbLog(SysLogEntity logEntity)
        {
            logEntity.F_Id = Common.GuId();
            logEntity.F_Date = DateTime.Now;
            logEntity.Create();
            service.Insert(logEntity);
        }
    }
}
