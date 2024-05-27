using AutoMapper;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Services
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Payment, PaymentTermModel>().ReverseMap();
            CreateMap<LaborHoursModel, LaborHoursCost>().ReverseMap();
            CreateMap<LaborHoursModel, WorkedLaborHours>().ReverseMap();
            CreateMap<PersonModel, Worker>().ReverseMap();
            CreateMap<KontrAgentModel, KontrAgent>().ReverseMap();
            CreateMap<Document, ArchiveDocumentModel>().ReverseMap();
            CreateMap<RelateDocuments, RelateDocumentModel>()
            .ForMember(dest => dest.RelatedDocumentId, opt => opt.MapFrom(src => src.Document2Id));
        }
    }
}
