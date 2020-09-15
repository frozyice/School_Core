using System;
using System.Collections.Generic;
using System.Linq;
using School_Core.Domain.Models.Students;

namespace School_Core.Domain.Models
{
    public class Lecture
    {
        public virtual Guid Id { get; private set; }
        public string Name { get; private set; }
        public LectureStatus Status { get; private set; }
        public StudyField FieldOfStudy { get; private set; }
        public int EnrollableFromYear { get; private set; }
        //1:1
        public Teacher Teacher { get; private set; }

        //M:M
        private List<Enrollment> _enrollments = new List<Enrollment>();
        public virtual IReadOnlyList<Enrollment> Enrollments => _enrollments.ToList();

        protected Lecture()
        {
        }

        public Lecture(string name, int enrollableFromYear = 1, StudyField fieldOfStudy = StudyField.None)
        {
            Id = Guid.NewGuid();
            Name = name;
            EnrollableFromYear = enrollableFromYear;
            FieldOfStudy = fieldOfStudy;
            Status = LectureStatus.Open; //todo: default: closed, lecture has teacher ? open
        }

        public void CloseLecture()
        {
            if (Status == LectureStatus.Open)
            {
                Status = LectureStatus.Closed;
            }
        }

        public bool CanArchive()
        {
            if (_enrollments.Count == 0)
            {
                return true;
            }

            return _enrollments.All(x => x.Grade != Grade.None);
        }

        public void ArchiveLecture()
        {
            if (CanArchive())
            {
                Status = LectureStatus.Archived;
            }
        }

        public bool CanEnroll(Student student)
        {
            var existingEnrollment = _enrollments.SingleOrDefault(x => x.StudentId == student.Id);
            if (existingEnrollment != null)
            {
                return false;
            }

            var a = EnrollableFromYear <= student.YearOfStudy;
            var b = FieldOfStudy == student.FieldOfStudy;
            var c = FieldOfStudy == StudyField.None;
            var d = b || c;
            var e = a && (b || c);
            
            if (EnrollableFromYear <= student.YearOfStudy && (FieldOfStudy == student.FieldOfStudy || FieldOfStudy == StudyField.None))
            {
                return true;
            }

            return false;
        }

        public void EnrollStudent(Guid studentId)
        {
            if (studentId == Guid.Empty) // kontroll ainult selleks et saaks testimist harjutada. 
            {
                throw new ArgumentException($"{studentId} :  is not valid {nameof(studentId)}");
            }

            var existingEnrollment = _enrollments.SingleOrDefault(x => x.StudentId == studentId);
            if (Status == LectureStatus.Open && existingEnrollment == null)
            {
                _enrollments.Add(new Enrollment(studentId));
            }
        }
    }

    public enum LectureStatus
    {
        Open,
        Closed,
        Archived
    }

    public enum StudyField
    {
        None,
        Law
    }
}