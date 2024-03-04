using Microsoft.AspNetCore.Mvc;
using WorkshopApi.Entities;
using WorkshopApi.Dto;
using WorkshopApi.Services;

namespace WorkshopApi.Controllers
{
    [Route("api/workshops")]
    [ApiController]
    public class WorkshopController(WorkshopService workshopService) : ControllerBase
    {
        private readonly WorkshopService _workshopService = workshopService;

        /// <summary>
        /// Obtains the list of all workshops.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/workshops
        ///
        /// </remarks>
        /// <returns>A list of Workshops</returns>
        /// <response code="200">Returns the list of Workshops</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Workshop>>> GetWorkshops()
        {
            var workshops = await _workshopService.GetWorkshops();
            return Ok(workshops);
        }

        /// <summary>
        /// Obtains a single workshop based on ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/workshops/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A single workshop</returns>
        /// <response code="200">Returns the workshop linked to the provided ID</response>
        /// <response code="404">No workshop linked to the provided ID was found</response>
        /// <response code="400">The provided ID format is not valid</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<Workshop>> GetWorkshop(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var workshop = await _workshopService.GetWorkshop(id);
            if (workshop == null)
            {
                return NotFound();
            }

            return Ok(workshop);
        }

        /// <summary>
        /// Edit the fields of an instance of a workshop.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workshopDTO"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/workshop/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        ///     {
        ///        "name": "xxxxxxxxxxx",
        ///        "description": "xxxxxxxxxxxxxxx",
        ///        "date": "yyyy-mm-dd",
        ///     }
        ///
        /// </remarks>
        /// <returns>The updated workshop</returns>
        /// <response code="200">Returns the updated workshop</response>
        /// <response code="404">No workshop linked to the provided ID was found</response>
        /// <response code="400">The provided ID or workshop body format is not valid</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<IActionResult> PutWorkshop(Guid id, WorkshopDTO workshopDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var workshop = await _workshopService.UpdateWorkshop(id, workshopDTO);
                if (workshop == null)
                {
                    return NotFound();
                }

                return NoContent();
            } catch
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Creates an instance of a workshop.
        /// </summary>
        /// <param name="workshopDTO"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/workshops
        ///     {
        ///        "name": "xxxxxxxxxxx",
        ///        "description": "xxxxxxxxxxxxxxx",
        ///        "date": "yyyy-mm-dd",
        ///     }
        ///
        /// </remarks>
        /// <returns>The ID of the new instance</returns>
        /// <response code="201">Successfully added a new workshop</response>
        /// <response code="400">The provided ID or workshop body format is not valid</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<Workshop>> PostWorkshop(WorkshopDTO workshopDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var workshop = await _workshopService.CreateWorkshop(workshopDTO);

            if (workshop == null)
            {
                return BadRequest();
            }

            var actionName = nameof(GetWorkshop);
            var objectValues = new { workshop.Id };

            return CreatedAtAction(actionName, objectValues, objectValues);
        }

        /// <summary>
        /// Delete an instance of a workshop.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/workshops/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        ///
        /// </remarks>
        /// <response code="204">Successfully deleted the workshop</response>
        /// <response code="404">No workshop linked to the provided ID was found</response>
        /// <response code="500">It wasn't possible to delete the workshop</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteWorkshop(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var workshop = await _workshopService.DeleteWorkshop(id);
                if (workshop == null)
                {
                    return NotFound();
                }

                return NoContent();

            } catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Link a collaborator to a workshop.
        /// </summary>
        /// <param name="workshopId"></param>
        /// <param name="collaboratorId"></param>
        /// <returns>The ID's of both workshop and collaborator.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/workshops/xxxxxxxx-xxxx/collaborators/yyyyyyyyyy-yyyyy
        ///
        /// </remarks>
        /// <response code="200">Successfully linked workshop and collaborator</response>
        /// <response code="404">No workshop or collaborator linked to the provided ID was found</response>
        /// <response code="400">One of the provided ID's is not valid</response>
        [HttpPost]
        [Route("{workshopId}/collaborators/{collaboratorId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<CollaboratorWorkshop>> AddCollaboratorToWorkshop(Guid workshopId, Guid collaboratorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var collaborator = await _workshopService.AddCollaborator(workshopId, collaboratorId);

                if (collaborator == null)
                {
                    return NotFound();
                }

                return Ok(new {workshopId, collaboratorId});
            } catch
            {
                throw;
            }
        }

        /// <summary>
        /// Find all collaborators linked to a specific workshop.
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns>A list with all associated collaborators.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/workshops/xxxxxxxx-xxxx/collaborators
        ///
        /// </remarks>
        /// <response code="200">Successfully linked workshop and collaborator</response>
        /// <response code="400">The provided workshop ID is not valid</response>
        /// <response code="404">No workshop linked to the provided ID was found</response>
        [HttpGet]
        [Route("{workshopId}/collaborators")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Collaborator>>> GetCollaborators(Guid workshopId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var exists = await _workshopService.GetWorkshop(workshopId);
            if (exists == null)
            {
                return NotFound();
            }

            var collaborators = await _workshopService.GetCollaborators(workshopId);

            return Ok(collaborators);
        }
    }
}
