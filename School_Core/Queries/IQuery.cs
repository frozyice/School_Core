using System.Collections.Generic;
using School_Core.Domain.Models;
using School_Core.Specifications;

namespace School_Core.Queries
{
    public interface IQuery<TEntity> where TEntity : Entity
    {
        IReadOnlyList<TEntity> GetAll(ISpecification<TEntity> spec = null);
        TEntity GetSingleOrDefault(ISpecification<TEntity> spec);
    }
}