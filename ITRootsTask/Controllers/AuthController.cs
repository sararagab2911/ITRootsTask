using ITRootsTask.Helpers;
using ITRootsTask.Models.DTOs;
using ITRootsTask.Services.Auth;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace ITRootsTask.Controllers
{
    public class AuthController : BaseController
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginDTO userLoginDTO)
        {
            SessionUser sessionUser;
            if (_authService.Login(userLoginDTO, out sessionUser))
            {
                FormsAuthentication.RedirectFromLoginPage(sessionUser.Id.ToString(), userLoginDTO.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginErrors = "Invalid UserName Or Password!";
            }
            return View(userLoginDTO);
        }


        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            if (await _authService.IsUserExistst(userRegisterDTO))
            {
                ViewBag.Errors = "Username or Email Exists Before";
                return View(userRegisterDTO);
            }

            var res = await _authService.Register(userRegisterDTO);
            if (res)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(userRegisterDTO);
        }


        [HttpGet]
        [Route("Activate")]
        public async Task<ActionResult> Activate(string email, string otp)
        {
            var res = await _authService.Activate(email, otp);
            if (res == true)
            {
                TempData["SuccessMsg"] = "Your Account activated Successfully";
                return RedirectToAction("Login");
            }
            return Content("ERROR!");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Clear(); // Clear sessions
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}