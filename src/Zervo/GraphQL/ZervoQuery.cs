using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using MediatR;
using Zervo.Domain.Services.Contracts;
using Zervo.Features.Customers;
using Zervo.GraphQL;

namespace Zervo.GraphQL
{
    public class ZervoQuery : ObjectGraphType<object>
    {
        public ZervoQuery(IMediator mediator)
        {
            Name = "Query";

            this.FieldAsync<ListGraphType<CustomerType>, object>(
                "customers",
                resolve: async context => await mediator.SendAsync(new GetCustomerListQuery()));

            this.FieldAsync<CustomerType,  object>(
                "customer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "id of the customer" }
                ),
                resolve: async context => await mediator.SendAsync(new GetCustomerQuery { Id = context.GetArgument<int>("id") })
            );
        }
    }
}
