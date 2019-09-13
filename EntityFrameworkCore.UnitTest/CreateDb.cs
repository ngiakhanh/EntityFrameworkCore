using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityFrameworkCore.UnitTest
{
    [TestClass]
    public class CreateDb
    {
        protected ContactsContext Context;
        [TestMethod]
        public void ShouldCreateDb()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            Context.Person.AddRange(new Person
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
            Context.SaveChanges();
        }

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<ContactsContext>();
            builder.UseSqlServer(
                "Server=.;Database=ContactsDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
            Context = new ContactsContext(builder.Options);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }
        }
    }
}