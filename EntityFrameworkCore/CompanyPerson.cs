namespace EntityFrameworkCore
{
    public class CompanyPerson
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}