using Zervo.Repositories.Contracts;

namespace Zervo.Models
{
    public class Customer : IEntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}