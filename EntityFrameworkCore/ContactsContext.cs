using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore
{
    public class ContactsContext : DbContext
    {
        public readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => {
                builder.AddConsole();
            }
        );
        public ContactsContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> Person { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<PersonPhone> PersonPhone { get; set; }
        public DbSet<PersonType> PersonType { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyPerson> CompanyPerson { get; set; }
        public DbSet<PersonResume> PersonResume { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory) //tie-up DbContext with LoggerFactory object
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("People", "Contacts").HasKey(a => a.MyId);
            modelBuilder.Entity<Person>().HasIndex(a => a.LastName).HasName("IX_LNAME");
            modelBuilder.Entity<Person>().HasAlternateKey(a => new {a.LastName, a.FirstName}).HasName("IX_LNAME_FNAME");
            modelBuilder.Ignore<AdditionalPersonData>();
            modelBuilder.Entity<Person>().Property(p => p.LastName)
                                         .IsRequired()
                                         .HasMaxLength(50)
                                         .IsUnicode(false)
                                         .HasColumnName("LNAME");

            modelBuilder.Entity<Person>().Property(p => p.BirthDate).HasColumnType("date")
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Person>().Property(p => p.IsActive).HasDefaultValue(true);

            //One to many
            modelBuilder.Entity<Person>()
                        .HasMany(g => g.Phones)
                        .WithOne(s => s.Person)
                        .HasForeignKey(a=>a.PersonId)
                        //.HasConstraintName("FK_People_PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

            //Many to many
            modelBuilder.Entity<CompanyPerson>().HasKey(sc => new { sc.CompanyId, sc.PersonId });
            modelBuilder.Entity<CompanyPerson>()
                .HasOne(g => g.Company)
                .WithMany(s => s.CompanyPersons)
                .HasForeignKey(a => a.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompanyPerson>()
                .HasOne(g => g.Person)
                .WithMany(s => s.CompanyPersons)
                .HasForeignKey(a => a.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            //One to one
            modelBuilder.Entity<PersonResume>().HasKey(e => e.PersonId);
            modelBuilder.Entity<Person>()
                .HasOne(a => a.PersonResume)
                .WithOne(a => a.Person)
                .HasForeignKey<PersonResume>(a => a.PersonId)
                .HasConstraintName("FK_PEOPLE_PERSONID")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}