using School_Core.Commands;
using System;

namespace School_Core.Util
{
    public class Messages
    {
        private readonly IServiceProvider _provider;

        public Messages(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Messages()
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

        //public T Dispatch<T>(IQuery<T> query)
        //{
        //    Type type = typeof(IQueryHandler<,>);
        //    Type[] typeArgs = { query.GetType(), typeof(T) };
        //    Type handlerType = type.MakeGenericType(typeArgs);

        //    dynamic handler = _provider.GetService(handlerType);
        //    T result = handler.Handle((dynamic)query);

        //    return result;
        //}
    }
}