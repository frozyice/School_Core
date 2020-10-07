using System.Collections.Generic;
using School_Core.Domain.Models.Lectures;

namespace School_Core.Domain.Models.Students
{
    public class Student : Entity
    {
        public int YearOfStudy { get; private set; }
        public StudyField FieldOfStudy { get; private set; }
        public ICollection<Enrollment> Enrollments { get; private set; }

        public Student(string name, int yearOfStudy = 1, StudyField field = StudyField.None) : base(name)
        {
            YearOfStudy = yearOfStudy;
            FieldOfStudy = field;
        }

        protected Student()
        {
        }
    }
}