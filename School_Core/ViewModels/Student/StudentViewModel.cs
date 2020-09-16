using System.Collections.Generic;
using Domain.Specifications;
using School_Core.Domain.Models;
using School_Core.Querys;
using School_Core.Domain.Models.Students;
using School_Core.Domain.Models.Students.Specs;

namespace School_Core.ViewModels.Student
{

    public class StudentViewModel
    {
        public string Name { get; set; }
        public int YearOfStudy { get; set; }
        public StudyField FieldOfStudy { get; set; }

        public interface IProvider
        {
            IEnumerable<StudentViewModel> Provide(bool filterFirstYearStudents, bool filterLawStudents);
        }

        public class Provider : IProvider
        {
            private readonly IStudentQuery _query;

            public Provider(IStudentQuery query)
            {
                _query = query;
            }
            public IEnumerable<StudentViewModel> Provide(bool filterFirstYearStudents, bool filterLawStudents)
            {
                Specification<Domain.Models.Students.Student> spec = null;
                if (filterFirstYearStudents)
                {
                    spec = new IsFirstYearStudentSpecification();
                }

                if (filterLawStudents)
                {
                    if (spec == null)
                    {
                        spec = new IsLawStudentSpecification();
                    }
                    else
                    {
                        spec = spec && new IsLawStudentSpecification();
                    }
                }
                
                var students = _query.GetStudents(spec);
                var studentViewModels = new List<StudentViewModel>();
                foreach (var student in students)
                {
                    studentViewModels.Add(new StudentViewModel()
                    {
                        Name = student.Name,
                        YearOfStudy = student.YearOfStudy,
                        FieldOfStudy = student.FieldOfStudy
                    });
                }

                return studentViewModels;
            }
        }
    }
}