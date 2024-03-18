using FluentValidation;
using MediatR;
using СontractAccountingSystem.Core;
using СontractAccountingSystem.Core.Services;

namespace СontractAccountingSystem.Server
{
    public static class Configure
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly1 = typeof(Configure).Assembly;
            var assembly2 = typeof(CoreConfigure).Assembly;

            services.AddMediatR(assembly1, assembly2);
            services.AddValidatorsFromAssembly(assembly1);
            services.AddValidatorsFromAssembly(assembly2);

            services.AddTransient<Repository>();
        }
    }
}
