using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.ViewModel
{
    public class ShiftOrderModel
    {
        public Int32? F_WeekInOrder { set; get; }
        public string F_Name { set; get; }
        public string F_Phone { set; get; }

        public Int32? F_WeekOutOrder { set; get; }
        public string F_WeekName { set; get; }
        public string WeekPhone { set; get; }

    }
}
