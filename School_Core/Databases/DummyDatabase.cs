using School_Core.Contexts;
using School_Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace School_Core.Databases
{

    public class DummyDatabase : IDatabase
    {
        public List<Lecture> Lectures { get; private set; }
        public List<Teacher> Teachers { get; private set; }
        public List<Student> Students { get; private set; }

        public DummyDatabase()
        {
            Lectures = ModelbuilderExtentions.GetLectures();
            Students = ModelbuilderExtentions.GetStudents();
            Teachers = ModelbuilderExtentions.GetTeachers();
            //Lectures = new List<Lecture>()
            //{
            //    new Lecture ("Ajalugu"),
            //    new Lecture ("Kirjandus"),
            //    new Lecture ("Emakeel"),
            //    new Lecture ("Füüsika"),
            //    new Lecture ("Geograafia")
            //};

            //Teachers = new List<Teacher>()
            //{
            //    new Teacher ("Männik","+372 55 874 747"),
            //    new Teacher ("Berg", "+372 56 295 987"),
            //    new Teacher ("Hummel", "+372 55 498 361"),
            //    new Teacher ("Kiisk", "+372 56 847 325"),
            //    new Teacher ("Lepik", "+372 58 673 184")
            //};

            //Students = new List<Student>()
            //{
            //    new Student ("Tiit", "Tiit@kool.ee"),
            //    new Student ("Teet", "Teet@kool.ee"),
            //    new Student ("Lea", "Lea@kool.ee"),
            //    new Student ("Jasper", "Jasper@kool.ee"),
            //    new Student ("Kuno", "Kuno@kool.ee"),
            //    new Student ("Rahel","Rahel@kool.ee"),
            //    new Student ("Willem", "Willem@kool.ee")
            //};
        }


        public Lecture GetLecture(Guid id)
        {
            var lecture = Lectures.FirstOrDefault(x => x.Id == id);
            return lecture;
        }

        public IEnumerable<Lecture> GetLectures()
        {
            return Lectures.OrderBy(Lecture => Lecture.Name);
        }

        public void DeleteLecture(Guid id)
        {
            Lectures.Remove(GetLecture(id));
        }

        public Guid AddNewLecture(Lecture lecture)
        {
            Lectures.Add(lecture);
            return lecture.Id;
        }

        public Guid EditLecture(Lecture lecture)
        {
            DeleteLecture(lecture.Id);
            AddNewLecture(lecture);
            return lecture.Id;
        }


        public Student GetStudent(Guid id)
        {
            return Students.FirstOrDefault(x => x.Id == id);
        }


        public IEnumerable<Student> GetStudents()
        {
            return Students;
        }


        public Teacher GetTeacher(Guid id)
        {
            return Teachers.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return Teachers;
        }

        public Student GetStudentByName(string name)
        {
            var student = Students.FirstOrDefault(x => x.Name == name);
            return student;
        }

        public Guid AddStudent(Student student)
        {
            Students.Add(student);
            return student.Id;
        }
    }
}
