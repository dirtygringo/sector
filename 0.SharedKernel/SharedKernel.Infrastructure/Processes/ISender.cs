using System.Threading.Tasks;
using NM.SharedKernel.Infrastructure.Messages;

namespace NM.SharedKernel.Infrastructure.Processes
{
    public interface ISender
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
