using School_Core.Models;

namespace School_Core.Domain.Models
{
    public class Teacher : Person
    {
        public Teacher(string name) : base(name)
        {
        }

        protected Teacher()
        {
        }
    }
}