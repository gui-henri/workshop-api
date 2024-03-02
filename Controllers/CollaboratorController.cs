using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Entities;
using WorkshopApi.Contexts;
using WorkshopApi.Dtos;

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
        public async Task<IActionResult> PutCollaborator(Guid id, CollaboratorDTO collaboratorDTO)
        {
            if (collaboratorDTO == null)
            {
                return BadRequest();
            }

            var collaborator = await _context.Collaborators.FindAsync(id);
            if (collaborator == null)
            {
                return NotFound();
            }

            collaborator.Update(collaboratorDTO.Name);

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
        public async Task<ActionResult<Collaborator>> PostCollaborator(CollaboratorDTO collaboratorDTO)
        {
            var collaborator = Collaborator.FromDTO(collaboratorDTO);
            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCollaborator), new { id = collaborator.Id }, collaborator);
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
