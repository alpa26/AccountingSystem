using System;
using System.Linq;
using System.Threading.Tasks;
using Salazki.Presentation;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.LaborHours.Controllers
{
    internal class EditLaborHoursController : Controller<EditLaborHoursPage>
    {
        protected override void Start()
        {
            Element.UpdateModelDelegate = UpdateModel;
            Element.SaveDelegate = Save;
        }

        private LaborHoursModel UpdateModel()
        {                
            var model = new LaborHoursModel { Id =  Element.Model.Id };
            model.HourlyRate = Element.HourlyRate.Value;
            model.Hours = Element.Hours.Value;
            model.FullAmount = Element.FullAmount.Value;
            model.WorkerName = Element.WorkerName.Value;
            return model;
        }
        
        private async Task Save(LaborHoursModel model)
        {
            await Task.Delay(200);
        }
    }
}
