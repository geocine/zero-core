using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zervo.Core.Models;
using Zervo.Models;

namespace Zervo.Mappings
{
    public class ModelToObjectModelMappingProfile : Profile
    {
        public ModelToObjectModelMappingProfile()
        {
            // Map Controller Model to Service Model

            CreateMap<CustomerModel, CustomerObjectModel>();
        }
    }
}
