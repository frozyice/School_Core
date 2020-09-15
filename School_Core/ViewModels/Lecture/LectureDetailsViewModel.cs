using School_Core.Domain.Models;
using School_Core.ViewModels.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using School_Core.Domain.Models.Students.Specs;
using School_Core.Querys;

namespace School_Core.ViewModels.Lecture
{
    public class LectureDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public StudyField FieldOfStudy { get; set; }

        public int CanTakeFromYear { get; set; }

        public TeacherDetailsViewModel Teacher { get; set; }
        public string TeacherName => Teacher == null ? "none" : Teacher.Name;
        public int StudentCount { get; set; }

        public LectureStatus Status { get; set; }

        public IEnumerable<string> StudentNamesInLecture { get; set; }

        public interface IProvider
        {
            LectureDetailsViewModel Provide(Guid id);
        }

        public class Provider : IProvider
        {
            private readonly TeacherDetailsViewModel.IProvider _teacherDetailsProvider;
            private readonly ILectureQuery _lectureQuery;
            private readonly IStudentQuery _studentQuery;

            public Provider(TeacherDetailsViewModel.IProvider teacherDetailsProvider, ILectureQuery lectureQuery, IStudentQuery studentQuery)
            {
                _teacherDetailsProvider = teacherDetailsProvider;
                _lectureQuery = lectureQuery;
                _studentQuery = studentQuery;
            }

            public LectureDetailsViewModel Provide(Guid id)
            {
                var lecture = _lectureQuery.GetLecture(id);
                var studentsNames = _studentQuery.GetStudents(new InLectureSpecification(id)).Select(x => x.Name);

                return new LectureDetailsViewModel()
                {
                    Id = lecture.Id,
                    Name = lecture.Name,
                    FieldOfStudy = lecture.FieldOfStudy,
                    CanTakeFromYear = lecture.EnrollableFromYear,
                    Teacher = lecture.Teacher != null ? _teacherDetailsProvider.Provide(lecture.Teacher.Id) : null,
                    StudentCount = lecture.Enrollments.Count,
                    StudentNamesInLecture = studentsNames,
                    Status = lecture.Status
                };
            }
        }
    }
}