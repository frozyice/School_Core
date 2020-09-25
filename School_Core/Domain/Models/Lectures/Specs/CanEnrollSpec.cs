using System;
using System.Linq;
using System.Linq.Expressions;
using School_Core.Domain.Models.Students;
using School_Core.Specifications;

namespace School_Core.Domain.Models.Lectures.Specs
{
    // public class IsWithFieldOfStudy<TEntity> : IsWithFieldOfStudyAbstract<TEntity> where TEntity : Lecture//, Student
    // {
    //
    //     public StudyField LectureFieldOfStudy { get; }
    //
    //     public IsWithFieldOfStudy(StudyField lectureFieldOfStudy)
    //     {
    //         LectureFieldOfStudy = lectureFieldOfStudy;
    //     }
    //     internal override Expression<Func<TEntity, bool>> Predicate => x => x.FieldOfStudy == LectureFieldOfStudy;
    // }
    //
    // public abstract class IsWithFieldOfStudyAbstract <TEntity> : Specification<TEntity>//, Student
    // {
    //     private Expression<Func<TEntity, bool>> Predicate { get; }
    // }

    public class IsWithFieldOfStudySpec : Specification<Lecture>
    {
        public StudyField LectureFieldOfStudy { get; }

        public IsWithFieldOfStudySpec(StudyField fieldOfStudy)
        {
            LectureFieldOfStudy = fieldOfStudy;
        }

        internal override Expression<Func<Lecture, bool>> Predicate => x => x.FieldOfStudy == LectureFieldOfStudy;
    }

    public class IsFieldOfStudyNoneSpec : Specification<Lecture>
    {
        internal override Expression<Func<Lecture, bool>> Predicate => x => x.FieldOfStudy == StudyField.None;
    }

    public class HasYearOfStudyToEnrollSpec : Specification<Lecture>
    {
        public int YearOfStudy { get; }

        public HasYearOfStudyToEnrollSpec(int yearOfStudy)
        {
            YearOfStudy = yearOfStudy;
        }

        internal override Expression<Func<Lecture, bool>> Predicate => x => x.EnrollableFromYear <= YearOfStudy;
    }

    public class HasExistingEnrollmentSpec : Specification<Lecture>
    {
        public Guid StudentId { get; }

        public HasExistingEnrollmentSpec(Guid studentId)
        {
            StudentId = studentId;
        }

        internal override Expression<Func<Lecture, bool>> Predicate => x => x.Enrollments.Any(x => x.StudentId == StudentId);
    }

    public class CanEnrollSpec : WrappedSpecification<Lecture>
    {
        public Guid StudentId { get; }
        public int YearOfStudy { get; }
        public StudyField FieldOfStudy { get; }

        public CanEnrollSpec(Student student)
        {
            StudentId = student.Id;
            YearOfStudy = student.YearOfStudy;
            FieldOfStudy = student.FieldOfStudy;
        }

        public override Specification<Lecture> Specification =>
            new HasExistingEnrollmentSpec(StudentId).Negate() && new HasYearOfStudyToEnrollSpec(YearOfStudy) &&
            (new IsWithFieldOfStudySpec(FieldOfStudy) || new IsFieldOfStudyNoneSpec());
    }
}