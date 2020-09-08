using System.Collections.Generic;

namespace School_Core.ViewModels.Teacher
{
    public class TeacherViewModel
    {
        public IEnumerable<TeacherListViewModel> TeacherListViewModels { get; set; }
        public string HeadingColor { get; set; }
        public string HeadingTitle { get; set; }

        public interface IProvider
        {
            TeacherViewModel GetViewModel();
        }

        public class Provider : IProvider
        {
            private readonly TeacherListViewModel.IProvider _teacherListProvider;
            public Provider(TeacherListViewModel.IProvider teacherListProvider)
            {
                _teacherListProvider = teacherListProvider;
            }

            public TeacherViewModel GetViewModel()
            {
                return new TeacherViewModel()
                {
                    TeacherListViewModels = _teacherListProvider.GetViewModels(),
                    HeadingTitle = "Teachers",
                    HeadingColor = "#B03A2E"
                };
            }
        }
    }
}
