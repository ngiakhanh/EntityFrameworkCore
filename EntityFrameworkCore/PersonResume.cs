namespace EntityFrameworkCore
{
    public class PersonResume
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string ResumeText { get; set; }
    }
}