using System.Collections.Generic;
using System.Linq;
using School_Core.Contexts;
using School_Core.Domain.Models.Teachers;
using School_Core.Specifications;

namespace School_Core.Queries
{
    public interface ITeacherQuery
    {
        IReadOnlyList<Teacher> GetAll();
        IReadOnlyList<Teacher> GetAllBySpec(ISpecification<Teacher> spec);
        Teacher GetSingleOrDefault(ISpecification<Teacher> spec);
    }

    public class TeacherQuery : ITeacherQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public TeacherQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<Teacher> GetAll()
        {
            return _dbContext.Teachers.ToList();
        }

        public IReadOnlyList<Teacher> GetAllBySpec(ISpecification<Teacher> spec)
        {
            var expression = spec.SatisfyEntitiesFrom(_dbContext.Teachers);
            return expression.ToList();
        }

        public Teacher GetSingleOrDefault(ISpecification<Teacher> spec)
        {
            return GetAllBySpec(spec).SingleOrDefault();
        }
        
    }
}