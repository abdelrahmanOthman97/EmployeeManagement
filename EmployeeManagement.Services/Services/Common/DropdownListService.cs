using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Domain.Interfaces.Services.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeManagement.Services.Services.Common
{
    public class DropdownListService : IDropdownListService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public DropdownListService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task<SelectList> GetEmployess(bool includeManagers, int? includeManger = null)
        {
            var employess = await _employeeRepository.GetAllAsync();
            if (!includeManagers)
            {
                var managers = employess.Select(c => c.Department?.ManagerId).Distinct().ToList();
                if (includeManger != null)
                    managers.Remove(includeManger);
                employess = employess.Where(c => !managers.Contains(c.Id)).ToList();
            }

            var customEmployess = employess.Select(s => new
            {
                Value = s.Id,
                Text = $"{s.Id} - {s.Name}"
            });
            return new SelectList(customEmployess, "Value", "Text");
        }
        public async Task<SelectList> GetDepartments()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var customDepartments = departments.Select(s => new
            {
                Value = s.Id,
                Text = $"{s.Id} - {s.Name}"
            });
            return new SelectList(customDepartments, "Value", "Text");
        }
    }
}
