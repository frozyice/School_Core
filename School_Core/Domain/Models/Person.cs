﻿using System;

namespace School_Core.Models
{
    public class Person
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Person(string name)
        {
            Id = Guid.NewGuid();  //todo see ei tohiks siin olla
            Name = name;
        }

        protected Person()
        {
            
        }
    }
}
