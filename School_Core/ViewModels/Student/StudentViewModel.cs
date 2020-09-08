using System.Collections.Generic;

namespace School_Core.ViewModels.Student
{

    public class StudentViewModel
    {
        public IEnumerable<StudentListViewModel> StudentListViewModels { get; set; }
        public string HeadingColor { get; set; }
        public string HeadingTitle { get; set; }

        public interface IProvider
        {
            StudentViewModel GetViewModel();
        }

        public class Provider : IProvider
        {
            private readonly StudentListViewModel.IProvider _studentListProvider;
            public Provider(StudentListViewModel.IProvider studentListProvider)
            {
                _studentListProvider = studentListProvider;
            }
            public StudentViewModel GetViewModel()
            {
                return new StudentViewModel()
                {
                    StudentListViewModels = _studentListProvider.GetViewModels(),
                    HeadingTitle = "Students",
                    HeadingColor = "#2874A6"
                };
            }
        }
    }
}
