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
    public class UpdateCustomerCommanHandler : AsyncRequestHandler<UpdateCustomerCommand>
    {

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public UpdateCustomerCommanHandler(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        protected override Task HandleCore(UpdateCustomerCommand message)
        {
            var customer = message.CustomerViewModel.ToDataModel(_mapper);
            customer.Id = message.Id;
            _customerService.Update(customer);
            return Task.FromResult<object>(null);
        }
    }
}
