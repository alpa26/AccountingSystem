using Microsoft.Extensions.DependencyInjection;
using Salazki.Presentation;
using Salazki.Services;
using Salazki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Server
{
    public class CoreModule : Module
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<Services.INotificationsService, Services.Fakes.NotificationsService>();
            services.AddAssemblyControllers();
        }
    }
}
