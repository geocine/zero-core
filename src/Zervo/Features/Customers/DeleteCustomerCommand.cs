using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Zervo.Features.Customers
{
    public class DeleteCustomerCommand: IAsyncRequest
    {
        public int Id { get; set; }
    }
}
