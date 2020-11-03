using System;

namespace School_Core.API.DTOs
{
    public class MedicalPostDto
    {
        public Guid StudentId { get; set; }
        public string Reason { get; set; }
    }
}