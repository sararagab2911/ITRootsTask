using System.Web;

namespace ITRootsTask.Helpers
{
    public static class SessionHelper
    {
        public static void SetSession(string name, string value)
        {
            HttpContext.Current.Session[name] = value;
        }

        public static string GetSession(string name)
        {
            if (HttpContext.Current.Session[name] != null)
                return HttpContext.Current.Session[name].ToString();
            return null;
        }

        public static void RemoveSession(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }
    }
}
