using Home.Models.DTOs;
using Home.Source.BusinessLayer;
using Home.Source.Common;
using Home.Source.Factories;
using Home.Source.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Home.Controllers
{
    public partial class ExpensesController : ControllerBase
    {       
        [HttpGet(template: "getSuperCategory/{id}")]
        public async Task<ActionResult<SuperCategoryDTO>> GetSuperCategory(int id)
        {
            var result = await expenseLayer.GetSuperCategoryAsync(userId: GetUserId(), id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpGet(template: "getSuperCategories")]
        public async Task<ActionResult<List<SuperCategoryDTO>>> GetSuperCategories(bool includeCategories = false)
        {
            return await expenseLayer.GetSuperCategoriesAsync(userId: GetUserId(), includeCategories);
        }

        [HttpPost(template: "createSuperCategory")]
        public async Task<ActionResult> CreateSuperCategory([FromBody] SuperCategoryEditorDTO dto)
        {
            var result = await expenseLayer.CreateSuperCategoryAsync(userId: GetUserId(), dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut(template: "updateSuperCategory/{id}")]
        public async Task<ActionResult> UpdateSuperCategory(int id, [FromBody] SuperCategoryEditorDTO dto)
        {
            var result = await expenseLayer.UpdateSuperCategoryAsync(userId: GetUserId(), id, dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete(template: "deleteSuperCategory/{id}")]
        public async Task<ActionResult> DeleteSuperCategory(int id)
        {
            var result = await expenseLayer.DeleteSuperCategoryAsync(userId: GetUserId(), id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
