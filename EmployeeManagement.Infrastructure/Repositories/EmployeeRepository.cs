using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee, EmployeeManagementContext>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeManagementContext context) : base(context)
        {
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var query = _context.Employees.AsQueryable().AsNoTracking();
            query = query.Include(i => i.Department).ThenInclude(i => i.Manager);
            return await query.ToListAsync();
        }
    }
}
