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
            CreateMap<Payment, Payment>();

            CreateMap<Document, ArchiveDocumentModel>().ReverseMap();

        }
    }
}
