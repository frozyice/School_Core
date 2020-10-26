using School_Core.Contexts;
using School_Core.Util;

namespace School_Core.Commands
{
    public class RegisterNewStudentCommand : ICommand
    {
        public string Name { get; private set; }
        public int YearOfStudy { get; private set; }
        public bool IsLawStudent { get; private set; }

        public RegisterNewStudentCommand(string name, int yearOfStudy, bool isLawStudent)
        {
            Name = name;
            YearOfStudy = yearOfStudy;
            IsLawStudent = isLawStudent;
        }
    }

    public class RegisterNewStudentCommandHandler : ICommandHandler<RegisterNewStudentCommand>
    {
        private readonly SchoolCoreDbContext _dbContext;

        public RegisterNewStudentCommandHandler(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result Handle(RegisterNewStudentCommand command)
        {
            //var student = new Student(command.Name, command.YearOfStudy, command.IsLawStudent);
            //_dbContext.Add(student);
            //_dbContext.SaveChanges();
            return Result.Success();
        }
    }
}