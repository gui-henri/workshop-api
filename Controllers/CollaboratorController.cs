using Microsoft.AspNetCore.Mvc;
using WorkshopApi.Entities;
using WorkshopApi.Dtos;
using WorkshopApi.Interfaces;

namespace WorkshopApi.Controllers
{
    [Route("api/colaboradores")]
    [ApiController]
    public class CollaboratorController(ICollaboratorRepository repository) : ControllerBase
    {
        private readonly ICollaboratorRepository _repository = repository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collaborator>>> GetCollaborators()
        {
            var collaborators = await _repository.GetCollaborators();
            return Ok(collaborators);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Collaborator>> GetCollaborator(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var collaborator = await _repository.GetCollaborator(id);

            if (collaborator == null)
            {
                return NotFound();
            }

            return collaborator;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollaborator(Guid id, CollaboratorDTO collaboratorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var collaborator = await _repository.UpdateCollaborator(id, collaboratorDTO);
            return collaborator == null ? NotFound() : Ok(collaborator);
        }

        [HttpPost]
        public async Task<ActionResult<Collaborator>> PostCollaborator(CollaboratorDTO collaboratorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var collaborator = await _repository.CreateCollaborator(collaboratorDTO);
            return collaborator == null ? NotFound() : Ok(collaborator);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollaborator(Guid id)
        {
            try
            {
                var collaborator = await _repository.DeleteCollaborator(id);
                if (collaborator == null)
                {
                    return NotFound();
                }

                return NoContent();

            } catch
            {
                return StatusCode(500);
            }
        }
    }
}
