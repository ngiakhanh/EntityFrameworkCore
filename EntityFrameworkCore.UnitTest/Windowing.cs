using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace EntityFrameworkCore.UnitTest
{
    [TestClass]
    public class Windowing : CreateDb
    {
        public void PagingMethodSyntax()
        {
            var pageSize = 2;
            var pageNumber = 1;

            var query = Context.Person.OrderBy(p => p.LastName).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            foreach (var person in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(person));
            }
        }
    }
}