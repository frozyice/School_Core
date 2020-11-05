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
        public Guid StudentId { get; set; }
        public List<MedicalReadDto> Medicals { get; set; } = new List<MedicalReadDto>();
        public MedicalWriteDto WriteDto { get; set; } = new MedicalWriteDto();

        public interface IProvider
        {
            StudentMedicalViewModel Provide(Guid studentId, List<MedicalReadDto> medicals);
        }
        
        public class Provider : IProvider
        {
            private readonly IQuery<Student> _query;

            public Provider(IQuery<Student> query)
            {
                _query = query;
            }
            
            public StudentMedicalViewModel Provide(Guid studentId,List<MedicalReadDto> medicals)
            {
                var student = _query.GetSingleOrDefault(new HasIdSpec<Student>(studentId));
                if (student is null)
                {
                    throw new ArgumentException(nameof(studentId));
                }
                
                return new StudentMedicalViewModel
                {
                    StudentName = student.Name,
                    StudentId = student.Id,
                    Medicals = medicals,
                    WriteDto = new MedicalWriteDto
                    {
                        Reason = null,
                    }
                };
            }
        }
    }
}