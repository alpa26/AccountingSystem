using Microsoft.Extensions.DependencyInjection;
using Salazki.Presentation;
using Salazki.Services;
using Salazki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using СontractAccountingSystem.Core.Data;

namespace СontractAccountingSystem.Core
{
    public class CoreModule : Module
    {
        //public CoreModule(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }
        
        public override void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<Services.INotificationsService, Services.Fakes.NotificationsService>();
            services.AddAssemblyControllers();
            //IConfiguration Configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql("Server=localhost;Port=5432;Database=;User Id=;Password=;");
            });

            //using (var services1 = services.BuildServiceProvider())
            //{
            //    var context = services1.GetRequiredService<AppDbContext>();
            //    context.Database.Migrate();
            //}

        }
    }
}
