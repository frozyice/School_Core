using System;

namespace School_Core.API.Models
{
    public class SickLeave
    {
        public Guid Id { get; private set; }
        public Guid StudentId { get; private set; }
        public DateTime DateFrom { get; private set; }
        public DateTime? DateTo { get; private set; }
        public string Reason { get; private set; }

        public SickLeave(Guid studentId, string reason)
        {
            Id = new Guid();
            StudentId = studentId;
            Reason = reason;
            DateFrom = DateTime.Now;
        }
        
        public SickLeave(Guid id, Guid studentId, string reason)
        {
            Id = id;
            StudentId = studentId;
            Reason = reason;
            DateFrom = DateTime.Now;
        }
        
    }
}