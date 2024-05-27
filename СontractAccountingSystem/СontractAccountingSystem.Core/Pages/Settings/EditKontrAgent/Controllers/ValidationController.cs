using Salazki.Presentation;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Pages.Logon;

namespace СontractAccountingSystem.Core.Pages.Settings.EditKontrAgent.Controllers
{
    internal class ValidationController : Controller<EditKontrAgentPage>
    {
        protected override void Start()
        {
            Element.INN.ValidationRules.Add(x =>
            {
                int minLength;
                if (Element.Type == "Юридическое лицо")
                    minLength = 10;
                else minLength = 12;
                if (x.Text.Length < minLength)
                    return ValidationResult.Error($"ИНН должен быть не менее {minLength} символов");
                if (!Regex.IsMatch(x.Text, @"^[0-9]+$"))
                    return ValidationResult.Error($"ИНН должен состоять из цифр");
                return ValidationResult.Valid;
            });

            //Element.KPP.ValidationRules.Add(x =>
            //{
            //    if (Element.Type == "Юридическое лицо") { 
            //            int minLength = 9;
            //        if (x.Text.Length < minLength)
            //            return ValidationResult.Error($"КПП должен быть не менее {minLength} символов");
            //        if (!Regex.IsMatch(x.Text, @"^[0-9]+$"))
            //            return ValidationResult.Error($"КПП должен состоять из цифр");
            //    }
            //    return ValidationResult.Valid;

            //});

            Element.OGRN.ValidationRules.Add(x =>
            {
                int minLength;
                if (Element.Type == "Юридическое лицо")
                    minLength = 13;
                else minLength = 15;
                if (x.Text.Length < minLength)
                    return ValidationResult.Error($"Рег. номер должен быть не менее {minLength} символов");
                if (!Regex.IsMatch(x.Text, @"^[0-9]+$"))
                    return ValidationResult.Error($"Рег.номер должен состоять из цифр");
                return ValidationResult.Valid;
            });

            Element.FullName.ValidationRules.Add(x =>
            {
                if (Element.Type == "Физическое лицо")
                    if (!Regex.IsMatch(x.Text, @"^\p{L}+\s\p{L}+\s\p{L}+$"))
                        return ValidationResult.Warning("Введите ФИО через пробел");
                return ValidationResult.Valid;
            });

            Element.ContactPersonName.ValidationRules.Add(x =>
            {
                if (Element.Type == "Юридичесоке лицо")
                    if (!Regex.IsMatch(x.Text, @"^\p{L}+\s\p{L}+\s\p{L}+$"))
                        return ValidationResult.Warning("Введите ФИО через пробел");
                return ValidationResult.Valid;
            });

            Element.ContactPhone.ValidationRules.Add(x =>
            {
                if (Element.Type == "Юридичесоке лицо")
                    if (!Regex.IsMatch(x.Text, @"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$"))
                        return ValidationResult.Warning("Введите корректный телефон");
                return ValidationResult.Valid;
            });

            //Element.ContactEmail.ValidationRules.Add(x =>
            //{
            //    Regex emailRegex = new Regex(@"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@([a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$");
            //    if (!emailRegex.IsMatch(x.Text.ToLower()))
            //        return ValidationResult.Error("Почта должна соответствовать формату: 'some@mail.ru'");
            //    return ValidationResult.Valid;
            //});
        }
    }
}
