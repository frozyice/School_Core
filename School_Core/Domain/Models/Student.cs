using School_Core.Models;
using System.Collections.Generic;

namespace School_Core.Domain.Models
{
    public class Student : Person
    {
        
        public int YearOfStudy { get; private set; }
        public StudyField Field { get; private set; }
        public List<Enrollment> Enrollments { get; private set; }

        public Student(string name, int yearOfStudy = 1, StudyField field = StudyField.None) : base(name)
        { 
            YearOfStudy = yearOfStudy;
            Field = field;
        }
    }
}

