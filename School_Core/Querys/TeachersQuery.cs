using System.Collections.Generic;
using System.Linq;
using School_Core.Contexts;
using School_Core.Domain.Models;

namespace School_Core.Querys
{
    public interface ITeacherQuery
    {
        IReadOnlyList<Teacher> GetAll();
    }

    public class TeachersQuery : ITeacherQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public TeachersQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<Teacher> GetAll() // pole specki varianti veel sisse jõudnud tuua 
        {
            return _dbContext.Teachers.ToList();
        }
    }
}