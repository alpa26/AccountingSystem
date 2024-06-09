using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.Commands.Users.UserRegister;
using СontractAccountingSystem.Server.Services.Interfaces;
using СontractAccountingSystem.Server.Services;
using System.Data;
using СontractAccountingSystem.Core.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Reflection;
using System.Security.Claims;

namespace СontractAccountingSystem.Server.Features.Commands.Users.ChangeUser
{
    public class ChangeUserCommandHandler : IRequestHandler<ChangeUserCommand, RequestResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly Repository _repository;
        private readonly UserRepository _userRepository;



        public ChangeUserCommandHandler(UserManager<User> userManager, Repository repository, UserRepository userRepository)
        {
            _userManager = userManager;
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<RequestResult> Handle(ChangeUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.User.Id);
            user.UserName = request.User.Login;
            user.Email = request.User.Email;

            user.Role = null;

            var role = request.User.Role.ToString();

            var roles =  await _repository.FindListByFilterAsync<Role, string>("Name", role);
            user.RoleId = roles.First().Id;


            user.FirstName = request.User.FirstName;
            user.SecondName = request.User.SecondName;
            user.LastName = request.User.LastName;

            user.Documents.Clear();
            user.Organizations.Clear();
            user.KontrAgents.Clear();

            foreach (var doc in request.User.Documents)
                user.Documents.Add(await _repository.FindByIdAsync<Document>(doc.RelatedDocumentId));
            foreach (var org in request.User.Organizations)
                user.Organizations.Add(await _repository.FindByIdAsync<Organization>(org.Id));
            foreach (var ka in request.User.KontrAgents)
                user.KontrAgents.Add(await _repository.FindByIdAsync<KontrAgent>(ka.Id));

            try { 
                await _userManager.UpdateAsync(user);
                await _repository.SaveChangesAsync();

                var claims = await _userManager.GetClaimsAsync(user);
                await _userManager.RemoveClaimAsync(user, claims.First(x => x.Type == ClaimTypes.Role));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
            }
            catch (Exception ex) { }
            return new RequestResult(true);
        }
    }
}
