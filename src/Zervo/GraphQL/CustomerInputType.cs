using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace Zervo.GraphQL
{
    public class CustomerInputType: InputObjectGraphType
    {
        public CustomerInputType()
        {
            Name = "CustomerInput";
            Field<StringGraphType>("firstName");
            Field<StringGraphType>("lastName");
            Field<StringGraphType>("email");
        }
    }
}
