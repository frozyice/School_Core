using System.ComponentModel.DataAnnotations;

namespace School_Core.API.Model
{
    public class City
    {
        [Required]
        [EmailAddress]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}