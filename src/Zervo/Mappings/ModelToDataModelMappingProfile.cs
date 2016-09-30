using AutoMapper;
using Zervo.Data.Models;
using Zervo.Models;

namespace Zervo.Mappings
{
    public class ModelToDataModelMappingProfile : Profile
    {
        public ModelToDataModelMappingProfile()
        {
            // Map Controller Model to Data Model

            CreateMap<CustomerModel, Person>();
            CreateMap<CustomerModel, Customer>()
                .ForMember(d => d.Person,
                    opt => opt.MapFrom(s => Mapper.Map<CustomerModel, Person>(s)));
        }
    }
}
