using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.ViewModels.Department
{
    public class EditDepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "must enter department name")]
        public string Name { get; set; }
        public int? ManagerId { get; set; }
    }
}
