using Domain.Specifications;
using School_Core.Contexts;
using School_Core.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace School_Core.Querys
{

    public interface IGetStudentQuery
    {
        IReadOnlyList<Student> GetStudents(Specification<Student> spec = null);
    }

    public class GetStudentQuery : IGetStudentQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public GetStudentQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IReadOnlyList<Student> GetStudents(Specification<Student> spec = null)
        {
            if (spec == null)
            {
                var allStudents = _dbContext.Students.ToList();
                return allStudents;
            }
            var studentsExpression = spec.SatisfyEntitiesFrom(_dbContext.Students);
            var students = studentsExpression.ToList();
            return students;
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