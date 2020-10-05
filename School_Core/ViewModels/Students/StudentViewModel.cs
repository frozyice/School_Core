using System.Collections.Generic;
using School_Core.Domain.Models.Lectures;
using School_Core.Domain.Models.Students;
using School_Core.Domain.Models.Students.Specs;
using School_Core.Queries;
using School_Core.Specifications;

namespace School_Core.ViewModels.Students
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
            private readonly IQuery<Student> _studentQuery;

            public Provider(IQuery<Student> studentQuery)
            {
                _studentQuery = studentQuery;
            }

            public IEnumerable<StudentViewModel> Provide(bool filterFirstYearStudents, bool filterLawStudents)
            {
                Specification<Domain.Models.Students.Student> spec = null;
                if (filterFirstYearStudents)
                {
                    spec = new IsFirstYearStudentSpec();
                }

                if (filterLawStudents)
                {
                    if (spec == null)
                    {
                        spec = new IsLawStudentSpec();
                    }
                    else
                    {
                        spec = spec && new IsLawStudentSpec();
                    }
                }

                var students = _studentQuery.GetAll(spec);
                var studentViewModels = new List<StudentViewModel>();
                foreach (var student in students)
                {
                    studentViewModels.Add(new StudentViewModel
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