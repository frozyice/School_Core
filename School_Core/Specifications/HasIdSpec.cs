using System;
using System.Linq.Expressions;
using School_Core.Domain.Models;

namespace School_Core.Specifications
{
    public class HasIdSpec<TEntity> : Specification<TEntity> where TEntity : Entity
    {
        public Guid Id { get; }

        public HasIdSpec(Guid id)
        {
            Id = id;
        }
        
        internal override Expression<Func<TEntity, bool>> Predicate => x => x.Id == Id;
    }
}