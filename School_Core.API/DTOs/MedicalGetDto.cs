using System;

namespace School_Core.API.DTOs
{
    public class MedicalGetDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string Active { get; set; }
        public string Reason { get; set; }
        
    }
}