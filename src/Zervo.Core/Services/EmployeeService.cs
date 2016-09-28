using System;
using System.Collections.Generic;
using System.Linq;
using Zervo.Core.Models;
using Zervo.Core.Services.Contracts;
using Zervo.Data.Models;
using Zervo.Data.Repositories;
using Zervo.Data.Repositories.Contracts;
using Zervo.Data.Repositories.Database;
using Employee = Zervo.Data.Models.Employee;

namespace Zervo.Core.Services
{
    public class EmployeeService : Service<EmployeeObjectModel>, IEmployeeService
    {
        private readonly IRepository<Employee> _repository;

        public EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public override IEnumerable<EmployeeObjectModel> List()
        {
            // Need to use automapper here
            var employees = _repository.GetAllDetails();
            // Also add the employee id x.Id
            var employeeList = employees.Select(x => new EmployeeObjectModel
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
