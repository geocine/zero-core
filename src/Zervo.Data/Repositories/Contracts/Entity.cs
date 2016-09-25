using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Zervo.Data.Infrastructure;

namespace Zervo.Data.Repositories.Contracts
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
