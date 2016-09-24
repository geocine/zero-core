using System;
using System.Collections.Generic;
using Zervo.Core.Services.Contracts;
using Zervo.Data.Models;
using Zervo.Data.Repositories;
using Zervo.Data.Repositories.Database;

namespace Zervo.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(ZervoContext context)
        {
            _employeeRepository = new EmployeeRepository(context);
        }

        public IEnumerable<Employee> List()
        {
            return _employeeRepository.GetAll();
        }

        public void Create(Employee model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
