using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zervo.Domain.Services.Contracts;
using Zervo.Mappings;
using Zervo.ViewModels;

namespace Zervo.Features.Customers
{
    public class GetCustomerListQueryHandler: IAsyncRequestHandler<GetCustomerListQuery, IEnumerable<CustomerViewModel>>
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public GetCustomerListQueryHandler(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public Task<IEnumerable<CustomerViewModel>> Handle(GetCustomerListQuery message)
        {
            var customers = _customerService.List();
            var customerList = customers.Select(x => x.ToViewModel(_mapper)).AsEnumerable();
            return Task.FromResult(customerList);
        }
    }
}
