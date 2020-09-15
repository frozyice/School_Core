using System;
using System.Collections.Generic;
using School_Core.Querys;

namespace School_Core.ViewModels.Student
{
    public class StudentListViewModel
    {
        private static string _headingTitle = "Students";
        private static string _headingColor = "#2874A6";
        public string HeadingColor { get; set; }
        public string HeadingTitle { get; set; }
        public IEnumerable<StudentViewModel> StudentViewModels { get; set; } = new List<StudentViewModel>();
        public interface IProvider
        {
            StudentListViewModel Provide();
        }
        public class Provider : IProvider
        {
            private readonly StudentViewModel.IProvider _provider;
            
            public Provider(StudentViewModel.IProvider provider)
            {
                _provider = provider;
            }
            public StudentListViewModel Provide()
            {
                var studentListViewModel = new StudentListViewModel()
                {
                    HeadingColor = _headingColor,
                    HeadingTitle = _headingTitle,
                    StudentViewModels = _provider.Provide()
                };
                return studentListViewModel;
            }
        }
    }

}
