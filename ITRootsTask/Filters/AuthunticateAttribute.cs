using ITRootsTask.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace ITRootsTask.Filters
{
    public class AuthunticateAttribute : ActionFilterAttribute
    {
        private readonly string roleName;
        public AuthunticateAttribute(string roleName)
        {
            this.roleName = roleName;
        }

        public override void OnActionExecuting(ActionExecutingContext executingContext)
        {
            if (ReadSession.User.Roles != roleName)
            {
                executingContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Auth",
                    action = "AccessDenied"
                }));
            }
            base.OnActionExecuting(executingContext);
        }

    }
}
