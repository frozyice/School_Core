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
            var sickLeaves = _dbContext.SickLeaves.ToList();
            var sickLeaveGetDtos = new List<SickLeaveGetDto>();
            foreach (var sickLeave in sickLeaves)
            {
                var sickLeaveGetDto = new SickLeaveGetDto()
                {
                    Id = sickLeave.Id,
                    StudentId = sickLeave.Id, 
                    Active = sickLeave.DateTo is null ? "Yes" : "No",
                    Reason = sickLeave.Reason
                };
                sickLeaveGetDtos.Add(sickLeaveGetDto);
            }
            return Ok(sickLeaveGetDtos);
        }

        // [HttpGet("{id}", Name = "GetMedical")]
        // public IActionResult GetMedical(int id)
        // {
        //     var medical = MedicalData.Current.Dtos.FirstOrDefault(x => x.Id == id);
        //     if (medical is null)
        //         return NotFound();
        //     return Ok(medical);
        // }
        //
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