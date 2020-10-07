using System;
using System.Linq.Expressions;
using School_Core.Domain.Models;

namespace School_Core.Specifications
{
    public class HasNameSpec<TEntity> : Specification<TEntity> where TEntity : Entity
    {
        public string Name { get; }

        public HasNameSpec(string name)
        {
            Name = name;
        }
        internal override Expression<Func<TEntity, bool>> Predicate => x => x.Name == Name;
    }
}