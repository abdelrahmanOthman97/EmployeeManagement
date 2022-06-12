using System.Collections.Generic;

namespace EmployeeManagement.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Each department has one manager
        public int? ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

        //The department has more than one employee
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
