using System.Web.Mvc;

namespace EMS.Web.Areas.LogManager
{
    public class LogManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "LogManager";
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
