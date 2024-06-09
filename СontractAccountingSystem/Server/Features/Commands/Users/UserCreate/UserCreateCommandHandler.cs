using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Features.Commands.Users.UserRegister
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, RequestResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly Repository _repository;
        private readonly IEmailService _emailService;



        public UserCreateCommandHandler( UserManager<User> userManager, Repository repository, IEmailService emailService)
        {
            _userManager = userManager;
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<RequestResult> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var check = await _userManager.FindByNameAsync(request.User.Login);
            if (check != null)
                return new RequestResult(false, "IsExist");

            var roles = await _repository.FindListAsync<Role>();
            var kontragents = await _repository.FindListAsync<KontrAgent>();
            var orgs = await _repository.FindListAsync<Organization>();

            var role = request.User.Role.ToString();
            var user = new User
            {
                UserName = request.User.Login,
                Email = request.User.Email,
                Role = null,
                RoleId = roles.First(x => x.Name == role).Id,
            };
            
            user.FirstName = request.User.FirstName;
            user.SecondName = request.User.SecondName;
            user.LastName = request.User.LastName;

            foreach (var doc in request.User.Documents)
                user.Documents.Add(await _repository.FindByIdAsync<Document>(doc.RelatedDocumentId));
            foreach (var org in request.User.Organizations)
                user.Organizations.Add(orgs.First(x => x.Id == org.Id));
            foreach (var ka in request.User.KontrAgents)
                user.KontrAgents.Add(kontragents.First(x=> x.Id == ka.Id));

            var password = GeneratePassword();
            try
            {
                var res = await _userManager.CreateAsync(user, password);
                if (res.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
                    await _emailService.SendEmailAsync(
                        user.Email,
                        "Данные для входа",
                        $"Логин и пароль для входа в систему:\n Логин:{user.UserName}; \n Пароль:{password}."
                        );
                    return new RequestResult(true);
                }
            }
            catch (Exception ex){}
            return new RequestResult(false, "NotSucceeded");
        }

        public static string GeneratePassword()
        {
            string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!";
            Random random = new Random();
            int passwordLength = random.Next(13, 15);
            StringBuilder password = new StringBuilder(passwordLength);
            for (int i = 0; i < passwordLength; i++)
                password.Append(Chars[random.Next(Chars.Length)]);
            return password.ToString();
        }
    }
}
