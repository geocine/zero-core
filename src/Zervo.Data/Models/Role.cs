using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Data.Models
{
    public class Role : Entity
    {
        public string Name { get; set; }
    }
}
