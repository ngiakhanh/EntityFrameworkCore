using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityFrameworkCore.UnitTest
{
    [TestClass]
    public class CreateDb
    {
        private ContactsContext _context;
        [TestMethod]
        public void ShouldCreateDb()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Person.AddRange(new Person
            {
                BirthDate = DateTime.Today,
                FirstName = "John",
                LastName = "Doe",
                IsActive = true
            }, new Person
            {
                BirthDate = DateTime.Today.AddYears(-1),
                FirstName = "Jane",
                LastName = "Doe",
                IsActive = true
            });
            _context.SaveChanges();
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