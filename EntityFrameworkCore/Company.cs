using System;
using System.Collections.Generic;

namespace EntityFrameworkCore
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }     
        public DateTime? DateAdded { get; set; }

        public ICollection<CompanyPerson> CompanyPersons { get; set; } = new HashSet<CompanyPerson>();
    }
}