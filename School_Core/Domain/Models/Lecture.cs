using System;
using System.Collections.Generic;
using System.Linq;

namespace School_Core.Domain.Models
{
    public class Lecture

    {

        public virtual Guid Id { get; private set; }
        public string Name { get; private set; }
        public LectureStatus Status { get; private set; }

        public StudyField FieldOfStudy { get; private set; }

        public int CanTakeFromYear { get; private set; }


        //1:1
        public Teacher Teacher { get; private set; }
        //M:M
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        // public List<Enrollment> _enrollments = new List<Enrollment>();
        // public virtual IReadOnlyList<Enrollment> Enrollments => _enrollments.ToList();
        
        // private ICollection<Enrollment> _enrollments;
        // protected virtual ICollection<Enrollment> EnrollmentsCollection
        // {
        //     get { return _enrollments ?? (_enrollments = new HashSet<Enrollment>()); }
        //     set { _enrollments = value; }
        // }
        //
        // public virtual IReadOnlyCollection<Enrollment> Enrollments => EnrollmentsCollection.ToList().AsReadOnly();
        
        
        

        protected Lecture()
        {
            
        }
        
        public Lecture(string name, int canTakeFromYear = 1, StudyField fieldOfStudy = StudyField.None)
        {
            Id = Guid.NewGuid();
            Name = name;
            CanTakeFromYear = canTakeFromYear;
            FieldOfStudy = fieldOfStudy;
            Status = LectureStatus.Open; //todo: default: closed, lecture has teacher ? open
            Enrollments = new List<Enrollment>();
        }

        public void CloseLectureEnrollment()
        {
            if (Status == LectureStatus.Open)
            {
                Status = LectureStatus.Closed;
            }
        }
        public bool CanArchive()
        {
            if (Enrollments.Count == 0)
            {
                return true;
            }

            var a = Enrollments;
            //var canArchive = Enrollments.All(x => x.Grade != Grade.None);

            //return canArchive;
            return true;
        }

        public void ArchiveLecture()
        {
            if (CanArchive())
            {
                Status = LectureStatus.Archived;
            }
        }

        public void EnrollStudent2(Enrollment e)
        {
            Enrollments.Add(e);
        }

        public void EnrollStudent(Guid studentId) 
        {
            //todo: juba on opilane seal  ?? kirjuta test enne näiteks / mingi exception ka mingil juhul
            if (studentId == Guid.Empty)
            {
                throw new ArgumentException($"{studentId} :  is not valid {nameof(studentId)} ");
            }
            var existingEnrollment = Enrollments.FirstOrDefault(x => x.StudentId == studentId); 
            //todo: throw
            if (Status == LectureStatus.Open && existingEnrollment==null)
            {
                Enrollments.Add(new Enrollment(studentId));
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