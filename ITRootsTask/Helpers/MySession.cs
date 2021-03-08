using Newtonsoft.Json;
using System.Collections.Generic;

namespace ITRootsTask.Helpers
{
    public class ReadSession
    {
        public static string Culture => CookieHelper.GetCookie(MyConstants.CultureCookieName);
        public static bool IsRTL => CookieHelper.GetCookie(MyConstants.CultureCookieName) == MyConstants.ArabicLanguage;
        public static bool IsArabic => CookieHelper.GetCookie(MyConstants.CultureCookieName) == MyConstants.ArabicLanguage;
        public static SessionUser User => SessionHelper.GetSession(MyConstants.SessionUserName) != null
            ? JsonConvert.DeserializeObject<SessionUser>(SessionHelper.GetSession(MyConstants.SessionUserName))
            : new SessionUser();
    }

    public class SessionUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Roles { get; set; }
    }
}