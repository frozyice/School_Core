using System;
using System.Collections.Generic;
using School_Core.API.DTOs;
using School_Core.Domain.Models.Students;
using School_Core.Queries;
using School_Core.Specifications;

namespace School_Core.ViewModels.Students
{
    public class StudentMedicalViewModel
    {
        public string StudentName { get; set; }
        public List<MedicalGetDto> Medicals { get; set; } = new List<MedicalGetDto>();
        
        public interface IProvider
        {
            StudentMedicalViewModel Provide(Guid studentId, List<MedicalGetDto> medicals);
        }
        
        public class Provider : IProvider
        {
            private readonly IQuery<Student> _query;

            public Provider(IQuery<Student> query)
            {
                _query = query;
            }
            
            public StudentMedicalViewModel Provide(Guid studentId,List<MedicalGetDto> medicals)
            {
                var student = _query.GetSingleOrDefault(new HasIdSpec<Student>(studentId));
                if (student is null)
                {
                    throw new ArgumentException(nameof(student));
                }
                
                return new StudentMedicalViewModel
                {
                    StudentName = student.Name,
                    Medicals = medicals
                };
            }
        }
    }
}