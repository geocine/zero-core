using Zervo.Data.Repositories.Contracts;

namespace Zervo.Data.Models
{
    public class Employee : Entity
    {
        public int PersonId { get; set; }
        public User User { get; set; }

        public float HourlyWage { get; set; }
    }
}
