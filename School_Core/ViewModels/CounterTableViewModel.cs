using School_Core.Repositories;
using System.Collections.Generic;
using System.Linq;

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
            private readonly ILectureRepository _lectureRepository;
            private readonly IStudentRepository _studentRepository;
            private readonly ITeacherRepository _teacherRepository;

            public Provider(ILectureRepository lectureRepository, IStudentRepository studentRepository, ITeacherRepository teacherRepository)
            {
                _lectureRepository = lectureRepository;
                _studentRepository = studentRepository;
                _teacherRepository = teacherRepository;
            }


            public CounterTableViewModel GetViewModel(string borderColor, bool shouldAddTeachers)
            {
                var model = new CounterTableViewModel
                {
                    BorderColor = borderColor,
                    PageTitle = "Counter" // vaatame pärast 
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


                model.CounterViewModels.Add(
                new CounterViewModel
                {
                    RowName = "Lectures",
                    Count = _lectureRepository.GetLectures().Count()
                }
                    );

                model.CounterViewModels.Add(new CounterViewModel
                {
                    RowName = "Students",
                    Count = _studentRepository.GetStudents().Count()
                });

                if (shouldAddTeachers)
                {
                    model.CounterViewModels.Add(new CounterViewModel
                    {
                        RowName = "Teachers",
                        Count = _teacherRepository.GetTeachers().Count()
                    });
                }

                return model;
            }
        }
    }
}
