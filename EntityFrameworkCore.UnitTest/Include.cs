using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace EntityFrameworkCore.UnitTest
{
    [TestClass]
    public class Include : CreateDb
    {

        [TestMethod]
        public void ChildDataInclude()
        {
            var query = Context.CompanyPerson.Include(p => p.Company)
                                             .Include(a => a.Person)
                                             .ThenInclude(a => a.Phones);
            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented, 
                    new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
            }
        }

        [TestMethod]
        public void GroupSyntax()
        {
            var group = Context.Person.GroupBy(a=>a.MyId, a=> a.Phones, (key, g)=> new {key, phonesCount=g.Count()});

            foreach (var person in group)
            {
                Trace.WriteLine(person);
            }
        }
    }
}