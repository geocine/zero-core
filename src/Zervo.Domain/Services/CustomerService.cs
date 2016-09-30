using System.Collections.Generic;
using AutoMapper;
using Zervo.Data.Models;
using Zervo.Data.Repositories.Contracts;
using Zervo.Domain.Services.Contracts;
using Zervo.Data.Repositories;
using System.Linq;

namespace Zervo.Domain.Services
{
    public class CustomerService : Service<Customer> , ICustomerService
    {
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public CustomerService(IMapper mapper, IRepository<Customer> repository )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override void Create(Customer customer)
        {
            _repository.Add(customer);
            _repository.SaveChanges();
        }

        public override IEnumerable<Customer> List()
        {
            var customers = _repository.GetAllDetails();
            return customers;
        }

    }
}
