using System;
using System.Collections.Generic;
using System.Linq;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Students;
using School_Core.Domain.Models.Students.Specs;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.ViewModels.Teachers;

namespace School_Core.ViewModels.Lectures
{
    public class LectureDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public StudyField FieldOfStudy { get; set; }

        public int CanTakeFromYear { get; set; }

        public TeacherDetailsViewModel Teacher { get; set; }
        public string TeacherName { get; set; }
        public int StudentCount { get; set; }

        public LectureStatus Status { get; set; }

        public IEnumerable<string> StudentNamesInLecture { get; set; }
        public bool IsRedirectedWithSuccess { get; set; }
        public object alert { get; set; }

        public interface IProvider
        {
            LectureDetailsViewModel Provide(Guid id, bool isRedirectedWithSuccess);
        }

        public class Provider : IProvider
        {
            private readonly TeacherDetailsViewModel.IProvider _teacherDetailsProvider;
            private readonly IQuery<Lecture> _lectureQuery;
            private readonly IQuery<Student> _studentQuery;

            public Provider(TeacherDetailsViewModel.IProvider teacherDetailsProvider, IQuery<Lecture> lectureQuery, IQuery<Student> studentQuery)
            {
                _teacherDetailsProvider = teacherDetailsProvider;
                _lectureQuery = lectureQuery;
                _studentQuery = studentQuery;
            }

            public LectureDetailsViewModel Provide(Guid id, bool isRedirectedWithSuccess)
            {
                var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
                if (lecture is null) throw new ArgumentException(nameof(id));
                
                var studentsNames = _studentQuery.GetAll(new InLectureSpec(id)).Select(x => x.Name);

                return new LectureDetailsViewModel
                {
                    Id = lecture.Id,
                    Name = lecture.Name,
                    FieldOfStudy = lecture.FieldOfStudy,
                    CanTakeFromYear = lecture.EnrollableFromYear,
                    TeacherName = lecture.Teacher?.Name ?? "none",
                    StudentCount = lecture.Enrollments.Count,
                    StudentNamesInLecture = studentsNames,
                    IsRedirectedWithSuccess = isRedirectedWithSuccess,
                    Status = lecture.Status
                };
            }
        }
    }
}