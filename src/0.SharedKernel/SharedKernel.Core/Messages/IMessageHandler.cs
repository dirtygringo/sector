using System.Threading.Tasks;

namespace NM.SharedKernel.Core.Abstraction.Messages
{
    public interface IMessageHandler<in TMessage> where TMessage : class, IMessage
    {
        Task HandleAsync(TMessage args);
    }
}
