using School_Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace School_Core.Databases
{
    public interface IDatabase
    {
        Guid AddNewLecture(Lecture lecture);
        Guid AddStudent(Student student);
        Guid EditLecture(Lecture lecture);
        Lecture GetLecture(Guid id);
        IEnumerable<Lecture> GetLectures();
        Student GetStudent(Guid id);
        Student GetStudentByName(string name);
        IEnumerable<Student> GetStudents();
        Teacher GetTeacher(Guid id);
        IEnumerable<Teacher> GetTeachers();
    }
}