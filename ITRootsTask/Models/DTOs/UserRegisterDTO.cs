using System.ComponentModel.DataAnnotations;

namespace ITRootsTask.Models.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        
        public string Phone { get; set; }
    }
}
