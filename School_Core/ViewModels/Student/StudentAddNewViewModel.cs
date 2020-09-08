using School_Core.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace School_Core.ViewModels.Student
{
    public class StudentAddNewViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //public string Error { get; set; }

        public interface IMapper
        {
            Guid AddNewStudent(StudentAddNewViewModel studentAddNewViewModel);
            string Validate(string studentName);
        }

        public class Mapper : IMapper
        {
            private readonly IStudentRepository _studentRepository;

            public Mapper(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }


            public Guid AddNewStudent(StudentAddNewViewModel studentAddNewViewModel)
            {
                var student = new Domain.Models.Student(studentAddNewViewModel.Name);
                _studentRepository.AddStudent(student);
                return student.Id;
            }


            public string Validate(string studentName) // validate meetod ei kuulu kindlasti siia klassi 
            {
                if (string.IsNullOrWhiteSpace(studentName))
                {
                    var error = "Student name is required.";
                    return error;
                }
                if (studentName.Length<2 || studentName.Length>10)
                {
                    return "Student name should be min 2 and max 10 characters.";
                }


                if (Regex.IsMatch(studentName, @"[ÄäÖö\d]"))
                {
                    return "Student name should not contain Ä,Ö or numbers.";
                }


                var name = _studentRepository.GetStudentByName(studentName);

                if (name!=null)
                {
                    var error = "Student with same name exists.";
                    return error;
                }

                return null;
            
            }
        }
    }
}
