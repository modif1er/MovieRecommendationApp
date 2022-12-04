using MRA.Domain.Services.Mail;

namespace MRA.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
