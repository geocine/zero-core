using Zervo.Data.Repositories.Contracts;

namespace Zervo.Data.Models
{
    public class Customer : Entity
    {
        // one-to-one
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}