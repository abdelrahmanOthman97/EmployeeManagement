using EmployeeManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagement.WebUI.APIs
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _employeeService.DeleteEmployee(id);
            return Ok(response);
        }
    }
}
