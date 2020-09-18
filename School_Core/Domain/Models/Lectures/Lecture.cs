using System;
using System.Collections.Generic;
using System.Linq;
using School_Core.Domain.Models.Students;
using School_Core.Domain.Models.Students.Specs;

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

        public void EnrollStudent(Student student)
        {
            if (student.Id == Guid.Empty) // kontroll ainult selleks et saaks testimist harjutada. 
            {
                throw new ArgumentException($"{student.Id} :  is not valid {nameof(student.Id)}");
            }

            var canEnroll = new CanEnrollSpec(student).IsSatisfiedBy(this);
            if (Status == LectureStatus.Open && canEnroll)
            {
                _enrollments.Add(new Enrollment(student.Id));
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