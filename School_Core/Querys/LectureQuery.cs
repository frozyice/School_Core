using System;
using School_Core.Contexts;
using School_Core.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace School_Core.Querys
{
    public interface ILectureQuery
    {
        IReadOnlyList<Lecture> GetLectures(Specification<Lecture> spec = null);
        Lecture GetLecture(Guid lectureId);
        Lecture GetTeacherLecture(Guid teacherId);
    }

    public class LectureQuery : ILectureQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public LectureQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IReadOnlyList<Lecture> GetLectures(Specification<Lecture> spec = null)
        {
            if (spec == null)
            {
                return _dbContext.Lectures.Include(x => x.Enrollments).Include(x=>x.Teacher).ToList();
            }
            var expression = spec.SatisfyEntitiesFrom(_dbContext.Lectures.Include(x => x.Enrollments).Include(x => x.Teacher));
            return expression.ToList();
        }

        public Lecture GetLecture(Guid lectureId)
        {
            return _dbContext.Lectures.Where(x => x.Id == lectureId).Include(e => e.Enrollments).SingleOrDefault();
        }

        public Lecture GetTeacherLecture(Guid teacherId) // pigem võisk specis olla 
        {
            return _dbContext.Lectures.Where(x => x.Teacher.Id == teacherId).SingleOrDefault();
        }
    }
}
