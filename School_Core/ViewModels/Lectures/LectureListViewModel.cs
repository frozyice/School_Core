using System.Collections.Generic;

namespace School_Core.ViewModels.Lectures
{
    public class LectureListViewModel
    {
        private static string _headingTitle = "Lectures";
        private static string _headingColor = "#1E8449";
        public IEnumerable<LectureViewModel> LectureViewModels { get; set; }
        public string HeadingColor { get; set; }
        public string HeadingTitle { get; set; }

        public interface IProvider
        {
            LectureListViewModel Provide();
        }

        public class Provider : IProvider
        {
            private readonly LectureViewModel.IProvider _lectureProvider;

            public Provider(LectureViewModel.IProvider lectureProvider)
            {
                _lectureProvider = lectureProvider;
            }

            public LectureListViewModel Provide()
            {
                return new LectureListViewModel() {LectureViewModels = _lectureProvider.Provide(), HeadingTitle = _headingTitle, HeadingColor = _headingColor};
            }
        }
    }
}