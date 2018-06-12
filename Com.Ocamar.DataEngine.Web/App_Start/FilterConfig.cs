using System.Web;
using System.Web.Mvc;

namespace Com.Ocamar.DataEngine.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
