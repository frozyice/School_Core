using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School_Core.Contexts;
using School_Core.Domain.Models.Students;
using School_Core.Specifications;

namespace School_Core.Queries
{
    public class StudentQuery : IQuery<Student>
    {
        private readonly SchoolCoreDbContext _dbContext;

        public StudentQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<Student> GetAll(ISpecification<Student> spec = null)
        {
            spec = spec ?? new MatchAllSpecification<Student>();
            return GetBySpec(spec).ToList();
        }
        
       private IQueryable<Student> GetBySpec(ISpecification<Student> spec)
        {
            var students = _dbContext.Students.Include(x => x.Enrollments);
            return spec.SatisfyEntitiesFrom(students);
        }

       public Student GetSingleOrDefault(ISpecification<Student> spec)
        {
            return GetBySpec(spec).SingleOrDefault();
        }
    }
}