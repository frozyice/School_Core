using School_Core.Repositories;
using System;
using System.Collections.Generic;

namespace School_Core.ViewModels.Teacher
{
    public class TeacherListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HeadingColor { get; set; }
        public string HeadingTitle { get; set; }
        public interface IProvider
        {
            IEnumerable<TeacherListViewModel> GetViewModels();
        }
        public class Provider : IProvider
        {
            private readonly ITeacherRepository _teacherRepository;
            public Provider(ITeacherRepository teacherRepository)
            {
                _teacherRepository = teacherRepository;
            }
            public IEnumerable<TeacherListViewModel> GetViewModels()
            {
                var teachers = _teacherRepository.GetTeachers();
                var teacherListViewModels = new List<TeacherListViewModel>();
                foreach (var teacher in teachers)
                {
                    teacherListViewModels.Add(new TeacherListViewModel()
                    {
                        Id = teacher.Id,
                        Name = teacher.Name ?? "",
                        HeadingTitle = "Teachers",
                        HeadingColor = "#B03A2E"
                    });

                }
                return teacherListViewModels;
            }
        }
    }
}
