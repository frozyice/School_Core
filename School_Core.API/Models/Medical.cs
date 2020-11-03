using System;

namespace School_Core.API.Models
{
    public class Medical
    {
        public Guid Id { get; private set; }
        public Guid StudentId { get; private set; }
        public DateTime DateFrom { get; private set; }
        public DateTime? DateTo { get; private set; }
        public string Reason { get; private set; }

        public Medical(Guid studentId, string reason)
        {
            Id = new Guid();
            StudentId = studentId;
            Reason = reason;
            DateFrom = DateTime.Now;
        }
        
        public Medical(Guid id, Guid studentId, string reason)
        {
            Id = id;
            StudentId = studentId;
            Reason = reason;
            DateFrom = DateTime.Now;
        }

        public void ChangeReason(string reason)
        {
            Reason = reason;
        }
    }
}