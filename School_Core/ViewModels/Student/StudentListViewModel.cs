using System.Collections.Generic;

namespace School_Core.ViewModels.Student
{
    public class StudentListViewModel
    {
        private static string _headingTitle = "Students";
        private static string _headingColor = "#2874A6";
        public string HeadingColor { get; set; }
        public string HeadingTitle { get; set; }
        public bool filterFirstYearStudents { get; set; }
        public bool FilterLawStudents { get; set; }
        public IEnumerable<StudentViewModel> StudentViewModels { get; set; } = new List<StudentViewModel>();
        public interface IProvider
        {
            StudentListViewModel Provide(bool filterFirstYearStudents, bool filterLawStudents);
        }
        public class Provider : IProvider
        {
            private readonly StudentViewModel.IProvider _provider;
            
            public Provider(StudentViewModel.IProvider provider)
            {
                _provider = provider;
            }
            public StudentListViewModel Provide(bool filterFirstYearStudents, bool filterLawStudents)
            {
                var studentListViewModel = new StudentListViewModel()
                {
                    HeadingColor = _headingColor,
                    HeadingTitle = _headingTitle,
                    StudentViewModels = _provider.Provide(filterFirstYearStudents, filterLawStudents)
                };
                return studentListViewModel;
            }
        }
    }

}
