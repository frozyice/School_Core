using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Specifications;

namespace School_Core.Queries
{
    public interface ILectureQuery
    {
        IReadOnlyList<Lecture> GetAll();
        IReadOnlyList<Lecture> GetAllBySpec(ISpecification<Lecture> spec);
        Lecture GetSingleOrDefault(ISpecification<Lecture> spec);
    }

    public class LectureQuery : ILectureQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public LectureQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<Lecture> GetAll()
        {
            var lectures = _dbContext.Lectures
                .Include(x => x.Enrollments)
                .Include(x => x.Teacher);

            return lectures.ToList();
        }

        public IReadOnlyList<Lecture> GetAllBySpec(ISpecification<Lecture> spec)
        {
            var lectures = _dbContext.Lectures
                .Include(x => x.Enrollments)
                .Include(x => x.Teacher);

            var expression = spec.SatisfyEntitiesFrom(lectures);
            return expression.ToList();
        }

        public Lecture GetSingleOrDefault(ISpecification<Lecture> spec)
        {
            return GetAllBySpec(spec).SingleOrDefault();
        }
    }
}