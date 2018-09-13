using System.Threading.Tasks;
using NM.SharedKernel.Core.Abstraction.Messages;

namespace NM.SharedKernel.Core.Abstraction.Processes
{
    public interface ISender
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
