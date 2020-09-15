using School_Core.Contexts;
using School_Core.Domain.Models;
using System;
using System.Linq;

namespace School_Core.Commands.Lecture
{
    public class ArchiveLectureCommand : ICommand
    {
        public Guid Id { get; private set; }

        public ArchiveLectureCommand(Guid id)
        {
            Id = id;
        }

        public class Handler : ICommandHandler<ArchiveLectureCommand>
        {
            private readonly SchoolCoreDbContext _dbContext;

            public Handler(SchoolCoreDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public bool Handle(ArchiveLectureCommand command)
            {
                var lecture = _dbContext.Lectures.Find(command.Id);
                if (lecture == null)
                {
                    return false;
                }
                if (lecture.CanArchive())
                {
                    lecture.ArchiveLecture();
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
