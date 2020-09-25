using System.Collections.Generic;
using System.Linq;
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
            //CounterTableViewModel GetViewModelForStudent(string borderColor, string title);
        }

        public class Provider : IProvider
        {
            private readonly ILectureQuery _lectureQuery;
            private readonly ITeacherQuery _teacherQuery;
            private readonly IStudentQuery _studentQuery;

            public Provider(ILectureQuery lectureQuery, ITeacherQuery teacherQuery, IStudentQuery studentQuery)
            {
                _lectureQuery = lectureQuery;
                _teacherQuery = teacherQuery;
                _studentQuery = studentQuery;
            }


            public CounterTableViewModel GetViewModel(string borderColor, bool shouldAddTeachers)
            {
                var model = new CounterTableViewModel
                {
                    BorderColor = borderColor, PageTitle = "Counter" // vaatame pärast 
                };


                //var allCountables  =  smts smt();

                //foreach (var a in allCountables)
                //{
                //    model.CounterViewModels.Add(new CounterViewModel
                //    {

                //        Count = meetod,
                //        RowName = a.name
                //    });
                //}


                model.CounterViewModels.Add(new CounterViewModel {RowName = "Lectures", Count = _lectureQuery.GetAll().ToList().Count()});

                model.CounterViewModels.Add(new CounterViewModel {RowName = "Students", Count = _studentQuery.GetStudents().ToList().Count()});

                if (shouldAddTeachers)
                {
                    model.CounterViewModels.Add(new CounterViewModel {RowName = "Teachers", Count = _teacherQuery.GetAll().Count()});
                }

                return model;
            }
        }
    }
}