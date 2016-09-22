using Zervo.Data.Repositories.Contracts;

namespace Zervo.Data.Models
{
    public class Customer : IEntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}