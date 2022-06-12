using EmployeeManagement.Domain.Interfaces.DomainEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.DomainEvents
{
    public class DomainEventManager : IDomainEventManager
    {
        private readonly IMediator _mediator;
        private List<INotification> _domainEvents;

        public DomainEventManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<INotification> DomainEvents => _domainEvents;
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public async Task<(bool, string)> DispatchDomainEvents()
        {
            try
            {
                foreach (var domainEvent in _domainEvents)
                {
                    await _mediator.Publish(domainEvent);
                }
                return (true, null);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
