using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<List<Department>> GetAllAsync();
    }
}
