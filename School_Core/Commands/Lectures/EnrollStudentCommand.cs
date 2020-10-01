using System;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Lectures.Specs;
using School_Core.Domain.Models.Students;
using School_Core.Queries;
using School_Core.Specifications;

namespace School_Core.Commands.Lectures
{
    public sealed class EnrollStudentCommand : ICommand
    {
        public EnrollStudentCommand(Guid lectureId, string studentName)
        {
            LectureId = lectureId;
            StudentName = studentName;
        }

        public Guid LectureId { get; }
        public string StudentName { get; }

        public sealed class Handler : ICommandHandler<EnrollStudentCommand>
        {
            private readonly SchoolCoreDbContext _dbContext;
            private readonly ILectureQuery _lectureQuery;
            private readonly IStudentQuery _studentQuery;

            public Handler(SchoolCoreDbContext dbContext, ILectureQuery lectureQuery, IStudentQuery studentQuery)
            {
                _dbContext = dbContext;
                _lectureQuery = lectureQuery;
                _studentQuery = studentQuery;
            }

            public bool Handle(EnrollStudentCommand command)
            {
                var lecture = _lectureQuery.GetSingleOrDefault(new HasNameSpec<Lecture>(command.StudentName));
                var student = _studentQuery.GetSingleOrDefault(new HasNameSpec<Student>(command.StudentName));

                if (lecture == null || student == null)
                {
                    return false;
                }

                var canEnroll = new CanEnrollSpec(student).IsSatisfiedBy(lecture);
                if (canEnroll)
                {
                    lecture.EnrollStudent(student);
                    _dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
        }
    }
}