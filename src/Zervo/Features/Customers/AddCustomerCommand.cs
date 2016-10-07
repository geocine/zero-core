using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Zervo.ViewModels;

namespace Zervo.Features.Customers
{
    public class AddCustomerCommand : IAsyncRequest<int>
    {
        public CustomerViewModel CustomerViewModel { get; set; }
    }
}
