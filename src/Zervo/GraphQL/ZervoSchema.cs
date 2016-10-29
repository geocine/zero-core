using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace Zervo.GraphQL
{
    public class ZervoSchema: Schema
    {
        // watch https://www.youtube.com/watch?v=UBGzsb2UkeY
        public ZervoSchema(Func<Type, GraphType> resolveType)
            : base(resolveType)
        {
            Query = (ZervoQuery)resolveType(typeof(ZervoQuery));
        }
    }
}
