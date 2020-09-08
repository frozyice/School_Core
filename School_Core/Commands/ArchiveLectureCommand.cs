using School_Core.Contexts;
using School_Core.Domain.Models;
using System;
using System.Linq;

namespace School_Core.Commands
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
                //var lecture = _dbContext.Lectures.Where(x => x.Id == command.Id).FirstOrDefault();
                //var result = lecture.Enrollments; // vüib olla null ?    // lazy puhul tehakse iga enrollmendi kohta lisa päring 
                var lecture = _dbContext.Lectures.Find(command.Id);

                var enrollments = _dbContext.Enrollments.Where(x => x.LectureId == command.Id).Where(x => x.Grade != Grade.None).FirstOrDefault();


                //?
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
