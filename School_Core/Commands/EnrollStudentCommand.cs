using System;
using School_Core.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace School_Core.Commands
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
                var lecture = _context.Lectures.Where(l => l.Id == command.LectureId).Include(x=>x.Enrollments).Single();
                
                var student = _context.Students.FirstOrDefault(s => s.Name == command.StudentName);

                if (lecture == null && student == null)
                {
                    return false;
                    
                }
                
                //var enrollment = new Enrollment(student.Id);
                lecture.EnrollStudent(student.Id);
                _context.SaveChanges();
                

                

                //if (lecture.IsLawLecture)
                //{
                //    var isLawStudent = new IsLawStudentSpecification().IsSatisfiedBy(student);
                //    if (!isLawStudent)
                //    {
                //        return false;
                //    }
                //}

                //if (command.LectureName.Equals("Constitutional Law"))
                //{
                //    var isLawStudent = new IsLawStudentSpecification().IsSatisfiedBy(student);
                //    var isFirstYearStudent = new IsFirstYearStudentSpecification().IsSatisfiedBy(student);
                //    if (!isLawStudent || isFirstYearStudent)
                //    {
                //        return false;
                //    }
                //}

                //var lectureStudents = new LectureStudents
                //{
                //    StudentId = student.Id,
                //    LectureId = lecture.Id
                //};
                //_context.Add(lectureStudents);
                //_context.SaveChanges();

                

                return true;

            }
        }

    }

}
