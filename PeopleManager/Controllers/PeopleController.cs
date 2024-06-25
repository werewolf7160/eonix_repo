using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Class;

namespace PeopleManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleDataContext _context;

        public PeopleController(PeopleDataContext context)
        {
            _context = context;
        }

        // GET: api/People
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterName">Name used to filter, not case sensitive</param>
        /// <param name="filterSurname">Surname used to filter, not case sensitive</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetPersons([FromQuery] string? filterName, [FromQuery] string? filterSurname)
        {
            var peopleQuery = _context.Persons.AsQueryable();
            if (!string.IsNullOrEmpty(filterName))
                peopleQuery = peopleQuery.Where(x => x.Name.ToLower().Contains(filterName.ToLower()));

            if (!string.IsNullOrEmpty(filterSurname))
                peopleQuery = peopleQuery.Where(x => x.Surname.ToLower().Contains(filterSurname.ToLower()));
            return peopleQuery.ToList();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (person.Id == default) person.Id = id; //to be able to modify without specify Guid two time
            if (id != person.Id)
            {
                return BadRequest($"Id of update and Id of person aren't the same");
            }

            if (string.IsNullOrEmpty(person.Name)) return BadRequest($"Name of person can't be null or empty");
            if (string.IsNullOrEmpty(person.Surname)) return BadRequest($"Surname of person can't be null or empty");


            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            if (PersonExists(person.Id))
            {
                return StatusCode(409,
                    $"Id '{person.Id}' already used in database. Delete it or modify it before retrying");
            }
            if (string.IsNullOrEmpty(person.Name)) return BadRequest($"Name of person can't be null or empty");
            if (string.IsNullOrEmpty(person.Surname)) return BadRequest($"Surname of person can't be null or empty");
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(Guid id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}
