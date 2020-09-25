using System;
using System.Linq.Expressions;
using School_Core.Domain.Models.Lectures;
using School_Core.Specifications;

namespace School_Core.Domain.Models.Students.Specs
{
    public class IsLawStudentSpec : Specification<Student>
    {
        internal override Expression<Func<Student, bool>> Predicate => s => s.FieldOfStudy == StudyField.Law;
    }
}