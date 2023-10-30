using Home.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Home.Controllers
{
    public partial class ExpensesController : ControllerBase
    {
        [HttpGet(template: "getCategory/{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var result = await expenseLayer.GetCategoryAsync(userId: GetUserId(), id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpPost(template: "createCategory")]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryEditorDTO dto)
        {
            var result = await expenseLayer.CreateCategoryAsync(userId: GetUserId(), dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut(template: "updateCategory/{id}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryEditorDTO dto)
        {
            var result = await expenseLayer.UpdateCategoryAsync(userId: GetUserId(), id, dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete(template: "deleteCategory/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var result = await expenseLayer.DeleteCategoryAsync(userId: GetUserId(), id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
