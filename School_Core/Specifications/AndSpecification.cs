using System;
using System.Linq.Expressions;

namespace Domain.Specifications
{
    public sealed class AndSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        public AndSpecification(params Specification<TEntity>[] specifications) : base(specifications)
        {
        }

        protected override Expression<Func<TEntity, bool>> CombineExpressions(Expression<Func<TEntity, bool>> exp1, Expression<Func<TEntity, bool>> exp2)
        {
            return ExpressionExtension.And(exp1, exp2);
        }
    }

    public static class AndSpecificationExtensions
    {
        public static Specification<T> And<T>(this Specification<T> specification1, Specification<T> specification2)
        {
            return new AndSpecification<T>(specification1, specification2);
        }

        public static Specification<T> AndIfNotNull<T>(this Specification<T> specification1, Specification<T> specification2)
        {
            if (specification2 == null)
                return specification1;

            return specification1.And(specification2);
        }
    }
}