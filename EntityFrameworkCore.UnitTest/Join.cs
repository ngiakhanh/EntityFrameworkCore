using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace EntityFrameworkCore.UnitTest
{
    [TestClass]
    public class Join : CreateDb
    {
        [TestMethod]
        public void InnerJoinsQuery()
        {
            var query = Context.Person
                .Join(
                    Context.PersonType,
                    person => person.PersonTypeId,
                    personType => personType.Id,
                    (person, type) => new
                    {
                        PersonLastName = person.LastName,
                        PersonType = type.Name
                    });
                //.Select(p=>new
                //{
                //    p.Persons.LastName,
                //    p.Persons.FirstName,
                //    p.PersonType.Name
                //});

            var i = query.Count();

            foreach (var person in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(person));
            }
        }
    }
}