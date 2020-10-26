using System;
using School_Core.Contexts;
using School_Core.Domain.Models.Lectures;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.Util;

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
            private readonly IQuery<Lecture> _lectureQuery;

            public Handler(SchoolCoreDbContext dbContext, IQuery<Lecture> lectureQuery)
            {
                _dbContext = dbContext;
                _lectureQuery = lectureQuery;
            }

            public Result Handle(CloseLectureCommand command)
            {
                var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(command.Id));
                if (lecture is null) throw new ArgumentException(nameof(command.Id));

                if (lecture.Status == LectureStatus.Closed)
                {
                    return Result.Fail("alert","Can not close, lecture is already closed.");
                }

                if (lecture.Status == LectureStatus.Archived)
                {
                    return Result.Fail("alert","Can not close, lecture is archived.");
                }

                lecture.CloseLecture();
                _dbContext.SaveChanges();
                return Result.Success();
            }
        }
    }
}