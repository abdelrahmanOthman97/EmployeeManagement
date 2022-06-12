using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department, EmployeeManagementContext>, IDepartmentRepository
    {
        public DepartmentRepository(EmployeeManagementContext context) : base(context)
        {
        }

        public async Task<List<Department>> GetAllAsync()
        {
            var query = _context.Departments.AsQueryable().AsNoTracking();
            query = query.Include(i => i.Manager);
            return await query.ToListAsync();
        }
    }
}
