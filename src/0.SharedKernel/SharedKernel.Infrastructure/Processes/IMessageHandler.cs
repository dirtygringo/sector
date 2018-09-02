using System.Threading.Tasks;
using NM.SharedKernel.Infrastructure.Messages;

namespace NM.SharedKernel.Infrastructure.Processes
{
    public interface IMessageHandler<in TMessage> where TMessage : class, IMessage
    {
        Task Handle(TMessage args);
    }
}
