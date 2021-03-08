using ITRootsTask.Context;
using ITRootsTask.Services.Auth;
using ITRootsTask.Services.Email;
using ITRootsTask.Services.InvoiceService;
using ITRootsTask.Services.UserService;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace ITRootsTask
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        public static void RegisterTypes()
        {
            var container = new UnityContainer();

            // context
            container.RegisterType<ApplicationDbContext>();
            // services
            container.RegisterType<IAuthService, AuthService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IInvoiceService, InvoiceService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
