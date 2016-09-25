using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Data.Models
{
    public class Person : Entity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        /*
         * needs mscorlib but can be solved by adding
         * Microsoft.NETCore.Portable.Compatibility
         * ignored as only used for one-to-one relationship
         * shall later be separated to Fractore
         */
        [JsonIgnore]
        public Customer Customer { get; set; }

        [JsonIgnore]
        public Employee Employee { get; set; }
    }
}
