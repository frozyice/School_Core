using School_Core.Repositories;
using System;
using System.Collections.Generic;

namespace School_Core.ViewModels.Student
{
    public class StudentListViewModel
    {

        public static string pageColor = "#2874A6";
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HeadingColor { get; set; }
        public string HeadingTitle { get; set; }
        public interface IProvider
        {
            IEnumerable<StudentListViewModel> GetViewModels();
        }
        public class Provider : IProvider
        {
            private readonly IStudentRepository _studentRepository;
            public Provider(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }
            public IEnumerable<StudentListViewModel> GetViewModels()
            {
                var students = _studentRepository.GetStudents();
                var studentListViewModels = new List<StudentListViewModel>();
                foreach (var student in students)
                {
                    studentListViewModels.Add(new StudentListViewModel()
                    {
                        Id = student.Id,
                        Name = student.Name ?? "",
                        HeadingTitle = "Students",
                        HeadingColor = pageColor
                    });

                }
                return studentListViewModels;
            }
        }
    }

}
