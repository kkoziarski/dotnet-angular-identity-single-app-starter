using CleanArchWeb.Domain.Common;
using System.Threading.Tasks;

namespace CleanArchWeb.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
