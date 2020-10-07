using System;
using School_Core.Commands;

namespace School_Core.Util
{
    public class Messages
    {
        private readonly IServiceProvider _provider;

        public Messages(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected Messages()
        {
        }

        public virtual bool Dispatch(ICommand command)
        {
            Type type = typeof(ICommandHandler<>);
            Type[] typeArgs = {command.GetType()};
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _provider.GetService(handlerType);
            dynamic result = handler.Handle((dynamic) command);

            return result;
        }
    }
}