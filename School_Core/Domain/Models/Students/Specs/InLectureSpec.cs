using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.Specifications;

namespace School_Core.Domain.Models.Students.Specs
{
    public class InLectureSpec : Specification<Student>
    {
        public Guid Id { get; }

        public InLectureSpec(Guid id)
        {
            Id = id;
        }

        internal override Expression<Func<Student, bool>> Predicate => s => s.Enrollments.Any(x => x.LectureId == Id);
    }
}