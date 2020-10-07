using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using School_Core.Domain.Models.Lectures;
using School_Core.Queries;
using School_Core.Specifications;

namespace School_Core.ViewModels.Lectures
{
    public class EnrollStudentViewModel
    {
        [HiddenInput] public Guid LectureId { get; set; }

        public IEnumerable<StudentNotEnrolledViewModel> StudentsNotEnrolled { get; set; } = new List<StudentNotEnrolledViewModel>();

        [Required]
        [MaxLength(50)]
        [DisplayName("Student name")]
        public string StudentName { get; set; }

        public interface IProvider
        {
            public EnrollStudentViewModel Provide(Guid id);
        }

        public class Provider : IProvider
        {
            private readonly StudentNotEnrolledViewModel.IProvider _provider;
            private readonly IQuery<Lecture> _lectureQuery;


            public Provider(StudentNotEnrolledViewModel.IProvider provider, IQuery<Lecture> lectureQuery)
            {
                _provider = provider;
                _lectureQuery = lectureQuery;
            }

            public EnrollStudentViewModel Provide(Guid id)
            {
                var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
                if (lecture is null) throw new ArgumentException(nameof(id));
                
                var lectureAddStudentViewModel = new EnrollStudentViewModel
                {
                    LectureId = id,
                    StudentsNotEnrolled = _provider.Provide(id)
                };
                
                return lectureAddStudentViewModel;
            }
        }
    }
}