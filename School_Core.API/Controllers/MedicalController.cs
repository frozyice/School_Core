using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Core.API.Contexts;
using School_Core.API.DTOs;
using School_Core.API.Models;

namespace School_Core.API.Controllers
{
    [ApiController]
    [Route("medical")]
    public class MedicalController : ControllerBase
    {
        private readonly SchoolMedicalDbContext _dbContext;

        public MedicalController(SchoolMedicalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetMedicals()
        {
            var medicals = await _dbContext.Medicals.ToListAsync();
            var medicalReadDtos = new List<MedicalReadDto>();
            foreach (var medical in medicals)
            {
                var medicalReadDto = new MedicalReadDto()
                {
                    Id = medical.Id,
                    StudentId = medical.StudentId, 
                    Active = medical.DateTo is null ? "Yes" : "No",
                    Reason = medical.Reason
                };
                medicalReadDtos.Add(medicalReadDto);
            }
            return Ok(medicalReadDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedical(Guid id)
        {
            var medical = await _dbContext.Medicals.FindAsync(id);
            if (medical is null)
            {
                return NotFound();
            }
            var medicalReadDto = new MedicalReadDto()
            {
                Id = medical.Id,
                StudentId = medical.StudentId, 
                Active = medical.DateTo is null ? "Yes" : "No",
                Reason = medical.Reason
            };
            return Ok(medicalReadDto);
        }
        
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentMedicals(Guid studentId)
        {
            var medicals = await _dbContext.Medicals.Where(x=>x.StudentId == studentId).ToListAsync();
            var medicalReadDtos = new List<MedicalReadDto>();
            foreach (var medical in medicals)
            {
                var medicalReadDto = new MedicalReadDto()
                {
                    Id = medical.Id,
                    StudentId = medical.StudentId, 
                    Active = medical.DateTo is null ? "Yes" : "No",
                    Reason = medical.Reason
                };
                medicalReadDtos.Add(medicalReadDto);
            }
            return Ok(medicalReadDtos);
        }
        
        [HttpPost("student/{studentId}")]
        public async Task<IActionResult> AddMedical(Guid studentId, MedicalWriteDto medicalWriteDto)
        {
            var medical = new Medical(studentId, medicalWriteDto.Reason);
            await _dbContext.AddAsync(medical);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMedical), new {id = medical.Id}, medicalWriteDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedical(Guid id, MedicalWriteDto medicalWriteDto)
        {
            var medical = await _dbContext.Medicals.FirstOrDefaultAsync(x => x.Id == id);
            if (medical == null)
            {
                return NotFound();
            }

            medical.ChangeReason(medicalWriteDto.Reason);
            await  _dbContext.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> CloseMedical(Guid id)
        {
            var medical = await _dbContext.Medicals.FindAsync(id);
            if (medical is null)
            {
                return NotFound();
            }

            medical.Close();
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}