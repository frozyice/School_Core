using School_Core.Repositories;
using System;

namespace School_Core.ViewModels.Teacher
{
    public class TeacherDetailsViewModel
    {
        public string Name { get; set; }
        public string Phonenumber { get; set; }

        public interface IProvider
        {
            TeacherDetailsViewModel GetViewModel(Guid id);
        }

        public class Provider : IProvider
        {
            private readonly ITeacherRepository _teacherRepository;

            public Provider(ITeacherRepository teacherRepository)
            {
                _teacherRepository = teacherRepository;
            }
            public TeacherDetailsViewModel GetViewModel(Guid id)
            {
                var teacher = _teacherRepository.GetTeacher(id);

                return new TeacherDetailsViewModel()
                {
                    Name = teacher.Name ?? "Nimi puudub",
                    
                };
            }
        }
    }

}
