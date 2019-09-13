using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityFrameworkCore.UnitTest
{
    [TestClass]
    public class Projection : CreateDb
    {
        [TestMethod]
        public void SelectMany()
        {
            var people = new List<Student>();

            // Select gets a list of lists of phone numbers
            var phoneLists = people.Select(p => p.PhoneNumbers);

            // SelectMany flattens it to just a list of phone numbers.
            var phoneNumbers = people.SelectMany(p => p.PhoneNumbers);

            // And to include data from the parent in the result: 
            // pass an expression to the second parameter (resultSelector) in the overload:
            var directory = people
                .SelectMany(p => p.PhoneNumbers,
                    (parent, child) => new { parent.Name, child.Number });
        }
    }

    public class PhoneNumber
    {
        public string Number { get; set; }
    }

    public class Student
    {
        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
        public string Name { get; set; }
    }
}