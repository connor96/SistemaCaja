using Microsoft.AspNetCore.Identity.UI.Services;

namespace CajaSistema.Helpers
{
    public class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
