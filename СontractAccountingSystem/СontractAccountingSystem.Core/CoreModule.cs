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
using СontractAccountingSystem.Core.Services;
using СontractAccountingSystem.Core.Services.Interfaces;
using DK.WebClient.Core.Services;

namespace СontractAccountingSystem.Core
{
    public class CoreModule : Module
    { 
        public override void ConfigureServices(IServiceCollection services)
        {

            //services.AddSingleton<Services.INotificationsService, Services.Fakes.NotificationsService>();
            services.AddAssemblyControllers();
            services.AddSingleton<IOrgStructureService>(x => new OrgStructureService());
            services.AddSingleton<IHttpClient>(x => new SingletonHttpClient());
            services.AddSingleton<ICookieService>(x => new CookieService());
            

        }
    }
}
