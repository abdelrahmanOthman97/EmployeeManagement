namespace EmployeeManagement.Domain.ViewModels.Employee
{
    public class GetEmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string DepartmentName { get; set; }
        public bool IsManager { get; set; }
        public int? ManagerId { get; set; }
        public string ManagerName { get; set; }
    }
}
