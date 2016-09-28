using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Zervo.Core.Models.Contracts;

namespace Zervo.Core.Services.Contracts
{
    public abstract class Service<TObjectModel> : IService<TObjectModel> where TObjectModel : class, IObjectModel
    {
        public virtual void Create(TObjectModel model)
        {
            
        }

        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TObjectModel> List()
        {
            throw new NotImplementedException();
        }
    }
}
