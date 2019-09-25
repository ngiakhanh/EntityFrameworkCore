using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ContactsContext _context;
        private readonly IMapper _mapper;
        public PeopleController(ContactsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Person.ToListAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPersonById(int id)
        {
            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }
            
            return person;
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> PutPerson(int id,[FromBody] Person person)
        {
            if (id != person.MyId)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(person).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPersonById", new { id = person.MyId }, person); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                throw;
            }
        }

        // POST: api/People
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            person.FirstName = "aaasdddsds";
            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonById", new {id = person.MyId}, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.MyId == id);
        }
    }
}