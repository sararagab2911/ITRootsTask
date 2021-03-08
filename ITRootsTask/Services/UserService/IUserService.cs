using ITRootsTask.Models.DTOs;
using ITRootsTask.Models.Entities;
using PagedList;
using System.Threading.Tasks;

namespace ITRootsTask.Services.UserService
{
    public interface IUserService
    {
        Task<IPagedList<User>> FilterPage(UserFilterDTO search, int pageNumber);
        Task<User> Get(long id);
        Task<bool> Save(User user);
        Task<bool> Delete(long id);
    }
}
