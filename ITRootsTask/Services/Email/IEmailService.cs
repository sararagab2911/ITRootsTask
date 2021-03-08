using System.Threading.Tasks;

namespace ITRootsTask.Services.Email
{
    public interface IEmailService
    {
        Task SendEmail(string to, string otp);
    }
}