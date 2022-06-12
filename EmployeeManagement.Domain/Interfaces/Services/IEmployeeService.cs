using EmployeeManagement.Domain.ViewModels.Common;
using EmployeeManagement.Domain.ViewModels.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<Response<bool>> CreateEmployee(CreateEmployeeVM createEmployeeVM);
        Task<Response<bool>> EditEmployee(EditEmployeeVM editEmployeeVM);
        Task<Response<EditEmployeeVM>> GetEmployeeForEdit(int employeeId);
        Task<Response<List<GetEmployeeVM>>> GetAllEmployees();
        Task<Response<bool>> DeleteEmployee(int employeeId);
    }
}
