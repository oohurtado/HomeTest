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
        [HttpGet(template: "getAccount/{id}")]
        public async Task<ActionResult<AccountDTO>> GetAccount(int id)
        {
            var result = await expenseLayer.GetAccountAsync(userId: GetUserId(), id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpGet(template: "getAccounts")]
        public async Task<ActionResult<List<AccountDTO>>> GetAccounts()
        {
            return await expenseLayer.GetAccountsAsync(userId: GetUserId());
        }

        [HttpGet(template: "getAccountOwners")]
        public async Task<ActionResult<List<string>>> GetAccountOwners()
        {
            return await expenseLayer.GetAccountOwnersAsync(userId: GetUserId());
        }

        [HttpPost(template: "createAccount")]
        public async Task<ActionResult> CreateAccount([FromBody] AccountEditorDTO dto)
        {
            var result = await expenseLayer.CreateAccountAsync(userId: GetUserId(), dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut(template: "updateAccount/{id}")]
        public async Task<ActionResult> UpdateAccount(int id, [FromBody] AccountEditorDTO dto)
        {
            var result = await expenseLayer.UpdateAccountAsync(userId: GetUserId(), id, dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPut(template: "updateAccountMoney")]
        public async Task<ActionResult> UpdateAccountMoney([FromBody] AccountMoneyEditorDTO dto)
        {
            var result = await expenseLayer.UpdateAccountMoneyAsync(userId: GetUserId(), dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete(template: "deleteAccount/{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            var result = await expenseLayer.DeleteAccountAsync(userId: GetUserId(), id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
