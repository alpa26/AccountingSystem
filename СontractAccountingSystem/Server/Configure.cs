using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using СontractAccountingSystem.Server.Services;
using СontractAccountingSystem.Server.Services.Interfaces;
using СontractAccountingSystem.Server.Settings;

namespace СontractAccountingSystem.Server
{
    public static class Configure
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly1 = typeof(Configure).Assembly;

            services.AddMediatR(assembly1);
            services.AddValidatorsFromAssembly(assembly1);

            services.AddTransient<Repository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<DocumentService>();
            services.AddSingleton<IEmailService, EmailService>();


            services.AddAutoMapper(typeof(AppMappingProfile));

        }
    }
}
