using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MRA.Application.Contracts.Infrastructure;
using MRA.Domain.Services.Mail;
using MRA.Infrastructure.Mail;

namespace MRA.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
