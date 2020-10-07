using System.Collections.Generic;
using System.Linq;
using School_Core.Contexts;
using School_Core.Domain.Models.Teachers;
using School_Core.Specifications;

namespace School_Core.Queries
{
    public class TeacherQuery : IQuery<Teacher>
    {
        private readonly SchoolCoreDbContext _dbContext;

        public TeacherQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<Teacher> GetAll(ISpecification<Teacher> spec = null)
        {
            spec = spec ?? new MatchAllSpecification<Teacher>();
            return GetBySpec(spec).ToList();
        }

        private IQueryable<Teacher> GetBySpec(ISpecification<Teacher> spec)
        {
            var teachers = _dbContext.Teachers;
            return spec.SatisfyEntitiesFrom(teachers);
        }

        public Teacher GetSingleOrDefault(ISpecification<Teacher> spec)
        {
            return GetBySpec(spec).SingleOrDefault();
        }
        
    }
}