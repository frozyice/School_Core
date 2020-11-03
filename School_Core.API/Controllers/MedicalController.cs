using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using School_Core.API.Contexts;
using School_Core.API.DTOs;
using School_Core.API.Models;

namespace School_Core.API.Controllers
{
    [ApiController]
    [Route("api/medical")]
    public class MedicalController : ControllerBase
    {
        private readonly SchoolMedicalDbContext _dbContext;

        public MedicalController(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public IActionResult GetMedicals()
        {
            var medicals = _dbContext.Medicals.ToList();
            var medicalsGetDtos = new List<MedicalGetDto>();
            foreach (var medical in medicals)
            {
                var sickLeaveGetDto = new MedicalGetDto()
                {
                    Id = medical.Id,
                    StudentId = medical.StudentId, 
                    Active = medical.DateTo is null ? "Yes" : "No",
                    Reason = medical.Reason
                };
                medicalsGetDtos.Add(sickLeaveGetDto);
            }
            return Ok(medicalsGetDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetMedical(Guid id)
        {
            var medical = _dbContext.Medicals.Find(id);
            if (medical is null)
            {
                return NotFound();
            }
            var sickLeaveGetDto = new MedicalGetDto()
            {
                Id = medical.Id,
                StudentId = medical.StudentId, 
                Active = medical.DateTo is null ? "Yes" : "No",
                Reason = medical.Reason
            };
            return Ok(sickLeaveGetDto);
        }
        
        [HttpGet("student/{studentId}")]
        public IActionResult GetStudentMedicals(Guid studentId)
        {
            var medicals = _dbContext.Medicals.Where(x=>x.StudentId == studentId).ToList();
            var medicalsGetDtos = new List<MedicalGetDto>();
            foreach (var medical in medicals)
            {
                var medicalGetDto = new MedicalGetDto()
                {
                    Id = medical.Id,
                    StudentId = medical.StudentId, 
                    Active = medical.DateTo is null ? "Yes" : "No",
                    Reason = medical.Reason
                };
                medicalsGetDtos.Add(medicalGetDto);
            }
            return Ok(medicalsGetDtos);
        }
        
        [HttpPost("student/{studentId}")]
        public IActionResult AddMedical(Guid studentId, MedicalPostDto medicalPostDto)
        {
            var medical = new Medical(studentId, medicalPostDto.Reason);
            _dbContext.Add(medical);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetMedical), new {id = medical.Id}, medicalPostDto);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateMedical(Guid id, MedicalPutDto medicalPutDto)
        {
            var medical = _dbContext.Medicals.FirstOrDefault(x => x.Id == id);
            if (medical == null)
            {
                return NotFound();
            }

            medical.ChangeReason(medicalPutDto.Reason);
            _dbContext.SaveChanges();
            return NoContent();
        }
        //
        // [HttpDelete("{id}")]
        // public IActionResult RemoveMedical(int id)
        // {
        //     var medical = MedicalData.Current.Dtos.FirstOrDefault(x => x.Id == id);
        //     MedicalData.Current.Dtos.Remove(medical);
        //     return NoContent();
        // }

    }
}