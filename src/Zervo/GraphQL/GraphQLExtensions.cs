using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace Zervo.GraphQL
{
    public static class GraphQLExtensions
    {
        public static FieldType FieldAsync<TGraphType, TSourceType>(
            this IObjectGraphType obj,
            string name,
            string description = null,
            QueryArguments arguments = null,
            Func<ResolveFieldContext<TSourceType>, Task<object>> resolve = null,
            string deprecationReason = null)
            where TGraphType : IGraphType
        {
            return obj.AddField(new FieldType
            {
                Name = name,
                Description = description,
                DeprecationReason = deprecationReason,
                Type = typeof(TGraphType),
                Arguments = arguments,
                Resolver = resolve != null
                    ? new FuncFieldResolver<TSourceType, Task<object>>(resolve)
                    : null,
            });
        }
    }
}
