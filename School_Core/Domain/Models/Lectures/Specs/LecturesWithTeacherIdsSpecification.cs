using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Specifications;

namespace School_Core.Domain.Models.Specs
{
    public class LecturesWithTeacherIdsSpecification : Specification<Lecture>
    {
        public IEnumerable<Guid> TeacherIds { get; }

        public LecturesWithTeacherIdsSpecification(IEnumerable<Guid> teacherIds)
        {
            TeacherIds = teacherIds;
        }

        internal override Expression<Func<Lecture, bool>> Predicate => lecture => TeacherIds.Any(x => x == lecture.Teacher.Id);
    }
}