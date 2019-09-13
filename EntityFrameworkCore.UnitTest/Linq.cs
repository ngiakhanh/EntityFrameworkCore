using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace EntityFrameworkCore.UnitTest
{
    [TestClass]
    public class Linq : CreateDb
    {
        [TestMethod]
        public void QuerySyntax()
        {
            var query = from person in Context.Person
                        select person;

            Trace.WriteLine("Hello");
            foreach (var a in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(a));
            }
        }

        [TestMethod]
        public void MethodSyntax()
        {
            var query = Context.Person;
            Trace.WriteLine("Hello");
            foreach (var item in query)
            {
                Trace.WriteLine(item);
            }
        }

        [TestMethod]
        public void RecordSyntax()
        {
            var item = Context.Person.SingleOrDefault();
            Trace.WriteLine(item);
        }

        [TestMethod]
        public void FilterQuerySyntax()
        {
            var item = Context.Person.Where(p => p.LastName.Contains("a"))
                                      .Select(a=>a.BirthDate)
                                      .OrderBy(p=>p.Date)
                                      .ThenByDescending(a=>a.DayOfYear);
            Trace.WriteLine(item);
        }

        [TestMethod]
        public void QuantifierQuerySyntax()
        {
            var item = Context.Person.All(p => p.LastName.Contains("a"));
            Trace.WriteLine(item);
        }
    }
}