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
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.PaymentTermList;
using СontractAccountingSystem.Core.Pages.EditPaymentTerm;
using СontractAccountingSystem.Core.Pages.Settings.ListPages.UserList;

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

            services.AddSingleton<ISecurityService>(x => new SecurityService());

        }

        public override void Start()
        {
            base.Start();
            ModelManager.RegisterModelConverter<ArchiveDocumentModel, DocumentListItemModel>(x => x.ConvertToListItem());
            ModelManager.RegisterModelConverter<PaymentTermModel, PaymentTermItem>(x => x.ConvertToListItem());
            ModelManager.RegisterModelConverter<UserModel, UserListItem>(x => x.ConvertToListItem());

        }
    }
}
