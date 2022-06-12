using MediatR;

namespace EmployeeManagement.Services.DomainEvents.Department
{
    public class DepartmentEditedDomainEvent: INotification
    {
        public Domain.Entities.Department OldDepartment { get; set; }
        public Domain.Entities.Department UpdatedDepartment { get; set; }
    }
}
