using AutoMapper;
using Home.Models.DTOs;
using Home.Models.Entities.Money;
using Home.Source.BusinessLayer;
using Home.Source.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Home.Controllers
{
    public partial class ExpensesController : ControllerBase
    {
        [HttpGet(template: "getEntry/{id}")]
        public async Task<ActionResult<EntryDTO>> GetEntry(int id)
        {
            var result = await expenseLayer.GetEntryAsync(userId: GetUserId(), id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpPost(template: "createEntry")]
        public async Task<ActionResult> CreateEntry([FromBody] EntryEditorDTO dto)
        {
            var result = await expenseLayer.CreateEntryAsync(userId: GetUserId(), dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut(template: "updateEntry/{id}")]
        public async Task<ActionResult> UpdateEntry(int id, [FromBody] EntryEditorDTO dto)
        {
            var result = await expenseLayer.UpdateEntryAsync(userId: GetUserId(), id, dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete(template: "deleteEntry/{id}")]
        public async Task<ActionResult> DeleteEntry(int id)
        {
            var result = await expenseLayer.DeleteEntryAsync(userId: GetUserId(), id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
