using System.Threading.Tasks;
using NM.SharedKernel.Core.Abstraction.Messages;

namespace NM.SharedKernel.Core.Abstraction.Workers
{
    public interface IPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}
