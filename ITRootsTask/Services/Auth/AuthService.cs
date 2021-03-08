using ITRootsTask.Context;
using ITRootsTask.Helpers;
using ITRootsTask.Models.DTOs;
using ITRootsTask.Services.Email;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ITRootsTask.Services.Auth
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public AuthService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }


        public bool Login(UserLoginDTO userLoginDTO, out SessionUser sessionUser)
        {
            sessionUser = null;
            var query = $"select top 1 * from users where Username = '{userLoginDTO.UserName}' and password = '{Hasher.ComputeSha256Hash(userLoginDTO.Password)}' and (roles = '{UserRoleEnum.Admin.ToString()}' or IsVerified = 1)";

            var user = _context.Users.SqlQuery(query).FirstOrDefault();
            if (user != null)
            {
                sessionUser = new SessionUser
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Username = user.Username,
                    Roles = user.Roles,
                };

                SessionHelper.SetSession(MyConstants.SessionUserName, JsonConvert.SerializeObject(sessionUser));
                return true;
            }
            return false;
        }

        public async Task<bool> Register(UserRegisterDTO userRegisterDTO)
        {
            string role = UserRoleEnum.User.ToString();
            var count = _context.Users.Count();
            if (count == 0) role = UserRoleEnum.Admin.ToString();

            Random rdm = new Random();
            var otp = rdm.Next(1000, 9999).ToString();

            var query = $"insert into Users (FullName, Username, Email, Phone, Password, Roles, OTP, createdOn)" +
                $" values ('{userRegisterDTO.FullName}', '{userRegisterDTO.Username}', '{userRegisterDTO.Email}', '{userRegisterDTO.Phone}', '{Hasher.ComputeSha256Hash(userRegisterDTO.Password)}', '{role}', {otp}, '{DateTime.Now}')" +
                $" select top 1 * from Users where Email = '{userRegisterDTO.Email}'";
            var res = await _context.Users.SqlQuery(query).FirstOrDefaultAsync();

            await _emailService.SendEmail(res.Email, otp);

            return res != null;
        }

        public async Task<bool> IsUserExistst(UserRegisterDTO userRegisterDTO)
        {
            return await _context.Users.AnyAsync(x => x.Username == userRegisterDTO.Username || x.Email == userRegisterDTO.Email);
        }

        public async Task<bool> Activate(string email, string otp)
        {
            var query = $"select top 1 * from users where Email = '{email}' and otp = '{otp}'";
            var user = await _context.Users.SqlQuery(query).FirstOrDefaultAsync();
            if (user != null)
            {
                string updateQuery = $"update Users set IsVerified = 1 where Email = '{email}'; select top 1 * from Users where Email = '{email}'";
                var res = await _context.Users.SqlQuery(updateQuery).FirstOrDefaultAsync();
                return res != null;
            }
            return false;
        }
    }
}