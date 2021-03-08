using System;
using System.Web;

namespace ITRootsTask.Helpers
{
    public static class CookieHelper
    {
        public static void SetCookie(string name, string value, int expirationPerDays = 365)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            cookie.Expires = DateTime.UtcNow.AddDays(expirationPerDays);
            HttpContext.Current.Response.Cookies.Remove(name);
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public static string GetCookie(string name)
        {
            if (HttpContext.Current.Request.Cookies[name] != null)
                return HttpContext.Current.Request.Cookies[name].Value;
            return null;
        }
    }
}