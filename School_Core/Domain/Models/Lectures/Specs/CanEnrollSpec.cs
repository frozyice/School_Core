﻿using System;
using System.Linq;
using System.Linq.Expressions;
using School_Core.Domain.Models.Students;
using School_Core.Specifications;

namespace School_Core.Domain.Models.Lectures.Specs
{
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
            new HasExistingEnrollmentSpec(StudentId).Negate() &&
            new HasYearOfStudyToEnrollSpec(YearOfStudy) &&
            new HasOpenLectureStatusSpec() &&
            new HasFieldOfStudyToEnrollSpec(FieldOfStudy);
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

    public class HasYearOfStudyToEnrollSpec : Specification<Lecture>
    {
        public int YearOfStudy { get; }

        public HasYearOfStudyToEnrollSpec(int yearOfStudy)
        {
            YearOfStudy = yearOfStudy;
        }

        internal override Expression<Func<Lecture, bool>> Predicate => x => x.EnrollableFromYear <= YearOfStudy;
    }
    
    public class HasOpenLectureStatusSpec : Specification<Lecture>
    {
        internal override Expression<Func<Lecture, bool>> Predicate => x => x.Status == LectureStatus.Open;
    }

    public class HasFieldOfStudyToEnrollSpec : WrappedSpecification<Lecture>
    {
        public StudyField FieldOfStudy { get; }

        public HasFieldOfStudyToEnrollSpec(StudyField fieldOfStudy)
        {
            FieldOfStudy = fieldOfStudy;
        }

        public override Specification<Lecture> Specification => new IsWithFieldOfStudySpec(FieldOfStudy) || new IsEnrollableWithAnyFieldOfStudySpec();
    }

    public class IsWithFieldOfStudySpec : Specification<Lecture>
    {
        public StudyField FieldOfStudy { get; }

        public IsWithFieldOfStudySpec(StudyField fieldOfStudy)
        {
            FieldOfStudy = fieldOfStudy;
        }

        internal override Expression<Func<Lecture, bool>> Predicate => x => x.FieldOfStudy == FieldOfStudy;
    }

    public class IsEnrollableWithAnyFieldOfStudySpec : WrappedSpecification<Lecture>
    {
        public override Specification<Lecture> Specification => new IsWithFieldOfStudySpec(StudyField.None);
    }
}