using MediatR;

namespace EmployeeManagement.Services.DomainEvents.Department
{
    public class DepartmentCreatedDomainEvent: INotification
    {
        public Domain.Entities.Department Department { get; set; }
    }
}
