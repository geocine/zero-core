using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using MediatR;
using Zervo.Features.Customers;
using Zervo.ViewModels;

namespace Zervo.GraphQL
{
    public class ZervoMutation: ObjectGraphType
    {
        public ZervoMutation(IMediator mediator)
        {
            Name = "Mutation";
            this.FieldAsync<CustomerType, object>("addCustomer", arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CustomerInputType>>
                    {
                        Name = "customer"
                    }
                ),
                resolve: async context =>
                {
                    var customer = context.GetArgument<CustomerViewModel>("customer");
                    var id =  await mediator.SendAsync(new AddCustomerCommand {CustomerViewModel = customer});
                    return await mediator.SendAsync(new GetCustomerQuery {Id = id});
                }
            );
        }
    }
}
