using School_Core.Domain.Models;
using School_Core.Repositories;
using School_Core.ViewModels.Teacher;
using System;
using System.Collections.Generic;

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
            private readonly ILectureRepository _lectureRepository;
            private readonly TeacherDetailsViewModel.IProvider _teacherDetailsProvider;

            public Provider(ILectureRepository lectureRepository, TeacherDetailsViewModel.IProvider teacherDetailsProvider)
            {
                _lectureRepository = lectureRepository;
                _teacherDetailsProvider = teacherDetailsProvider;
            }
            public LectureDetailsViewModel Provide(Guid id)
            {
                var lecture = _lectureRepository.GetLecture(id);
                var studentNamesInLecture = new List<string>();
                if (lecture.Enrollments!=null)
                { 
                    foreach (var student in lecture.Enrollments)
                    {
                        //todo: pole vajalik
                        //studentNamesInLecture.Add(student.Student.Name);
                    }
                }

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
