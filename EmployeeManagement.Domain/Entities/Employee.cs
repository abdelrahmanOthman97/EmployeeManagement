using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }

        //Each employee has one department
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        //Each department has one manager
        public virtual Department DepartmentManager { get; set; }
    }
}
