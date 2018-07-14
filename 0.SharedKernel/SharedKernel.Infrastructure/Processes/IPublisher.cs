using System.Threading.Tasks;
using NM.SharedKernel.Infrastructure.Messages;

namespace NM.SharedKernel.Infrastructure.Processes
{
    public interface IPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
