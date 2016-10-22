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
    public class TokenService : Service<Token> , ITokenService
    {
        private readonly IRepository<Token> _repository;

        public TokenService(IRepository<Token> repository ) : 
            base(repository)
        {
            _repository = repository;
        }

        public override IEnumerable<Token> List()
        {
            return _repository.GetAll();
        }

    }
}
