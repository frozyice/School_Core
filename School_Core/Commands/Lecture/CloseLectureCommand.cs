using School_Core.Contexts;
using System;
using School_Core.Domain.Models.Lectures;

namespace School_Core.Commands.Lecture
{
    public class CloseLectureCommand : ICommand
    {
        public Guid Id { get; private set; }

        public CloseLectureCommand(Guid id)
        {
            Id = id;
        }

        public class Handler : ICommandHandler<CloseLectureCommand>
        {
            private readonly SchoolCoreDbContext _dbContext;

            public Handler(SchoolCoreDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            //todo: kui me tahame midagi kasutajale tagastada siis kontroll controlleri 
            // kui on mingi põhjus et me ei saa valideerimist teha domeenis siis paneme Commandi 
            public bool Handle(CloseLectureCommand command)
            {
                var lecture = _dbContext.Lectures.Find(command.Id);
                if (lecture == null)
                {
                    return false;
                }

                if (lecture.Status == LectureStatus.Open)
                {
                    lecture.CloseLecture();
                    _dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
        }
    }
}