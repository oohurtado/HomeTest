using Home.Models.DTOs;
using Home.Source.BusinessLayer;
using Home.Source.Common;
using Home.Source.Factories;
using Home.Source.Helpers;
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
    public class PeopleController : ControllerBase
    {
        private readonly PersonLayer personLayer;

        public PeopleController(PersonLayer personLayer)
        {
            this.personLayer = personLayer;
        }

        [HttpGet(template: "getPerson/{id}")]
        public async Task<ActionResult<PersonDTO>> GetPerson(int id)
        {
            var result = await personLayer.GetPersonAsync(userId: GetUserId(), id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpGet(template: "getPeopleByPage/{orderColumn}/{order}/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PageData<PersonDTO>>> GetPeopleByPage(string orderColumn, string order, int pageNumber, int pageSize, string term = null!, string searchColumn = null!)
        {                 
            var page = PaginatorFactory.CreatePage(PaginatorPage.People, orderColumn, order, pageNumber, pageSize, term, searchColumn);
            return await personLayer.GetPeopleByPageAsync(userId: GetUserId(), page);
        }

        [HttpPost(template: "createPerson")]
        public async Task<ActionResult> CreatePerson([FromBody] PersonEditorDTO dto)
        {
            var result = await personLayer.CreatePersonAsync(userId: GetUserId(), dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut(template: "updatePerson/{id}")]
        public async Task<ActionResult> UpdatePerson(int id, [FromBody] PersonEditorDTO dto)
        {
            var result = await personLayer.UpdatePersonAsync(userId: GetUserId(), id, dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete(template: "deletePerson/{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var result = await personLayer.DeletePersonAsync(userId: GetUserId(), id);
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
