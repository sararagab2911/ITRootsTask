using ITRootsTask.Filters;
using ITRootsTask.Models.DTOs;
using ITRootsTask.Models.Entities;
using ITRootsTask.Models.Shared;
using ITRootsTask.Services.UserService;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ITRootsTask.Controllers
{
   // [Authunticate("Admin")]
    public class UserController : BaseAuthController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult> Index(UserFilterDTO filter, int page = 1)
        {
            var model = await _userService.FilterPage(filter, page);

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/User/Partial/UsersList.cshtml", model);
            }

            return View(new UserViewModel
            {
                Filter = filter,
                List = model,
            });
        }

        [Authunticate("Admin")]
        [HttpGet]
        [AjaxOnly]
        public async Task<ActionResult> UserPartial(long Id)
        {
            var model = Id == 0 ? new User() : await _userService.Get(Id);
            return PartialView("~/Views/User/Partial/UserData.cshtml", model);
        }

        [Authunticate("Admin")]
        [HttpPost]
        [AjaxOnly]
        public async Task<ActionResult> Save(User user)
        {
            var res = await _userService.Save(user);
            return Json(new Response { Success = res, Message = res ?  "Success" : "Error" });
        }

        [Authunticate("Admin")]
        [HttpPost]
        [AjaxOnly]
        public async Task<ActionResult> Delete(long Id)
        {
            var res = await _userService.Delete(Id);
            return Json(new Response { Success = res, Message = res ? "Success" : "Error" });
        }
    }
}