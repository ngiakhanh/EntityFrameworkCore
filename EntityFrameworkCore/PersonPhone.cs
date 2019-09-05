namespace EntityFrameworkCore
{
    public class PersonPhone
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}