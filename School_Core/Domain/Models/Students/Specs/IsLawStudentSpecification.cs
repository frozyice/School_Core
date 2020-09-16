using System;
using System.Linq.Expressions;
using Domain.Specifications;

namespace School_Core.Domain.Models.Students.Specs
{
    public class IsLawStudentSpecification : Specification<Student>
    {
        internal override Expression<Func<Student, bool>> Predicate => s => s.FieldOfStudy == StudyField.Law;
    }
}