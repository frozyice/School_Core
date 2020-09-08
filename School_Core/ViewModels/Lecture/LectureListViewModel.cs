using System.Collections.Generic;

namespace School_Core.ViewModels.Lecture
{
    public class LectureListViewModel
    {
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
                return new LectureListViewModel()
                {
                    LectureViewModels = _lectureProvider.Provide(),
                    HeadingTitle = "Lectures",
                    HeadingColor = "#1E8449"
                };
            }
        }

    }
}
