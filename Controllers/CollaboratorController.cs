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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collaborator>>> GetCollaborators()
        {
            return await _context.Collaborators.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Collaborator>> GetCollaborator(Guid id)
        {
            var Collaborator = await _context.Collaborators.FindAsync(id);

            if (Collaborator == null)
            {
                return NotFound();
            }

            return Collaborator;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollaborator(Guid id, Collaborator Collaborator)
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

        [HttpPost]
        public async Task<ActionResult<Collaborator>> PostCollaborator(Collaborator Collaborator)
        {
            _context.Collaborators.Add(Collaborator);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCollaborator), new { id = Collaborator.Id }, Collaborator);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollaborator(Guid id)
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

        private bool CollaboratorExists(Guid id)
        {
            return _context.Collaborators.Any(e => e.Id == id);
        }
    }
}
