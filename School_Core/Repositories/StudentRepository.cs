using School_Core.Domain.Models;
using School_Core.Databases;
using System;
using System.Collections.Generic;

namespace School_Core.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();
        Student GetStudent(Guid id);
        Student GetStudentByName(string name);
        Guid AddStudent(Student student);
    }
    
    public class StudentRepository : IStudentRepository
    {
        private readonly IDatabase _database;

        public StudentRepository(IDatabase database)
        {
            _database = database;
        }

        public Guid AddStudent(Student student)
        {
            return _database.AddStudent(student);
        }

        public Student GetStudent(Guid id)
        {
            return _database.GetStudent(id);
        }

        public Student GetStudentByName(string name)
        {
            return _database.GetStudentByName(name);
        }

        public IEnumerable<Student> GetStudents()
        {
            return _database.GetStudents();
        }
    }
}
