using Zervo.Core.Models.Contracts;

namespace Zervo.Core.Models
{
    public class CustomerObjectModel : ObjectModel, IObjectModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}
