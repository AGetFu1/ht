using EMS.Data;
using EMS.Domain.Entity.ShiftManage;
using EMS.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.IRepository.ShiftManage
{
    public interface ICalendarRepository : IRepositoryBase<CalendarEntity>
    {
        void SubmitForm(CalendarEntity calendarEntity, string keyValue);
        List<CalendarModel> getList();
        List<CalendarModel> getModel(string date);
        List<CalendarModel> getWeekModel(DateTime weekDate);
        List<ShiftOrderModel> GetShiftOrder();
        List<CalendarModel> getHolidayModel();
        List<string> inWeekitModel(CalendarEntity calendarEntity);
        List<string> outWeekitModel(CalendarEntity calendarEntity);
        List<CalendarModel> getITList();
        List<CalendarModel> getcs2List();
        List<CalendarModel> getZZ1List();
        List<string> inWeekzz1Model(CalendarEntity calendarEntity);
        List<string> outWeekzz1Model(CalendarEntity calendarEntity);
    }
}
