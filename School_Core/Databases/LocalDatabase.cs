using Microsoft.EntityFrameworkCore;
using School_Core.Contexts;
using School_Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace School_Core.Databases
{
    public class LocalDatabase : IDatabase
    {
        private readonly SchoolCoreDbContext _dbContext;
        public LocalDatabase(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddNewLecture(Lecture lecture)
        {
            _dbContext.Add(lecture);
            _dbContext.SaveChanges();
            return lecture.Id;
        }

        public Guid AddStudent(Student student)
        {
            _dbContext.Add(student);
            _dbContext.SaveChanges();
            return student.Id;
        }

        public Guid EditLecture(Lecture lecture)
        {
            _dbContext.Update(lecture);
            _dbContext.SaveChanges();
            return lecture.Id;

        }

        public Lecture GetLecture(Guid id)
        {
            //var lecture = _dbContext.Lectures.FirstOrDefault(x => x.Id == id);
            //Eager loading
            var lecture = _dbContext.Lectures.Include(t => t.Teacher).Include(x => x.Enrollments).ThenInclude(x => x.Student).First(e=>e.Id == id);
            return lecture;
        }

        public IEnumerable<Lecture> GetLectures()
        {
            return _dbContext.Lectures.OrderBy(x => x.Name);
        }

        public Student GetStudent(Guid id)
        {
            return _dbContext.Students.FirstOrDefault(x => x.Id == id);
        }

        public Student GetStudentByName(string name)
        {
            return _dbContext.Students.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Student> GetStudents()
        {
            return _dbContext.Students.OrderBy(x => x.Name);
        }

        public Teacher GetTeacher(Guid id)
        {
            return _dbContext.Teachers.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return _dbContext.Teachers.OrderBy(x => x.Name);
        }
    }
}
