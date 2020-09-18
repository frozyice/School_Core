using System;
using System.Linq.Expressions;

namespace Domain.Specifications
{
    public class NotSpecification<TEntity> : Specification<TEntity>
    {
        public Specification<TEntity> Specification { get; private set; }

        public NotSpecification(Specification<TEntity> specification)
        {
            Specification = specification;
        }

        internal sealed override Expression<Func<TEntity, bool>> Predicate => Specification.Predicate.Not();
    }

    public static class NotSpecificationExtensions
    {
        public static NotSpecification<T> Negate<T>(this Specification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }
    }

    public static class Not
    {
        public static NotSpecification<T> Of<T>(Specification<T> specification)
        {
            return specification.Negate();
        }
    }
}