using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Server.Features.Commands.Organizations.CreateOrganization
{
    public class CreateOrganizationCommand : IRequest<RequestResult>
    {
        public OrganizationModel OrganizationModel { get; set; }
        public CreateOrganizationCommand(OrganizationModel org)
        {
            OrganizationModel = org;
        }
    }
}
