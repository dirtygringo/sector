using System.Threading.Tasks;
using NM.SharedKernel.Core.Messages;

namespace NM.SharedKernel.Core.Processes
{
    public interface IPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}
