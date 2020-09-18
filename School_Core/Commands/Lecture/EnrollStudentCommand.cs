using System;
using School_Core.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School_Core.Domain.Models.Students.Specs;

namespace School_Core.Commands.Lecture
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
            private readonly SchoolCoreDbContext _context;

            public Handler(SchoolCoreDbContext context)
            {
                _context = context;
            }

            public bool Handle(EnrollStudentCommand command)
            {
                //todo teoorias võiks spec olla ( vb peaks queryt täiendama, et saaks include lisada ) 
                var lecture = _context.Lectures.Where(l => l.Id == command.LectureId).Include(x => x.Enrollments).FirstOrDefault();
                var student = _context.Students.FirstOrDefault(s => s.Name == command.StudentName); // getStudentsWithNameSpec

                if (lecture == null || student == null)
                {
                    return false;
                }

                var canEnroll = new CanEnrollSpec(student).IsSatisfiedBy(lecture);
                if (canEnroll)
                {
                    lecture.EnrollStudent(student);
                    _context.SaveChanges();
                    return true;
                }

                return false;
            }
        }
    }
}