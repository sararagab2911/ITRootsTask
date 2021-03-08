using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ITRootsTask.Services.Email
{
    public class EmailService : BaseService, IEmailService
    {
        public EmailService()
        {

        }


        public async Task SendEmail(string to, string otp)
        {
            await SmtpClientSend(to, otp);
        }


        private async Task SmtpClientSend(string to, string otp)
        {
            try
            {
                var host = $"https://{HttpContext.Current.Request.Url.Host}:{HttpContext.Current.Request.Url.Port}";
                MailMessage msg = new MailMessage(new MailAddress("itrootstask@gmail.com"), new MailAddress(to))
                {
                    Subject = "Activate Account",
                    Body = $"Click On This Link To Activate You Account <br /> <a style='font-weight:bold;' href='{host}/Auth/Activate?otp={otp}&email={to}'>Click Me</a>",
                    IsBodyHtml = true,
                };
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("itrootstask@gmail.com", "itrootstaskP@ssrd");
                client.EnableSsl = true;
                await client.SendMailAsync(msg);
            }
            catch (Exception ex) { }
        }

    }
}
