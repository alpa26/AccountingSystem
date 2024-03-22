using Salazki.Presentation;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Pages.Logon.Controllers
{
    internal class RegValidationController : Controller<AppRegisterPage>
    {
        protected override void Start()
        {
            Element.Login.ValidationRules.Add(x =>
            {
                if (x.Text.Length < 4)
                    return ValidationResult.Error("Логин должен быть не менее 4 символов");
                return ValidationResult.Valid;
            });

            Element.FullName.ValidationRules.Add(x =>
            {
                if(!Regex.IsMatch(x.Text, @"^\p{L}+\s\p{L}+\s\p{L}+$"))
                    return ValidationResult.Warning("Введите ФИО через пробел");
                return ValidationResult.Valid;
            });

            Element.Email.ValidationRules.Add(x =>
            {
                Regex emailRegex = new Regex(@"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@([a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$");
                if (!emailRegex.IsMatch(x.Text.ToLower()))
                    return ValidationResult.Error("Почта должна соответствовать формату: 'some@mail.ru'");
                return ValidationResult.Valid;
            });

            Element.Password.ValidationRules.Add(x =>
            {
                if (x.Text.Length >= 8)
                {
                    if (x.Text.Any(Char.IsLower))
                    {
                        if (x.Text.Any(Char.IsUpper))
                        {
                            if (x.Text.Any(Char.IsDigit))
                            {
                                return ValidationResult.Valid;
                            }
                            else
                                return ValidationResult.Error("Пароль должнен содержать цифры");
                        }
                        else
                            return ValidationResult.Error("Пароль должнен содержать заглавные буквы");
                    }
                    else
                        return ValidationResult.Error("Пароль должнен содержать строчные буквы 'a-z'");
                }
                else
                    return ValidationResult.Error("Пароль должнен быть не менее 8 символов");
            });
        }
    }
}
