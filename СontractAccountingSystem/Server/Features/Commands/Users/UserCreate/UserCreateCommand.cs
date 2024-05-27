using MediatR;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Controllers;
using СontractAccountingSystem.Server.Entities;
using static СontractAccountingSystem.Server.Controllers.AuthController;

namespace СontractAccountingSystem.Server.Features.Commands.Users.UserRegister
{
    public class UserCreateCommand : IRequest<RequestResult>
    {
        public UserModel User { get; set; }
        public UserCreateCommand(UserModel user)
        {
            User = user;
        }
    }
}
