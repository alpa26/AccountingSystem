using FluentValidation;
using MediatR;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server
{
    public static class Configure
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(Configure).Assembly;

            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient<Repository>();
        }
    }
}
