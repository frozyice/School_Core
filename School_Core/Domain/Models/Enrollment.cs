using System;

namespace School_Core.Domain.Models
{
    public class Enrollment
    {
        public Guid Id { get; private set; }
        public Guid LectureId { get; private set; }
        public Guid StudentId { get; private set; }
        public Grade Grade { get; private set; }

        public Enrollment(Guid studentId, Grade grade = Grade.None)
        {
            StudentId = studentId;
            Grade = grade;
        }
    }

    public enum Grade
    {
        None,
        A,
        B,
        C,
        D,
        E,
        F
    }
}