using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITRootsTask.Helpers
{
    public static class MyConstants
    {
        #region cookies
        public static string ArabicLanguage => "ar-EG";
        public static string EnglishLanguage => "en-US";
        public static string DefaultLanguage => ArabicLanguage;
        public static string CultureCookieName => "SysLanguage";
        #endregion

        #region sessions
        public static string SessionUserName => "SessionInfo";
        #endregion

    }
}