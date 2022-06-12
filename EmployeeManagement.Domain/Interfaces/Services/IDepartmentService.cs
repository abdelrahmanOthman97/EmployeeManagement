using EmployeeManagement.Domain.ViewModels.Common;
using EmployeeManagement.Domain.ViewModels.Department;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<Response<bool>> CreateDepartment(CreateDepartmentVM createDepartmentVM);
        Task<Response<bool>> EditDepartment(EditDepartmentVM editDepartmentVM);
        Task<Response<EditDepartmentVM>> GetDepartmentForEdit(int departmentId);
        Task<Response<List<GetDepartmentVM>>> GetAllDepartments();
        Task<Response<bool>> DeleteDepartment(int departmentId);
    }
}
