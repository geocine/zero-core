using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zervo.Data.Repositories.Contracts
{
    public abstract class EntityBase : IEntityBase
    {
        public int Id { get; set; }
    }
}
