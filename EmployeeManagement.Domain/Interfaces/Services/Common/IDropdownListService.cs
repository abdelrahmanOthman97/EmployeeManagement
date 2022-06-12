using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Services.Common
{
    public interface IDropdownListService
    {
        Task<SelectList> GetEmployess(bool includeManagers, int? includeManger = null);
        Task<SelectList> GetDepartments();
    }
}
