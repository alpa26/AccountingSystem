using Salazki.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.EditDocument;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;
using СontractAccountingSystem.Core.Pages.Settings.EditUser;

namespace СontractAccountingSystem.Core.Pages.ViewPages.Controllers
{
    internal class ViewUserPageController : Controller<ViewUserPage>
    {
        protected override void Start()
        {
            Element.EditButton.ActionDelegate = ShowEditPage;
        }

        private void ShowEditPage()
        {
            Element.Navigation.ShowPageOver(new EditUserPage(Element.Model));
        }
    }
}
