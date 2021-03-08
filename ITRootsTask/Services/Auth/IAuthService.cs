using ITRootsTask.Helpers;
using ITRootsTask.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ITRootsTask.Services.Auth
{
    public interface IAuthService
    {
        bool Login(UserLoginDTO userLoginDTO, out SessionUser sessionUser);
        Task<bool> Register(UserRegisterDTO userRegisterDTO);
        Task<bool> IsUserExistst(UserRegisterDTO userRegisterDTO);
        Task<bool> Activate(string email, string otp);
    }
}