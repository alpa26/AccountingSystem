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
        public NavigationButton WorkersButton { get; private set; }
        public NavigationButton OrganizationButton { get; private set; }

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
                CreatePageDelegate = () => new EmptyPage
                {
                    Title = "Контрагенты",
                    Text = "РАЗДЕЛ КОНТРАГЕНТОВ В РАЗРАБОТКЕ"
                }
            };

            EmployeeButton = new NavigationButton
            {
                Text = "Пользователи",
                Icon = PanelIconType.Person,
                IndicatorCounter = 25,
                IndicatorLevel = IndicatorLevel.Low,
                CreatePageDelegate = () => new EmptyPage
                {
                    Title = "Пользователи системы",
                    Text = "РАЗДЕЛ ПОЛЬЗОВАТЕЛЕЙ В РАЗРАБОТКЕ"
                }
            };

            WorkersButton = new NavigationButton
            {
                Text = "Работники",
                Icon = PanelIconType.Drawer,
                CreatePageDelegate = () => new EmptyPage
                {
                    Title = "Работники",
                    Text = "РАЗДЕЛ РАБОТНИКОВ В РАЗРАБОТКЕ"
                }
            };

            OrganizationButton = new NavigationButton
            {
                Text = "Организации",
                Icon = PanelIconType.DocumentBoxes,
                CreatePageDelegate = () => new EmptyPage
                {
                    Title = "Организации",
                    Text = "РАЗДЕЛ ОРГАНИЗАЦИЙ В РАЗРАБОТКЕ"
                }
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
                                   .AddRange(KontgAgentButton, EmployeeButton, WorkersButton, OrganizationButton);
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
