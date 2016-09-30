using AutoMapper;
using Zervo.Data.Models;
using Zervo.Models;

namespace Zervo.Mappings
{
    public class DataModelToViewModelMappingProfile : Profile
    {
        public DataModelToViewModelMappingProfile()
        {
            // Map Data Model to View Model

            CreateMap<Customer, CustomerViewModel>()
                .ForMember(d => d.FirstName,
                    opt => opt.MapFrom(s => s.Person.FirstName))
                .ForMember(d => d.LastName,
                    opt => opt.MapFrom(s => s.Person.LastName))
                .ForMember(d => d.Email,
                    opt => opt.MapFrom(s => s.Person.Email));
        }
    }
}
