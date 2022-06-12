using EmployeeManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DepartmentManagement.WebUI.APIs
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentApiController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentApiController(IDepartmentService DepartmentService)
        {
            _departmentService = DepartmentService;
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _departmentService.DeleteDepartment(id);
            return Ok(response);
        }
    }
}
