using System.Threading.Tasks;

namespace ParkMate.ApplicationServices.Interfaces
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task<bool> Handle(TCommand command);
    }
}