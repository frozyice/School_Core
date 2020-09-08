using School_Core.Domain.Models;
using School_Core.Databases;
using System;
using System.Collections.Generic;

namespace School_Core.Repositories
{
    public interface ITeacherRepository
    {
        IEnumerable<Teacher> GetTeachers();
        Teacher GetTeacher(Guid id);
    }
    
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IDatabase _database;

        public TeacherRepository(IDatabase database)
        {
            _database = database;
        }

        public Teacher GetTeacher(Guid id)
        {
            return _database.GetTeacher(id);
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return _database.GetTeachers();
        }
    }
}
