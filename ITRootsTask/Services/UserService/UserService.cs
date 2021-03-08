using ITRootsTask.Context;
using ITRootsTask.Helpers;
using ITRootsTask.Models.DTOs;
using ITRootsTask.Models.Entities;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ITRootsTask.Services.UserService
{
    public class UserService : BaseService, IUserService
    {
        private readonly ApplicationDbContext _context;


        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IPagedList<User>> FilterPage(UserFilterDTO filter, int pageNumber)
        {
            return _context.Users
                .Where(x => string.IsNullOrEmpty(filter.Search) || x.FullName.Contains(filter.Search) || x.Username.Contains(filter.Search))
                .OrderBy(x => x.Id)
                .ToPagedList(pageNumber, 10);
        }

        public async Task<User> Get(long id)
        {
            return await _context.Users.SqlQuery($"EXEC [dbo].[GetUser] {id}").FirstOrDefaultAsync();
        }

        public async Task<bool> Save(User user)
        {
            var res = await _context.SP.SqlQuery($"EXEC [dbo].[AddEditUser] {user.Id}, '{user.FullName}', '{user.Username}', '{user.Email}', " +
                $"'{(user.Password != null ? Hasher.ComputeSha256Hash(user.Password) : string.Empty)}', '{user.Phone}', {ReadSession.User.Id}").ToListAsync();
            return res != null;

            //if (user.Id == 0)
            //{
            //    user.IsVerified = true;
            //    user.createdOn = DateTime.Now;
            //    user.CreatedBy = ReadSession.User.Id;
            //    user.Roles = UserRoleEnum.User.ToString();
            //    user.Password = Hasher.ComputeSha256Hash(user.Password);
            //    _context.Users.Add(user);
            //}
            //else
            //{
            //    var dbUser = _context.Users.AsNoTracking().FirstOrDefault(x => x.Id == user.Id);
            //    user.Password = dbUser.Password;
            //    _context.Entry<User>(user).State = EntityState.Modified;
            //}
            //return await SaveChanges();
        }

        public async Task<bool> Delete(long id)
        {
            var res = await _context.SP.SqlQuery($"EXEC [dbo].[DelteUser] {id}").ToListAsync();
            return res != null;
            //var user = _context.Users.Find(id);
            //if (user != null)
            //{
            //    _context.Users.Remove(user);
            //}
            //return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}