using System.Collections.Generic;
using System.Linq;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Students;
using School_Core.Domain.Models.Teachers;
using School_Core.Queries;

namespace School_Core.ViewModels
{
    public class CounterTableViewModel
    {
        public List<CounterViewModel> CounterViewModels { get; set; } = new List<CounterViewModel>();
        public string PageTitle { get; set; }
        public string BorderColor { get; set; }

        public class CounterViewModel
        {
            public string RowName { get; set; }
            public int Count { get; set; }
        }

        public interface IProvider
        {
            CounterTableViewModel GetViewModel(string borderColor, bool shouldAddTeachers = true);
        }

        public class Provider : IProvider
        {
            private readonly IQuery<Lecture> _lectureQuery;
            private readonly IQuery<Teacher> _teacherQuery;
            private readonly IQuery<Student> _studentQuery;

            public Provider(IQuery<Lecture> lectureQuery, IQuery<Teacher> teacherQuery, IQuery<Student> studentQuery)
            {
                _lectureQuery = lectureQuery;
                _teacherQuery = teacherQuery;
                _studentQuery = studentQuery;
            }


            public CounterTableViewModel GetViewModel(string borderColor, bool shouldAddTeachers)
            {
                var model = new CounterTableViewModel
                {
                    BorderColor = borderColor, PageTitle = "Counter"
                };

                model.CounterViewModels.Add(new CounterViewModel {RowName = "Lectures", Count = _lectureQuery.GetAll().ToList().Count()});

                model.CounterViewModels.Add(new CounterViewModel {RowName = "Students", Count = _studentQuery.GetAll().ToList().Count()});

                if (shouldAddTeachers)
                {
                    model.CounterViewModels.Add(new CounterViewModel {RowName = "Teachers", Count = _teacherQuery.GetAll().Count()});
                }

                return model;
            }
        }
    }
}