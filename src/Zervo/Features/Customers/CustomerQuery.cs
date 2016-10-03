using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Zervo.Models;

namespace Zervo.Features.Customers
{
    public class CustomerQuery: IAsyncRequest<CustomerViewModel>
    {
        public int Id { get; set; }
    }
}
