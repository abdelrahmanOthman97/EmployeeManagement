using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.ViewModels.Department
{
    public class CreateDepartmentVM
    {
        [Required(ErrorMessage = "must enter department name")]
        public string Name { get; set; }
        public int? ManagerId { get; set; }
    }
}
