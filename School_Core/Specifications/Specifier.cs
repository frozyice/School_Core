using System.Collections.Generic;
using System.Linq;

namespace School_Core.Specifications
{
    /// <summary>
    /// </summary>
    public interface ISpecifier
    {
        bool IsEntitySatisfiedBy<T>(T entity, Specification<T> specification);
        IQueryable<T> SatisfyEntitiesFrom<T>(IQueryable<T> entityQuery, Specification<T> specification);
        IEnumerable<T> SatisfyEntitiesFrom<T>(IEnumerable<T> entityQuery, Specification<T> specification);
    }

    public class Specifier : ISpecifier
    {
        public bool IsEntitySatisfiedBy<T>(T entity, Specification<T> specification)
        {
            return specification.IsSatisfiedBy(entity);
        }

        public IQueryable<T> SatisfyEntitiesFrom<T>(IQueryable<T> entityQuery, Specification<T> specification)
        {
            return specification.SatisfyEntitiesFrom(entityQuery);
        }

        public IEnumerable<T> SatisfyEntitiesFrom<T>(IEnumerable<T> entityQuery, Specification<T> specification)
        {
            return specification.SatisfyEntitiesFrom(entityQuery.AsQueryable());
        }
    }
}