using Salazki.Presentation.Elements;
using System.Net.Http.Json;
using System.Net.Http;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;
using СontractAccountingSystem.Core.Pages.Logon;

namespace СontractAccountingSystem.Core.Pages.Main
{
    public class MainPage : RootPage
    {
        public NavigationButton AddButton { get; private set; }
        public NavigationButton NotificationsMenuItem { get; private set; }
        public NavigationButton ArchiveMenuItem { get; private set; }
        public NavigationButton FeedbackItem { get; private set; }
        public NavigationButton EmployersItem { get; private set; }
        public NavigationButton DocsItem { get; private set; }
        public NavigationButton Settings { get; private set; }

        public NavigationButton KontrAgentIcon { get; private set; }
        public NavigationButton Calendar { get; private set; }


        public MainPage()
        {
            NotificationsMenuItem = new NavigationButton
            {
                Text = "Уведомления",
                Icon = PanelIconType.Bell,
                IndicatorCounter = 5,
                IndicatorLevel = IndicatorLevel.High,
                //CreatePageDelegate = () => new NotificationsPage()
            };

            Calendar = new NavigationButton
            {
                Text = "Финансовый план",
                Icon = PanelIconType.Timer,
                IndicatorCounter = 25,
                IndicatorLevel = IndicatorLevel.Low,
                //CreatePageDelegate = () => new ArchivePage()
            };

            EmployersItem = new NavigationButton
            {
                Text = "Сотрудники",
                Icon = PanelIconType.Person,
                //CreatePageDelegate = () => new StylesPage()
            };

            DocsItem = new NavigationButton
            {
                Text = "Документы",
                Icon = PanelIconType.Pile,
                //CreatePageDelegate = () => new StylesPage()
            };


            Settings = new NavigationButton
            {
                Text = "Настройки",
                Icon = PanelIconType.HorizontalPlates,
                //CreatePageDelegate = () => new StylesPage()
            };
            //OrgStructure = new NavigationButton
            //{
            //    Text = "Оргструкутра",
            //    Icon = PanelIconType.Set,
            //    CreatePageDelegate = () => new OrgStructureTree.OrgStructurePage()
            //};

            KontrAgentIcon = new NavigationButton
            {
                Text = "Контрагенты",
                Icon = PanelIconType.Book,
                //CreatePageDelegate = () => new Calendar.CalendarSamplesListPage()
            };

            AddButton = new NavigationButton
            {
                Text = "Новый договор",
                Icon = PanelIconType.Plus,
                //CreatePageDelegate = () => new DocumentTypesListPage()
            };

            FeedbackItem = new NavigationButton("Техподдержка");

            NavigationPanel.NavigationButtons
                .AddRange(AddButton, DocsItem, NotificationsMenuItem, Calendar, KontrAgentIcon, EmployersItem, Settings);

            //FeedbackItem.Selected = true;
            //ArchiveMenuItem.Selected = true;

            AddToolbar();
        }

        private void AddToolbar()
        {
            /*
            var profileButton = new BarComboButton<PersonModel>(PanelIconType.User)
            {
                Items = OrgStructureService.Persons.ToArray()
            };
            profileButton.RegisterBuildItemDelegate(x => new ToolbarProfileItem(x));
            profileButton.SelectedModel = profileButton.Items[0];
            profileButton.IndicatorLevel = IndicatorLevel.High;
            */
            var supportButton = new BarButton(PanelIconType.Message)
            {
                Hint = "Техподдержка",
                CreatePageDelegate = () => new EmptyPage
                {
                    Title = "Техподдержка",
                    Text = "РАЗДЕЛ ТЕХПОДДЕРЖКА В РАЗРАБОТКЕ"
                }
            };
            var infoButton = new BarButton(PanelIconType.Help)
            {
                Hint = "Справка",
                CreatePageDelegate = () => new EmptyPage
                {
                    Title = "Справка",
                    Text = "РАЗДЕЛ СПРАВКА В РАЗРАБОТКЕ"
                }
            };
            var logoutButton = new BarButton(PanelIconType.None)
            {
                Hint = "Выход",
                ActionDelegate = async () => {
                    var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
                    var response = await httpClient.PostAsync("api/auth/logout", null);
                    Application.Current.Logoff();
                },
        };

            //Application.Current.Bar.Buttons.Add(profileButton);
            Application.Current.Bar.Buttons.AddRange(supportButton, infoButton,logoutButton);
        }
    }
}
