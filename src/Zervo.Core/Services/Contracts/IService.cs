using System.Collections.Generic;
using Zervo.Core.Models.Contracts;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Core.Services.Contracts
{
    public interface IService<T> where T : class, IObjectModel, new()
    {
        IEnumerable<T> List();
        void Create(T model);
        void Delete(int id);
    }
}
