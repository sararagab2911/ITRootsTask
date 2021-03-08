using ITRootsTask.Services;
using System.Web.Mvc;

namespace ITRootsTask.Controllers
{
    public class HomeController : BaseAuthController
    {
        public HomeController(BaseService baseService)
        {

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
