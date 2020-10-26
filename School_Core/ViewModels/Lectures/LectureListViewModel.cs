using System.Collections.Generic;

namespace School_Core.ViewModels.Lectures
{
    public class LectureListViewModel
    {
        private static string _headingTitle = "Lectures";
        private static string _headingColor = "#1E8449";
        public object alert;
        public IEnumerable<LectureViewModel> LectureViewModels { get; set; }
        public string HeadingColor { get; set; }
        public string HeadingTitle { get; set; }
        public bool IsRedirectedWithSuccess { get; set; }

        public interface IProvider
        {
            LectureListViewModel Provide(bool isRedirectedWithSuccess);
        }

        public class Provider : IProvider
        {
            private readonly LectureViewModel.IProvider _lectureProvider;

            public Provider(LectureViewModel.IProvider lectureProvider)
            {
                _lectureProvider = lectureProvider;
            }

            public LectureListViewModel Provide(bool isRedirectedWithSuccess)
            {
                return new LectureListViewModel
                {
                    LectureViewModels = _lectureProvider.Provide(), 
                    HeadingTitle = _headingTitle, 
                    HeadingColor = _headingColor,
                    IsRedirectedWithSuccess = isRedirectedWithSuccess
                };
            }
        }
    }
}