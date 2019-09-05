﻿using System.Collections.Generic;

namespace EntityFrameworkCore
{
    public class PersonType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Person> People { get; set; }
    }
}