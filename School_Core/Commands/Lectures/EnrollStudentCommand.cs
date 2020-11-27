using System;
using System.Collections.Generic;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Lectures.Specs;
using School_Core.Domain.Models.Students;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.Util;

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

            public Result Handle(EnrollStudentCommand command)
            {
                var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(command.LectureId));
                if (lecture is null) throw new ArgumentException(nameof(command.LectureId));

                var student = _studentQuery.GetSingleOrDefault(new HasNameSpec<Student>(command.StudentName));
                if (student is null) throw new ArgumentException(nameof(command.StudentName));

                var hasOpenLectureStatus = new HasOpenLectureStatusSpec().IsSatisfiedBy(lecture);
                if (!hasOpenLectureStatus)
                {
                    return Result.Fail("alert", "Can not enroll, lecture is not open for enrollments");
                }
                
                var errors = new List<Result.KeyErrorPair>();
                
                var hasExistingEnrollment = new HasExistingEnrollmentSpec(student.Id).IsSatisfiedBy(lecture);
                if (hasExistingEnrollment)
                {
                    errors.Add(new Result.KeyErrorPair(null, "Can not enroll, lecture has already this student enrolled"));
                }
                
                var hasYearOfStudyToEnroll = new HasYearOfStudyToEnrollSpec(student.YearOfStudy).IsSatisfiedBy(lecture);
                if (!hasYearOfStudyToEnroll)
                {
                    errors.Add(new Result.KeyErrorPair(null, "Can not enroll, student does not have required years of study."));
                }
                
                var hasFieldOfStudyToEnroll = new HasFieldOfStudyToEnrollSpec(student.FieldOfStudy).IsSatisfiedBy(lecture);
                if (!hasFieldOfStudyToEnroll)
                {
                    errors.Add(new Result.KeyErrorPair(null,"Can not enroll, student does not have correct field of study."));
                }

                var canEnroll = new CanEnrollSpec(student).IsSatisfiedBy(lecture);
                if (!canEnroll)
                {
                    return Result.Fail(errors);
                }
                    lecture.EnrollStudent(student);
                    _dbContext.SaveChanges();
                    return Result.Success();
                
            }
        }
    }
}