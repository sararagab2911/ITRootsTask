using System.ComponentModel.DataAnnotations;

namespace ITRootsTask.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}