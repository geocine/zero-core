using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zervo.Domain.Services.Contracts;
using Zervo.Mappings;

namespace Zervo.Features.Customers
{
    public class AddCustomerCommandHandler : IAsyncRequestHandler<AddCustomerCommand,int>
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public AddCustomerCommandHandler(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public Task<int> Handle(AddCustomerCommand message)
        {
            var customer = message.CustomerViewModel.ToDataModel(_mapper);
            return Task.FromResult(_customerService.Create(customer));
        }
    }
}
