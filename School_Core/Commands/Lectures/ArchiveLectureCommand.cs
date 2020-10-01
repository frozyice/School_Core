using System;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Queries;
using School_Core.Specifications;

namespace School_Core.Commands.Lectures
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
            private readonly ILectureQuery _lectureQuery;

            public Handler(SchoolCoreDbContext dbContext, ILectureQuery lectureQuery)
            {
                _dbContext = dbContext;
                _lectureQuery = lectureQuery;
            }

            public bool Handle(ArchiveLectureCommand command)
            {
                var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(command.Id));
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