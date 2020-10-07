using System;

namespace School_Core.Domain.Models
{
    public class Entity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Entity(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        protected Entity()
        {
        }
    }
}