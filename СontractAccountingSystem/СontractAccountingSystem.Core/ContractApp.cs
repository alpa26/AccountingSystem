using Salazki.Presentation.Elements;
using СontractAccountingSystem.Core;
using СontractAccountingSystem.Core.Pages;
using СontractAccountingSystem.Core.Pages.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK.WebClient.Core.Services;
using System.Net;
using СontractAccountingSystem.Core.Pages.Logon;
using СontractAccountingSystem.Core.Services;

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

            /*
             Страница регистрации настроена только для добавления пользователя.
             Чтобы сделать автоматический код с куки нужно раскомментировать Close(); в AppRegisterPage()
             и добавить HttpContext.SignInAsync(...) в контроллер регистрации. 
            */
            //return new AppRegisterPage();
        }

        protected override async Task<bool> TryAuthorize()
        {
            SecurityService.UserRole = "admin";
            return true;
            var cookieService = Service<ICookieService>.GetInstance();
            var sid = await cookieService.GetValue(".AspNetCore.Cookies");
            try
            {
                if ((!string.IsNullOrEmpty(sid)))
                {
                    return true;
                }
            }
            catch(HttpRequestException ex) 
            {
                if(ex.StatusCode is HttpStatusCode.Unauthorized)
                {
                    await cookieService.SetValue(".AspNetCore.Cookies", null);
                }
                else
                {
                    throw;
                } 
            }
            return false;
        }
    }
}
