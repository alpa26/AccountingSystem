using Salazki.Presentation.Elements;
using System.Net.Http.Json;
using System.Net.Http;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;
using СontractAccountingSystem.Core.Pages.Logon;
using СontractAccountingSystem.Core.Pages.DocumentList;
using СontractAccountingSystem.Core.Pages.DocumentTypesList;
using СontractAccountingSystem.Core.Pages.PaymentTermList;
using СontractAccountingSystem.Core.Pages.Settings;
using СontractAccountingSystem.Core.Pages.Settings.EditUser;

namespace СontractAccountingSystem.Core.Pages.Main
{
    public class MainPage : RootPage
    {
        public NavigationButton AddButton { get; private set; }
        public NavigationButton NotificationsMenuButton{ get; private set; }
        public NavigationButton DocumentsButton { get; private set; }
        public NavigationButton PaymentsButton{ get; private set; }


        // Settings buttons
        public NavigationButton KontgAgentButton { get; private set; }
        public NavigationButton EmployeeButton { get; private set; }
        public NavigationButton Setting { get; private set; }

        public MainPage()
        {

            NotificationsMenuButton = new NavigationButton
            {
                Text = "Уведомления",
                Icon = PanelIconType.Bell,
                CreatePageDelegate = () => new EmptyPage
                {
                    Title = "Уведомления",
                    Text = "РАЗДЕЛ УВЕДОМЛЕНИЙ В РАЗРАБОТКЕ"
                }
            };

            DocumentsButton = new NavigationButton
            {
                Text = "Договоры",
                Icon = PanelIconType.Pile,
                IndicatorCounter = 0,
                IndicatorLevel = IndicatorLevel.None,
                CreatePageDelegate = () => new DocumentListPage()
            };

            PaymentsButton = new NavigationButton
            {
                Text = "Оплаты",
                Icon = PanelIconType.Timer,
                IndicatorCounter = 0,
                IndicatorLevel = IndicatorLevel.None,
                CreatePageDelegate = () => new PaymentTermListPage()
            };

            AddButton = new NavigationButton
            {
                Text = "Новый договор",
                Icon = PanelIconType.Plus,
                CreatePageDelegate = () => new DocumentTypesListPage()
            };


            // Settings

            KontgAgentButton = new NavigationButton
            {
                Text = "Контрагенты",
                Icon = PanelIconType.Book,
                IndicatorCounter = 5,
                IndicatorLevel = IndicatorLevel.High,
                CreatePageDelegate = () => new KontrAgentTabsPage()
            };

            EmployeeButton = new NavigationButton
            {
                Text = "Пользователи",
                Icon = PanelIconType.Person,
                IndicatorCounter = 25,
                IndicatorLevel = IndicatorLevel.Low,
                CreatePageDelegate = () => new UserTypeListPage()
            };

            Setting = new NavigationButton
            {
                Text = "Сущности",
                Icon = PanelIconType.Pile,
                CreatePageDelegate = () => new SettingsPage()
            };


            NavigationPanel.NavigationButtons
                .AddRange(AddButton, NotificationsMenuButton, DocumentsButton, PaymentsButton);

            //FeedbackItem.Selected = true;
            DocumentsButton.Selected = true;

            AddToolbar();
        }

        private void AddToolbar()
        {
            BarButton supportButton = new BarButton(PanelIconType.Gear);
            BarButton docButton = new BarButton(PanelIconType.DocumentBox);
           
            

            var infoButton = new BarButton(PanelIconType.Help)
            {
                Hint = "Справка",
                CreatePageDelegate = () => new EmptyPage
                {
                    Title = "Справка",
                    Text = "РАЗДЕЛ СПРАВКА В РАЗРАБОТКЕ"
                }
            };
            var logoutButton = new BarButton(PanelIconType.ArrowBack)
            {
                Hint = "Выход",
                ActionDelegate = async () => {
                    var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
                    var response = await httpClient.PostAsync("api/auth/logout", null);
                    Application.Current.Logoff();
                },
            };
            supportButton.VisibleIfInRole("admin");

            supportButton = new BarButton(PanelIconType.Gear)
            {
                Hint = "Настройки",
                ActionDelegate = () =>
                {
                    NavigationPanel.NavigationButtons.Clear();
                    NavigationPanel.NavigationButtons
                                   .AddRange(KontgAgentButton, EmployeeButton, Setting);
                    KontgAgentButton.Selected = true;

                    Application.Current.Bar.Buttons.Clear();
                    Application.Current.Bar.Buttons.AddRange(docButton, infoButton, logoutButton);
                }
            };

            docButton = new BarButton(PanelIconType.DocumentBox)
            {
                Hint = "Документы",
                ActionDelegate = () =>
                {
                    NavigationPanel.NavigationButtons.Clear();
                    NavigationPanel.NavigationButtons
                                   .AddRange(AddButton, NotificationsMenuButton, DocumentsButton, PaymentsButton);
                    DocumentsButton.Selected = true;

                    Application.Current.Bar.Buttons.Clear();
                    Application.Current.Bar.Buttons.AddRange(supportButton, infoButton, logoutButton);
                }
            };


            //Application.Current.Bar.Buttons.Add(profileButton);
            Application.Current.Bar.Buttons.Clear();
            Application.Current.Bar.Buttons.AddRange(supportButton, infoButton,logoutButton);
        }
    }
}
