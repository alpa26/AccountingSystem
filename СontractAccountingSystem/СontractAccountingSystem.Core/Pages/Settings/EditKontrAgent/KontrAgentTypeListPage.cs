using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Pages.Settings.EditKontrAgent
{
    public class KontrAgentTypeListPage : ListPage<string>
    {
        public KontrAgentTypeListPage()
        {
            Title = "Новый КонтрАгент";
            Subtitle = "Выберите тип контрагента";
            DataSource.Fill("Физическое лицо", "Юридическое лицо");
            CreateItemPageDelegate = CreateEditPage;
        }

        private Page CreateEditPage(string selectedItem)
        {
            return new EditKontrAgentPage(selectedItem);
        }
    }
}
