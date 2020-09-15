using System;
using System.Linq.Expressions;

namespace Domain.Specifications
{
    public sealed class OrSpecification<TEntity> : CompositeSpecification<TEntity>
    {

        public OrSpecification(params Specification<TEntity>[] specifications) : base(specifications)
        {
        }

        protected override Expression<Func<TEntity, bool>> CombineExpressions(Expression<Func<TEntity, bool>> exp1, Expression<Func<TEntity, bool>> exp2)
        {
            return ExpressionExtension.Or(exp1, exp2);
        }
    }

    public static class OrSpecificationExtensions
    {
        public static Specification<T> Or<T>(this Specification<T> specification1, Specification<T> specification2)
        {
            return new OrSpecification<T>(specification1, specification2);
        }
    }
}
