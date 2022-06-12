using MediatR;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.DomainEvents
{
    public interface IDomainEventManager
    {
        void AddDomainEvent(INotification eventItem);
        void RemoveDomainEvent(INotification eventItem);
        Task<(bool, string)> DispatchDomainEvents();
    }
}
