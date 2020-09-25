using System;
using System.Linq.Expressions;
using School_Core.Specifications;

namespace School_Core.Domain.Models.Students.Specs
{
    public class IsFirstYearStudentSpec : Specification<Student>
    {
        internal override Expression<Func<Student, bool>> Predicate => s => s.YearOfStudy == 1;
    }
}