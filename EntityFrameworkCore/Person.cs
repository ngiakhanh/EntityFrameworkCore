using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCore
{
    public class Person
    {
        public int MyId { get; set; }
        [MaxLength(3)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
        public AdditionalPersonData AdditionalPersonData { get; set; }

        public ICollection<PersonPhone> Phones { get; set; } = new HashSet<PersonPhone>();

        public PersonType PersonType { get; set; }

        public ICollection<CompanyPerson> CompanyPersons { get; set; } = new HashSet<CompanyPerson>();

        public PersonResume PersonResume { get; set; }
    }

    public class Student : Person
    {
        public string College { get; set; }
    }
}