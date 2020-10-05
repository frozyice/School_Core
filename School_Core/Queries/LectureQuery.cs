using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Specifications;

namespace School_Core.Queries
{
    public class LectureQuery : IQuery<Lecture>
    {
        private readonly SchoolCoreDbContext _dbContext;

        public LectureQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<Lecture> GetAll(ISpecification<Lecture> spec = null)
        {
            spec = spec ?? new MatchAllSpecification<Lecture>();
            return GetBySpec(spec).ToList();
        }

        private IQueryable<Lecture> GetBySpec(ISpecification<Lecture> spec)
        {
            var lectures = _dbContext.Lectures
                .Include(x => x.Enrollments)
                .Include(x => x.Teacher);

            return spec.SatisfyEntitiesFrom(lectures);
        }

        public Lecture GetSingleOrDefault(ISpecification<Lecture> spec)
        {
            return GetBySpec(spec).SingleOrDefault();
        }
    }
}