using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Zervo.Data.Models;
using Zervo.ViewModels;

namespace Zervo.Mappings
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            // Data Model to Controller View Model

            CreateMap<Customer, CustomerViewModel>()
                .ForMember(d => d.FirstName,
                    opt => opt.MapFrom(s => s.Person.FirstName))
                .ForMember(d => d.LastName,
                    opt => opt.MapFrom(s => s.Person.LastName))
                .ForMember(d => d.Email,
                    opt => opt.MapFrom(s => s.Person.Email));

            //  Controller View Model to Data Model

            CreateMap<CustomerViewModel, Person>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CustomerViewModel, Customer>()
                .ForMember(d => d.Person,
                    opt => opt.MapFrom(
                        s => Mapper.Map<CustomerViewModel, Person>(s)
                    )
                )
                .ForMember(x => x.Id, opt => opt.Ignore());

        }
    }


    public static class CustomerExtensions
    {
        public static Customer ToDataModel(this CustomerViewModel customerViewModel, IMapper mapper)
        {
            var customer = mapper.Map<CustomerViewModel, Customer>(customerViewModel);
            return customer;
        }

        public static CustomerViewModel ToViewModel(this Customer customer, IMapper mapper)
        {
            return mapper.Map<Customer, CustomerViewModel>(customer);
        }
    }
}
