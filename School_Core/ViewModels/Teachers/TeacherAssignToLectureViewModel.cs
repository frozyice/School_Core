using System;
using System.Collections.Generic;
using School_Core.Domain.Models.Teachers;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.ViewModels.Lectures;

namespace School_Core.ViewModels.Teachers
{
    public class TeacherAssignToLectureViewModel
    {
        public string TempDummyVal { get; set; } = "missing";
        public Guid TeacherId { get; set; }
        public Guid LectureId { get; set; }
        public IEnumerable<LectureViewModel> Lectures { get; set; }
        public interface IProvider
        {
            TeacherAssignToLectureViewModel Provide(Guid teacherId);
        }

        public class Provider : IProvider
        {
            private readonly LectureViewModel.IProvider _lectureProvider;
            private readonly IQuery<Teacher> _teacherQuery;

            public Provider(LectureViewModel.IProvider lectureProvider, IQuery<Teacher> teacherQuery)
            {
                _lectureProvider = lectureProvider;
                _teacherQuery = teacherQuery;
            }
            public TeacherAssignToLectureViewModel Provide(Guid teacherId)
            {
                var teacher = _teacherQuery.GetSingleOrDefault(new HasIdSpec<Teacher>(teacherId));
                if (teacher is null) throw new ArgumentException(nameof(teacherId));
                
                return new TeacherAssignToLectureViewModel
                {
                    TeacherId = teacherId,
                    Lectures = _lectureProvider.Provide() 
                };
            }
        }

    }
}