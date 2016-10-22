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
    public class UserService : Service<User> , IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository ) : 
            base(repository)
        {
            _repository = repository;
        }

        public override IEnumerable<User> List()
        {
            return _repository.GetAll();
        }

        public User FindByUserName(string userName)
        {
            return _repository.GetSingle(x => x.Username.ToLower() == userName);
        }

    }
}
