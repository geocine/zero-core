using OpenIddict;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Data.Models
{
    public class Token : OpenIddictToken<int>, IEntity
    {
        public override int Id { get; set; }
    }
}