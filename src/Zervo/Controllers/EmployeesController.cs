using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zervo.Core.Services.Contracts;

namespace Zervo.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET api/customers
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _employeeService.List().Select(x => x.Person).ToList();
            return Ok(customers);
        }
    }
}
