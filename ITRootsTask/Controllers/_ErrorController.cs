using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITRootsTask.Controllers
{
    public class _ErrorController : BaseController
    {
        [HttpGet] // 500
        public ActionResult ERROR()
        {
            return View();
        }
    }
}