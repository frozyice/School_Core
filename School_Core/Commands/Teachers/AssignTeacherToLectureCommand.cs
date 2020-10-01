using System;
using System.Collections.Generic;
using System.Linq;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Lectures.Specs;
using School_Core.Domain.Models.Teachers;
using School_Core.Queries;
using School_Core.Specifications;

namespace School_Core.Commands.Teachers
{
    public class AssignTeacherToLectureCommand : ICommand
    {
        public Guid LectureId { get; set; }
        public Guid TeacherId { get; set; }

        public class Handler : ICommandHandler<AssignTeacherToLectureCommand>
        {
            private readonly ITeacherQuery _teacherQuery;
            private readonly ILectureQuery _lectureQuery;
            private readonly SchoolCoreDbContext _dbContext;

            public Handler(ITeacherQuery teacherQuery, ILectureQuery lectureQuery, SchoolCoreDbContext dbContext)
            {
                _teacherQuery = teacherQuery;
                _lectureQuery = lectureQuery;
                _dbContext = dbContext;
            }

            public bool Handle(AssignTeacherToLectureCommand command)
            {
                var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(command.LectureId));
                var teacher = _teacherQuery.GetSingleOrDefault(new HasIdSpec<Teacher>(command.TeacherId));

                if (lecture == null || teacher == null)
                {
                    return false;
                }

                var hasTeacherLectures = _lectureQuery.GetAllBySpec(new LecturesWithTeacherIdsSpec(new List<Guid>(){command.TeacherId})).Any();
                
                if (lecture.Status != LectureStatus.Archived && !hasTeacherLectures)
                {
                    lecture.AssignTeacher(teacher);
                    _dbContext.SaveChanges(); //todo Kuidas saada teada et, _dbContext.SaveChanges(); kutsutakse
                    return true;
                }

                return false;
            }
        }
    }
}