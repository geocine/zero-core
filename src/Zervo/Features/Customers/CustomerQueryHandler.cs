using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zervo.Data.Models;
using Zervo.Domain.Services.Contracts;
using Zervo.Models;

namespace Zervo.Features.Customers
{
    public class CustomerQueryHandler: IAsyncRequestHandler<CustomerQuery,CustomerViewModel>
    {
        private ICustomerService _customerService;
        private IMapper _mapper;
        public CustomerQueryHandler(ICustomerService customerService, IMapper mapper )
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public async Task<CustomerViewModel> Handle(CustomerQuery message)
        {
            // Implement Async
            var customer = _customerService.Get(message.Id);
            return _mapper.Map<Customer, CustomerViewModel>(customer);
        }
    }
}
