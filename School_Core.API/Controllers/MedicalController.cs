using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using School_Core.API.Contexts;
using School_Core.API.DTOs;

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
        
        // [HttpPost]
        // public IActionResult AddMedical(SickLeaveGetDto sickLeaveGetDto)
        // {
        //     var data = MedicalData.Current.Dtos;
        //     data.Add(sickLeaveGetDto);
        //     return CreatedAtAction(nameof(GetMedical), new {id = sickLeaveGetDto.Id}, sickLeaveGetDto);
        // }
        //
        // [HttpPut("{id}")]
        // public IActionResult UpdateMedical(int id, SickLeaveGetDto sickLeaveGetDto)
        // {
        //     var medical = MedicalData.Current.Dtos.FirstOrDefault(x => x.Id == id);
        //     if (medical == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     MedicalData.Current.Dtos.Remove(medical);
        //     medical = sickLeaveGetDto;
        //     MedicalData.Current.Dtos.Add(medical);
        //     return NoContent();
        // }
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