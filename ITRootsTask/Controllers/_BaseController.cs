using ITRootsTask.Helpers;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace ITRootsTask.Controllers
{
    public class BaseController : Controller
    {
        [NonAction]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region culture
            string culture = MyConstants.DefaultLanguage;
            if (CookieHelper.GetCookie(MyConstants.CultureCookieName) == null)
            {
                CookieHelper.SetCookie(MyConstants.CultureCookieName, culture);
            }
            else
            {
                culture = CookieHelper.GetCookie(MyConstants.CultureCookieName);
            }
            // THREAD
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            #endregion

            base.OnActionExecuting(filterContext);
        }


        [HttpGet]
        public ActionResult ChangeLanguage()
        {
            string culture = MyConstants.DefaultLanguage;
            if (CookieHelper.GetCookie(MyConstants.CultureCookieName) != null)
            {
                culture = (CookieHelper.GetCookie(MyConstants.CultureCookieName) == MyConstants.EnglishLanguage ? MyConstants.ArabicLanguage : MyConstants.EnglishLanguage);
            }

            CookieHelper.SetCookie(MyConstants.CultureCookieName, culture);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.ToString());

            return null;
        }

        [HttpGet]
        public ActionResult TaskError()
        {
            return RedirectToAction("ERROR", "Error");
        }
    }


    [Authorize]
    public class BaseAuthController : BaseController
    {

    }
}