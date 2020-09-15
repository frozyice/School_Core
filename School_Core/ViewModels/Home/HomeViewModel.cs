using System.Collections.Generic;
using System.Linq;
using School_Core.Domain.Models;
using School_Core.Querys;

namespace School_Core.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<string> Labels { get; set; } = new List<string>();
        public List<int> Data { get; set;  } = new List<int>();
        public HomeViewModel()
        {
            Labels = new List<string>();
            Data = new List<int>();
        }
        
        public interface IProvider
        {
            HomeViewModel Provide();
        }
        
        public class Provider : IProvider
        {
            private readonly ILectureQuery _query;

            public Provider(ILectureQuery query)
            {
                _query = query;
            }

            public HomeViewModel Provide()
            {
                
                var homeViewModel = new HomeViewModel()
                {
                    Labels = new List<string>() { "Open", "Closed", "Archived"}
                };
                
                var lectures = _query.GetLectures();
                
                var openLectureCount = lectures.Where(x => x.Status == LectureStatus.Open).Count();
                
                var closedLectureCount = lectures.Where(x => x.Status == LectureStatus.Closed).Count();
                
                var archivedLectureCount = lectures.Where(x => x.Status == LectureStatus.Archived).Count();
                
                homeViewModel.Data.AddRange(new List<int>()
                {
                    openLectureCount,
                    closedLectureCount,
                    archivedLectureCount
                });

                return homeViewModel;
            }
        }
    }
}