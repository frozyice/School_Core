using System;
using System.Linq;
using System.Linq.Expressions;
using School_Core.Specifications;

namespace School_Core.Domain.Models.Students.Specs
{
    public class InLectureSpec : Specification<Student>
    {
        public InLectureSpec(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }


        internal override Expression<Func<Student, bool>> Predicate => s => s.Enrollments.Any(x => x.LectureId == Id);
    }
}