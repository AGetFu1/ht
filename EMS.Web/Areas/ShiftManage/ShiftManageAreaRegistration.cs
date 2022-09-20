using System.Web.Mvc;

namespace EMS.Web.Areas.ShiftManager
{
    public class ShiftManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ShiftManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
            this.AreaName + "_Default",
            this.AreaName + "/{controller}/{action}/{id}",
            new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
            new string[] { "EMS.Web.Areas." + this.AreaName + ".Controllers" }
           );
        }
    }
}
