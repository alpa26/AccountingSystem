using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Features.Commands.Users.UserRegister
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, RequestResult>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        private readonly Repository _repository;
        private readonly IEmailService _emailService;



        public UserCreateCommandHandler(IMediator mediator, UserManager<User> userManager, Repository repository, IEmailService emailService)
        {
            _mediator = mediator;
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
            var fullname = request.User.FullName.Split(' ');
            var role = request.User.Role.ToString();
            var user = new User
            {
                UserName = request.User.Login,
                FirstName = fullname[1],
                SecondName = fullname[0],
                LastName = fullname[2],
                Email = request.User.Email,
                Role = null,
                RoleId = roles.First(x => x.Name == role).Id
            };
            var password = GeneratePassword();
            try
            {
                var res = await _userManager.CreateAsync(user, password);
                if (res.Succeeded)
                {
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
            string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=";
            Random random = new Random();
            int passwordLength = random.Next(13, 15);
            StringBuilder password = new StringBuilder(passwordLength);
            for (int i = 0; i < passwordLength; i++)
                password.Append(Chars[random.Next(Chars.Length)]);
            return password.ToString();
        }
    }
}
