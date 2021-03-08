namespace ITRootsTask.Models.Entities
{
    public class User : Entity
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Roles { get; set; }

        public bool IsVerified { get; set; }
        public string OTP { get; set; }
    }
}
