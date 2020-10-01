using System.Collections.Generic;

namespace School_Core.ViewModels.Teachers
{
    public class TeacherListViewModel
    {
        private static string _headingTitle = "Teachers";
        private static string _headingColor = "#B03A2E";
        public string HeadingColor { get; set; }
        public string HeadingTitle { get; set; }

        public IEnumerable<TeacherViewModel> Teachers { get; set; }

        public interface IProvider
        {
            TeacherListViewModel Provide();
        }

        public class Provider : IProvider
        {
            private readonly TeacherViewModel.IProvider _teacherProvider;

            public Provider(TeacherViewModel.IProvider teacherProvider)
            {
                _teacherProvider = teacherProvider;
            }
            

            public TeacherListViewModel Provide()
            {
                var teacherListViewModel = new TeacherListViewModel()
                {
                    HeadingColor = _headingColor,
                    HeadingTitle = _headingTitle,
                    Teachers = _teacherProvider.Provide()
                };

                return teacherListViewModel;
            }
        }
    }
}