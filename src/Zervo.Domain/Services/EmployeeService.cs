using System.Collections.Generic;
using System.Linq;
using Zervo.Data.Repositories;
using Zervo.Data.Repositories.Contracts;
using Zervo.Domain.Services.Contracts;
using Employee = Zervo.Data.Models.Employee;

namespace Zervo.Domain.Services
{
    public class EmployeeService : Service<Employee>, IEmployeeService
    {
        private readonly IRepository<Employee> _repository;

        public EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public override IEnumerable<Employee> List()
        {
            // Need to use automapper here
            var employees = _repository.GetAllDetails();
            return employees;
        }

    }
}
