namespace EmployeeManagement.Domain.ViewModels.Department
{
    public class GetDepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ManagerId { get; set; }
        public string ManagerName { get; set; }
    }
}
