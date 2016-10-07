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
    public class CustomerService : Service<Customer> , ICustomerService
    {
        private readonly IRepository<Customer> _repository;

        public CustomerService(IRepository<Customer> repository ) : 
            base(repository)
        {
            _repository = repository;
        }

        public override IEnumerable<Customer> List()
        {
            return _repository.GetAllDetails();
        }

        public Customer Get(int id)
        {
            return _repository.GetCustomerDetails(id);
        }

    }
}
