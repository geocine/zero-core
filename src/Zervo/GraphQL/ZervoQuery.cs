using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using MediatR;
using Zervo.Domain.Services.Contracts;
using Zervo.Features.Customers;

namespace Zervo.GraphQL
{
    public class ZervoQuery : ObjectGraphType<object>
    {
        public ZervoQuery(IMediator mediator)
        {
            Name = "Query";

            Field<ListGraphType<CustomerType>>(
                "customers",
                resolve: context => mediator.SendAsync(new GetCustomerListQuery()).Result
            );

            Field<CustomerType>(
                "customer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "id of the human" }
                ),
                resolve: context => mediator.SendAsync(new GetCustomerQuery { Id = context.GetArgument<int>("id") })
            );
        }
    }
}
