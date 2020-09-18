using System;
using Domain.Specifications;
using School_Core.Contexts;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School_Core.Domain.Models.Students;

namespace School_Core.Querys
{

    public interface IStudentQuery
    {
        IReadOnlyList<Student> GetStudents(Specification<Student> spec = null);
        Student GetStudent(Guid id);
    }

    public class StudentQuery : IStudentQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public StudentQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IReadOnlyList<Student> GetStudents(Specification<Student> spec = null)
        {
            if (spec == null)
            {
                var allStudents = _dbContext.Students.Include(x=>x.Enrollments).ToList();
                return allStudents;
            }
            var studentsExpression = spec.SatisfyEntitiesFrom(_dbContext.Students).Include(x=>x.Enrollments);
            var students = studentsExpression.ToList();
            return students;
        }

        public Student GetStudent(Guid id)
        {
            return _dbContext.Students.SingleOrDefault(x => x.Id == id);
        }
    }
}



//    public class GetStudetsQuery : IQuery<IReadOnlyList<Student>>
//    {
//        public Specification<Student> Spec { get; private set; }
//        public GetStudetsQuery(Specification<Student> spec)
//        {
//            Spec = spec;
//        }

//        public GetStudetsQuery()
//        {
//        }

//    }

//    public sealed class GetStudetsQueryHandler : IQueryHandler<GetStudetsQuery, IReadOnlyList<Student>>
//    {
//        private readonly LocalDbContext _dbContext;

//        public GetStudetsQueryHandler(LocalDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }
//        public IReadOnlyList<Student> Handle(GetStudetsQuery query)
//        {

//            if (query.Spec == null)
//            {
//                var allStudents = _dbContext.Students.ToList();
//                return allStudents;
//            }

//            var studentsExpression = query.Spec.SatisfyEntitiesFrom(_dbContext.Students);

//            var students = studentsExpression.ToList();


//            return students;
//        }
//    }

//}
//System.InvalidOperationException: 'The LINQ expression 'DbSet<Student>
//    .Where(s => s.IsLawStudent == True && s.YearOfStudy == 1)' 
//    could not be translated. Either rewrite the query in a form that can be translated, 
//    or switch to client evaluation explicitly by inserting a call to either 
//    AsEnumerable(), AsAsyncEnumerable(), ToList(), or ToListAsync(). 
//    See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.'