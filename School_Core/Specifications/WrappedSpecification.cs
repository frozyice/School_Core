using System;
using System.Linq.Expressions;

namespace School_Core.Specifications
{
    public abstract class WrappedSpecification<TEntity> : Specification<TEntity>
    {
        public abstract Specification<TEntity> Specification { get; }

        internal sealed override Expression<Func<TEntity, bool>> Predicate => Specification.Predicate;
    }
}