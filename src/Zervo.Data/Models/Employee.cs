using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Data.Models
{
    public class Employee : EntityBase
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public float HourlyWage { get; set; }
    }
}
