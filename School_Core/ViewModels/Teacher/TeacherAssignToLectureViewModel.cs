using System;
using System.Collections.Generic;
using School_Core.ViewModels.Lecture;

namespace School_Core.ViewModels.Teacher
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

            public Provider(LectureViewModel.IProvider lectureProvider)
            {
                _lectureProvider = lectureProvider;
            }
            public TeacherAssignToLectureViewModel Provide(Guid teacherId)
            {
                return new TeacherAssignToLectureViewModel()
                {
                    TeacherId = teacherId,
                    Lectures = _lectureProvider.Provide() 
                };
            }
        }

    }
}