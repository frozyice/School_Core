using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace School_Core.ViewModels.Lecture
{
    public class EnrollStudentViewModel
    {
        [HiddenInput]
        public Guid LectureId { get; set; }

        public IEnumerable<StudentNotEnrolledViewModel> StudentsNotEnrolled { get; set; } = new List<StudentNotEnrolledViewModel>();
        
        [Required]
        [MaxLength(50)]
        [DisplayName("Student name")]
        public string StudentName { get; set; }
        public List<string> Errors { get; set; }
        
        public interface IProvider
        {
            public EnrollStudentViewModel Provide(Guid id);
        }

        public class Provider : IProvider
        {
            private readonly StudentNotEnrolledViewModel.IProvider _provider;


            public Provider(StudentNotEnrolledViewModel.IProvider provider)
            {
                _provider = provider;
            }

            public EnrollStudentViewModel Provide(Guid id)
            {
                var lectureAddStudentViewModel = new EnrollStudentViewModel()
                {
                    LectureId = id,
                    StudentsNotEnrolled = _provider.Provide(id)
                };
                // var lecture = _lectureRepository.GetLecture(id);
                // lectureAddStudentViewModel.Id = id;
                // lectureAddStudentViewModel.Name = lecture.Name;
                
                
                return lectureAddStudentViewModel;
            }
        }
    }
}