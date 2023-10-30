using Home.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Home.Controllers
{
    public partial class ExpensesController : ControllerBase
    {
        /// <summary>
        /// Get expenses by year and month
        /// </summary>
        [HttpGet(template: "getExpenses_ByYearMonth/{year}/{month}/{isRegular}")]
        public async Task<ActionResult<List<SuperCategoryDTO>?>> GetExpenses_ByYearMonth(int year, int month, bool isRegular)
        {
            var result = await expenseLayer.GetExpenses_ByYearMonth_Async(userId: GetUserId(), year, month, isRegular);
            return Ok(result);
        }
    }
}
