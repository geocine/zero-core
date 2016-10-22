using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zervo.Domain.Services.Contracts;

namespace Zervo.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
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
            var employees = _employeeService.List();
            return Ok(employees);
        }
    }
}
