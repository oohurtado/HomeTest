using Home.Models.DTOs;
using Home.Source.BusinessLayer;
using Home.Source.Common;
using Home.Source.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Home.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventLayer eventLayer;

        public EventsController(EventLayer eventLayer)
        {
            this.eventLayer = eventLayer;
        }

        [HttpGet(template: "getEvent/{personId}/{eventId}")]
        public async Task<ActionResult<EventDTO>> GetEvent(int personId, int eventId)
        {
            var result = await eventLayer.GetEventAsync(userId: GetUserId(), personId, eventId);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpGet(template: "getEventsByPage/{personId}/{orderColumn}/{order}/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PageData<EventDTO>>> GetEventsByPage(int personId, string orderColumn, string order, int pageNumber, int pageSize, string term = null!, string searchColumn = null!)
        {
            var page = PaginatorFactory.CreatePage(PaginatorPage.Events, orderColumn, order, pageNumber, pageSize, term, searchColumn);
            return await eventLayer.GetEventsByPageAsync(userId: GetUserId(), personId: personId, page);
        }

        [HttpPost(template: "createEvent/{personId}")]
        public async Task<ActionResult> CreateEvent([FromBody] EventEditorDTO dto, int personId)
        {
            var result = await eventLayer.CreateEventAsync(userId: GetUserId(), personId: personId, dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut(template: "updateEvent/{personId}/{eventId}")]
        public async Task<ActionResult> UpdateEvent(int personId, int eventId, [FromBody] EventEditorDTO dto)
        {
            var result = await eventLayer.UpdateEventAsync(userId: GetUserId(), personId, eventId, dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete(template: "deleteEvent/{personId}/{eventId}")]
        public async Task<ActionResult> DeleteEvent(int personId, int eventId)
        {
            var result = await eventLayer.DeleteEventAsync(userId: GetUserId(), personId, eventId);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
