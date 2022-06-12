using EmployeeManagement.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.DomainEvents.Department
{
    public class DepartmentCreatedDomainEventHandler : INotificationHandler<DepartmentCreatedDomainEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DepartmentCreatedDomainEventHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(DepartmentCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Department.ManagerId == null)
                return;

            var employee = await _employeeRepository.GetByIdAsync((int)notification.Department.ManagerId);
            if (employee == null)
                throw new Exception("Failed to set manager!");

            employee.DepartmentId = notification.Department.Id;
            _employeeRepository.Update(employee);

            return;
        }
    }
}
