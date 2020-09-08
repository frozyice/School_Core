using School_Core.Repositories;
using System;

namespace School_Core.ViewModels.Student
{
    public class StudentDetailsViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public interface IProvider
        {
            StudentDetailsViewModel GetViewModel(Guid id);
        }

        public class Provider : IProvider
        {
            private readonly IStudentRepository _studentRepository;

            public Provider(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }
            public StudentDetailsViewModel GetViewModel(Guid id)
            {
                var student = _studentRepository.GetStudent(id);
                return new StudentDetailsViewModel()
                {
                    Name = student.Name ?? "",
                };
            }
        }
    }
}
