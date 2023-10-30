using Home.Models.DTOs;
using Home.Models.Entities.Money;
using Home.Models.Entities.Other;
using Home.Source.Common;
using Home.Source.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Home.Source.BusinessLayer
{
    public partial class ExpensesLayer
    {
        public async Task<BaseResponse<AccountDTO>> GetAccountAsync(string userId, int id)
        {
            var account = await expenseRepository.GetAccount(userId, id).FirstOrDefaultAsync();

            if (account == null)
            {
                return BaseResponseHelper.RecordNotFound_OnGet<AccountDTO>();
            }

            return new BaseResponse<AccountDTO>()
            {
                Data = mapper.Map<AccountDTO>(account)
            };
        }

        public async Task<ActionResult<List<AccountDTO>>> GetAccountsAsync(string userId)
        {
            var data = await expenseRepository.GetAccounts(userId)
                .Select(p => new AccountDTO()
                {
                    Amount = p.Amount,
                    Description = p.Description,
                    Id = p.Id,
                    Name = p.Name,
                    Owner = p.Owner
                })
                .ToListAsync();

            return data;
        } 
        
        public async Task<ActionResult<List<string>>> GetAccountOwnersAsync(string userId)
        {
            var data = await expenseRepository.GetAccounts(userId)
                .Select(p => new
                {
                    p.Owner
                })
                .Select(p => p.Owner)
                .Distinct()
                .ToListAsync();

            return data;
        }

        public async Task<BaseResponse> CreateAccountAsync(string userId, AccountEditorDTO dto)
        {
            var account = new Account()
            {
                UserId = userId,
            };


            mapper.Map(dto, account);
            await expenseRepository.CreateAccountAsync(account);
            await expenseRepository.CreateAccountActivityAsync(account.Id, account.Amount, DateTime.Today);
            return await expenseRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> UpdateAccountAsync(string userId, int id, AccountEditorDTO dto)
        {
            var account = await expenseRepository.GetAccount(userId, id).FirstOrDefaultAsync();
            if (account == null)
            {
                return BaseResponseHelper.RecordNotFound_OnUpdate();
            }
      
            if (dto.Amount != account.Amount)
            {
                await expenseRepository.CreateAccountActivityAsync(account.Id, dto.Amount - account.Amount, DateTime.Today);
            }
            mapper.Map(dto, account);

            return await expenseRepository.SaveChangesAsync();
        }
        
        public async Task<BaseResponse> DeleteAccountAsync(string userId, int id)
        {
            var account = await expenseRepository.GetAccount(userId, id).FirstOrDefaultAsync();
            if (account == null)
            {
                return BaseResponseHelper.RecordNotFound_OnDelete();
            }

            expenseRepository.DeleteAccount(account!);
            return await expenseRepository.SaveChangesAsync();
        }

        public async Task<BaseResponse> UpdateAccountMoneyAsync(string userId, AccountMoneyEditorDTO dto)
        {
            var account = await expenseRepository.GetAccount(userId, dto.AccountId).FirstOrDefaultAsync();
            if (account == null)
            {
                return BaseResponseHelper.RecordNotFound_OnUpdate();
            }

            account.Amount += dto.Amount;
            await expenseRepository.CreateAccountActivityAsync(account.Id, dto.Amount, DateTime.Today);
            return await expenseRepository.SaveChangesAsync(); ;
        }
    }
}
