using System.Threading.Tasks;
using NM.SharedKernel.Core.Messages;

namespace NM.SharedKernel.Core.Workers
{
    public interface IPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}
