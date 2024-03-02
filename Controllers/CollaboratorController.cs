using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Models;

namespace WorkshopApi.Controllers
{
    [Route("api/colaboradores")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly WorkshopContext _context;

        public CollaboratorController(WorkshopContext context)
        {
            _context = context;
        }

        // GET: api/WorkshopContext
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collaborator>>> GetCollaborators()
        {
            return await _context.Collaborators.ToListAsync();
        }

        // GET: api/Collaborator/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Collaborator>> GetCollaborator(int id)
        {
            var Collaborator = await _context.Collaborators.FindAsync(id);

            if (Collaborator == null)
            {
                return NotFound();
            }

            return Collaborator;
        }

        // PUT: api/Collaborator/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollaborator(int id, Collaborator Collaborator)
        {
            if (id != Collaborator.Id)
            {
                return BadRequest();
            }

            _context.Entry(Collaborator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollaboratorExists(id))
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

        // POST: api/Collaborator
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Collaborator>> PostCollaborator(Collaborator Collaborator)
        {
            _context.Collaborators.Add(Collaborator);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCollaborator), new { id = Collaborator.Id }, Collaborator);
        }

        // DELETE: api/Collaborator/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollaborator(int id)
        {
            var Collaborator = await _context.Collaborators.FindAsync(id);
            if (Collaborator == null)
            {
                return NotFound();
            }

            _context.Collaborators.Remove(Collaborator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollaboratorExists(int id)
        {
            return _context.Collaborators.Any(e => e.Id == id);
        }
    }
}
