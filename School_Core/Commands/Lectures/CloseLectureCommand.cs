using System;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Queries;
using School_Core.Specifications;

namespace School_Core.Commands.Lectures
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
            private readonly ILectureQuery _lectureQuery;

            public Handler(SchoolCoreDbContext dbContext, ILectureQuery lectureQuery)
            {
                _dbContext = dbContext;
                _lectureQuery = lectureQuery;
            }

            // kui me tahame midagi kasutajale tagastada siis kontroll controlleri 
            // kui on mingi põhjus et me ei saa valideerimist teha domeenis siis paneme Commandi 
            public bool Handle(CloseLectureCommand command)
            {
                var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(command.Id));
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