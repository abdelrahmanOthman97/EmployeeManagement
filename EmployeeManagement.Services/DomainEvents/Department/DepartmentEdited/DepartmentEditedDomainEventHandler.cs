using EmployeeManagement.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.DomainEvents.Department
{
    public class DepartmentEditedDomainEventHandler : INotificationHandler<DepartmentEditedDomainEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DepartmentEditedDomainEventHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(DepartmentEditedDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.UpdatedDepartment.ManagerId == null)
                return;

            if (notification.OldDepartment.ManagerId == notification.UpdatedDepartment.ManagerId)
                return;

            var employee = await _employeeRepository.GetByIdAsync((int)notification.UpdatedDepartment.ManagerId);
            if (employee == null)
                throw new Exception("Failed to set manager!");

            employee.DepartmentId = notification.UpdatedDepartment.Id;
            _employeeRepository.Update(employee);

            return;
        }
    }
}
