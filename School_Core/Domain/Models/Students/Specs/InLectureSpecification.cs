using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.Specifications;

namespace School_Core.Domain.Models.Students.Specs
{
    public class InLectureSpecification : Specification<Student>
    {
        public Guid Id { get; }

        public InLectureSpecification(Guid id)
        {
            Id = id;
        }

        internal override Expression<Func<Student, bool>> Predicate =>
            s => s.Enrollments.FirstOrDefault(x => x.LectureId == Id) != null;
    }
}