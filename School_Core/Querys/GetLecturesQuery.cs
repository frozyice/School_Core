using School_Core.Contexts;
using School_Core.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using Domain.Specifications;

namespace School_Core.Querys
{

    public interface IGetLectureQuery
    {
        IReadOnlyList<Lecture> GetLectures(Specification<Lecture> spec = null);
    }

    public class GetLectureQuery : IGetLectureQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public GetLectureQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IReadOnlyList<Lecture> GetLectures(Specification<Lecture> spec = null)
        {
            if (spec == null)
            {
                return _dbContext.Lectures.ToList();
            }
            var expression = spec.SatisfyEntitiesFrom(_dbContext.Lectures);
            return expression.ToList();
        }
    }

}
