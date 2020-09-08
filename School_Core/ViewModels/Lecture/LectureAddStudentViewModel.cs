using Microsoft.AspNetCore.Mvc;
using School_Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace School_Core.ViewModels.Lecture
{
    public class LectureAddStudentViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [HiddenInput]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Student name")]
        public string StudentName { get; set; }
        public List<string> Errors { get; set; }

        public interface IMapper
        {
            public Guid AddStudentToLecture(LectureAddStudentViewModel lectureAddStudentViewModel);
            public List<string> Validate(Guid lectureId, string studentName);
        }



        public class Mapper : IMapper
        {
            private readonly ILectureRepository _lectureRepository;
            private readonly IStudentRepository _studentRepository;
            public Mapper(ILectureRepository lectureRepository, IStudentRepository studentRepository)
            {
                _lectureRepository = lectureRepository;
                _studentRepository = studentRepository;
            }
            public Guid AddStudentToLecture(LectureAddStudentViewModel lectureAddStudentViewModel)
            {
                var lecture = _lectureRepository.GetLecture(lectureAddStudentViewModel.Id);
                var student = _studentRepository.GetStudentByName(lectureAddStudentViewModel.StudentName);
                //lecture.AddStudent(student);
                _lectureRepository.EditLecture(lecture);
                return lecture.Id;
            }

            public List<string> Validate(Guid lectureId, string studentName)
            {
                var errors = new List<string>();
                var lecture = _lectureRepository.GetLecture(lectureId);
                var student = _studentRepository.GetStudentByName(studentName);
                if (student == null)
                {
                    errors.Add("Added student does not exist.");
                }
                if (lecture.Enrollments != null)
                {
                    if (lecture.Enrollments.Count >= 15)
                    {
                        errors.Add("Max number of student is 15.");
                    }

               
                    //if (lecture.HasStudent(student))
                    //{
                    //    errors.Add("Added student is already added to lecture.");
                    //}
                }
                return errors;
            }

        }
        public interface IProvider
        {
            public LectureAddStudentViewModel Provide(Guid id);
        }

        public class Provider : IProvider
        {
            private readonly ILectureRepository _lectureRepository;

            public Provider(ILectureRepository lectureRepository)
            {
                _lectureRepository = lectureRepository;
            }

            public LectureAddStudentViewModel Provide(Guid id)
            {
                var lecture = _lectureRepository.GetLecture(id);
                var lectureAddStudentViewModel = new LectureAddStudentViewModel();
                lectureAddStudentViewModel.Id = id;
                lectureAddStudentViewModel.Name = lecture.Name;
                return lectureAddStudentViewModel;
            }
        }
    }
}
