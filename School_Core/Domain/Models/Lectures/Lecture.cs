using System;
using System.Collections.Generic;
using System.Linq;
using School_Core.Domain.Models.Lectures.Specs;
using School_Core.Domain.Models.Students;
using School_Core.Domain.Models.Teachers;

namespace School_Core.Domain.Models.Lectures
{
    public class Lecture : Entity 
    {
        public virtual LectureStatus Status { get; private set; }
        public StudyField FieldOfStudy { get; private set; }

        public int EnrollableFromYear { get; private set; }

        //1:1
        public virtual Teacher Teacher { get; private set; }

        //M:M
        private List<Enrollment> _enrollments = new List<Enrollment>();
        public virtual IReadOnlyList<Enrollment> Enrollments => _enrollments.ToList();

        protected Lecture()
        {
        }

        public Lecture(string name, int enrollableFromYear = 1, StudyField fieldOfStudy = StudyField.None) : base (name)
        {
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

        public void ArchiveLecture()
        {
            if (CanArchive())
            {
                Status = LectureStatus.Archived;
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

        public void EnrollStudent(Student student)
        {
            if (student.Id == Guid.Empty)
            {
                throw new ArgumentException($"{student.Id} :  is not valid {nameof(student.Id)}");
            }

            var canEnroll = new CanEnrollSpec(student).IsSatisfiedBy(this);
            if (Status == LectureStatus.Open && canEnroll)
            {
                _enrollments.Add(new Enrollment(student.Id));
            }
        }

        public virtual void AssignTeacher(Teacher teacher)
        {
            Teacher = teacher;
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