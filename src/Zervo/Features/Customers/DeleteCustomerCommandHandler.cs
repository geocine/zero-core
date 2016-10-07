using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Zervo.Domain.Services;
using Zervo.Domain.Services.Contracts;

namespace Zervo.Features.Customers
{
    public class DeleteCustomerCommandHandler : AsyncRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerService _customerService;

        public DeleteCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        protected override Task HandleCore(DeleteCustomerCommand message)
        {
            _customerService.Delete(message.Id);
            return Task.FromResult<object>(null);
        }
    }
}
