using System.Web;
using System.Web.Mvc;

namespace CMS_AFGA_Facturacion
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
