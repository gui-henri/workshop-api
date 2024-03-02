using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Entities;

namespace WorkshopApi.Controllers
{
    [Route("api/workshops")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {
        private readonly WorkshopContext _context;

        public WorkshopController(WorkshopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workshop>>> GetWorkshop()
        {
            return await _context.Workshop.ToListAsync();
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
        public async Task<IActionResult> PutWorkshop(Guid id, Workshop workshop)
        {
            if (id != workshop.Id)
            {
                return BadRequest();
            }

            _context.Entry(workshop).State = EntityState.Modified;

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
        public async Task<ActionResult<Workshop>> PostWorkshop(Workshop workshop)
        {
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

        private bool WorkshopExists(Guid id)
        {
            return _context.Workshop.Any(e => e.Id == id);
        }
    }
}
