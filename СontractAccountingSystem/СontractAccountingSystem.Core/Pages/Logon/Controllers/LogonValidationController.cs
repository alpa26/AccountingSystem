using Salazki.Presentation;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Pages.Logon.Controllers
{
    internal class LogonValidationController : Controller<AppLogonPage>
    {
        protected override void Start()
        {
            Element.Login.ValidationRules.Add(x =>
            {
                if(x.Text.Length<4)
                    return ValidationResult.Error("Логин должнен быть не менее 4 символов");
                return ValidationResult.Valid;
            });

            Element.Password.ValidationRules.Add(x =>
            {
                if (x.Text.Length >= 8) 
                {
                    if(x.Text.Any(Char.IsLower))
                    {
                        if (x.Text.Any(Char.IsUpper))
                        {
                            if (x.Text.Any(Char.IsDigit))
                            {
                                return ValidationResult.Valid;
                            }
                            else
                                return ValidationResult.Error("Пароль должнен содержать цифры");
                        } else
                            return ValidationResult.Error("Пароль должнен содержать заглавные буквы");
                    } else
                        return ValidationResult.Error("Пароль должнен содержать строчные буквы 'a-z'");
                } else
                    return ValidationResult.Error("Пароль должнен быть не менее 8 символов");
            });
        }
    }
}
