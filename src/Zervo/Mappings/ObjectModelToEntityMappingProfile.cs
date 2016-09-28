using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zervo.Core.Models;
using Zervo.Data.Models;

namespace Zervo.Mappings
{
    public class ObjectModelToEntityMappingProfile : Profile
    {
        public ObjectModelToEntityMappingProfile()
        {
            //Map Service Model to Entity Model

            CreateMap<CustomerObjectModel, Person>();
            CreateMap<CustomerObjectModel, Customer>()
                .ForMember(d => d.Person, 
                    opt => opt.MapFrom(s => Mapper.Map<CustomerObjectModel, Person>(s)));

        }
    }
}
