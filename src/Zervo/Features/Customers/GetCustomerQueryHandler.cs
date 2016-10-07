using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zervo.Data.Models;
using Zervo.Domain.Services.Contracts;
using Zervo.ViewModels;

namespace Zervo.Features.Customers
{
    public class GetCustomerQueryHandler: IAsyncRequestHandler<GetCustomerQuery,CustomerViewModel>
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public GetCustomerQueryHandler(ICustomerService customerService, IMapper mapper )
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public Task<CustomerViewModel> Handle(GetCustomerQuery message)
        {
            var customer = _customerService.Get(message.Id);
            return Task.FromResult(_mapper.Map<Customer, CustomerViewModel>(customer));
        }
    }
}
