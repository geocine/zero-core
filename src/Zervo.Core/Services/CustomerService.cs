using System;
using System.Collections.Generic;
using Zervo.Core.Services.Contracts;
using Zervo.Data.Repositories;
using Zervo.Data.Repositories.Database;
using System.Linq;
using AutoMapper;
using Zervo.Core.Models;
using Zervo.Data.Models;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Core.Services
{
    public class CustomerService : Service<CustomerObjectModel> , ICustomerService
    {
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public CustomerService(IMapper mapper, IRepository<Customer> repository )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override void Create(CustomerObjectModel customer)
        {
            var customerModel = _mapper.Map<CustomerObjectModel, Customer>(customer);
            _repository.Add(customerModel);
            _repository.SaveChanges();
        }

        public override IEnumerable<CustomerObjectModel> List()
        {
            var customers = _repository.GetAllDetails();
            var customerList = customers.Select(x => _mapper.Map<Customer, CustomerObjectModel>(x)).ToList();
            return customerList;
        }

    }
}
