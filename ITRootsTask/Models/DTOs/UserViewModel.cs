using ITRootsTask.Models.Entities;
using PagedList;

namespace ITRootsTask.Models.DTOs
{
    public class UserViewModel
    {
        public UserFilterDTO Filter { get; set; }
        public IPagedList<User> List { get; set; }
    }
}