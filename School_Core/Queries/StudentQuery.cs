using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School_Core.Contexts;
using School_Core.Domain.Models.Students;
using School_Core.Specifications;

namespace School_Core.Queries
{
    public interface IStudentQuery
    {
        IReadOnlyList<Student> GetAll();
        IReadOnlyList<Student> GetAllBySpec(ISpecification<Student> spec);
        Student GetSingleOrDefault(ISpecification<Student> spec);
    }

    public class StudentQuery : IStudentQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public StudentQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<Student> GetAll()
        {
            var students = _dbContext.Students.Include(x => x.Enrollments);
            return students.ToList();
        }

        public IReadOnlyList<Student> GetAllBySpec(ISpecification<Student> spec)
        {
            var students = _dbContext.Students.Include(x => x.Enrollments);
            var expression = spec.SatisfyEntitiesFrom(students);
            return expression.ToList();
        }

        public Student GetSingleOrDefault(ISpecification<Student> spec)
        {
            return GetAllBySpec(spec).SingleOrDefault();
        }
    }
}