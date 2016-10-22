using System.Collections.Generic;
using AutoMapper;
using Zervo.Data.Models;
using Zervo.Data.Repositories.Contracts;
using Zervo.Domain.Services.Contracts;
using Zervo.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zervo.Domain.Services
{
    public class RoleService : Service<Role> , IRoleService
    {
        private readonly IRepository<Role> _repository;

        public RoleService(IRepository<Role> repository ) : 
            base(repository)
        {
            _repository = repository;
        }

        public override IEnumerable<Role> List()
        {
            return _repository.GetAll();
        }

        public Role FindByName(string name)
        {
            return _repository.GetSingle(x => x.Name == name);
        }

    }
}
