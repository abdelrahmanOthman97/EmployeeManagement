using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.ViewModels.Employee
{
    public class EditEmployeeVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "must enter employee name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "must enter employee salary")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "must select employee department")]
        public int DepartmentId { get; set; }
    }
}
