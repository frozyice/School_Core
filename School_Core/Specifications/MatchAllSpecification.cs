using System;
using System.Linq.Expressions;

namespace Domain.Specifications
{
    public class MatchAllSpecification<TEntity> : Specification<TEntity>
    {
        internal override Expression<Func<TEntity, bool>> Predicate => entity => true;
    }
}
