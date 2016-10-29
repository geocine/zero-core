using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Zervo.Data.Models;
using Zervo.Domain.Services.Contracts;
using Zervo.ViewModels;

namespace Zervo.GraphQL
{
    public class CustomerType : ObjectGraphType<CustomerViewModel>
    {
        public CustomerType(ICustomerService customerService)
        {
            Name = "Customer";
            Description = "This is the customer";
            Field(h => h.Id).Description("The id of the customer.");
            Field(h => h.FirstName).Description("The name of the customer.");
            Field("lastName", h => h.LastName, nullable: true).Description("The last name of the customer.");
            Field(h => h.Email).Description("The email of the customer.");
        }
    }
}
