using System.Threading.Tasks;
using NM.SharedKernel.Core.Messages;

namespace NM.SharedKernel.Core.Processes
{
    public interface IMessageHandler<in TMessage> where TMessage : class, IMessage
    {
        Task HandleAsync(TMessage args);
    }
}
