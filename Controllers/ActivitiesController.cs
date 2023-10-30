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
    public class ActivitiesController : ControllerBase
    {
        private readonly ActivityLayer activityLayer;

        public ActivitiesController(ActivityLayer activityLayer)
        {
            this.activityLayer = activityLayer;
        }

        [HttpGet(template: "getActivity/{id}")]
        public async Task<ActionResult<ActivityDTO>> GetActivity(int id)
        {
            var result = await activityLayer.GetActivityAsync(userId: GetUserId(), id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpGet(template: "getActivitiesByDate/{orderColumn}/{order}/{month}/{year}")]
        public async Task<ActionResult<PageData<ActivityDTO>>> GetActivitiesByDate(string orderColumn, string order, int month, int year, string term = null!, string searchColumn = null!)
        {
            var page = PaginatorFactory.CreatePage(PaginatorPage.Activities, orderColumn, order, term, searchColumn);
            return await activityLayer.GetActivitiesByDateAsync(userId: GetUserId(), month, year, page);
        }

        [HttpPost(template: "createActivity")]
        public async Task<ActionResult> CreateActivity([FromBody] ActivityEditorDTO dto)
        {
            var result = await activityLayer.CreateActivityAsync(userId: GetUserId(), dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut(template: "updateActivity/{id}")]
        public async Task<ActionResult> UpdateActivity(int id, [FromBody] ActivityEditorDTO dto)
        {
            var result = await activityLayer.UpdateActivityAsync(userId: GetUserId(), id, dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut(template: "updateActivityStatus/{id}")]
        public async Task<ActionResult> UpdateActivityStatus(int id, [FromBody] ActivityStatusEditorDTO dto)
        {
            var result = await activityLayer.UpdateActivityStatusAsync(userId: GetUserId(), id, dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete(template: "deleteActivity/{id}")]
        public async Task<ActionResult> DeleteActivity(int id)
        {
            var result = await activityLayer.DeleteActivityAsync(userId: GetUserId(), id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpGet(template: "getTags")]
        public async Task<ActionResult<List<string>>> GetTags()
        {
            var tags = await activityLayer.GetTagsAsync(userId: GetUserId());       
            return Ok(tags);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
