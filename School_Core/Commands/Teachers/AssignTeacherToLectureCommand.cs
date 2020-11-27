using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Lectures.Specs;
using School_Core.Domain.Models.Teachers;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.Util;

namespace School_Core.Commands.Teachers
{
    public class AssignTeacherToLectureCommand : ICommand
    {
        public Guid LectureId { get; set; }
        public Guid TeacherId { get; set; }

        public class Handler : ICommandHandler<AssignTeacherToLectureCommand>
        {
            private readonly IQuery<Teacher> _teacherQuery;
            private readonly IQuery<Lecture> _lectureQuery;
            private readonly SchoolCoreDbContext _dbContext;

            public Handler(IQuery<Teacher> teacherQuery, IQuery<Lecture> lectureQuery, SchoolCoreDbContext dbContext)
            {
                _teacherQuery = teacherQuery;
                _lectureQuery = lectureQuery;
                _dbContext = dbContext;
            }

            public Result Handle(AssignTeacherToLectureCommand command)
            {
                var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(command.LectureId));
                if (lecture is null) throw new ArgumentException(nameof(command.LectureId));

                var teacher = _teacherQuery.GetSingleOrDefault(new HasIdSpec<Teacher>(command.TeacherId));
                if (teacher is null) throw new ArgumentException(nameof(command.TeacherId));

                if (lecture.Status == LectureStatus.Archived)
                {
                    return Result.Fail("alert", "Can not assign, lecture is archived.");
                }

                var isTeacherAssignToAnyLectures = _lectureQuery.GetAll(new LecturesWithTeacherIdsSpec(new List<Guid> {teacher.Id})).Any();
                if (isTeacherAssignToAnyLectures)
                {
                    return Result.Fail("alert", "Can not assign, teacher has assigned to a lecture.");
                }

                lecture.AssignTeacher(teacher);
                _dbContext.SaveChanges(); //todo Kuidas saada teada et, _dbContext.SaveChanges(); kutsutakse
                return Result.Success();
            }
        }
    }
}