using System;
using System.Collections.Generic;
using System.Linq;
using Zervo.Core.Models;
using Zervo.Core.Services.Contracts;
using Zervo.Data.Repositories;
using Zervo.Data.Repositories.Database;

namespace Zervo.Core.Services
{
    public class EmployeeService : Service<Employee>, IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(ZervoContext context)
        {
            _employeeRepository = new EmployeeRepository(context);
        }

        public override IEnumerable<Employee> List()
        {
            // Need to use automapper here
            var employees = _employeeRepository.GetAll();
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
