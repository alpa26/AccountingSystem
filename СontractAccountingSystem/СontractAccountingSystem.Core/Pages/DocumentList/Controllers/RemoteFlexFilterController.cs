using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services.Interfaces;
using СontractAccountingSystem.Core.Services;
using Salazki.Presentation;
using СontractAccountingSystem.Core.Pages.PaymentTermList;

namespace СontractAccountingSystem.Core.Pages.DocumentList.Controllers
{
    internal class RemoteFlexFilterController : Controller<DocumentListPage>
    {
        private readonly TokenType<string> _documentNumberTokenType = new("Номер документа");
        //private readonly TokenType<string> _detailNameTokenType = new("Наименование документа");
        private readonly TokenType<KontrAgentModel> _kontrAgentTokenType = new("Контрагент");
        private readonly TokenType<OrganizationModel> _organozationTokenType = new("Исполнитель");
        private readonly TokenType<int> _adoptionYearTokenType = new("Год");
        private readonly TokenType<string> _adoptionMonthTokenType = new("Месяц");

        private readonly TokenType<string> _customPatternTokenType = new("Текст");

        //private readonly TokenType<decimal> _priceTokenType = new("Сумма");



        /// <summary>
        /// Вызов любого метода Element.Filtering.SetupXXX переопределяет панель фильтрации, чтобы проверить как работатет данный способ фильтрации, нужно
        /// в CanBind вернуть true, а в XXXFilterExampleController установить CanBind => false;
        /// </summary>
        protected override bool CanStart => true;

        protected override async void Start()
        {
            Element.DataSource.LoadAsyncDelegate = LoadItems;

            Element.Filtering.SetupRemoteFlexFilter(x =>
            {
                x.RefreshTokensListsAsyncDelegate = LoadTokensLists;
                x.SearchTokensAsyncDelegate = SearchTokens;
                x.BuildNewTokensFromPatternDelegate = BuildNewToken;
            });
            Element.Filtering.HideFilterPanel();
        }

        private Task<List<Token>> BuildNewToken(string text)
        {
            var result = new List<Token>();
            result.AddRange(_customPatternTokenType.GetToken(text));
            return Task.FromResult(result);
        }

        private async Task<List<TokensList>> SearchTokens(SearchTokensRequest arg)
        {
            var possibleTokensList = new List<Token>();
            var documents = await LoadDocuments();

            foreach (var document in documents)
            {
                if (arg.UsedTokens.Any(x => x.Type == _documentNumberTokenType && x.Text == document.DocumentNumber))
                    continue;
                if (!document.DocumentNumber.Contains(arg.SearchPattern))
                    continue;
                possibleTokensList.Add(_documentNumberTokenType.GetToken(document.DocumentNumber));
            }

            var kontrAgents = await Service<IOrgStructureService>.GetInstance().LoadKontrAgents();
            foreach (var kontrAgent in kontrAgents)
            {
                if (arg.UsedTokens.Any(x => x.Type == _kontrAgentTokenType && x.Text == kontrAgent.FullName))
                    continue;
                if (!kontrAgent.FullName.ToLower().Contains(arg.SearchPattern))
                    continue;
                possibleTokensList.Add(_kontrAgentTokenType.GetToken(kontrAgent));
            }

            var organizations = await Service<IOrgStructureService>.GetInstance().LoadOrganizations();
            foreach (var organization in organizations)
            {
                if (arg.UsedTokens.Any(x => x.Type == _organozationTokenType && x.Text == organization.Name))
                    continue;
                if (!organization.Name.ToLower().Contains(arg.SearchPattern))
                    continue;
                possibleTokensList.Add(_organozationTokenType.GetToken(organization));
            }

            possibleTokensList.Sort((x, y) =>
            {
                var result = x.Type.Name.CompareTo(y.Type.Name);
                if (result != 0)
                    return result;
                return x.Text.CompareTo(y.Text);
            });
            var tokensLists = new List<TokensList>();
            TokensList current = null;
            for (int i = 0; i < possibleTokensList.Count; i++)
            {
                var token = possibleTokensList[i];
                if (current == null || current.TokenType != token.Type)
                {
                    current = new TokensList(token.Type);
                    tokensLists.Add(current);
                }
                current.Add(token);
            }
            return tokensLists;
        }

