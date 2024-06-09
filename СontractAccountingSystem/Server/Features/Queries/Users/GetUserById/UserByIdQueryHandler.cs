using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Salazki.Presentation.Elements;
using System.Xml.Linq;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Queries.Users.GetUserById
{
    public class UserByIdQueryHandler : IRequestHandler<UserByIdQuery, UserModel>
    {
        private readonly Repository _repository;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserByIdQueryHandler(Repository repository, UserRepository userRepository,IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper; 
        }

        public async Task<UserModel> Handle(UserByIdQuery request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.FindByIdAsync(request.Id);
            var role = await _repository.FindByIdAsync<Role>(user.RoleId);
            var res = new UserModel()
            {
                Id = user.Id,
                Login = user.UserName,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Role = (RoleEnum)Enum.Parse(typeof(RoleEnum), role.Name)
            };
            foreach(var item in user.Documents)
            {
                res.Documents.Add(new RelateDocumentModel()
                {
                    RelatedDocumentId = item.Id,
                    DocumentName = item.Name,
                    DocumentNumber = item.Number,
                }) ;
            }
            foreach (var item in user.KontrAgents)
            {
                var model = _mapper.Map<KontrAgentModel>(item);
                model.ContactPersonName = item.ContactPerson;
                var type = await _repository.FindByIdAsync<KontrAgentType>(item.TypeId);
                model.Type = type.Name;
                res.KontrAgents.Add(model);
            }

            foreach (var item in user.Organizations)
                res.Organizations.Add(_mapper.Map<OrganizationModel>(item));

            return res;
        }
    }
}
