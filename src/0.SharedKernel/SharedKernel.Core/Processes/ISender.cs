using System.Threading.Tasks;
using NM.SharedKernel.Core.Messages;

namespace NM.SharedKernel.Core.Processes
{
    public interface ISender
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
