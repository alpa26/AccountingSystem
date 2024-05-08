using FluentValidation;
using MediatR;
using СontractAccountingSystem.Server.Services;

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
            services.AddTransient<DocumentService>();


            services.AddAutoMapper(typeof(AppMappingProfile));

        }
    }
}
