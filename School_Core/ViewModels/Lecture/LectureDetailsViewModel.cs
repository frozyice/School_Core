using School_Core.Domain.Models;
using School_Core.Repositories;
using School_Core.ViewModels.Teacher;
using System;
using System.Collections.Generic;
using School_Core.Querys;

namespace School_Core.ViewModels.Lecture
{
    public class LectureDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public TeacherDetailsViewModel Teacher;
        public int StudentCount { get; set; }

        public LectureStatus Status { get; set; }

        public List<string> StudentNamesInLecture { get; set; }

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
                var studentNamesInLecture = new List<string>();
                if (lecture.Enrollments!=null)
                { 
                    foreach (var enrollment in lecture.Enrollments)
                    {
                        var student = _studentQuery.GetStudent(enrollment.StudentId);
                        studentNamesInLecture.Add(student.Name);
                    }
                }
//todo mis see on?
                TeacherDetailsViewModel teacher = null;
                if (lecture.Teacher!=null)
                {
                    teacher =_teacherDetailsProvider.GetViewModel(lecture.Teacher.Id);
                }
                //else
                //{
                //    teacher = new TeacherDetailsViewModel();
                //}


                return new LectureDetailsViewModel()
                {
                    Id = lecture.Id,
                    Name = lecture.Name ?? "",
                    Teacher = teacher,
                    StudentCount = lecture.Enrollments.Count,
                    StudentNamesInLecture = studentNamesInLecture,
                    Status = lecture.Status
                };
            }
        }

    }
}
