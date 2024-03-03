using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Entities;
using WorkshopApi.Contexts;
using WorkshopApi.Dto;

namespace WorkshopApi.Controllers
{
    [Route("api/workshops")]
    [ApiController]
    public class WorkshopController(WorkshopContext context) : ControllerBase
    {
        private readonly WorkshopContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workshop>>> GetWorkshop()
        {
            return await _context.Workshop.Include(c => c.CollaboratorWorkshops.Where(cw => cw.WorkshopId == c.Id)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Workshop>> GetWorkshop(Guid id)
        {
            var workshop = await _context.Workshop.FindAsync(id);

            if (workshop == null)
            {
                return NotFound();
            }

            return workshop;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkshop(Guid id, WorkshopDTO workshopDTO)
        {
            if (workshopDTO == null)
            {
                return BadRequest();
            }

            var workshop = await _context.Workshop.FindAsync(id);

            if (workshop == null)
            {
                return NotFound();
            }

            workshop.Update(workshopDTO.Name, workshopDTO.Description, workshopDTO.Date);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkshopExists(id))
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
        public async Task<ActionResult<Workshop>> PostWorkshop(WorkshopDTO workshopDTO)
        {
            var workshop = Workshop.FromDTO(workshopDTO);

            if (workshop == null)
            {
                return BadRequest();
            }

            _context.Workshop.Add(workshop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkshop", new { id = workshop.Id }, workshop);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkshop(Guid id)
        {
            var workshop = await _context.Workshop.FindAsync(id);
            if (workshop == null)
            {
                return NotFound();
            }

            _context.Workshop.Remove(workshop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("{workshopId}/collaborators/{collaboratorId}")]
        public async Task<ActionResult<CollaboratorWorkshop>> AddCollaboratorToWorkshop(Guid workshopId, Guid collaboratorId)
        {
            var workshop = await _context.Workshop.FindAsync(workshopId);
            var collaborator = await _context.Collaborators.FindAsync(collaboratorId);

            if (workshop == null || collaborator == null)
            {
                return NotFound();
            }


            var collaboratorWorkshop = new CollaboratorWorkshop
            {
                CollaboratorId = collaboratorId,
                WorkshopId = workshopId
            };

            workshop.CollaboratorWorkshops.Add(collaboratorWorkshop);
            collaborator.CollaboratorWorkshops.Add(collaboratorWorkshop);

            _context.Workshop.Update(workshop);
            _context.Collaborators.Update(collaborator);
            _context.CollaboratorWorkshop.Add(collaboratorWorkshop);
            await _context.SaveChangesAsync();

            return Created();
        }

        private bool WorkshopExists(Guid id)
        {
            return _context.Workshop.Any(e => e.Id == id);
        }
    }
}