        private async Task<List<TokensList>> LoadTokensLists(IReadOnlyCollection<Token> selectedTokens)
        {

            var kontrAgentsTokens = new TokensList(_kontrAgentTokenType);
            var kontrAgents = await Service<IOrgStructureService>.GetInstance().LoadKontrAgents();
            foreach (var selectedToken in selectedTokens)
            {
                if (selectedToken is not Token<PersonModel> token || selectedToken.Type != _kontrAgentTokenType)
                    continue;
                var person = kontrAgents.FirstOrDefault(x => x.Id == token.Value.Id);
                if (person != null)
                    kontrAgents.Remove(person);
            }
            kontrAgentsTokens.AddRange(kontrAgents.Select(x => _kontrAgentTokenType.GetToken(x)));

            var organizationsTokens = new TokensList(_organozationTokenType);
            var organizations = await Service<IOrgStructureService>.GetInstance().LoadOrganizations();
            foreach (var selectedToken in selectedTokens)
            {
                if (selectedToken is not Token<PersonModel> token || selectedToken.Type != _organozationTokenType)
                    continue;
                var person = organizations.FirstOrDefault(x => x.Id == token.Value.Id);
                if (person != null)
                    organizations.Remove(person);
            }
            organizationsTokens.AddRange(organizations.Select(x => _organozationTokenType.GetToken(x)));

            var yearTokens = new TokensList(_adoptionYearTokenType);
            for (int i = 0; i < 7; i++)
            {
                var year = DateTime.Now.Year - i;
                if (selectedTokens.Where(x => x.Type == _adoptionYearTokenType).Any(x => ((Token<int>)x).Value == year))
                    continue;
                yearTokens.Add(_adoptionYearTokenType.GetToken(year));
            }

            var monthTokens = new TokensList(_adoptionMonthTokenType);
            for (int month = 1; month <= 12; month++)
            {
                if (selectedTokens.Where(x => x.Type == _adoptionMonthTokenType).Any(x => ((Token<string>)x).Value == MonthDictionary[month]))
                    continue;
                monthTokens.Add(_adoptionMonthTokenType.GetToken(MonthDictionary[month]));
            }

            return new List<TokensList> { kontrAgentsTokens, organizationsTokens, yearTokens, monthTokens };
        }

        private async Task<List<DocumentListItemModel>> LoadItems(DataRequest request)
        {
            var result = await LoadDocuments();
            var tokens = request.GetTokens();
            if (tokens.Count == 0)
                return result.ToList();
            return result.Where(model =>
            {
                foreach (var token in tokens)
                {
                    if (!MatchToken(token, model))
                        return false;
                }
                return true;
            }).ToList();
        }

        private bool MatchToken(Token token, DocumentListItemModel model)
        {
            if (token.Type == _documentNumberTokenType && (model.DocumentNumber == token.Text))
                return true;
            if (token.Type == _kontrAgentTokenType && (model.KontrAgentName?.Id == ((Token<KontrAgentModel>)token).Value.Id))
                return true;
            if (token.Type == _organozationTokenType && (model.OrganizationName?.Id == ((Token<OrganizationModel>)token).Value.Id))
                return true;
            if (token.Type == _adoptionYearTokenType && (model.DeadlineStart.Year <= ((Token<int>)token).Value)
                && ((Token<int>)token).Value <= model.DeadlineEnd.Year)
                return true;
            if (token.Type == _adoptionMonthTokenType && (model.DeadlineStart.Month <= MonthReverseDictionary[((Token<string>)token).Value])
                && (MonthReverseDictionary[((Token<string>)token).Value]) <= model.DeadlineEnd.Month)
                return true;
            if (token.Type == _customPatternTokenType && (model.Name.ToLower().Contains(token.Text)))
                return true;
            return false;
        }

        public Dictionary<int, string> MonthDictionary = new Dictionary<int, string>()
        {
            { 1, "Январь"},{ 2, "Февраль"},{ 3, "Март"},
            { 4, "Апрель"},{ 5, "Май"},{ 6, "Июнь"},
            { 7, "Июль"},{ 8, "Август"},{ 9, "Сентябрь"},
            { 10, "Октябрь"},{ 11, "Ноябрь"},{ 12, "Декабрь"},
        };

        public Dictionary<string, int> MonthReverseDictionary = new Dictionary<string, int>()
        {
            {"Январь",1 },{"Февраль",2},{"Март",3},
            {"Апрель",4},{"Май",5},{"Июнь",6},
            {"Июль",7},{"Август",8},{"Сентябрь",9},
            {"Октябрь",10},{ "Ноябрь",11},{"Декабрь",12},
        };

        public async Task<List<DocumentListItemModel>> LoadDocuments()
        {
            var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
            var response = await httpClient.GetAsync("api/documents/geteditlist");
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsAsync<IEnumerable<DocumentListItemModel>>();
                return res.OrderByDescending(x => x.CreateDate).ToList();
            }
            else return null;
        }
    }
}
