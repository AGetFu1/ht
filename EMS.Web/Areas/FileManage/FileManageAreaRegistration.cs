using System.Web.Mvc;

namespace EMS.Web.Areas.LogManager
{
    public class FileManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FileManage";
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
