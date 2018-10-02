using System.Threading.Tasks;

namespace NM.SharedKernel.Core.Messages
{
    public interface IMessageHandler<in TMessage> where TMessage : class, IMessage
    {
        Task HandleAsync(TMessage args);
    }
}
