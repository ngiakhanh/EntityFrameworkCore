using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityFrameworkCore.UnitTest
{
    [TestClass]
    public class Aggregation : CreateDb
    {
        [TestMethod]
        public void MinSyntax()
        {
            var min = Context.Person.Min(a=>a.MyId);
            Trace.WriteLine(min);
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