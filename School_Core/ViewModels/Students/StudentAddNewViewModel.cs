using System.ComponentModel.DataAnnotations;

namespace School_Core.ViewModels.Students
{
    public class StudentAddNewViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}