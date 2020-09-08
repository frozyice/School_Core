namespace School_Core.Commands
{
    public interface ICommand
    {
    }
    //public interface ICommand<out TResult> : ICommand
    //{

    //}
    //public interface ICommandHandler<in TCommand, out TResult> where TCommand : ICommand<TResult>
    //{
    //    TResult Handle(TCommand command);
    //}

    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        bool Handle(TCommand command);
    }
}
