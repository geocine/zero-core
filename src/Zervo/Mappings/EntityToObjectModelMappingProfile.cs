using AutoMapper;
using Zervo.Core.Models;
using Zervo.Data.Models;

namespace Zervo.Mappings
{
    public class EntityToObjectModelMappingProfile : Profile
    {
        public EntityToObjectModelMappingProfile()
        {
            // Map Entity Model to Service Model

            CreateMap<Customer, CustomerObjectModel>()
                .ForMember(d => d.FirstName,
                    opt => opt.MapFrom(s => s.Person.FirstName))
                .ForMember(d => d.LastName,
                    opt => opt.MapFrom(s => s.Person.LastName))
                .ForMember(d => d.Email,
                    opt => opt.MapFrom(s => s.Person.Email));
        }
    }
}
