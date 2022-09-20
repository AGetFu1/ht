using EMS.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Entity.ShiftManage
{
    public class CalendarEntity : IEntity<CalendarEntity>,ICreationAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        [ModifyFields("排班人员")]
        public string F_Text { get; set; }
        [ModifyFields("排班模式")]
        public string F_Title { get; set; }
        public string F_backgroundColor { get; set; }
        [ModifyFields("排班日期")]
        public string F_Start { get; set; }
        public string F_End { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public bool? F_DeleteMark { get; set; }
        public string F_Team { get; set; }
        public string F_Department { get; set; }
    }
}
