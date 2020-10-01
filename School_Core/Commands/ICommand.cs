namespace School_Core.Commands
{
    public interface ICommand
    {
    }
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        bool Handle(TCommand command);
    }
}