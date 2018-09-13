using System.Threading.Tasks;
using NM.SharedKernel.Core.Abstraction.Messages;

namespace NM.SharedKernel.Core.Abstraction.Processes
{
    public interface IMessageHandler<in TMessage> where TMessage : class, IMessage
    {
        Task HandleAsync(TMessage args);
    }
}
