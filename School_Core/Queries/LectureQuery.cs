using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Teachers;
using School_Core.Specifications;

namespace School_Core.Queries
{
    public interface ILectureQuery
    {
        IReadOnlyList<Lecture> GetAll(ISpecification<Lecture> spec = null);
        Lecture Get(Guid lectureId);
    }

    public class LectureQuery : ILectureQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public LectureQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<Lecture> GetAll(ISpecification<Lecture> spec = null)
        {
            var lectures = _dbContext.Lectures
                .Include(x => x.Enrollments)
                .Include(x => x.Teacher);
            
            if (spec == null)
            {
                return lectures.ToList();
            }

            var expression = spec.SatisfyEntitiesFrom(lectures);
            return expression.ToList();
        }

        public Lecture Get(Guid lectureId)
        {
            return _dbContext.Lectures.Where(x => x.Id == lectureId).Include(e => e.Enrollments).Include(t => t.Teacher).SingleOrDefault();
        }
    }
}