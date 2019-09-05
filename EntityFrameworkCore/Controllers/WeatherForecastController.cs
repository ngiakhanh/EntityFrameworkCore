using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm1", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ContactsContext _context;

        public WeatherForecastController(ContactsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            var newPerson = new Person()
            {
                FirstName = "Sergey",
                LastName = "Barksley"
            };
            _context.Person.Add(newPerson);

            await _context.SaveChangesAsync();
            return _context.Person.ToList();
        }
    }
}