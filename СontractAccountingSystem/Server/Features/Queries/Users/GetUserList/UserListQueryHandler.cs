using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Salazki.Security;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.Users.GetUsersList
{
    public class UserListQueryHandler : IRequestHandler<UserListQuery, List<UserModel>>
    {
        private readonly Repository _repository;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserListQueryHandler(Repository repository,UserRepository userRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserModel>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            var items = await _userRepository.FindAsync();
            var res = new List<UserModel>();
            foreach (var item in items) {
                var role = await _repository.FindByIdAsync<Role>(item.RoleId);
                var user = new UserModel()
                {
                    Id = item.Id,
                    Login = item.UserName,
                    FirstName = item.FirstName,
                    SecondName = item.SecondName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Phone = item.PhoneNumber,
                    Role = (RoleEnum)Enum.Parse(typeof(RoleEnum), role.Name)
                };
                foreach (var doc in item.Documents)
                {
                    user.Documents.Add(new RelateDocumentModel()
                    {
                        RelatedDocumentId = doc.Id,
                        DocumentName = doc.Name,
                        DocumentNumber = doc.Number,
                    }) ;
                }
                foreach (var ka in item.KontrAgents)
                {
                    var model = _mapper.Map<KontrAgentModel>(ka);
                    model.ContactPersonName = ka.ContactPerson;
                    var type = await _repository.FindByIdAsync<KontrAgentType>(ka.TypeId);
                    model.Type = type.Name;
                    user.KontrAgents.Add(model);
                }
                foreach (var org in item.Organizations)
                    user.Organizations.Add(_mapper.Map<OrganizationModel>(org));

                res.Add(user);

            }
            return res;
        }
    }
}
