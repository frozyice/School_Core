using System;
using System.Collections.Generic;
using System.Linq;
using School_Core.Contexts;
using School_Core.Domain.Models.Teachers;

namespace School_Core.Queries
{
    public interface ITeacherQuery
    {
        IReadOnlyList<Teacher> GetAll();
        Teacher Get(Guid id);
    }

    public class TeacherQuery : ITeacherQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public TeacherQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<Teacher> GetAll() // pole specki varianti veel sisse jõudnud tuua 
        {
            return _dbContext.Teachers.ToList();
        }

        public Teacher Get(Guid id)
        {
            return _dbContext.Teachers.Find(id);
        }
    }
}