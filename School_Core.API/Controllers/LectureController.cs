using System;
using Microsoft.AspNetCore.Mvc;
using School_Core.Commands.Lectures;
using School_Core.Domain.Models.Lectures;
using School_Core.Queries;
using School_Core.Specifications;
using School_Core.Util;

namespace School_Core.API.Controllers
{
    [ApiController]
    [Route("api/lectures")]
    public class LectureController : ControllerBase
    {
        private readonly Messages _messages;
        private readonly IQuery<Lecture> _lectureQuery;

        public LectureController(Messages messages, IQuery<Lecture> lectureQuery)
        {
            _messages = messages;
            _lectureQuery = lectureQuery;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var lectures = _lectureQuery.GetAll();
            return Ok(lectures);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
            if (lecture is null) return NotFound();
            
            return Ok(lecture);
        }

        [HttpPut("{id}/status/close")]
        public IActionResult CloseLecture(Guid id)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
            if (lecture is null)
            {
                return NotFound();
            }
            
            var command = new CloseLectureCommand(id);
            var result = _messages.Dispatch(command);

            if (!result.isSuccess)
            {
                return NotFound();
            } 
            return Ok();
        }

        [HttpPut("{id}/status/archive")]
        public IActionResult ArchiveLecture(Guid id)
        {
            var lecture = _lectureQuery.GetSingleOrDefault(new HasIdSpec<Lecture>(id));
            if (lecture is null)
            {
                return NotFound();
            }
            
            var command = new ArchiveLectureCommand(id);
            var result = _messages.Dispatch(command);
            if (!result.isSuccess)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}