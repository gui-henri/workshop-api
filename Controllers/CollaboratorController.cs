using Microsoft.AspNetCore.Mvc;
using WorkshopApi.Entities;
using WorkshopApi.Dtos;
using WorkshopApi.Services;

namespace WorkshopApi.Controllers
{
    [Route("api/colaboradores")]
    [ApiController]
    public class CollaboratorController(CollaboratorService service) : ControllerBase
    {
        private readonly CollaboratorService _service = service;

        /// <summary>
        /// Obtains the list of all collaborators.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/colaboradores
        ///
        /// </remarks>
        /// <returns>A list of Collaborators</returns>
        /// <response code="200">Returns the list of Collaboratos</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Collaborator>>> GetCollaborators()
        {
            var collaborators = await _service.GetCollaborators();
            return Ok(collaborators);
        }

        /// <summary>
        /// Obtains a single collaborator based on ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/colaboradores/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A single Collaborators</returns>
        /// <response code="200">Returns the collaborator linked to the provided ID</response>
        /// <response code="404">No collaborator linked to the provided ID was found</response>
        /// <response code="400">The provided ID format is not valid</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<Collaborator>> GetCollaborator(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var collaborator = await _service.GetCollaborator(id);

            if (collaborator == null)
            {
                return NotFound();
            }

            return collaborator;
        }

        /// <summary>
        /// Edit the fields of an instance of a collaborator.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collaboratorDTO"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/colaboradores/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        ///     {
        ///        "name": "xxxxxxxxxxx"
        ///     }
        ///
        /// </remarks>
        /// <returns>The updated collaborator</returns>
        /// <response code="200">Returns the updated collaborator</response>
        /// <response code="404">No collaborator linked to the provided ID was found</response>
        /// <response code="400">The provided ID or collaborator body format is not valid</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<IActionResult> PutCollaborator(Guid id, CollaboratorDTO collaboratorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var collaborator = await _service.UpdateCollaborator(id, collaboratorDTO);
            return collaborator == null ? NotFound() : Ok(collaborator);
        }

        /// <summary>
        /// Creates an instance of a collaborator.
        /// </summary>
        /// <param name="collaboratorDTO"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/colaboradores
        ///     {
        ///        "name": "xxxxxxxxxxx"
        ///     }
        ///
        /// </remarks>
        /// <returns>The ID of the new instance</returns>
        /// <response code="201">Successfully added a new collaborator</response>
        /// <response code="400">The provided ID or collaborator body format is not valid</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<Collaborator>> PostCollaborator(CollaboratorDTO collaboratorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var collaborator = await _service.CreateCollaborator(collaboratorDTO);
            if (collaborator == null)
            {
                return BadRequest();
            }

            var actionName = nameof(GetCollaborator);
            var objectValues = new { collaborator.Id };

            return CreatedAtAction(actionName, objectValues, objectValues);
        }

        /// <summary>
        /// Delete an instance of a collaborator.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/colaboradores/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        ///
        /// </remarks>
        /// <response code="204">Successfully deleted the collaborator</response>
        /// <response code="404">No collaborator linked to the provided ID was found</response>
        /// <response code="500">It wasn't possible to delete the collaborator</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteCollaborator(Guid id)
        {
            try
            {
                var collaborator = await _service.DeleteCollaborator(id);
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
