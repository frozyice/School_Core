using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using School_Core.Specifications;

namespace School_Core.Domain.Models.Lectures.Specs
{
    public class LecturesWithTeacherIdsSpec : Specification<Lecture>
    {
        public IEnumerable<Guid> TeacherIds { get; }

        public LecturesWithTeacherIdsSpec(IEnumerable<Guid> teacherIds)
        {
            TeacherIds = teacherIds;
        }

        internal override Expression<Func<Lecture, bool>> Predicate => lecture => TeacherIds.Any(x => x == lecture.Teacher.Id);
    }
}