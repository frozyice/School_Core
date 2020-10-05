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
        public Guid LectureId { get; }
        public string StudentName { get; }

        public EnrollStudentCommand(Guid lectureId, string studentName)
        {
            LectureId = lectureId;
            StudentName = studentName;
        }

        public sealed class Handler : ICommandHandler<EnrollStudentCommand>
        {
            private readonly SchoolCoreDbContext _dbContext;
            private readonly IQuery<Lecture> _lectureQuery;
            private readonly IQuery<Student> _studentQuery;

            public Handler(SchoolCoreDbContext dbContext, IQuery<Lecture> lectureQuery, IQuery<Student> studentQuery)
            {
                _dbContext = dbContext;
                _lectureQuery = lectureQuery;
                _studentQuery = studentQuery;
            }

            public bool Handle(EnrollStudentCommand command)
            {
                var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(command.LectureId));
                if (lecture is null) throw new ArgumentException(nameof(command.LectureId));

                var student = _studentQuery.GetSingleOrDefault(new HasNameSpec<Student>(command.StudentName));
                if (student is null) throw new ArgumentException(nameof(command.StudentName));

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