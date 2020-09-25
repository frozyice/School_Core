using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Students;
using School_Core.Domain.Models.Teachers;

namespace School_Core.Contexts
{
    public static class ModelbuilderExtentions
    {
        private static List<Lecture> lectures = new List<Lecture>()
        {
            new Lecture("Philosophy", 2),
            new Lecture("Sociology"),
            new Lecture("Introduction To Common Law", 1, StudyField.Law), //IsLawStudent
            new Lecture("Constitutional Law", 2, StudyField.Law) //IsLawStudent && YearOfStudy>1
        };

        private static List<Student> students = new List<Student>()
        {
            new Student("Angus", 1), new Student("Kane", 1, StudyField.Law), new Student("Lian", 2, StudyField.Law), new Student("Alissa", 2),
        };

        private static List<Teacher> teachers = new List<Teacher>() {new Teacher("Freestone"), new Teacher("Richmont"), new Teacher("Laker"), new Teacher("McCarroll")};

        public static List<Lecture> GetLectures()
        {
            return lectures;
        }

        public static List<Student> GetStudents()
        {
            return students;
        }

        public static List<Teacher> GetTeachers()
        {
            return teachers;
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lecture>().HasData(GetLectures());

            modelBuilder.Entity<Teacher>().HasData(GetTeachers());

            modelBuilder.Entity<Student>().HasData(GetStudents());
        }
    }
}