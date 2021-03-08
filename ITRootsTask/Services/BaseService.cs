using ITRootsTask.Context;
using ITRootsTask.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace ITRootsTask.Services
{
    public class BaseService
    {
        public BaseService()
        {
            ApplicationDbContext _context = ApplicationDbContext.Create();

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


            #region user related info
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                SessionUser sessionUser = new SessionUser();
                if (SessionHelper.GetSession(MyConstants.SessionUserName) == null)
                {
                    int userId = int.Parse(HttpContext.Current.User.Identity.Name);
                    var user = _context.Users.FirstOrDefault(x => x.Id == userId);
                    if (user != null)
                    {
                        sessionUser = new SessionUser()
                        {
                            Id = user.Id,
                            Username = user.Username,
                            FullName = user.FullName,
                            Roles = user.Roles,
                        };
                        SessionHelper.SetSession(MyConstants.SessionUserName, JsonConvert.SerializeObject(sessionUser));
                    }
                }
            }
            #endregion
        }
    }
}