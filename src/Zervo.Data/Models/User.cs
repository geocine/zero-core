using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Zervo.Data.Repositories.Contracts;
using OpenIddict;

namespace Zervo.Data.Models
{
    public class User : Entity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }

        /*
         * JsonIgnore needs mscorlib but can be solved by adding
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
