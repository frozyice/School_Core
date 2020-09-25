using System;
using System.Linq.Expressions;

namespace School_Core.Specifications
{
    public class MatchNoneSpecification<TEntity> : Specification<TEntity>
    {
        internal override Expression<Func<TEntity, bool>> Predicate => entity => false;
    }
}