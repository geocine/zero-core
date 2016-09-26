using System;
using System.Collections.Generic;
using System.Linq;
using Zervo.Core.Models;
using Zervo.Core.Services.Contracts;
using Zervo.Data.Repositories;
using Zervo.Data.Repositories.Contracts;
using Zervo.Data.Repositories.Database;

namespace Zervo.Core.Services
{
    public class EmployeeService : Service<Employee>, IEmployeeService
    {
        private readonly IRepository<Data.Models.Employee> _repository;

        public EmployeeService(IRepository<Data.Models.Employee> repository)
        {
            _repository = repository;
        }

        public override IEnumerable<Employee> List()
        {
            // Need to use automapper here
            var employees = _repository.GetAllDetails();
            // Also add the employee id x.Id
            var employeeList = employees.Select(x => new Employee
            {
                Id = x.Id,
                FirstName = x.Person.FirstName,
                LastName = x.Person.LastName,
                Email = x.Person.Email,
                HourlyWage = x.HourlyWage
            }).ToList();
            return employeeList;
        }

    }
}
