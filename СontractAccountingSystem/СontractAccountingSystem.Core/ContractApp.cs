using Salazki.Presentation.Elements;
using СontractAccountingSystem.Core;
using СontractAccountingSystem.Core.Pages;
using СontractAccountingSystem.Core.Pages.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core
{
    public class ContractApp : Application
    {
        public ContractApp()
        {
            Title = "Система";
            AccentColor = Color.Cerulean;
            PreloadDelegate = Preload;
        }

        private Task Preload()
        {
            //Таким образом можно разогревать приложение без "заморозки" UI
            return Task.CompletedTask;
            /*
            await OrgStructure.OrgStructureApi.Start(options =>
            {
                options.ConnectToProductionServer();
                options.EnableSearchView();
            });
            */
        }

        protected override RootPage CreateRootPage()
        {
            return new MainPage();
        }

        protected override LogonPage CreateLogonPage()
        {
            return new AppLogonPage();
        }
    }
}
