using System.Web;
using System.Web.Mvc;

namespace DownLoadMp3FromYoutube
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
