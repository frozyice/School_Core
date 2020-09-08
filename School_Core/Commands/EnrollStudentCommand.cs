using School_Core.Contexts;
using System.Linq;

namespace School_Core.Commands
{


    public sealed class EnrollStudentCommand : ICommand
    {
        public string LectureName { get; }
        public string StudentName { get; }

        public EnrollStudentCommand(string lectureName, string studentName)
        {
            LectureName = lectureName;
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
                var lecture = _context.Lectures.FirstOrDefault(l => l.Name == command.LectureName);
                var student = _context.Students.FirstOrDefault(s => s.Name == command.StudentName);

                if (lecture == null && student == null)
                {
                    return false;
                    
                }

                

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
