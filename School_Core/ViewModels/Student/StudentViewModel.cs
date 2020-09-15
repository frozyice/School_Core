using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using School_Core.Domain.Models;
using School_Core.Querys;

namespace School_Core.ViewModels.Student
{

    public class StudentViewModel
    {
        public string Name { get; set; }
        public int YearOfStudy { get; set; }
        public StudyField FieldOfStudy { get; set; }

        public interface IProvider
        {
            IEnumerable<StudentViewModel> Provide();
        }

        public class Provider : IProvider
        {
            private readonly IStudentQuery _query;

            public Provider(IStudentQuery query)
            {
                _query = query;
            }
            public IEnumerable<StudentViewModel> Provide()
            {
                var students = _query.GetStudents();
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
