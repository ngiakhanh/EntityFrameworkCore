using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityFrameworkCore.UnitTest
{
    [TestClass]
    public class Query
    {
        private ContactsContext _context;

        [TestMethod]
        public void QuerySyntax()
        {
            var query = from person in _context.Person
                select person;

            Trace.WriteLine("Hello");
            foreach (var item in query)
            {
                Trace.WriteLine(item);
            }
        }

        [TestMethod]
        public void MethodSyntax()
        {

        }

        [TestMethod]
        public void RecordSyntax()
        {

        }

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<ContactsContext>();
            builder.UseSqlServer(
                "Server=.;Database=ContactsDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
            _context = new ContactsContext(builder.Options);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}