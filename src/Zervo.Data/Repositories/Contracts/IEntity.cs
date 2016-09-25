using System.ComponentModel.DataAnnotations.Schema;
using Zervo.Data.Infrastructure;

namespace Zervo.Data.Repositories.Contracts
{
    public interface IEntity
    {
        int Id { get; set; }

        // Exclude from database mapping
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}
